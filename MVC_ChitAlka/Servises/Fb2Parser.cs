using DB_ChitAlka;
using System.Xml;

namespace MVC_ChitAlka.Servises
{
    public class Fb2Parser
    {

        public string _path;

        public Fb2Parser(string path)
        {
            _path = path;
        }

        public Book Parse()
        {
            Book book = new Book();
            Author author = new Author();
            List<Section> sections = new List<Section>();

            var xml = new XmlDocument();
            var test = File.ReadAllText(_path);
            xml.LoadXml(test);
            var namePref = new XmlNamespaceManager(xml.NameTable);
            namePref.AddNamespace("ns", "http://www.gribuser.ru/xml/fictionbook/2.0");
            var descriptionXML = xml.SelectSingleNode("//ns:description", namePref);

            var titleInfoXML = descriptionXML.SelectSingleNode("//ns:title-info", namePref);
            var genreXML = titleInfoXML.SelectSingleNode("descendant::ns:genre", namePref).InnerText;
            var authorXML = titleInfoXML.SelectSingleNode("descendant::ns:author", namePref);
            var authorFirstNameXML = authorXML.SelectSingleNode("descendant::ns:first-name", namePref).InnerText;
            var authorLastNameXml = authorXML.SelectSingleNode("descendant::ns:last-name", namePref).InnerText;
            var bookTitleHml = titleInfoXML.SelectSingleNode("descendant::ns:book-title", namePref).InnerText;
            var annotationXML = titleInfoXML.SelectSingleNode("descendant::ns:annotation", namePref).InnerText;
            //var sequenceXML = titleInfoXML.SelectSingleNode("descendant::ns:sequence", namePref);
            //var sequenceNameXML = sequenceXML.Attributes.GetNamedItem("name").InnerText;
            var binaryXML = xml.SelectSingleNode("//ns:binary", namePref).InnerText;
            var documentinfoXML = descriptionXML.SelectSingleNode("//ns:document-info", namePref);
            var bookIdXML = documentinfoXML.SelectNodes("descendant::ns:id", namePref);

            var bodyXml = xml.SelectSingleNode("//ns:body", namePref);
            var section = bodyXml.SelectNodes("descendant::ns:section", namePref);

            foreach (XmlNode node in section)
            {
                var sectionTitleXML = node.SelectSingleNode("descendant::ns:title", namePref).InnerText;
                var textBookXML = node.SelectNodes("descendant::ns:p", namePref);
                List<String> textList = new List<string>();
                string text = "";
                foreach (XmlNode node_t in textBookXML)
                {
                    textList.Add(node_t.InnerText);
                }
                textList.Remove(textList[0]);
                textList.ForEach(word =>
                {
                    text += word;
                });
                Section sectionText = new Section();
                sectionText.Title = sectionTitleXML;
                sectionText.Text = text;
                sections.Add(sectionText);
            }
            int i = 0;
            foreach (XmlNode node in bookIdXML)
            {
                var d = node.LastChild.InnerText;
                if (i == 1)
                {
                    book.BookNumber = Convert.ToInt32(d);
                }
                i++;
            }
            book.Genre = genreXML;
            book.BookTitle = bookTitleHml;
            book.Annotation = annotationXML;
            book.Sections = sections;
            book.BookImage = binaryXML;

            author.FirstName = authorFirstNameXML;
            author.LastName = authorLastNameXml;
            book.Author = author;
            book.Author.FirstName = authorFirstNameXML;
            book.Author.LastName = authorLastNameXml;
            return book;
        }
    }
}
