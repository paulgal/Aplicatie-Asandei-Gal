using InterfaceModelClasses.Models;
using ServiceCaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Interface.Controllers
{
    public class NoteController : Controller
    {
        private ServiceCaller<NoteModel> _noteServiceCaller;
        public ServiceCaller<NoteModel> NoteServiceCaller
        {
            get
            {
                return _noteServiceCaller ?? (_noteServiceCaller = new ServiceCaller<NoteModel>());
            }
            set
            {
                _noteServiceCaller = value;
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
        // GET: Note
        [HttpGet]
        public ActionResult Index(int id)
        {
            var course = CourseServiceCaller.Get(id);
            ViewBag.CourseName = course.Name;
            ViewBag.CourseId = id;
            var noteList = NoteServiceCaller.GetAll().Where(x => x.CourseId == id);
            return View(noteList);
        }

        public ActionResult AddNote(int id, string content)
        {
            var course = CourseServiceCaller.Get(id);

            var newNote = new NoteModel()
            {
                CourseName = course.Name,
                CourseId = course.Id,
                Information = content
            };
            ViewBag.CourseId = id;
            NoteServiceCaller.Add(newNote);
            return RedirectToAction("Index", new { Id = id });
        }

    }
}