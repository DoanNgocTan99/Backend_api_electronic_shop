using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using WebsiteApi.Model.Dtos;
using WebsiteApi.Services.IServices;

namespace WebsiteApi.Controllers
{
    public class RatingController : BaseApiController
    {
        private readonly IRatingService _ratingService;
        private readonly IHttpClientFactory _factory;
        private readonly IProductService _productService;
        public RatingController(IRatingService ratingService, IHttpClientFactory factory, IProductService productService)
        {
            _ratingService = ratingService;
            _factory = factory;
            _productService = productService;
        }

        /// <summary>
        /// Thực hiện sử dụng Recommend
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ProductDto>> Recommend(int id)
        {
            try
            {
                List<ProductDto> result = new List<ProductDto>();
                HttpClient client = _factory.CreateClient();
                client.BaseAddress = new Uri("https://recommend-api-tandn.herokuapp.com");
                var response = client.GetAsync(string.Format("/rcm/{0}", id)).Result;
                string jsonData = response.Content.ReadAsStringAsync().Result;
                List<int> data = JsonSerializer.Deserialize<List<int>>(jsonData);
                foreach (var item in data)
                {
                    try
                    {
                        result.Add(_productService.GetById(item));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
