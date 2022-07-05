using WebApplication1.Domain.Repositories;

namespace WebApplication1.Domain
{
	public class DataManager
	{
		public ITextFieldsRepository TextFields { get; set; }
		public IServiceItemsRepository ServiceItems { get; set; }

		public DataManager(ITextFieldsRepository textFields, IServiceItemsRepository serviceItems)
		{
			TextFields = textFields;
			ServiceItems = serviceItems;
		}
	}
}
