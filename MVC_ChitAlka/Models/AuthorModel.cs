using System;
using System.Collections.Generic;

namespace MVC_ChitAlka
{
    public partial class AuthorModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BookModel> BookModel { get; set; }
    }
}
