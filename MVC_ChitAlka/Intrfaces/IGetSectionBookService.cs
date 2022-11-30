namespace MVC_ChitAlka.Intrfaces
{
    public interface IGetSectionBookService
    {
       List<SectionModel> GetSection(int bookId, int sectionID);
    }
}
