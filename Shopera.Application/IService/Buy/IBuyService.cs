using Shopera.Application.DTOs.Buy;

namespace Shopera.Application.IService.Buy
{
    public interface IBuyService
    {
        Task<CheckoutResultDto> CheckoutAsync(string userId);
    }
}