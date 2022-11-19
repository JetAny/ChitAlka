using DB_ChitAlka;
using DB_ChitAlka.Interfases;
using Microsoft.EntityFrameworkCore;
using MVC_ChitAlka.Intrfaces;

namespace MVC_ChitAlka.Servises
{
    public class SearchBookService : ISearchBookService
    {
        private readonly IDbContext _dbContext;
        public SearchBookService(IDbContext dbContext)
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
