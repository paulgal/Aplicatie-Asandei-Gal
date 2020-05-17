using Calculation;
using InterfaceModelClasses.Models;
using ServiceCaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interface.Controllers
{
    public class HomeController : Controller
    {
        private ServiceCaller<AccountModel> _accountServiceCaller;
        public ServiceCaller<AccountModel> AccountServiceCaller
        {
            get
            {
                return _accountServiceCaller ?? (_accountServiceCaller = new ServiceCaller<AccountModel>());
            }
            set
            {
                _accountServiceCaller = value;
            }
        }
        private ServiceCaller<OrganizerModel> _organizerServiceCaller;
        public ServiceCaller<OrganizerModel> OrganizerServiceCaller
        {
            get
            {
                return _organizerServiceCaller ?? (_organizerServiceCaller = new ServiceCaller<OrganizerModel>());
            }
            set
            {
                _organizerServiceCaller = value;
            }
        }
        public ActionResult Index()
        {
            var nextDay = CalculationClass.GetWeekDayFromDateTime(DateTime.Now.AddDays(1).DayOfWeek);
            ViewBag.NextDay = nextDay.ToString();
            if (Request.IsAuthenticated)
            {        
                var account = AccountServiceCaller.Get(User.Identity.Name);
                var organizer = OrganizerServiceCaller.Get(account.OrganizerId);
                var courseList = CalculationClass.GetCoursesFromDay(organizer.Courses.ToList(), nextDay);
                ViewBag.CourseList = courseList;
                var nextExam = CalculationClass.GetNextExam(organizer.Exams.ToList());
                var daysTillTheNextExam = 0;
                if (nextExam != null)
                    daysTillTheNextExam = CalculationClass.CalculateNumberOfDaysTillActivity(nextExam.Date);
                ViewBag.NextExam = nextExam;
                ViewBag.DaysTillTheNextExam = daysTillTheNextExam;
            }
            else
            {
                ViewBag.CourseList = new List<CourseModel>();
                ViewBag.NextExam = null;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}