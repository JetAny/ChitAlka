﻿using System;
using System.Collections.Generic;

namespace DB_ChitAlka
{
    public partial class Author
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //public Book BookId { get; set; }
        //public virtual ICollection<Book> Books { get; set; }
    }
}
