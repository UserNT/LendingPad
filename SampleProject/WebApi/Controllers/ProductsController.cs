using AutoMapper;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IProductsService productsService;

        public ProductsController(IMapper mapper, IProductsService productsService)
        {
            this.mapper = mapper;
            this.productsService = productsService;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll(bool includeDeleted = false)
        {
            var entities = productsService.GetAll(includeDeleted);
            
            var dtos = mapper.Map<IEnumerable<ProductInfoDTO>>(entities);

            return Ok(dtos);
        }

        [Route("{id:guid}")]
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = productsService.Get(id);

            if (entity == null)
                return NotFound();

            var dto = mapper.Map<ProductInfoDTO>(entity);

            return Ok(dto);
        }

        [Route("")]
        [HttpDelete]
        public IHttpActionResult DeleteAll()
        {
            productsService.DeleteAll();
            return Ok();
        }

        [Route("{id:guid}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            var entity = productsService.Get(id);

            if (entity == null)
                return NotFound();

            productsService.Delete(entity);
            return Ok();
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] CreateProductDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = productsService.Create(model.Name, model.Price, model.Description);

            if (entity == null)
                return Conflict();

            var dto = mapper.Map<ProductInfoDTO>(entity);
            
            return Ok(dto);
        }

        [Route("{id:guid}")]
        [HttpPut]
        public IHttpActionResult Update(Guid id, [FromBody] UpdateProductDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = productsService.Get(id);

            if (entity == null)
                return NotFound();

            productsService.Update(entity, model.Price, model.Description);

            var dto = mapper.Map<ProductInfoDTO>(entity);
            
            return Ok(dto);
        }
    }
}