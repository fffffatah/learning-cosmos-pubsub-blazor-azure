using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CosmosRepository;
using CosmosRepositoryPatternCRUD.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

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
            book.Type = "Book";
            await _bookRepository.CreateAsync(book);
            return Ok(new { Code = "200", Status = "Ok", Data = "Book Added" });
        }
        [Route("get/books")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks([FromHeader]string ConToken, int pageSize)
        {
            //var books = await _bookRepository.GetByQueryAsync("select * from container");
            /// <summary>
            /// The continuation token has to be the newest one each time fetching
            /// the next 5 items. pageSize is the number of items to fetch.
            /// </summary>
            var page = await _bookRepository.PageAsync(pageSize: pageSize, continuationToken: (ConToken == "empty") ? null : Regex.Unescape(ConToken));
            return Ok(new { Code = "200", Status = "Ok", Data = new { ItemCount = page.Total, ConToken = page.Continuation, Items = page.Items } });
        }
        [Route("get/book")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBook(string bookid)
        { 
            //var book = await _bookRepository.GetByQueryAsync($"select * from container b where b.id='{bookid}'");
            //var book = await _bookRepository.GetAsync(b => b.Id == bookid);
            var book = await _bookRepository.GetAsync(bookid);
            return Ok(new { Code = "200", Status = "Ok", Data = book });
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
