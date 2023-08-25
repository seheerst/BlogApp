using BlogApp.Data.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository _postRepository;
        private ITagRepository _tagRepository;

        public PostController(IPostRepository postRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            return View(
                new PostViewModel
                {
                    Posts = _postRepository.Posts.ToList(),
                    Tags = _tagRepository.Tags.ToList()
                }
            );
        }


    }
}