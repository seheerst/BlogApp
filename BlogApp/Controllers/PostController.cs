using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;

        public PostController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task<IActionResult> Index(string? tag)
        {
          
            var posts = _postRepository.Posts;
            
            if (!string.IsNullOrEmpty(tag))
            {
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }
            return View(
                new PostViewModel{Posts = await posts.ToListAsync()}
            );
        }

        public async Task<IActionResult> Details(string? url)
        {
            return View(await _postRepository.Posts
                .Include(x=> x.Tags)
                .Include(x=> x.Comments).ThenInclude(
                    x=>x.User)
                .FirstOrDefaultAsync(p=> p.Url== url));
        }

        public JsonResult AddComment(int PostId, string UserName, string Text)
        {
            var entity = new Comment
            {
                Text = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                User = new User
                {
                    Name = UserName,
                    Image = "avatar.jpg"
                }
            };
            _commentRepository.CreateComment(entity);

            return Json(new
            {
                UserName,
                Text,
                entity.PublishedOn,
                entity.User.Image
            });
        }


    }
}