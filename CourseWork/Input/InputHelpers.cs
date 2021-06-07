using System.Linq;
using System.Text;

namespace CourseWork.Input
{
    public static class InputHelpers
    {
        private const int IsbnMaxLength = 13;

        public static string FilterIsbnDashes(string isbn)
        {
            var sb = new StringBuilder(IsbnMaxLength);

            foreach (var ch in isbn.Where(char.IsDigit))
            {
                sb.Append(ch);
            }

            return sb.ToString();
        }
    }
}