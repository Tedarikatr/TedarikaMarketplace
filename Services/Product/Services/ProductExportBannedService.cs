using AutoMapper;
using Data.Dtos.Product;
using Domain.Products.Events;
using Entity.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Product.IRepositorys;
using Services.Product.IServices;

namespace Services.Product.Services
{
    public class ProductExportBannedService : IProductExportBannedService
    {
        private readonly IProductExportBannedRepository _bannedRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductExportBannedService> _logger;
        private readonly IMediator _mediator;

        public ProductExportBannedService(IProductExportBannedRepository bannedRepository, IMapper mapper, ILogger<ProductExportBannedService> logger, IMediator mediator)
        {
            _bannedRepository = bannedRepository;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }
        public async Task AddRestrictionAsync(ProductExportBannedCreateDto dto)
        {
            if (dto.CountryCodes == null || !dto.CountryCodes.Any())
            {
                _logger.LogWarning("ProductId {ProductId} için ihracat yasağı eklenemedi. CountryCodes boş.", dto.ProductId);
                throw new ArgumentException("En az bir ülke kodu belirtilmelidir.");
            }

            foreach (var countryCode in dto.CountryCodes)
            {
                var exists = await _bannedRepository.GetQueryable()
                    .AnyAsync(x => x.ProductId == dto.ProductId && x.CountryCode == countryCode);

                if (exists)
                {
                    _logger.LogWarning("ProductId {ProductId} için CountryCode {CountryCode} zaten yasaklı.", dto.ProductId, countryCode);
                    continue;
                }

                var restriction = new ProductExportBanned
                {
                    ProductId = dto.ProductId,
                    GTIPCode = dto.GTIPCode,
                    CountryCode = countryCode,
                    IsExportBanned = true,
                    Reason = dto.Reason,
                    CreatedAt = DateTime.UtcNow
                };

                await _bannedRepository.AddAsync(restriction);

                _logger.LogInformation("ProductId {ProductId} için {CountryCode} ülkesine ihracat yasağı eklendi. Sebep: {Reason}",
                    dto.ProductId, countryCode, dto.Reason);

                await _mediator.Publish(new ProductExportBannedEvent
                {
                    ProductId = dto.ProductId,
                    GTIPCode = dto.GTIPCode,
                    CountryCode = countryCode,
                    Reason = dto.Reason
                });
            }
        }

        public async Task RemoveRestrictionAsync(int restrictionId)
        {
            var entity = await _bannedRepository.GetByIdAsync(restrictionId);
            if (entity == null) return;

            await _mediator.Publish(new ProductExportBanLiftedEvent
            {
                ProductId = entity.ProductId,
                GTIPCode = entity.GTIPCode,
                CountryCode = entity.CountryCode
            });

            await _bannedRepository.RemoveAsync(entity);
            _logger.LogInformation("RestrictionId {Id} silindi.", restrictionId);
        }

        public async Task<List<ProductExportBannedDto>> GetRestrictionsByProductIdAsync(int productId)
        {
            var list = await _bannedRepository.GetQueryable()
                .Where(x => x.ProductId == productId)
                .ToListAsync();

            return _mapper.Map<List<ProductExportBannedDto>>(list);
        }
    }
}
