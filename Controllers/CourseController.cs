using Calculation;
using Interface.Models;
using InterfaceModelClasses.Models;
using ModelProject.Model;
using ServiceCaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interface.Controllers
{
    public class CourseController : Controller
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
        private ServiceCaller<CourseModel> _courseServiceCaller;
        public ServiceCaller<CourseModel> CourseServiceCaller
        {
            get
            {
                return _courseServiceCaller ?? (_courseServiceCaller = new ServiceCaller<CourseModel>());
            }
            set
            {
                _courseServiceCaller = value;
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
        // GET: Course
        public ActionResult Index()
        {
            var currentUser = AccountServiceCaller.Get(User.Identity.Name);
            var userOrganizer = OrganizerServiceCaller.Get(currentUser.OrganizerId);
            var courseList = userOrganizer.Courses;
            courseList = CalculationClass.SortCoursesByDayAndHour(courseList.ToList());
            return View(courseList);
        }
        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCourse(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var currentUser = AccountServiceCaller.Get(User.Identity.Name);
            var userOrganizer = OrganizerServiceCaller.Get(currentUser.OrganizerId);
            var newCourse = new CourseModel();
            newCourse.OrganizerId = userOrganizer.Id;
            newCourse.Name = model.CourseName;
            newCourse.Hour = model.CourseHour;
            newCourse.Professor = model.CourseProfessor;

            newCourse.Day = GetWeekDayFromString(model.CourseDay);

            try
            {
                CourseServiceCaller.Add(newCourse);
                return RedirectToAction("Index", "Course");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public WeekDay GetWeekDayFromString(string day)
        {
            switch (day)
            {
                case "Luni":
                    {
                        return WeekDay.Luni;
                    }
                case "Marti":
                    {
                        return WeekDay.Marti;
                    }
                case "Miercuri":
                    {
                        return WeekDay.Miercuri;
                    }
                case "Joi":
                    {
                        return WeekDay.Joi;
                    }
                case "Vineri":
                    {
                        return WeekDay.Vineri;
                    }
            }
            return WeekDay.Default;
        }
        public ActionResult Delete(int id)
        {
            CourseServiceCaller.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            ViewBag.CourseId = id;
            var course = CourseServiceCaller.Get(id);
            var courseModel = new EditCourseViewModel()
            {
                CourseDay = course.Day.ToString(),
                CourseHour = course.Hour,
                CourseName = course.Name,
                CourseProfessor = course.Professor
            };
            return View(courseModel);
        }

        [HttpPost]
        public ActionResult EditCourse(int id,EditCourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var course = CourseServiceCaller.Get(id);
            if (!String.IsNullOrEmpty(model.CourseName)) course.Name = model.CourseName;
            if (!String.IsNullOrEmpty(model.CourseProfessor)) course.Professor = model.CourseProfessor;
            if (!String.IsNullOrEmpty(model.CourseHour.ToString())) course.Hour = model.CourseHour ?? 0;
            if (!model.CourseDay.Equals("Default")) course.Day = GetWeekDayFromString(model.CourseDay);
            try
            {
                CourseServiceCaller.Update(course);
                return RedirectToAction("Index", "Course");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}