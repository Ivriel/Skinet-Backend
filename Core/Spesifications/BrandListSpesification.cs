using Core.Entities;

namespace Core.Spesifications
{
    public class BrandListSpesification : BaseSpesification<Product, string>
    {
        public BrandListSpesification()
        {
            AddSelect(x => x.Brand);
            ApplyDistinct();
        }
    }
}