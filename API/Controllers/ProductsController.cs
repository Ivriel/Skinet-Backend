using Core.Entities;
using Core.Interfaces;
using Core.Spesifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IGenericRepository<Product> repo): ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand,string? type,string? sort)
        {
            var spec = new ProductSpesification(brand, type);
            var products = await repo.ListAsync(spec);
            return Ok(products);
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

            return Ok();
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok();
        }
        
        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }
    }
}