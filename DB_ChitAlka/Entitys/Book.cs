using System;
using System.Collections.Generic;

namespace DB_ChitAlka
{
    public partial class Book
    {
        public int Id { get; set; }
        public string BookTitle { get; set; } = null!;
        public string Annotation { get; set; } = null!;
        public string? Genre { get; set; }
        public string? BookImage { get; set; }
        //public List<Section>? Section { get; set; }
        public int? AuthorId { get; set; }
    }
}
