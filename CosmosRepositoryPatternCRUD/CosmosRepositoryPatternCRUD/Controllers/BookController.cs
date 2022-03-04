using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CosmosRepository;
using CosmosRepositoryPatternCRUD.Models;
using System.Text.Json;
using System.Text.RegularExpressions;
using Polly;
using Polly.Retry;
using System.Linq.Expressions;

namespace CosmosRepositoryPatternCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        ILogger<BookController> _logger;
        IRepository<Book> _bookRepository;
        AsyncRetryPolicy<ActionResult> _retryPolicy;
        public BookController(ILogger<BookController> logger, IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
            _logger = logger;
            _retryPolicy = Policy<ActionResult>.Handle<HttpRequestException>().RetryAsync(retryCount: 5);
        }

        [Route("add/book")]
        [HttpPost]
        public async Task<ActionResult> AddBook([FromForm] Book book)
        {
            book.Id = Guid.NewGuid().ToString();
            book.Type = "Book";
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                var res = await _bookRepository.CreateAsync(book);
                if (res != null) return Ok(new { Code = "200", Status = "Ok", Data = "Book Added" });
                return BadRequest(new { Code = "400", Status = "BadRequest", Data = "Unable to add book!" });
            });
        }

        [Route("get/books/scroll")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks([FromHeader]string ConToken, int pageSize)
        {
            /// <summary>
            /// The continuation token has to be the newest one each time fetching
            /// the next 5 items (Assuming, pageSize = 5). pageSize is the number of items to fetch.
            /// </summary>
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                var page = await _bookRepository.PageAsync(pageSize: pageSize, continuationToken: (ConToken == "empty") ? null : Regex.Unescape(ConToken));
                if (page != null) return Ok(new { Code = "200", Status = "Ok", Data = new { ItemCount = page.Total, ConToken = page.Continuation, Items = page.Items } });
                return BadRequest(new { Code = "400", Status = "BadRequest", Data = "Unable to get books!" });
            });
        }

        [Route("get/books/genre/scroll")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooksByGenre([FromHeader] string ConToken, int pageSize, string genre)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                Expression<Func<Book, bool>> expression = b => b.Genre == genre;
                var page = await _bookRepository.PageAsync(expression, pageSize: pageSize, continuationToken: (ConToken == "empty") ? null : Regex.Unescape(ConToken));
                if (page != null) return Ok(new { Code = "200", Status = "Ok", Data = new { ItemCount = page.Total, ConToken = page.Continuation, Items = page.Items } });
                return BadRequest(new { Code = "400", Status = "BadRequest", Data = "Unable to get books!" });
            });
        }

        [Route("get/books/page")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooksByPage(int pageSize, int pageNo)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                var page = await _bookRepository.PageAsync(pageNumber:pageNo, pageSize: pageSize);
                if (page != null) return Ok(new { Code = "200", Status = "Ok", Data = new { ItemCount = page.Total, ConToken = page.Continuation, Items = page.Items } });
                return BadRequest(new { Code = "400", Status = "BadRequest", Data = "Unable to get books!" });
            });
        }

        [Route("get/book")]
        [HttpGet]
        public async Task<ActionResult<Book>> GetBook(string bookid, string genre)
        {
            //var book = await _bookRepository.GetByQueryAsync($"select * from container b where b.id='{bookid}'");
            //var book = await _bookRepository.GetAsync(b => b.Id == bookid);
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                var res = await _bookRepository.GetAsync(id: bookid, partitionKeyValue: genre);
                if (res != null) return Ok(new { Code = "200", Status = "Ok", Data = res });
                return BadRequest(new { Code = "400", Status = "BadRequest", Data = "Unable to get book!" });
            });
        }

        [Route("update/book")]
        [HttpPost]
        public async Task<ActionResult> UpdateBook([FromForm]Book book)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                var res = await _bookRepository.UpdateAsync(book);
                if (res != null) return Ok(new { Code = "200", Status = "Ok", Data = "Book Updated" });
                return BadRequest(new { Code = "400", Status = "BadRequest", Data = "Unable to update book!" });
            });
        }

        [Route("delete/book")]
        [HttpGet]
        public async Task<ActionResult> DeleteBook(string bookid)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                await _bookRepository.DeleteAsync(bookid);
                return Ok(new { Code = "200", Status = "Ok", Data = "Book Deleted" });
            });
        }
    }
}
