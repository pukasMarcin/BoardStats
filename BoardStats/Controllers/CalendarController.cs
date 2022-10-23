
using BoardStats.Data;
using BoardStats.Data.Services;
using BoardStats.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BoardStats.Controllers
{
    [Route("Calendar")]
    public class CalendarController : Controller
    {
        private readonly ICalendarServices _calendarService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;
        public CalendarController(ICalendarServices calendarService, IHttpContextAccessor httpContextAccessor)
        {
            _calendarService = calendarService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }
        public async Task<IActionResult> IndexAsync()
        {

            var dropdown =  await _calendarService.GetAllGames();
            ViewBag.Games = new SelectList(dropdown.Where(m => m.Expansion == false), "Name", "Name");
            
            return View();
        }

        [HttpPost("SaveCalendarData")]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData([FromBody] CalendarAppVM data)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var us = _calendarService.GetUser(userId);
            string userName = us.UserName;

            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _calendarService.AddUpdate(data, userId, userName).Result;
                if (commonResponse.status == 1)
                {
                    commonResponse.message = Helper.appointmentUpdated;
                }
                if (commonResponse.status == 2)
                {
                    commonResponse.message = Helper.appointmentAdded;
                }
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }

            return Ok(commonResponse);
        }


        [HttpPost("SaveCalendarData2")]
        [Route("SaveCalendarData2")]
        public IActionResult SaveCalendarData2([FromBody] CalendarAppVM data)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var us = _calendarService.GetUser(userId);
            string userName = us.UserName;

            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _calendarService.AddUpdate2(data, userId, userName).Result;
                if (commonResponse.status == 1)
                {
                    commonResponse.message = Helper.appointmentUpdated;
                }
                if (commonResponse.status == 2)
                {
                    commonResponse.message = Helper.appointmentAdded;
                }
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }

            return Ok(commonResponse);
        }






        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData()


        {


            CommonResponse<List<CalendarAppVM>> commonResponse = new CommonResponse<List<CalendarAppVM>>();


            try


            {


                if (role == "User")


                {


                    commonResponse.dataenum = _calendarService.MatchesById(loginUserId);


                    commonResponse.status = Helper.success_code;


                }


               
              


            }


            catch (Exception e)


            {


                commonResponse.message = e.Message;


                commonResponse.status = Helper.failure_code;


            }


            return Ok(commonResponse);


        }


        [HttpGet]
        [Route("GetCalendarDataById/{id}")]
        public IActionResult GetCalendarDataById(string id)


        {
            CommonResponse<CalendarAppVM> commonResponse = new CommonResponse<CalendarAppVM>();

            try

            {

                commonResponse.dataenum = _calendarService.GetById(id);
                commonResponse.status = Helper.success_code;
            }


            catch (Exception e)

            {

                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;

            }


            return Ok(commonResponse);


        }











    }
}
