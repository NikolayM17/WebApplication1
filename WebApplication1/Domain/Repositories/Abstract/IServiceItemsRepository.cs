using System;
using System.Linq;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Domain.Repositories
{
	public interface IServiceItemsRepository
	{
		IQueryable<ServiceItem> GetServiceItems();
		ServiceItem GetServiceItemById(Guid id);
		void SaveServiceItem(ServiceItem entity);
		void DeleteServiceItem(Guid id);
	}
}
