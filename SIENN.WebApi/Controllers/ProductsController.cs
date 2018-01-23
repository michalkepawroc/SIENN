using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SIENN.DbAccess.Repositories;
using SIENN.WebApi.Models;

namespace SIENN.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Products/[action]")]
    public class ProductsController : CrudController<Product, int>
    {
        public ProductsController(DbContext dbContext) : base (dbContext)
        {}

        [HttpGet]
        public async Task<object> AvailableProductsList(int pageIndex,  int pageSize = 10)
        {
            if (pageIndex <= 0)
                pageIndex = 1;
            var query = await _crudService.GetAll();
            
            return await PagingList.CreateAsync
                (
                    query.
                    AsNoTracking().
                    Where(x => x.IsAvailable == true).
                    OrderBy(p => p.ProductId), 
                    pageSize, pageIndex
                );
            
        }

        [HttpGet]
        public async Task<object> AvailableFilteredProductList(int? productType, string category, string unitCode, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var query = await _crudService.GetAll();

                var model = query
                    .Include(c => c.ProductCategory)
                    .Include(u => u.Unit)
                    .AsQueryable();

                int productTypeId = productType.GetValueOrDefault(0);
                if (productTypeId > 0)
                {
                    model = model.Where(p => p.ProductTypeId == productTypeId);
                }
                if (!string.IsNullOrWhiteSpace(category))
                {
                    model = model.Where(c => c.ProductCategory.Any(x => x.Category.Code == category));
                }
                if (!string.IsNullOrWhiteSpace(unitCode))
                {
                    model = model.Where(u => u.Unit.Code == unitCode);
                }
                
                return await PagingList.CreateAsync
                    (
                        model.
                        AsNoTracking().
                        OrderBy(p => p.ProductId),
                        pageSize, pageIndex
                    );
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<object> GetProductsSpecial()
        {
            try
            {
                var query = await _crudService.GetAll();

                var model = query
                    .Include(c => c.ProductCategory)
                    .Include(u => u.Unit)
                    .Include(p => p.ProductType)
                    .AsQueryable();

                var customized = from m in model
                                 select new
                                 {
                                     ProductDescription = $"({m.Code}) {m.Description}",
                                     Price = m.Price.ToString("C2"),
                                     IsAvailable = m.IsAvailable ? "Dostępny" : "Niedostępny",
                                     DeliveryDate = m.DeliveryDate.ToString("MM.dd.yyyy"),
                                     CategoriesCount = m.ProductCategory.Count(),
                                     TypeCode = m.ProductType.Code,
                                     UnitCode = m.Unit.Code

                                 };

                return Ok(customized);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET: api/Products/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProduct([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id == 0)
        //        return NotFound();

        //    var product = _repository.Get(id); 

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(product);
        //}

        //// PUT: api/Products/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != product.ProductId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(product).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Products
        //[HttpPost]
        //public async Task<IActionResult> PostProduct([FromBody] Product product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Product_1.Add(product);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        //}

        // DELETE: api/Products/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var product = await _context.Product_1.SingleOrDefaultAsync(m => m.ProductId == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Product_1.Remove(product);
        //    await _context.SaveChangesAsync();

        //    return Ok(product);
        //}

        //private bool ProductExists(int id)
        //{
        //    return _context.Product_1.Any(e => e.ProductId == id);
        //}
    }
}