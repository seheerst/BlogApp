using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;


namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository _repository;

        public PostController(IPostRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.Posts.ToList());
        }


    }
}