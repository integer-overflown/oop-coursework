using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Avalonia.Media.Imaging;

namespace CourseWork.Models
{
    public class Book
    {
        public long Id { get; set; }
        public int NumberOfPages { get; init; } = 0;
        [Required]
        public string? Name { get; init; }
        public string? Publisher { get; init; }
        public IBitmap? Cover { get; init; }
        [NotMapped]
        public string? CoverUrl { get; init; }
        public bool IsPresent { get; set; } = true;
        public int PublishingYear { get; init; } = 0;
        public IEnumerable<Author> Authors { get; init; } = Enumerable.Empty<Author>();
        public IEnumerable<Subject> Subjects { get; init; } = Enumerable.Empty<Subject>();
    }
}