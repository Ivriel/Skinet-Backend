using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers


{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>(
            IGenericRepository<T> repo,
            ISpesification<T> spec, int pageIndex, int pageSize
            ) where T : BaseEntity
        {
            var items = await repo.ListAsync(spec);
            var count = await repo.CountAsync(spec);

            var pagination = new Pagination<Product>(
                specParams.PageIndex,specParams.PageSize,count,products
            )
        }
    }
}