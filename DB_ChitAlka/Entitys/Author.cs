namespace DB_ChitAlka
{
    public partial class Author
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Book> Book { get; set; }
        //public virtual ICollection<Book> Books { get; set; }
    }
}
