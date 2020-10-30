using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Models;
using Order.BusinessLogic;
using Order.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly IMapper _mapper;

        public OrderController(IOrderBusiness orderBusiness,
            IMapper mapper)
        {
            _orderBusiness = orderBusiness;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderBusiness.GetAllAsync();
            if (orders == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<List<OrderModel>>(orders);

            return Ok(model);
        }
        
        [HttpGet]
        public async Task<ActionResult<OrderModel>> Get(string orderNumber)
        {
            var order = await _orderBusiness.GetAsync(orderNumber);
            if (order == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<OrderModel>(order);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderModel model)
        {
            var entity = _mapper.Map<Order.Entity.Order>(model);
            var request = new OrderSaveRequest<Order.Entity.Order>
            {
                Entity = entity,
                UpdatedDate = DateTime.Now
            };

            var response = await _orderBusiness.CreateAsync(request);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Process(string orderNumber)
        {
            var response = await _orderBusiness.ProcessAsync(orderNumber);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Delivery(string orderNumber)
        {
            var response = await _orderBusiness.DeliveryAsync(orderNumber);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Complete(string orderNumber)
        {
            var response = await _orderBusiness.CompleteAsync(orderNumber);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Cancel(string orderNumber)
        {
            var response = await _orderBusiness.CancelAsync(orderNumber);

            return Ok(response);
        }
    }
}
