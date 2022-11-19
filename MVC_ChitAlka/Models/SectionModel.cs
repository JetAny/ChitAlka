using System;
using System.Collections.Generic;

namespace MVC_ChitAlka
{
    public partial class SectionModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public int? BookModelId { get; set; }
        public int LastSection { get; set; }
    }
}
