using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CosmosRepository;
using CosmosRepositoryPatternCRUD.Models;

namespace CosmosRepositoryPatternCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        ILogger<BookController> _logger;
        IRepository<Book> _bookRepository;
        public BookController(ILogger<BookController> logger, IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }
        [Route("add/book")]
        [HttpPost]
        public async Task<ActionResult> AddBook([FromForm] Book book)
        {
            book.Id = Guid.NewGuid().ToString();
            book.Type = "default";
            await _bookRepository.CreateAsync(book);
            return Ok(new { Code = "200", Status = "Ok", Data = "Book Added" });
        }
        [Route("get/books")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _bookRepository.GetByQueryAsync("select * from container");
            return Ok(new { Code = "200", Status = "Ok", Data = books });
        }
        [Route("get/book")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBook(string bookid)
        {
            var books = await _bookRepository.GetByQueryAsync($"select * from container where Id='{bookid}'");
            return Ok(new { Code = "200", Status = "Ok", Data = books });
        }
        [Route("update/book")]
        [HttpPost]
        public async Task<ActionResult> UpdateBook([FromForm]Book book)
        {
            await _bookRepository.UpdateAsync(book);
            return Ok(new { Code = "200", Status = "Ok", Data = "Book Updated" });
        }
        [Route("delete/book")]
        [HttpGet]
        public async Task<ActionResult> DeleteBook(string bookid)
        {
            await _bookRepository.DeleteAsync(bookid);
            return Ok(new { Code = "200", Status = "Ok", Data = "Book Deleted" });
        }
    }
}
