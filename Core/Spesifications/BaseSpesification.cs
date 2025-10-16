using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Spesifications
{
    public class BaseSpesification<T>(Expression<Func<T, bool>>? criteria) : ISpesification<T>
    {
        protected BaseSpesification() : this(null) {}
        public Expression<Func<T, bool>>? Criteria => criteria;
    }
}