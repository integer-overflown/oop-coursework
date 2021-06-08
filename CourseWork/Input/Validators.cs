using System.Linq;
using System.Text.RegularExpressions;

namespace CourseWork.Input
{
    public static class Validators
    {
        public static IValidator<string> Isbn => new IsbnValidator();
        public static IValidator<string> Digits => new DigitOnlyStringValidator();
    }

    public class IsbnValidator : IValidator<string>
    {
        private static readonly Regex IsbnRegex = new(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d\re\-]+$");
        private static readonly Regex SequenceRegex = new(@"^[0-9\-]*$");

        public bool IsValid(string value)
        {
            var digits = value.Count(char.IsDigit);
            return digits is 10 or 13 && IsbnRegex.IsMatch(value);
        }

        public bool IsPermitted(string value)
        {
            return SequenceRegex.IsMatch(value);
        }
    }

    public class DigitOnlyStringValidator : IValidator<string>
    {
        public bool IsValid(string value)
        {
            return value.All(char.IsDigit);
        }

        public bool IsPermitted(string value)
        {
            return IsValid(value);
        }
    }
}