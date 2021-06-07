using System;
using System.Globalization;
using System.Linq;
using CourseWork.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace CourseWork.Networking
{
    public class BookJsonInfoParser : IParser<Book>
    {
        private const string PublishDateFormat = "MMMM yyyy";

        private static readonly DateTimeConverterBase PublishDateConverter = new IsoDateTimeConverter
        {
            DateTimeFormat = PublishDateFormat
        };

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
                .ToList();

            var publisher = book.SelectToken("$.publishers..name")?.ToString();
            var publishingDateString = book["publish_date"]?.ToString();
            var coverLargeSource = book["cover"]?["large"]?.ToString();

            if (publisher == null)
            {
                Console.WriteLine("INFO: no publisher");
                return null;
            }

            var publishingYear = publishingDateString == null
                ? -1
                : DateTime.ParseExact(publishingDateString,
                    PublishDateFormat,
                    new CultureInfo("en-US")).Year;

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