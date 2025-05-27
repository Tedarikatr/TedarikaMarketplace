using AutoMapper;
using Data.Dtos.Companies;
using Domain.Companies.Events;
using Entity.Auths;
using Entity.Companies;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Companys.IRepositorys;
using Repository.Auths.IRepositorys;
using Services.Companys.IServices;

namespace Services.Companys.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyService> _logger;
        private readonly IBuyerUserRepository _buyerUserRepository;
        private readonly ISellerUserRepository _sellerUserRepository;

        public CompanyService(ICompanyRepository companyRepository, IMediator mediator, IMapper mapper, ILogger<CompanyService> logger,
            IBuyerUserRepository buyerUserRepository, ISellerUserRepository sellerUserRepository)
        {
            _companyRepository = companyRepository;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _buyerUserRepository = buyerUserRepository;
            _sellerUserRepository = sellerUserRepository;
        }

        public async Task<string> RegisterCompanyAsync(CompanyCreateDto companyCreateDto, int userId, Entity.Auths.UserType userType)
        {
            try
            {
                _logger.LogInformation("Yeni şirket kaydı başlatıldı: {CompanyNumber}", companyCreateDto.CompanyNumber);

                var existingCompany = await _companyRepository.SingleOrDefaultAsync(c => c.TaxNumber == companyCreateDto.TaxNumber);

                if (existingCompany != null)
                {
                    if (existingCompany.BuyerAccount && userType == UserType.Seller)
                    {
                        if (existingCompany.SellerUserId.HasValue && existingCompany.SellerAccount)
                        {
                            throw new Exception("Bu şirket zaten kayıtlı.");
                        }

                        existingCompany.SellerUserId = userId;
                        existingCompany.SellerAccount = true;
                        await _companyRepository.UpdateAsync(existingCompany);

                        return "Şirket zaten bir alıcı tarafından eklenmişti, şimdi satıcı olarak onaylandı.";
                    }

                    else if (existingCompany.SellerAccount && userType == UserType.Buyer)
                    {
                        if (existingCompany.BuyerUserId.HasValue && existingCompany.BuyerAccount)
                        {
                            throw new Exception("Bu şirket zaten kayıtlı.");
                        }

                        existingCompany.BuyerUserId = userId;
                        existingCompany.BuyerAccount = true;
                        await _companyRepository.UpdateAsync(existingCompany);

                        return "Şirket zaten bir satıcı tarafından eklenmişti, şimdi alıcı olarak onaylandı.";
                    }

                    throw new Exception("Bu şirket zaten kayıtlı.");
                }

                var newCompany = _mapper.Map<Company>(companyCreateDto);
                newCompany.IsVerified = false;
                newCompany.IsActive = false;

                if (userType == UserType.Buyer)
                {
                    newCompany.BuyerUserId = userId;
                    newCompany.BuyerAccount = true;
                }
                else if (userType == UserType.Seller)
                {
                    newCompany.SellerUserId = userId;
                    newCompany.SellerAccount = true;
                    newCompany.IsVerified = true;
                }

                await _companyRepository.AddAsync(newCompany);
                _logger.LogInformation("Şirket kaydı başarıyla tamamlandı: {CompanyNumber}", newCompany.CompanyNumber);

                string userArn = string.Empty;
                if (userType == UserType.Buyer)
                {
                    var buyer = await _buyerUserRepository.GetByIdAsync(userId);
                    userArn = buyer?.AwsIamUserArn;
                }
                else if (userType == UserType.Seller)
                {
                    var seller = await _sellerUserRepository.GetByIdAsync(userId);
                    userArn = seller?.AwsIamUserArn;
                }

                await _mediator.Publish(new CompanyCreatedEvent(newCompany.Id, userId, userType, userArn));

                return "Şirket kaydı başarılı!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket kaydı sırasında hata oluştu: {CompanyNumber}", companyCreateDto.CompanyNumber);
                throw;
            }
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(int companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId)
                ?? throw new Exception("Şirket bulunamadı.");

            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> GetCompanyBySellerUserIdAsync(int sellerUserId)
        {
            var company = await _companyRepository.GetCompanyBySellerIdAsync(sellerUserId);
            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<CompanyDto> GetCompanyByBuyerUserIdAsync(int buyerUserId)
        {
            var company = await _companyRepository.SingleOrDefaultAsync(c => c.BuyerUserId == buyerUserId);
            return _mapper.Map<CompanyDto>(company);
        }

        #region Admin İşlemleri

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task<bool> UpdateCompanyAsync(CompanyUpdateDto companyUpdateDto)
        {
            var company = await _companyRepository.GetByIdAsync(companyUpdateDto.Id)
                ?? throw new Exception("Şirket bulunamadı.");

            _mapper.Map(companyUpdateDto, company);
            return await _companyRepository.UpdateBoolAsync(company);
        }

        public async Task<bool> VerifyCompanyAsync(int companyId, bool isVerified)
        {
            var company = await _companyRepository.GetByIdAsync(companyId)
                ?? throw new Exception("Şirket bulunamadı.");

            company.IsVerified = isVerified;
            return await _companyRepository.UpdateBoolAsync(company);
        }

        public async Task<bool> ToggleCompanyStatusAsync(int companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId)
                ?? throw new Exception("Şirket bulunamadı.");

            company.IsActive = !company.IsActive;
            return await _companyRepository.UpdateBoolAsync(company);
        }

        #endregion
    }
}
