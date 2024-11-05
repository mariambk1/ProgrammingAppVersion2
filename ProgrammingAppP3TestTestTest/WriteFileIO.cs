using ProgrammingApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ProgrammingApp
{

    public class WriteFile
    {
        private string filepath;
        public WriteFile(string filePath)
        {
            filepath = filePath;

        }

        public void TextExport(List<Command> commands)
        {
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach (var command in commands)
                {
                    writer.WriteLine(command.ToString()); 
                }
            }
        }



        public void HtmlExport(List<Command> commandList, string title = "Export Program")
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    writer.WriteLine("<html>");
                    writer.WriteLine("<head><title>Program Export</title></head>");
                    writer.WriteLine("<body>");
                    writer.WriteLine($"<h1>{title}</h1>");
                    writer.WriteLine("<ul>");

                    foreach (Command command in commandList)
                    {
                        writer.WriteLine($"<li><b>{command.ToHtml()}</b></li>");
                    }

                    writer.WriteLine("</ul>");
                    writer.WriteLine("</body>");
                    writer.WriteLine("</html>");
                }
                Console.WriteLine($"Program exported to HTML at {filepath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an error occurred while exporting to HTML: " + ex.Message);
            }
        }
    }
}
