using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_ChitAlka
{
    public partial class Userlibrary
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public List<Book> Books { get; set; }

        //public User? UserId { get; set; }
        //[NotMapped]
        //public Book? Book { get; set; }
        //public int CapterId { get; set; }
    }
}
