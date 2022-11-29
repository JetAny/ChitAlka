using MVC_ChitAlka.Intrfaces;

namespace MVC_ChitAlka.Servises
{
    public class CreateBookService : ICreateBookService
    {
        string path = "note1.txt";
        string text = "Hello World\nHello METANIT.COM";

        public async Task<CreateBookService> CreateBook()
        {
            // полная перезапись файла ,если файл существует,
            // то он будет перезаписан. Если не существует, он будет создан.
            // И в нее будет записан текст из переменной text.
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                await writer.WriteLineAsync(text);
            }

            // добавление в файл, файл открывается для дозаписи, и будут записаны атомарные данные - строка и число.
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                await writer.WriteLineAsync("Addition");
                await writer.WriteAsync("4,5");
            }

            //Сначала считаем текст полностью из ранее записанного файла:
            using (StreamReader reader = new StreamReader(path))
            {
                string text = await reader.ReadToEndAsync();
                Console.WriteLine(text);
            }
            return new CreateBookService();
        }
    }

}

