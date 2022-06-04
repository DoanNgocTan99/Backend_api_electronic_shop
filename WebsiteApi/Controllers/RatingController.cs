using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Services.IServices;

namespace WebsiteApi.Controllers
{
    public class RatingController : BaseApiController
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                var list = new List<ProductRating>()
                {
                   new ProductRating()
                   {
                       userId = 6,
                    productId = 10
                   },
                   new ProductRating()
                   {
                       userId = 5,
                    productId = 10
                   },
                   new ProductRating()
                   {
                       userId = 3,
                    productId = 10
                   },
                   new ProductRating()
                   {
                       userId = 4,
                    productId = 10
                   },
                   new ProductRating()
                   {
                       userId = 2,
                    productId = 10
                   },
                   new ProductRating()
                   {
                       userId = 7,
                    productId = 10
                   }
                };
                _ratingService.Recommend(list);
                return Ok(string.Empty);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
