using System.Text.RegularExpressions;

namespace CourseWork.Input
{
    public static class Validators
    {
        public static IValidator<string> Isbn => new IsbnValidator();
    }

    public class IsbnValidator : IValidator<string>
    {
        private static readonly Regex IsbnRegex = new(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d\re-]+$");

        public bool IsValid(string value)
        {
            return value.Length is 10 or 13 && IsbnRegex.IsMatch(value);
        }
    }
}