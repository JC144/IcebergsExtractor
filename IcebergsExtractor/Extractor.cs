using BookmarksManager;
using BookmarksManager.Icebergs;
using System;
using System.IO;
using System.Web;

namespace IcebergsExtractor
{
    public class Extractor
    {
        public void Compute(string inputFilePath, string outputFilePath)
        {
            Console.WriteLine("Starting...");

            BookmarkFolder bookmarks = GetBookmarksFromJson(inputFilePath);
            string html = JsonToHtmlPage(bookmarks);
            WriteHtmlFile(html, outputFilePath);

            Console.WriteLine("Finished.");
        }

        protected BookmarkFolder GetBookmarksFromJson(string inputFilePath)
        {
            BookmarkFolder bookmarkFolder = null;

            var _reader = new IcebergsBookmarksReader()
            {
                HtmlDecoder = HttpUtility.HtmlDecode
            };

            using (var file = File.OpenRead(inputFilePath))
            {
                bookmarkFolder = _reader.Read(file);
            }

            return bookmarkFolder;
        }

        protected string JsonToHtmlPage(BookmarkFolder bookmarkFolder)
        {
            string html = GenerateHtmlOpening();
            
            foreach (var item in bookmarkFolder.AllLinks)
            {
                string line = String.Format("<li><a href=\"{0}\">{1}:{2}</a></li>\n", item.Url, item.Title, item.Description);
                html += line;
                Console.WriteLine(line);
            }            

            html += GenerateHtmlClosing();

            return html;
        }

        private string GenerateHtmlOpening()
        {
            return "<!DOCTYPE html>\n\n<html>\n<head>\n<title>Iceberg export</title>\n</head>\n\n<body>\n<ul>";
        }

        private string GenerateHtmlClosing()
        {
            return "</ul>\n</body></html>";
        }

        protected void WriteHtmlFile(string html, string outputPath)
        {
            File.WriteAllText(outputPath, html);
        }
    }
}
