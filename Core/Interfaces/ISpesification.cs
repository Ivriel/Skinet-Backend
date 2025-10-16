using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface ISpesification<T>
    {
        Expression<Func<T,bool>>? Criteria { get; }
    }
}