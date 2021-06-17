using System;
using System.Linq;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
    public class SearchScreenViewModel : ViewModelBase
    {
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? Subject { get; set; }

        public Func<Book, bool> CreateFilter()
        {
            return book => AuthorFilter(book, Author)
                           && PublisherFilter(book, Publisher)
                           && SubjectFilter(book, Subject);
        }

        private static bool NotNullAndContains(string? s, string value)
        {
            return s != null && s.Contains(value, StringComparison.OrdinalIgnoreCase);
        }

        private static bool AuthorFilter(Book book, string? author)
        {
            return string.IsNullOrWhiteSpace(author) || book.Authors
                .Select(a => a.Name)
                .Any(n => NotNullAndContains(n, author));
        }

        private static bool SubjectFilter(Book book, string? subject)
        {
            return string.IsNullOrWhiteSpace(subject) || book.Subjects
                .Select(a => a.Name)
                .Any(n => NotNullAndContains(n, subject));
        }

        private static bool PublisherFilter(Book book, string? publisher)
        {
            return string.IsNullOrWhiteSpace(publisher) || NotNullAndContains(book.Publisher, publisher);
        }
    }
}