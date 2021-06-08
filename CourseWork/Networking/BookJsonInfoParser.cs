using System;
using System.Linq;
using System.Text.RegularExpressions;
using CourseWork.Models;
using Newtonsoft.Json.Linq;

namespace CourseWork.Networking
{
    public class BookJsonInfoParser : IParser<Book>
    {
        private const int SubjectsMaxCount = 5;
        private static readonly Regex YearRegex = new(@"(\d{4})");

        public Book? Parse(string input)
        {
            var result = JObject.Parse(input);
            JToken? book;
            if (!result.HasValues || (book = result.Root.First?.First) == null) return null;

            var authors = book
                .SelectTokens("$.authors..name")
                .Select(token => new Author {Name = token.ToString()})
                .ToList();
            var subjects = book
                .SelectTokens("$.subjects..name")
                .Select(token => new Subject {Name = token.ToString()})
                .Take(SubjectsMaxCount)
                .ToList();

            var publisher = book.SelectToken("$.publishers..name")?.ToString();
            var publishingDateString = book["publish_date"]?.ToString();
            var coverLargeSource = book["cover"]?["large"]?.ToString();

            if (publisher == null) Console.WriteLine("INFO: no publisher");

            var publishingYear = -1;
            if (publishingDateString != null)
            {
                var groups = YearRegex.Match(publishingDateString).Groups;
                if (groups.Count < 1)
                {
                    Console.WriteLine("WARNING: failed to determine publishing year");
                }
                else
                {
                    publishingYear = int.Parse(groups[1].Value);
                }
            }

            return new Book
            {
                Authors = authors,
                CoverUrl = coverLargeSource,
                Name = book["title"]?.ToObject<string>(),
                Subjects = subjects,
                Publisher = publisher,
                NumberOfPages = book["number_of_pages"]?.ToObject<int>() ?? 0,
                PublishingYear = publishingYear
            };
        }
    }
}