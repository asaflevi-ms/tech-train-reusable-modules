using common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechTrain.ReusableModules.WebApi.Common;
using TechTrain.ReusableModules.WebApi.Models;

namespace TechTrain.ReusableModules.WebApi.Controllers
{
    [ApiController]
    public class CartController : ControllerBase
    {
        [HttpGet]
        [Route("/users/{userId}/סל")]
        public Task<Cart> GetCart([FromRoute]string userName)
        {
            throw new NotImplementedException();
        }

		[HttpGet]
		[Route("/users/{userId}/{anotherid}/test")]
		public Task<Cart> GetCart([FromRoute] string userName, string anotherid)
		{
			throw new NotImplementedException();
		}

		[HttpGet]
        [Route("/carts/{cartId}/items")]
        public Task<IEnumerable<CartItem>> ListCartItems([FromRoute]int cartId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
		[SkipApiValidator(ValidationTypes.ValidateParameters)]
	    [Route("/carts/{cartId}/{cartName}/items")]
        public Task<IActionResult> AddItemToCart([FromRoute]int cartId, [FromBody]CartItem item)
        {
            throw new NotImplementedException();
        }    
    }
}
