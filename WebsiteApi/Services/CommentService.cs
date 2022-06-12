using WebsiteApi.Repositories.IRepositories;
using WebsiteApi.Services.IServices;

namespace WebsiteApi.Services
{
    public class CommentService: ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public string Create()
        {
            return _commentRepository.Create();
        }
    }
}
