using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIENN.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIENN.WebApi.Controllers
{
    public class CrudController<TEntity, TKey> : Controller
                                                    where TEntity : class
                                                    where TKey : IEquatable<TKey>
    {
        protected readonly ICrudService<TEntity, TKey> _crudService;

        public CrudController(DbContext dbContext)
        {
            _crudService = new CrudService<TEntity, TKey>(dbContext);
        }

        [HttpGet]
        public async Task<object> List()
        {
            return await _crudService.GetAll();
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(TKey id)
        {
            if (id == null)
                return NotFound();
            try
            {
                return Ok(await _crudService.Get(id));
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<object> Create([FromBody] TEntity model)
        {
            try
            {
                await _crudService.Add(model);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }

            return "SUCCESS";
        }

        [HttpPost]
        public async Task<object> Update([FromBody] TEntity model)
        {
            try
            {
                await _crudService.Update(model);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }

            return "SUCCESS";
        }

        [HttpDelete]
        public async Task<object> Delete(TKey id)
        {
            try
            {
                await _crudService.Remove(id);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }

            return "SUCCESS";
        }


    }
}
