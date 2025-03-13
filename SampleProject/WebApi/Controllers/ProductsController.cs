using AutoMapper;
using BusinessEntities;
using Core.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : BaseApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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
            Logger.Info($"{Request.Method} {Request.RequestUri}");

            var entities = productsService.GetAll(includeDeleted);
            
            var dtos = mapper.Map<IEnumerable<ProductInfoDTO>>(entities);

            return Ok(dtos);
        }

        [Route("{id:guid}")]
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            Logger.Info($"{Request.Method} {Request.RequestUri}");

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
            Logger.Info($"{Request.Method} {Request.RequestUri}");

            productsService.DeleteAll();
            return Ok();
        }

        [Route("{id:guid}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            Logger.Info($"{Request.Method} {Request.RequestUri}");

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
            Logger.Info($"{Request.Method} {Request.RequestUri}");

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
            Logger.Info($"{Request.Method} {Request.RequestUri}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = productsService.Get(id);

            if (entity == null)
                return NotFound();

            productsService.Update(entity, model.Price, model.Description);

            var dto = mapper.Map<ProductInfoDTO>(entity);
            
            return Ok(dto);
        }

        [Route("list")]
        [HttpPost]
        public IHttpActionResult Get([FromBody] FilterRequestDTO model)
        {
            Logger.Info($"{Request.Method} {Request.RequestUri}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var predicate = BuildFilterPredicate<Product>(model);
            var orderBy = BuildOrderByLambda<Product>(model.OrderBy);

            var result = productsService.Get(predicate, orderBy, model.IsDescOrder, model.Skip, model.Take).ToList();

            return Ok(result);
        }
    }
}