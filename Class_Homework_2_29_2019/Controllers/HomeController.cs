using Class_Homework_2_29_2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Class_Homework_2_29_2019.Controllers
{
    public class HomeController : Controller
    {
        ToDoItemsManager mng = new ToDoItemsManager(Properties.Settings.Default.TaskConStr);
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Tasks()
        {
            IEnumerable<Categories> categories = mng.GetAllIncomplete();
            bool NoTask = categories.All(c => c.ToDoItems == null);
            if (NoTask == true)
            {
                categories = null;
            }
            return View(categories);
        }
        public ActionResult CompletedTasks()
        {
            IEnumerable<Categories> categories = mng.GetAllComplete();
            return View(categories);
        }

        public ActionResult AddTaskPage()
        {
            IEnumerable<Categories> categories = mng.GetAllCategories();
            return View(categories);
        }

        public ActionResult AddCategoryPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTask(ToDoItem toDoItem)
        {
            mng.AddToDoItem(toDoItem);
            return Redirect("~/Home/Tasks");
        }

        [HttpPost]
        public ActionResult Complete(int Id, DateTime? Date)
        {
            if (Date == null)
            {
                Date = DateTime.Now;
            }
            DateTime date = (DateTime)Date;
            mng.AddCompletedDate(Id, date);
            return Redirect("~/Home/Tasks");
        }
        [HttpPost]
        public ActionResult AddCategory(string Name)
        {
            mng.AddCategory(Name);
            return Redirect("~/Home/Tasks");
        }
    }
}