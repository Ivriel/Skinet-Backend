using Core.Entities;

namespace Core.Spesifications
{
    public class ProductSpesification : BaseSpesification<Product>
    {
        public ProductSpesification(string? brand,string? type) : base(x => 
            (string.IsNullOrWhiteSpace(brand) || x.Brand == brand) &&
            (string.IsNullOrWhiteSpace(type) || x.Type == type)
        )
        {
            
        }
    }
}