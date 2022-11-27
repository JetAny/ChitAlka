namespace DB_ChitAlka
{
    public partial class Userlibrary
    {
        public int Id { get; set; }

        //public User? UserId { get; set; }
        //[NotMapped]
        public Book? Book { get; set; }
        public int CurentSectionId { get; set; }
        
    }
}
