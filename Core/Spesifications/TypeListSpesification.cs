
using Core.Entities;

namespace Core.Spesifications
{
    public class TypeListSpesification : BaseSpesification<Product,string>
    {
        public TypeListSpesification()
        {
            AddSelect(x => x.Type);
            ApplyDistinct();
        }
    }
}