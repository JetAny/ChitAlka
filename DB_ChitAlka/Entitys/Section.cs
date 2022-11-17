using System;
using System.Collections.Generic;

namespace DB_ChitAlka
{
    public partial class Section
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public int? BookId { get; set; }
    }
}
