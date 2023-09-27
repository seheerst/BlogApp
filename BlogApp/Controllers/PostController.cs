using System.Security.Claims;
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

        public JsonResult AddComment(int PostId, string Text)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);
            var entity = new Comment
            {
                Text = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                UserId = int.Parse(userId ?? ""),
                
            };
            _commentRepository.CreateComment(entity);

            return Json(new
            {
                userName,
                Text,
                entity.PublishedOn,
                avatar
            });
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _postRepository.CreatePost(
                    new Post
                    {
                        Title = model.Title,
                        Content = model.Content,
                        Description = model.Description,
                        Url = model.PostUrl,
                        UserId = int.Parse(userId ?? ""),
                        PublishedOn = DateTime.Now,
                        Image = "1.jpg",
                        IsActive = false
                    });
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        public async Task<IActionResult> List()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);

            var posts = _postRepository.Posts;

            if (string.IsNullOrEmpty(role))
            {
                posts = posts.Where(i => i.UserId == userId);
            }
            
            return View(await posts.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var post =await _postRepository.Posts.FirstOrDefaultAsync(i => i.PostId == id);
            if (post== null)
            {
                return NotFound();
            }
            
            return View(new CreateViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                IsActive = post.IsActive,
                PostUrl = post.Url
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreateViewModel post)
        {
            if (ModelState.IsValid)
            {
                var entityToUpdate = new Post
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Description = post.Description,
                    Content = post.Content,
                    Url = post.PostUrl,
                };
                
                if(User.FindFirstValue(ClaimTypes.Role) == "admin")
                {
                    entityToUpdate.IsActive = post.IsActive;
                }
                
                _postRepository.EditPost(entityToUpdate);
                return RedirectToAction("List");
            }
            return View(post);
        }
    }
}