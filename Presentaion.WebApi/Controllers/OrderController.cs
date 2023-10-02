using Business.Domain.Catalog.OrderNP;
using Business.Models;
using Data.Entity.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("info")]
        public async Task<IActionResult> CreateUserOrderInfo(CreateUserOrderInfoModel model)
        {
            var info = new UserOrderInformationEntity()
            {
                UserId = model.UserId,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Province = model.Province,
                District = model.District,
                Ward = model.Ward,
                Address = model.Address,
                IsDefault = model.IsDefault,
                AddressType = (AddressType)Enum.Parse(typeof(AddressType), model.AddressType)
            };

            var result = await _orderService.CreateUserOrderInfoAsync(info);
            if (result.IsSuccessed)
            {
                return StatusCode(StatusCodes.Status202Accepted);
            }
            return BadRequest(result.Message);
        }
        [HttpDelete("info/{id}")]
        public async Task<IActionResult> DeleteUserOrderInfo([FromRoute] int id)
        {
            var result = await _orderService.DeleteUserOrderInfoAsync(id);
            if (result.IsSuccessed)
            {
                return StatusCode(StatusCodes.Status202Accepted);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int orderId)
        {
            var result = await _orderService.DeleteOrderAsync(orderId);
            if (result.IsSuccessed)
            {
                return StatusCode(StatusCodes.Status202Accepted);
            }
            return BadRequest(result?.Message);
        }

        [HttpGet("info/{userId}")]
        public async Task<IActionResult> GetUserOrderInfo([FromRoute] Guid userId)
        {
            var result = await _orderService.GetListUserOrderInfoAsync(userId);
            if (result.IsSuccessed)
            {
                var sa = result.Result.Select(x => new UserOrderInformationModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Province = x.Province,
                    District = x.District,
                    Ward = x.Ward,
                    Address = x.Address,
                    IsDefault = x.IsDefault,
                    AddressType = x.AddressType,
                });

                return Ok(sa);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("{userId}/{addressId}")]
        public async Task<IActionResult> CreateOrder([FromRoute] Guid userId, [FromRoute] int addressId)
        {
            var result = await _orderService.CreateOrderAsync(userId, addressId);
            if (result.IsSuccessed)
            {
                return StatusCode(StatusCodes.Status202Accepted);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrderByUserId([FromRoute] Guid userId)
        {
            var result = await _orderService.GetOrderByUserIdAsync(userId);
            if (result.IsSuccessed)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet()]
        public async Task<IActionResult> GetOrder()
        {
            var result = await _orderService.GetOrderAsync();
            if (result.IsSuccessed)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{orderId}/{orderStatus}")]
        public async Task<IActionResult> SetOrderStatus([FromRoute] int orderId, [FromRoute] int orderStatus)
        {
            var enumtype = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatus.ToString());
            var result = await _orderService.SetOrderStatusAsync(orderId , enumtype);
            if (result.IsSuccessed)
            {
                return StatusCode(StatusCodes.Status202Accepted);
            }
            return BadRequest(result.Message);
        }
    }
}
