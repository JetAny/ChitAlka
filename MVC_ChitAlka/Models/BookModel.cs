using System;
using System.Collections.Generic;

namespace MVC_ChitAlka
{
    public partial class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Genre { get; set; }
        public string BookImage { get; set; } = null!;
        public List<SectionModel>? SectionModel { get; set; }
        public AuthorModel AuthorModel { get; set; }
    }
}
