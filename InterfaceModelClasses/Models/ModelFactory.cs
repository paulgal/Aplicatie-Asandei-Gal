using ModelProject.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceModelClasses.Models
{
    public class ModelFactory
    {
        private IRepository _repository;
        public ModelFactory(IRepository repository)
        {
            _repository = repository;
        }
        public AccountModel Create(Account account)
        {
            return new AccountModel()
            {
                Id = account.Id,
                Username = account.Username,
                Password = account.Password,
                FullName = account.FullName,
                Email = account.Email,
                OrganizerId = account.AccountOrganizer.Id,
                PhoneNumber = account.PhoneNumber,
                SettingsId = account.AccountSettings.Id
            };
        }
        public SettingsModel Create(Settings settings)
        {
            return new SettingsModel()
            {
                Id = settings.Id,
                Username = settings.UserAccount.Username,
                AccountId = settings.UserAccount.Id,
                CourseDuration = settings.CourseDuration,
                DefaultStartHour = settings.DefaultStartHour,
                StartDay = settings.WeekStartDay
            };
        }

        public OrganizerModel Create(Organizer organizer)
        {
            return new OrganizerModel()
            {
                Id = organizer.Id,
                Username = organizer.UserAccount.Username,
                AccountId = organizer.UserAccount.Id,
                Courses = organizer.Courses.Select(x => Create(x)).ToList(),
                Exams = organizer.Exams.Select(x =>Create(x)).ToList()
            };
        }

        public ExamModel Create(Exam exam)
        {
            return new ExamModel()
            {
                Id = exam.Id,
                CourseName = exam.CourseName,
                Date = exam.Date,
                Difficulty = exam.Difficulty,
                Hour = exam.Hour,
                Room = exam.Room,
                OrganizerId = exam.AccountOrganizer.Id
            };
        }

        public CourseModel Create(Course course)
        {
            return new CourseModel()
            {
                Id = course.Id,
                Day = course.Day,
                Hour = course.Hour,
                Professor = course.Professor,
                Name = course.Name,
                OrganizerId = course.AccountOrganizer.Id
            };
        }

        public NoteModel Create(Note notes)
        {
            return new NoteModel()
            {
                Id = notes.Id,
                CourseName = notes.Course.Name,
                Information = notes.Information,
                CourseId = notes.Course.Id
            };
        }
        public ReminderModel Create(Reminder reminder)
        {
            ReminderModel reminderModel =  new ReminderModel()
            {
                Id = reminder.Id,
                Date = reminder.Date,
                ReminderInfo = reminder.ReminderInfo
            };
            if(reminder.ReminderActivity is Course)
            {
                Course course = reminder.ReminderActivity as Course;
                reminderModel.ActivityId = course.Id;
                reminderModel.Type = ActivityType.Course;
            }
            else
            {
                Exam exam = reminder.ReminderActivity as Exam;
                reminderModel.ActivityId = exam.Id;
                reminderModel.Type = ActivityType.Exam;
            }
            return reminderModel;
        }
        public Account Parse(AccountModel model)
        {
            Account account = new Account()
            {
                Id = model.Id,
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                AccountOrganizer = _repository.GetOrganizer(model.OrganizerId) ?? new Organizer() ,
                AccountSettings = _repository.GetSettings(model.OrganizerId) ?? new Settings()
            };
            return account;
        }
        public Settings Parse(SettingsModel model)
        {
            Settings settings = new Settings()
            {
                Id = model.Id,
                UserAccount = _repository.GetAccount(model.AccountId) ?? new Account(),
                CourseDuration = model.CourseDuration,
                DefaultStartHour = model.DefaultStartHour,
                WeekStartDay = model.StartDay                         
            };
            return settings;
        }
        public Organizer Parse(OrganizerModel model)
        {
            Organizer organizer = new Organizer()
            {
                Id = model.Id,
                UserAccount = _repository.GetAccount(model.AccountId) ?? new Account(),
                Courses = model.Courses.Select(x=> Parse(x)).ToList(),
                Exams = model.Exams.Select(x=> Parse(x)).ToList()
            };
            return organizer;
        }
        public Exam Parse(ExamModel model)
        {
            Exam exam = new Exam()
            {
                Id = model.Id,
                AccountOrganizer = _repository.GetOrganizer(model.OrganizerId),
                CourseName = model.CourseName,
                Date = model.Date,
                Difficulty = model.Difficulty,
                Hour = model.Hour,
                Room = model.Room
            };
            return exam;
        }
        public Course Parse(CourseModel model)
        {
            
            Course course = new Course()
            {
                Id = model.Id,
                AccountOrganizer = _repository.GetOrganizer(model.OrganizerId),
                Day = model.Day,
                Hour = model.Hour,
                Name = model.Name,
                Professor = model.Professor
            };
            return course;
        }
        public Note Parse(NoteModel model)
        {
            Note note = new Note()
            {
                Id = model.Id,
                Course = _repository.GetCourse(model.CourseId),
                Information = model.Information
             
            };
            return note;
        }
        public Reminder Parse(ReminderModel model)
        {
            Reminder reminder = new Reminder()
            {
                Id = model.Id,
                Date = model.Date,
                ReminderInfo = model.ReminderInfo

            };
            switch(model.Type)
            {
                case ActivityType.Course:
                    {
                        reminder.ReminderActivity = _repository.GetCourse(model.ActivityId);
                        break;
                    }
                case ActivityType.Exam:
                    {
                        reminder.ReminderActivity = _repository.GetExam(model.ActivityId);
                        break;
                    }
            }
            return reminder;
        }
    }
}