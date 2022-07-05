using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain;
using WebApplication1.Domain.Entities;
using WebApplication1.Service;

namespace WebApplication1.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class TextFieldsController : Controller
	{
		private readonly DataManager _dataManager;

		public TextFieldsController(DataManager dataManager)
		{
			_dataManager = dataManager;
		}

		public IActionResult Edit(string codeWord)
		{
			var entity = _dataManager.TextFields.GetTextFieldByCodeWord(codeWord);

			return View(entity);
		}

		[HttpPost]
		public IActionResult Edit(TextField model)
		{
			if (ModelState.IsValid)
			{
				_dataManager.TextFields.SaveTextField(model);

				return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
			}

			return View(model);
		}
	}
}
