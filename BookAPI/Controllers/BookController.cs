using BookAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XAct.Messages;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DataContext _context;

        public BookController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]

        public async Task<ActionResult<List<Book>>> GetBook()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var dbbook = await _context.Books.FindAsync(id);

            if (dbbook == null)
            {
                return BadRequest(" Book not found");
            }
            return Ok(dbbook);
        }


        [HttpGet("{size}/{page}")]
        public IActionResult GetAllBook(int size,int page)
        {
            var customers = _context.Books.ToList(); // Fetch all customers from the database

            var totalRecords = customers.Count;
            var pagedCustomers = customers
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            var response = new PagedResponse<Book>
            {
                PageIndex = page,
                PageSize = size,
                TotalCount = totalRecords,
                Data = pagedCustomers,
                
            };

            return Ok(response); 
        }


        [HttpPost]

        public async Task<ActionResult<Book>> CreateBook(Book book)

        {

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            var dbBook = await _context.Books.FindAsync(book.id);

            if (dbBook != null)
            {
                dbBook.cover = book.cover.Replace("C:\\fakepath\\", ".\\assets\\img\\");
                await _context.SaveChangesAsync();
                return Ok(await _context.Books.ToListAsync());
            }
            return Ok(await _context.Books.ToListAsync());


        }


        [HttpPut]
        public async Task<ActionResult<List<Book>>> UpdateBook(Book book)
        {
            var dbBook = await _context.Books.FindAsync(book.id);

            if (dbBook == null)
            {
                return BadRequest("Book not found");
            }

            else

            {


                dbBook.cover = book.cover.Replace("C:\\fakepath\\", ".\\assets\\img\\");
                dbBook.title = book.title;
                dbBook.description = book.description;
                dbBook.author = book.author;
                dbBook.year = book.year;
                dbBook.pages = book.pages;




                await _context.SaveChangesAsync();

                return Ok(await _context.Books.ToListAsync());
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Book>>> DeleteBook(int id)
        {
            var dbBook = await _context.Books.FindAsync(id);

            if (dbBook == null)
            {
                return BadRequest("Book not found");
            }

            else
            {

                _context.Books.Remove(dbBook);

                await _context.SaveChangesAsync();

                return Ok(await _context.Books.ToListAsync());
            }
        }



    }
}
