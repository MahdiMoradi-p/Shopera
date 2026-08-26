using Shopera.Application.DTOs.Invoice;
using Shopera.Application.Interfaces.Repositories;
using Shopera.Application.IService.Invoice;

namespace Shopera.Application.Service.Invoice
{
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly IInvoiceDetailRepository _detailRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;

        public InvoiceDetailService(
            IInvoiceDetailRepository detailRepository,
            IInvoiceRepository invoiceRepository,
            IProductRepository productRepository)
        {
            _detailRepository = detailRepository;
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
        }

        public async Task<InvoiceDetailDto> CreateAsync(
            CreateInvoiceDetailDto dto)
        {
            var invoice =
                await _invoiceRepository.GetByIdAsync(dto.InvoiceId);

            if (invoice == null)
                throw new Exception("Invoice not found.");

            var product =
                await _productRepository.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new Exception("Product not found.");

            if (dto.Quantity <= 0)
                throw new Exception(
                    "Quantity must be greater than zero.");

            var detail = new Domain.Entities.InvoiceDetail
            {
                InvoiceId = dto.InvoiceId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = product.Price,
                TotalPrice = product.Price * dto.Quantity
            };

            var result =
                await _detailRepository.AddAsync(detail);

            return new InvoiceDetailDto
            {
                Id = result.Id,
                InvoiceId = result.InvoiceId,
                ProductId = result.ProductId,
                Quantity = result.Quantity,
                UnitPrice = result.UnitPrice,
                TotalPrice = result.TotalPrice
            };
        }

        public async Task<List<InvoiceDetailDto>> GetAllAsync()
        {
            var details =
                await _detailRepository.GetAllAsync();

            return details.Select(x => new InvoiceDetailDto
            {
                Id = x.Id,
                InvoiceId = x.InvoiceId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.TotalPrice
            }).ToList();
        }

        public async Task<InvoiceDetailDto?> GetByIdAsync(int id)
        {
            var detail =
                await _detailRepository.GetByIdAsync(id);

            if (detail == null)
                return null;

            return new InvoiceDetailDto
            {
                Id = detail.Id,
                InvoiceId = detail.InvoiceId,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                UnitPrice = detail.UnitPrice,
                TotalPrice = detail.TotalPrice
            };
        }

        public async Task<List<InvoiceDetailDto>> GetByInvoiceIdAsync(
            int invoiceId)
        {
            var invoice =
                await _invoiceRepository.GetByIdAsync(invoiceId);

            if (invoice == null)
                throw new Exception("Invoice not found.");

            var details =
                await _detailRepository.GetByInvoiceIdAsync(invoiceId);

            return details.Select(x => new InvoiceDetailDto
            {
                Id = x.Id,
                InvoiceId = x.InvoiceId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.TotalPrice
            }).ToList();
        }

        public async Task<bool> UpdateAsync(
            UpdateInvoiceDetailDto dto)
        {
            var detail =
                await _detailRepository.GetByIdAsync(dto.Id);

            if (detail == null)
                return false;

            var product =
                await _productRepository.GetByIdAsync(dto.ProductId);

            if (product == null)
                throw new Exception("Product not found.");

            if (dto.Quantity <= 0)
                throw new Exception(
                    "Quantity must be greater than zero.");

            detail.ProductId = dto.ProductId;
            detail.Quantity = dto.Quantity;
            detail.UnitPrice = product.Price;
            detail.TotalPrice = product.Price * dto.Quantity;

            await _detailRepository.UpdateAsync(detail);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var detail =
                await _detailRepository.GetByIdAsync(id);

            if (detail == null)
                return false;

            await _detailRepository.DeleteAsync(detail);

            return true;
        }
    }
}