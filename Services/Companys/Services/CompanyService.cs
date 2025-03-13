using AutoMapper;
using Data.Dtos.Companys;
using Entity.Companies;
using Microsoft.Extensions.Logging;
using Repository.Companys.IRepositorys;
using Services.Companys.IServices;

namespace Services.Companys.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper, ILogger<CompanyService> logger)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> RegisterCompanyAsync(CompanyCreateDto companyCreateDto)
        {
            try
            {
                _logger.LogInformation("Yeni şirket kaydı başlatıldı: {CompanyNumber}", companyCreateDto.CompanyNumber);

                var existingCompany = await _companyRepository.FindAsync(c => c.CompanyNumber == companyCreateDto.CompanyNumber);
                if (existingCompany.Any())
                {
                    throw new Exception("Bu şirket numarası zaten kayıtlı.");
                }

                var company = _mapper.Map<Company>(companyCreateDto);
                company.IsVerified = false;
                company.IsActive = false;

                await _companyRepository.AddAsync(company);
                _logger.LogInformation("Şirket kaydı başarıyla tamamlandı: {CompanyNumber}", company.CompanyNumber);

                return "Şirket kaydı başarılı! Admin onayı bekleniyor.";
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

        public async Task<bool> VerifyCompanyAsync(int companyId, bool isVerified)
        {
            try
            {
                _logger.LogInformation("Şirket onay durumu değiştiriliyor. ID: {CompanyId}", companyId);

                var company = await _companyRepository.GetByIdAsync(companyId);
                if (company == null)
                {
                    throw new Exception("Şirket bulunamadı.");
                }

                company.IsVerified = isVerified;
                var isUpdated = await _companyRepository.UpdateBoolAsync(company);

                return isUpdated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket onay işlemi sırasında hata oluştu. ID: {CompanyId}", companyId);
                return false;
            }
        }

        public async Task<bool> ChangeCompanyStatusAsync(int companyId, bool isActive)
        {
            try
            {
                _logger.LogInformation("Şirket aktiflik durumu değiştiriliyor. ID: {CompanyId}", companyId);

                var company = await _companyRepository.GetByIdAsync(companyId);
                if (company == null)
                {
                    throw new Exception("Şirket bulunamadı.");
                }

                company.IsActive = isActive;
                var isUpdated = await _companyRepository.UpdateBoolAsync(company);

                return isUpdated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket durumu değiştirilirken hata oluştu. ID: {CompanyId}", companyId);
                return false;
            }
        }
    }
}
