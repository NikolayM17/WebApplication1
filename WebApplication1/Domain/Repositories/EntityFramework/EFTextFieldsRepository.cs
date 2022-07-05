using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Domain.Repositories.EntityFramework
{
	public class EFTextFieldsRepository : ITextFieldsRepository
	{
		private readonly AppDbContext _context;

		public EFTextFieldsRepository(AppDbContext context)
		{
			_context = context;
		}

		public TextField GetTextFieldByCodeWord(string codeWord)
			=> _context.TextFields.First(x => x.CodeWord == codeWord);

		public TextField GetTextFieldById(Guid id)
			=> _context.TextFields.FirstOrDefault(x => x.Id == id);

		public IQueryable<TextField> GetTextFields() => _context.TextFields;

		public void SaveTextField(TextField entity)
		{
			if (entity.Id == default)
			{
				_context.Entry(entity).State = EntityState.Added;
			}
			else
			{
				_context.Entry(entity).State = EntityState.Modified;
			}

			_context.SaveChanges();
		}

		public void DeleteTextField(Guid id)
		{
			_context.TextFields.Remove(new TextField { Id = id });

			_context.SaveChanges();
		}
	}
}
