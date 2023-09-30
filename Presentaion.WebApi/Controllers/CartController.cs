using AutoMapper;
using Business.Domain.Catalog.CartNP;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartModel model)
        {
            var result = await _cartService.AddToCartAsync(model.UserId, model.ProductId, model.Quantity);
            if (result.IsSuccessed) return StatusCode(StatusCodes.Status201Created);
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeQuantity(AddToCartModel model)
        {
            var result = await _cartService.ChangeQuantityAsync(model.UserId, model.ProductId, model.Quantity);
            if (result.IsSuccessed) return StatusCode(StatusCodes.Status202Accepted);
            return BadRequest(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveToCart(RemoveToCartModel model)
        {
            var result = await _cartService.RemoveToCartAsync(model.UserId, model.ProductId);
            if (result.IsSuccessed) return StatusCode(StatusCodes.Status202Accepted);
            return BadRequest(result.Message);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(Guid userId)
        {
            var result = await _cartService.GetCartAsync(userId);
            if (result.IsSuccessed) return Ok(_mapper.Map<IEnumerable<ProductInCartResponseEntity>, IEnumerable<ProductInCartModel>>(result.Result));
            return BadRequest(result.Message);
        }

        [HttpGet("number/{userId}")]
        public async Task<IActionResult> GetNumberProductInCart([FromRoute] Guid userId)
        {
            var result = await _cartService.GetNumberOfProductInCart(userId);
            if (result.IsSuccessed)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.Message);
        }
    }
}
