using StoreBackend.Models;

namespace StoreBackend.Services.Contracts;
public interface IProductService
{
    Task<ProductDetailViewModel?> Detail(int id);
    Task<int> Create(ProductCreateParameters productCreateDTO);
}