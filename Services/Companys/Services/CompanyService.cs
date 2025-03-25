using AutoMapper;
using Data.Dtos.Companies;
using Domain.Companies.Events;
using Entity.Auths;
using Entity.Companies;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Companys.IRepositorys;
using Services.Companys.IServices;

namespace Services.Companys.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(ICompanyRepository companyRepository, IMediator mediator, IMapper mapper, ILogger<CompanyService> logger)
        {
            _companyRepository = companyRepository;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
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
                await _mediator.Publish(new CompanyCreatedEvent(newCompany.Id, userId, userType));

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
            try
            {
                _logger.LogInformation("Şirket bilgisi getiriliyor. ID: {CompanyId}", companyId);

                var company = await _companyRepository.GetByIdAsync(companyId);
                if (company == null)
                {
                    throw new Exception("Şirket bulunamadı.");
                }

                return _mapper.Map<CompanyDto>(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket bilgisi getirilirken hata oluştu. ID: {CompanyId}", companyId);
                throw;
            }
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            try
            {
                _logger.LogInformation("Tüm şirketler listeleniyor.");
                var companies = await _companyRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CompanyDto>>(companies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirketler listelenirken hata oluştu.");
                throw;
            }
        }

        public async Task<bool> UpdateCompanyAsync(CompanyUpdateDto companyUpdateDto)
        {
            try
            {
                _logger.LogInformation("Şirket güncelleniyor. ID: {CompanyId}", companyUpdateDto.Id);

                var company = await _companyRepository.GetByIdAsync(companyUpdateDto.Id);
                if (company == null)
                {
                    throw new Exception("Şirket bulunamadı.");
                }

                _mapper.Map(companyUpdateDto, company);
                var isUpdated = await _companyRepository.UpdateBoolAsync(company);

                return isUpdated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket güncellenirken hata oluştu. ID: {CompanyId}", companyUpdateDto.Id);
                return false;
            }
        }

        public async Task<bool> VerifyCompanyAsync(int companyId)
        {
            try
            {
                _logger.LogInformation("Şirket onaylanıyor. ID: {CompanyId}", companyId);

                var company = await _companyRepository.GetByIdAsync(companyId);
                if (company == null)
                {
                    throw new Exception("Şirket bulunamadı.");
                }

                company.IsVerified = true;
                var isUpdated = await _companyRepository.UpdateBoolAsync(company);

                return isUpdated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket onaylama işlemi sırasında hata oluştu. ID: {CompanyId}", companyId);
                return false;
            }
        }

        public async Task<bool> ToggleCompanyStatusAsync(int companyId)
        {
            try
            {
                _logger.LogInformation("Şirket aktiflik durumu değiştiriliyor. ID: {CompanyId}", companyId);

                var company = await _companyRepository.GetByIdAsync(companyId);
                if (company == null)
                {
                    throw new Exception("Şirket bulunamadı.");
                }

                company.IsActive = !company.IsActive; 
                var isUpdated = await _companyRepository.UpdateBoolAsync(company);

                return isUpdated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket aktiflik durumu değiştirilirken hata oluştu. ID: {CompanyId}", companyId);
                return false;
            }
        }
    }
}
