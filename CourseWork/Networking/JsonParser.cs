using System;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CourseWork.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace CourseWork.Networking
{
    public class JsonParser : IParser<Book>
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
            if (!result.HasValues || (book = result.Root.First) == null) return null;

            var authors = book
                .SelectTokens("$.authors..name")
                .Select(token => new Author {Name = token.ToString()})
                .ToList();
            var subjects = book
                .SelectTokens("$.subjects..name")
                .Select(token => new Subject {Name = token.ToString()})
                .ToList();
            var publisher = book.SelectToken("$.publishers..name")?.ToString();
            var publishingDate =
                JsonConvert.DeserializeObject<DateTime>(book["publish_date"]?.ToString()!, PublishDateConverter);

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