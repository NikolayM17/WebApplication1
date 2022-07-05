using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Domain;

namespace WebApplication1.Models.ViewComponents
{
	public class SidebarViewComponent : ViewComponent
	{
		private readonly DataManager _dataManager;

		public SidebarViewComponent(DataManager dataManager)
		{
			_dataManager = dataManager;
		}

		public Task<IViewComponentResult> InvokeAsync()
		{
			return Task.FromResult((IViewComponentResult)View("Default", _dataManager.ServiceItems.GetServiceItems()));
		}
	}
}
