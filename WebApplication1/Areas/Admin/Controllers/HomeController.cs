using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.Domain;

namespace WebApplication1.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		private readonly DataManager _dataManager;

		public HomeController(DataManager dataManager)
		{
			_dataManager = dataManager;
		}

		public IActionResult Index()
		{
			return View(_dataManager.ServiceItems.GetServiceItems());
		}
	}
}
