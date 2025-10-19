using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Spesifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IGenericRepository<Product> repo): BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductSpesification(specParams);
            var products = await repo.ListAsync(spec);
            var count = await repo.CountAsync(spec);

            var pagination = new Pagination<Product>(specParams.PageIndex,specParams.PageSize,count,products);
            return Ok(pagination);
        }   

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return NotFound();
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);
            if(await repo.SaveAllAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }

            return BadRequest("Tidak bisa membuat product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id,Product product)
        {
            if (product.Id != id || !ProductExists(id)) 
            return BadRequest("Tidak bisa mengupdate produk");

            repo.Update(product);

            if(await repo.SaveAllAsync())
            {
                return Ok("Berhasil mengupdate produk");
            }

            return BadRequest("Tidak bisa mengupdate produk");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return NotFound();
            repo.Remove(product);

            if (await repo.SaveAllAsync())
            {
                return Ok("Berhasil menghapus produk");
            }

            return BadRequest("Tidak bisa menghapus produk");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpesification();
            return Ok(await repo.ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpesification();
            return Ok(await repo.ListAsync(spec));
        }
        
        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }
    }
}