using System;
using System.Collections.Generic;

namespace DB_ChitAlka
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Genre { get; set; }
        public string BookImage { get; set; } = null!;
    }
}
