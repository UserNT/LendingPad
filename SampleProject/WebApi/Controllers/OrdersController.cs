using AutoMapper;
using BusinessEntities;
using Core.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrdersController : BaseApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMapper mapper;
        private readonly IOrdersService ordersService;

        public OrdersController(IMapper mapper, IOrdersService ordersService)
        {
            this.mapper = mapper;
            this.ordersService = ordersService;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll(bool includeDeleted = false)
        {
            Logger.Info($"{Request.Method} {Request.RequestUri}");

            var entities = ordersService.GetAll(includeDeleted);

            var dtos = mapper.Map<IEnumerable<OrderInfoDTO>>(entities);

            return Ok(dtos);
        }

        [Route("{id:guid}")]
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            Logger.Info($"{Request.Method} {Request.RequestUri}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = ordersService.Get(id);

            if (entity == null)
                return NotFound();

            var dto = mapper.Map<OrderInfoDTO>(entity);

            return Ok(dto);
        }

        [Route("")]
        [HttpDelete]
        public IHttpActionResult DeleteAll()
        {
            Logger.Info($"{Request.Method} {Request.RequestUri}");

            ordersService.DeleteAll();
            return Ok();
        }

        [Route("{id:guid}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            Logger.Info($"{Request.Method} {Request.RequestUri}");

            var entity = ordersService.Get(id);

            if (entity == null)
                return NotFound();

            ordersService.Delete(entity);
            return Ok();
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] CreateOrderDTO model)
        {
            Logger.Info($"{Request.Method} {Request.RequestUri}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = ordersService.Create(model.Items);

            if (entity == null)
                return NotFound();

            var dto = mapper.Map<OrderInfoDTO>(entity);

            return Ok(dto);
        }

        [Route("list")]
        [HttpPost]
        public IHttpActionResult Get([FromBody] FilterRequestDTO model)
        {
            Logger.Info($"{Request.Method} {Request.RequestUri}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var predicate = BuildFilterPredicate<Order>(model);
            var orderBy = BuildOrderByLambda<Order>(model.OrderBy);

            var result = ordersService.Get(predicate, orderBy, model.IsDescOrder, model.Skip, model.Take).ToList();

            return Ok(result);
        }
    }
}