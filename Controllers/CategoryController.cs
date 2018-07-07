using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Catalog.Models;
using System.Collections;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CatalogContext _context;

        public CategoryController(CatalogContext context)
        {
            _context = context;
            
            if (_context.Categories.Count() == 0)
            {
                _context.Categories.Add(new Category { Id = 1, Name = "Обувь", ParentId = 0 });
                _context.Categories.Add(new Category { Id = 2, Name = "Туфли женские", ParentId = 1 });
                _context.Categories.Add(new Category { Id = 3, Name = "Туфли мужские", ParentId = 1 });
                _context.Categories.Add(new Category { Id = 4, Name = "Открытые", ParentId = 2 });
                _context.Categories.Add(new Category { Id = 5, Name = "Закрытые", ParentId = 2 });
                _context.Categories.Add(new Category { Id = 6, Name = "Одежда", ParentId = 0 });                 
                _context.Categories.Add(new Category { Id = 7, Name = "Для женщин", ParentId = 6 });
                _context.Categories.Add(new Category { Id = 8, Name = "Платья", ParentId = 7 });
                _context.Categories.Add(new Category { Id = 9, Name = "Юбки", ParentId = 7 });
                _context.Categories.Add(new Category { Id = 10, Name = "Для мужчин", ParentId = 6 });
                _context.Categories.Add(new Category { Id = 11, Name = "Брюки", ParentId = 10 });
                _context.Categories.Add(new Category { Id = 12, Name = "Свитера", ParentId = 10 });
                _context.Categories.Add(new Category { Id = 13, Name = "Кроссовки", ParentId = 1 });
                _context.Categories.Add(new Category { Id = 14, Name = "Туфли детские", ParentId = 1 });
                _context.Categories.Add(new Category { Id = 15, Name = "Тапочки", ParentId = 1 });
                _context.Categories.Add(new Category { Id = 16, Name = "Сапоги", ParentId = 1 }); 
                _context.SaveChanges();
            }
        }       

        [HttpGet]
        public ActionResult<List<Category>> GetAll()
        {
            return _context.Categories.ToList();
        }

        [HttpGet("Tree")]
        public ActionResult<List<Category>> GetTree()
        {
            var childsHash = _context.Categories.ToLookup(cat => cat.ParentId);

            foreach (var cat in _context.Categories)
            {
                cat.SubCategories = childsHash[cat.Id].ToList();           
            }         

            return _context.Categories.Where( cat => cat.ParentId == 0).ToList();
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<Category> GetById(long id)
        {
            var item = _context.Categories.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }        
    }    
}