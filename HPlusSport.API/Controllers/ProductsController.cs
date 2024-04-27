using HPlusSport.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace HPlusSport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _shopContext;
        public ProductsController(ShopContext context)
        {
            _shopContext = context;

            _shopContext.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var product = await _shopContext.Products.ToListAsync();
            return Ok(product);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _shopContext.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]

        public async Task<ActionResult<Product>> PostProduct(Product product)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _shopContext.Products.Add(product);
            await _shopContext.SaveChangesAsync();

            return CreatedAtAction(
                "GetProductById",
                new { id = product.Id },
                product

                );



        }

        [HttpPut("{id}")]

        public async Task<ActionResult> PutProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _shopContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _shopContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            bool ProductExist(int id)
            {
                return _shopContext.Products.Any(x => x.Id == id);
            }

            return Ok();
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _shopContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _shopContext.Products.Remove(product);

            await _shopContext.SaveChangesAsync();



            return product;
        }
        
    }
}  
