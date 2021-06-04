using System;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CourseWork.Models;
using Newtonsoft.Json;
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

            if (publisher == null || publishingDateString == null)
            {
                Console.Error.Write("Cannot retrieve publishing info");
                return null;
            }

            var publishingDate = DateTime.ParseExact(publishingDateString, PublishDateFormat, new CultureInfo("en-US"));

            return new Book
            {
                Authors = authors,
                Cover = null, // TODO
                Name = book["title"]?.ToObject<string>(),
                Subjects = subjects,
                Publisher = publisher,
                NumberOfPages = book["number_of_pages"]?.ToObject<int>() ?? 0,
                PublishingYear = publishingDate.Year
            };
        }
    }
}