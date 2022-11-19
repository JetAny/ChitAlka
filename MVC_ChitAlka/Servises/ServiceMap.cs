using DB_ChitAlka;

namespace MVC_ChitAlka.Servises
{
    public class ServiceMap
    {
        public List<AuthorModel> MapDbModel(List<Author> allAuthorDB)
        {
            List<AuthorModel> allAuthors = new List<AuthorModel>();
            foreach (Author model in allAuthorDB)
            {
                var author = new AuthorModel();
                author.Id = model.Id;
                author.FirstName = model.FirstName;
                author.LastName = model.LastName;


                List<BookModel> allBooks = new List<BookModel>();

                foreach (Book bookmodel in model.Book)
                {
                    var book = new BookModel();
                    book.Id = bookmodel.Id;
                    book.Name = bookmodel.BookTitle;
                    book.Genre = bookmodel.Genre;
                    book.Description = bookmodel.Annotation;
                    book.BookImage = bookmodel.BookImage;
                    book.AuthorModel = author;

                    allBooks.Add(book);
                }
                author.BookModel = allBooks;
                allAuthors.Add(author);
            }
            return allAuthors;

        }
        public BookModel MapDbBook(List<Book> BookDB, int Id)
        {
            var author = new AuthorModel();
            var book = new BookModel();
            List<SectionModel> section = new List<SectionModel>();
            foreach (Book bookmodel in BookDB)
            {
                if (bookmodel.Id == Id)
                {
                    book.AuthorModel = author;
                    book.Id = bookmodel.Id;
                    book.Name = bookmodel.BookTitle;
                    book.Genre = bookmodel.Genre;
                    book.Description = bookmodel.Annotation;
                    book.BookImage = bookmodel.BookImage;
                    foreach (Section s in bookmodel.Section)
                    {
                        var sectionmodel = new SectionModel();
                        sectionmodel.Id = s.Id;
                        sectionmodel.Title = s.Title;
                        sectionmodel.Text = s.Text;
                        section.Add(sectionmodel);
                    }
                    book.SectionModel = section;
                    author.FirstName = bookmodel.Author.FirstName;
                    author.LastName = bookmodel.Author.LastName;
                    book.AuthorModel = author;
                }
            }
            return book;
        }
        internal List<SectionModel> MapDbSection(List<Book> bookDB, int bookId, int sectionID)
        {
            var st = 0;
            var book = new BookModel();
            List<SectionModel> actualSection = new List<SectionModel>();
            foreach (Book bookmodel in bookDB)
            {
                if (bookmodel.Id == bookId)
                {
                    foreach (Section s in bookmodel.Section)
                    {
                        st=st+1;
                        var sectionmodel = new SectionModel();
                       
                        sectionmodel.Id = st;
                        sectionmodel.Title = s.Title;
                        sectionmodel.Text = s.Text;
                        sectionmodel.BookModelId=bookmodel.Id;
                        if (st == sectionID)
                        {
                            actualSection.Add(sectionmodel);
                            return actualSection;
                        }
                    }
                    if (actualSection.Count == 0)
                    {
                        return actualSection;
                    }

                }
            }           
            return actualSection;
        }


    }
}
