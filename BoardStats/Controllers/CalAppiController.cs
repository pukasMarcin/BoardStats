using BoardStats.Data.Services;
using BoardStats.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BoardStats.Data.ViewModels;

namespace BoardStats.Controllers
{
    [Route("appi/BoardStats")]
    [ApiController]
    public class CalAppiController : Controller
    {
        private readonly ICalendarServices _calendarService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;
        public CalAppiController(ICalendarServices calendarService, IHttpContextAccessor httpContextAccessor)
        {
            _calendarService = calendarService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
