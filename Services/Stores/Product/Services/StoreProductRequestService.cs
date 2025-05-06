using AutoMapper;
using Data.Dtos.Stores.Products;
using Entity.Stores.Products;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Product.IRepositorys;
using Repository.Stores.Product.IRepositorys;
using Services.Files.IServices;
using Services.Stores.Product.IServices;

namespace Services.Stores.Product.Services
{
    public class StoreProductRequestService : IStoreProductRequestService
    {
        private readonly IStoreProductRequestRepository _requestRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFilesService _filesService;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreProductRequestService> _logger;
        private readonly IMediator _mediator;

        public StoreProductRequestService(IStoreProductRequestRepository requestRepository, IProductRepository productRepository, IFilesService filesService, IMapper mapper, ILogger<StoreProductRequestService> logger, IMediator mediator)
        {
            _requestRepository = requestRepository;
            _productRepository = productRepository;
            _filesService = filesService;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<string> CreateStoreProductRequestAsync(int storeId, StoreProductRequestCreateDto dto)
        {
            try
            {
                var request = _mapper.Map<StoreProductRequest>(dto);

                if (dto.Image != null)
                {
                    var uploadResult = await _filesService.UploadFileAsync(dto.Image, "product-images");
                    request.ImageUrl = uploadResult.Url;
                }

                request.StoreId = storeId;
                request.CreatedAt = DateTime.UtcNow;
                request.Status = StoreProductRequestStatus.Pending;

                await _requestRepository.AddAsync(request);
                return "Ürün başvurusu başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün başvurusu oluşturulurken hata oluştu.");
                return "Ürün başvurusu oluşturulurken bir hata meydana geldi.";
            }
        }

        public async Task<List<StoreProductRequestDto>> GetMyRequestsAsync(int storeId)
        {
            try
            {
                var requests = await _requestRepository.FindAsync(x => x.StoreId == storeId);
                return _mapper.Map<List<StoreProductRequestDto>>(requests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza başvuruları getirilirken hata oluştu.");
                return new List<StoreProductRequestDto>();
            }
        }

        public async Task<StoreProductRequestDetailDto> GetRequestDetailAsync(int requestId, int storeId)
        {
            try
            {
                var request = await _requestRepository.SingleOrDefaultAsync(x => x.Id == requestId && x.StoreId == storeId);
                return _mapper.Map<StoreProductRequestDetailDto>(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru detayı getirilirken hata oluştu.");
                return null;
            }
        }

        public async Task<RequestSummaryDto> GetRequestSummaryAsync(int storeId)
        {
            try
            {
                var requests = await _requestRepository.FindAsync(x => x.StoreId == storeId);
                return new RequestSummaryDto
                {
                    Total = requests.Count(),
                    Approved = requests.Count(x => x.Status == StoreProductRequestStatus.Approved),
                    Rejected = requests.Count(x => x.Status == StoreProductRequestStatus.Rejected),
                    Pending = requests.Count(x => x.Status == StoreProductRequestStatus.Pending)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru özet bilgisi getirilirken hata oluştu.");
                return new RequestSummaryDto();
            }
        }

        public async Task<List<StoreProductRequestDto>> GetAllPendingRequestsAsync()
        {
            try
            {
                var requests = await _requestRepository.FindAsync(x => x.Status == StoreProductRequestStatus.Pending);
                return _mapper.Map<List<StoreProductRequestDto>>(requests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bekleyen başvurular getirilirken hata oluştu.");
                return new List<StoreProductRequestDto>();
            }
        }

        public async Task<StoreProductRequestDetailDto> GetRequestDetailAsync(int requestId)
        {
            try
            {
                var request = await _requestRepository.GetByIdAsync(requestId);
                return _mapper.Map<StoreProductRequestDetailDto>(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin başvuru detayı alınırken hata oluştu.");
                return null;
            }
        }

        public async Task<string> ApproveRequestAsync(int requestId)
        {
            try
            {
                var request = await _requestRepository.GetByIdAsync(requestId);
                if (request == null) return "Başvuru bulunamadı.";

                request.Status = StoreProductRequestStatus.Approved;
                request.IsApproved = true;
                request.ApprovedAt = DateTime.UtcNow;
                request.ReviewedAt = DateTime.UtcNow;

                await _requestRepository.UpdateAsync(request);
                return "Başvuru onaylandı.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru onaylanırken hata oluştu.");
                return "Başvuru onaylanırken bir hata oluştu.";
            }
        }

        public async Task<string> RejectRequestAsync(int requestId, string adminNote)
        {
            try
            {
                var request = await _requestRepository.GetByIdAsync(requestId);
                if (request == null) return "Başvuru bulunamadı.";

                request.Status = StoreProductRequestStatus.Rejected;
                request.ReviewedAt = DateTime.UtcNow;
                request.AdminNote = adminNote;

                await _requestRepository.UpdateAsync(request);
                return "Başvuru reddedildi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru reddedilirken hata oluştu.");
                return "Başvuru reddedilirken bir hata oluştu.";
            }
        }

        public async Task<bool> CreateProductFromApprovedRequestAsync(int requestId)
        {
            try
            {
                var request = await _requestRepository.GetByIdAsync(requestId);
                if (request == null || request.Status != StoreProductRequestStatus.Approved)
                {
                    _logger.LogWarning("Onaylanmamış veya bulunamayan başvuru.");
                    return false;
                }

                var product = _mapper.Map<Entity.Products.Product>(request);
                product.CreatedAt = DateTime.UtcNow;

                await _productRepository.AddAsync(product);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün oluşturulurken hata oluştu.");
                return false;
            }
        }

    }
}
