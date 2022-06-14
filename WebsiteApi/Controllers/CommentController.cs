using Microsoft.AspNetCore.Mvc;
using WebsiteApi.Services.IServices;

namespace WebsiteApi.Controllers
{
    public class CommentController : BaseApiController
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("ToolCreateData")]
        public ActionResult ToolCreateData()
        {
            try
            {
                return Ok(_commentService.Create());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
