using System.Web.Mvc;
using WebApiUserManagement.Repository;

namespace WebApiUserManagement.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: Admin/User
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Detail(int id)
        {
            return View(_userRepository.GetById(id));
        }
    }
}