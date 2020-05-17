using Calculation;
using Interface.Models;
using InterfaceModelClasses.Models;
using ServiceCaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interface.Controllers
{
    public class ExamController : Controller
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
        private ServiceCaller<ExamModel> _examServiceCaller;
        public ServiceCaller<ExamModel> ExamServiceCaller
        {
            get
            {
                return _examServiceCaller ?? (_examServiceCaller = new ServiceCaller<ExamModel>());
            }
            set
            {
                _examServiceCaller = value;
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
        // GET: Exam
        public ActionResult Index()
        {
            var currentUser = AccountServiceCaller.Get(User.Identity.Name);
            var userOrganizer = OrganizerServiceCaller.Get(currentUser.OrganizerId);
            var examList = CalculationClass.SortExamsByDifficulty(userOrganizer.Exams.ToList());

            return View(examList);
        }

        public ActionResult AddExam()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddExam(ExamViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var currentUser = AccountServiceCaller.Get(User.Identity.Name);
            var userOrganizer = OrganizerServiceCaller.Get(currentUser.OrganizerId);
            var newExam = new ExamModel();
            newExam.OrganizerId = userOrganizer.Id;
            newExam.CourseName = model.CourseName;
            newExam.Hour = model.Hour;
            newExam.Date = model.ExamDay;
            newExam.Room = model.ExamRoom;
            newExam.Difficulty = model.Difficulty;

            try
            {
                ExamServiceCaller.Add(newExam);
                return RedirectToAction("Index", "Exam");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult Delete(int id)
        {
            ExamServiceCaller.Delete(id);

            return RedirectToAction("Index", "Exam");
        }
        [HttpGet]
        public ActionResult EditExam(int id)
        {
            ViewBag.CourseId = id;
            var exam = ExamServiceCaller.Get(id);
            var examModel = new ExamEditViewModel()
            {
                CourseName = exam.CourseName,
                Difficulty = exam.Difficulty,
                ExamDay = exam.Date,
                ExamRoom = exam.Room,
                Hour = exam.Hour
            };
            return View(examModel);
        }

        [HttpPost]
        public ActionResult EditExam(int id, ExamEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var exam = ExamServiceCaller.Get(id);
            if (!String.IsNullOrEmpty(model.CourseName)) exam.CourseName = model.CourseName;
            if (model.ExamDay != null) exam.Date = model.ExamDay ?? new DateTime();
            if (!String.IsNullOrEmpty(model.Hour.ToString())) exam.Hour = model.Hour ?? 0;
            if (!String.IsNullOrEmpty(model.Difficulty.ToString())) exam.Difficulty = model.Difficulty ?? 0;
            if (!String.IsNullOrEmpty(model.ExamRoom)) exam.Room = model.ExamRoom;
            try
            {
                ExamServiceCaller.Update(exam);
                return RedirectToAction("Index", "Exam");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}