using DB_ChitAlka.Areas.Identity.Data;
using MVC_ChitAlka.Intrfaces;
using Microsoft.EntityFrameworkCore;

namespace MVC_ChitAlka.Servises
{
    public class SearchBookService : ISearchBookService
    {
        private readonly MyDbContext _dbContext;
        public SearchBookService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BookModel SearchBook(string value)
        {
            var book = new BookModel();
            var author= new AuthorModel();
            
            var allAuthorDB = _dbContext.Authors
                .Include(u => u.Book)
                .ToList();
            ServiceMap _serviceMap = new ServiceMap();
            var getAllAuthors = _serviceMap.MapDbModel(allAuthorDB);
             foreach (var authorService in getAllAuthors)
            {
                if(authorService.FirstName == value)
                {
                    
                    book.AuthorModel= authorService;
                    
                    return book;
                }
                if(authorService.LastName == value)
                {
                    book.AuthorModel = authorService;
                    return book;
                }
                foreach(var bookService in authorService.BookModel )
                {
                    if(bookService.Name == value)
                    {
                        book.AuthorModel = authorService;
                        return book;
                    }
                }
            }
            return null;
        }
    }
}
