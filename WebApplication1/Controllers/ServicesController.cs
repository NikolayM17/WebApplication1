using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.Domain;

namespace WebApplication1.Controllers
{
	public class ServicesController : Controller
	{
		private readonly DataManager _dataManager;

		public ServicesController(DataManager dataManager)
		{
			_dataManager = dataManager;
		}

		public IActionResult Index(Guid id)
		{
			if (id != default)
			{
				return View("Show", _dataManager.ServiceItems.GetServiceItemById(id));
			}

			ViewBag.TextField = _dataManager.TextFields.GetTextFieldByCodeWord("PageServices");
			return View(_dataManager.ServiceItems.GetServiceItems());
		}
	}
}
