using CourseWork.Input;
using NUnit.Framework;

namespace CourseWork.Tests
{
    [TestFixture]
    public class InputValidationTests
    {
        [Test]
        [TestCase("1234567890")]
        [TestCase("978-1234567890")]
        [TestCase("978-3-16-148410-0")]
        [TestCase("978-2-1234-5678-3")]
        public void IsValid_IsbnEntries_ValidationSucceeds(string isbn)
        {
            var validator = new IsbnValidator();
            Assert.That(validator.IsValid(isbn), Is.True);
        }

        [Test]
        [TestCase("12345678901234")] // too long
        [TestCase("978_1234567890")] // invalid char '_'
        [TestCase("1234")] // too short
        [TestCase("isbn978-1234567890")] // should contain only digits and dashes
        public void IsValid_IsbnEntries_ValidationFails(string isbn)
        {
            var validator = new IsbnValidator();
            Assert.That(validator.IsValid(isbn), Is.False);
        }
    }
}