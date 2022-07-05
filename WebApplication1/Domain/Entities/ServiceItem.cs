using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Domain.Entities
{
	public class ServiceItem : EntityBase
	{
		[Required(ErrorMessage = "Заполните название услуги")]
		[Display(Name = "Название услуги")]
		public string Title { get; set; }

		[Display(Name = "Краткое описание услуги")]
		public override string SubTitle { get; set; }

		[Display(Name = "Полное содержание услуги")]
		public override string Text { get; set; }
	}
}
