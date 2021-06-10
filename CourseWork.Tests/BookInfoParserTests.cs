using System.IO;
using System.Linq;
using CourseWork.Models;
using CourseWork.Networking;
using NUnit.Framework;

namespace CourseWork.Tests
{
    [TestFixture]
    public class BookInfoParserTests
    {
        private readonly IParser<Book> _parser = new BookJsonInfoParser();

        [Test]
        public void ParseJson_ReceivedEmpty_ReturnsNull()
        {
            Assert.That(_parser.Parse("{}"), Is.Null);
        }

        [Test]
        public void ParseJson_ValidInput_SetsModelFieldsCorrectly()
        {
            const string exampleFileName = @"example-api-response.json";
            var result = _parser.Parse(File.ReadAllText(Path.Combine("Resources", exampleFileName)));
            Assert.That(result, Is.Not.Null);
            Assume.That(result.PublishingYear, Is.EqualTo(2009));
            Assume.That(result.NumberOfPages, Is.EqualTo(92));
            Assume.That(result.Name, Is.EqualTo("Slow reading"));
            Assume.That(result.Authors.Count(), Is.GreaterThan(0));
            Assume.That(result.Subjects.Count(), Is.GreaterThan(0));
            Assume.That(result.CoverUrl, Is.Not.Null);
        }
    }
}