using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTaskApp.Models;

namespace WebTaskApp.Controllers
{
	public class HomeController : Controller
	{
		int userCount = 0;
		public ActionResult Index()
		{
			userCount++;

			return View();
		}

		public ActionResult Welcome()
		{
			return View();
		}

		public ActionResult TaskList()
		{
			TaskListEntities orm = new TaskListEntities();

			ViewBag.Tasks = orm.Tasks.ToList();

			return View();
		}

		public ActionResult TaskView()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult About()
		{
			return View();
		}
		public ActionResult AddTask(int UserID)
		{
			TaskListEntities orm = new TaskListEntities();
			User Temp = orm.Users.FirstOrDefault(x => x.UserID == UserID);
			return View(Temp);
		}
		public ActionResult ListedTask(string Description, DateTime DueDate, bool Status, int UserID)
		{
			TaskListEntities orm = new TaskListEntities();
			Task Mytask = new Task();
			User Temp = orm.Users.FirstOrDefault(x => x.UserID == UserID);


			Mytask.Description = Description;
			Mytask.DueDate = DueDate;
			Mytask.Status = Status;
			Mytask.UserID = UserID;

			if (ModelState.IsValid)
			{
				orm.Tasks.Add(Mytask);
				orm.SaveChanges();
				ViewBag.AddTask = Mytask;
			}

			return RedirectToAction("Greeting", Temp);
		}

		public ActionResult Greeting(User newUser)
		{

			TaskListEntities orm = new TaskListEntities();

			if (ModelState.IsValid)
			{
				orm.Users.Add(newUser);
				orm.SaveChanges();
				ViewBag.UserName = newUser.Name;
				ViewBag.UserID = newUser.UserID;
			}
			User Temp = orm.Users.FirstOrDefault(x => x.Name == newUser.Name);
			newUser = Temp;
			return View(newUser);
		}
	}
}