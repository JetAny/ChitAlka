using System.ComponentModel.DataAnnotations;

namespace DB_ChitAlka
{
    public partial class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Нет названия книги")]
        public string BookTitle { get; set; } = null!;
        public string? Annotation { get; set; }
        [Required(ErrorMessage = "Неизвестный жанр")]
        public string? Genre { get; set; }
        public string? BookImage { get; set; }
        public int? BookNumber { get; set; }
        public List<Section>? Sections { get; set; }
        public Author? Author { get; set; }
    }
}
