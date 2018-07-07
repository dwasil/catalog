using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;


namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IHostingEnvironment _environment;

        public ProductController(CatalogContext context, IHostingEnvironment IHostingEnvironment)
        {
            _context = context;           
            _environment = IHostingEnvironment;
            
            if (_context.Products.Count() == 0)
            {
                _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                 _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                 _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                 _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                 _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                 _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                 _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                 _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                 _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                 _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                 _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                 _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                 _context.Products.Add(
                    new Product { Name = "Туфли женские", Picture = "images/shoes.jpg", Price = 34.5, Quantity = 9, CategoryId = 1 }
                );
                
               _context.Products.Add(
                    new Product { Name = "Туфли мужские", Picture = "images/shoesf.jpg", Price = 34.5, Quantity = 9, CategoryId = 2 }
                );                
               _context.Products.Add(
                    new Product { Name = "Туфли мужские", Picture = "images/shoesf.jpg", Price = 34.5, Quantity = 9, CategoryId = 2 }
                );                
               _context.Products.Add(
                    new Product { Name = "Туфли мужские", Picture = "images/shoesf.jpg", Price = 34.5, Quantity = 9, CategoryId = 2 }
                );                
               _context.Products.Add(
                    new Product { Name = "Туфли мужские", Picture = "images/shoesf.jpg", Price = 34.5, Quantity = 9, CategoryId = 2 }
                );                
               _context.Products.Add(
                    new Product { Name = "Туфли мужские", Picture = "images/shoesf.jpg", Price = 34.5, Quantity = 9, CategoryId = 2 }
                );                
               _context.Products.Add(
                    new Product { Name = "Туфли мужские", Picture = "images/shoesf.jpg", Price = 34.5, Quantity = 9, CategoryId = 2 }
                );                
               _context.Products.Add(
                    new Product { Name = "Туфли мужские", Picture = "images/shoesf.jpg", Price = 34.5, Quantity = 9, CategoryId = 2 }
                );                
               _context.Products.Add(
                    new Product { Name = "Туфли мужские", Picture = "images/shoesf.jpg", Price = 34.5, Quantity = 9, CategoryId = 2 }
                );                
               _context.Products.Add(
                    new Product { Name = "Туфли мужские", Picture = "images/shoesf.jpg", Price = 34.5, Quantity = 9, CategoryId = 2 }
                );                
               _context.Products.Add(
                    new Product { Name = "Туфли мужские", Picture = "images/shoesf.jpg", Price = 34.5, Quantity = 9, CategoryId = 2 }
                );                
                _context.SaveChanges();
            }
        }               

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return _context.Products.ToList();
        }     

        [HttpGet("ByCategory/{id}")]
        public IQueryable<Product> GetByCategory(long id)
        {
            IQueryable<Product> prod = from s in _context.Products
                                    select s;               

            return prod.Where(s => s.CategoryId == id);
        }     

        [HttpGet("ByCategory2/{id}")]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;   // количество элементов на странице
             
            IQueryable<Product> source = from s in _context.Products
                                    select s;

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
 
            PageData pageData = new PageData(count, page, pageSize);
            IndexData data = new IndexData
            {
                PageData = pageData,
                Products = items
            };
            return Ok(new { data });
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<Product> GetById(long id)
        {
            var item = _context.Products.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(Product item)
        {
            _context.Products.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetProduct", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Product item)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = item.Name;            
            product.Price = item.Price;
            product.Quantity = item.Quantity;

            if(product.Picture != item.Picture){

                if(!String.IsNullOrEmpty(item.Picture))
                {
                     item.Picture = this.moveImageFromTmp(item.Picture);
                }

                product.Picture = item.Picture;
            }            

            _context.Products.Update(product);
            _context.SaveChanges();
            return NoContent();
        }        

        // todo: batch mode
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Products.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Products.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }        


        [HttpPost("UploadImage")]
        public async Task<IActionResult> Post(IFormFile image)
        {                           
            var path = string.Empty;

            if (image != null && image.Length > 0)
            {                
                var fileName = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');                
                //Assigning Unique Filename (Guid)
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                var FileExtension = Path.GetExtension(fileName);
                var newFileName = myUniqueFileName + FileExtension;
                path = @"upload/tmp/"+ newFileName;
                fileName = Path.Combine(_environment.WebRootPath, path);

                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }            
            }    

            return Ok(new { image.Length, path});
        }        
        
        protected String moveImageFromTmp  (String filePath)
        {
            if (filePath.Length <= 0){
                return String.Empty;
            } 

            var fullPath = Path.Combine(_environment.WebRootPath, filePath);

            if (!System.IO.File.Exists(fullPath)) {
                return String.Empty;
            } 

            var fileName = Path.GetFileName(filePath);
            var newPath = @"upload/images/"+fileName;            
            var newFullPath = Path.Combine(_environment.WebRootPath, newPath);           
            System.IO.File.Move(fullPath, newFullPath);            
            
            return newPath;
        }
    }
}