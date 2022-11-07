using System;
using System.Collections.Generic;

namespace MVC_ChitAlka
{
    public partial class UserlibraryModel
    {
        public int Id { get; set; }
        public string? User { get; set; }
        public string? Book { get; set; }
        public string? CapterId { get; set; }
    }
}
