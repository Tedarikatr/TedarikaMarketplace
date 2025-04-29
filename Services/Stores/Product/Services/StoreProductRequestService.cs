using AutoMapper;
using Data.Dtos.Stores.Products;
using Domain.Products.Events;
using Entity.Stores.Products;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Stores.Product.IRepositorys;
using Services.Files.IServices;
using Services.Stores.Product.IServices;

namespace Services.Stores.Product.Services
{
    public class StoreProductRequestService : IStoreProductRequestService
    {
        private readonly IStoreProductRequestRepository _requestRepo;
        private readonly IFilesService _filesService;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreProductRequestService> _logger;
        private readonly IMediator _mediator;

        public StoreProductRequestService(
            IStoreProductRequestRepository requestRepo,
            IFilesService filesService,
            IMapper mapper,
            ILogger<StoreProductRequestService> logger,
            IMediator mediator)
        {
            _requestRepo = requestRepo;
            _filesService = filesService;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<string> CreateStoreProductRequestAsync(StoreProductRequestCreateDto dto)
        {
            _logger.LogInformation("Yeni ürün başvurusu alınıyor: {Name}", dto.Name);

            var request = _mapper.Map<StoreProductRequest>(dto);
            request.CreatedAt = DateTime.UtcNow;

            if (dto.Image != null)
            {
                var uploadResult = await _filesService.UploadFileAsync(dto.Image, "store-product-requests");
                request.ImageUrl = uploadResult.Url;
            }

            await _requestRepo.AddAsync(request);
            _logger.LogInformation("Başvuru başarıyla eklendi. ID: {RequestId}", request.Id);
            return "Ürün başvurusu başarıyla oluşturuldu.";
        }

        public async Task<IEnumerable<StoreProductRequestDto>> GetPendingRequestsAsync()
        {
            var requests = await _requestRepo.GetPendingRequestsAsync();
            return _mapper.Map<IEnumerable<StoreProductRequestDto>>(requests);
        }

        public async Task<string> ApproveStoreProductRequestAsync(int requestId)
        {
            _logger.LogInformation("Ürün başvurusu onaylanıyor. ID: {RequestId}", requestId);

            var request = await _requestRepo.GetByIdAsync(requestId);
            if (request == null)
                throw new Exception("Başvuru bulunamadı.");

            if (request.IsApproved)
                return "Bu başvuru zaten onaylanmış.";

            request.IsApproved = true;
            request.ApprovedAt = DateTime.UtcNow;

            await _requestRepo.UpdateAsync(request);

            await _mediator.Publish(new StoreProductRequestApprovedEvent
            {
                RequestId = request.Id,
                StoreId = request.StoreId,
                Name = request.Name,
                Description = request.Description,
                Brand = request.Brand,
                UnitTypes = request.UnitTypes,
                CategoryId = request.CategoryId,
                CategorySubId = request.CategorySubId,
                ImageUrl = request.ImageUrl,

            });

            _logger.LogInformation("Başvuru onaylandı ve event yayınlandı. ID: {RequestId}", request.Id);
            return "Başvuru başarıyla onaylandı.";
        }
    }
}
