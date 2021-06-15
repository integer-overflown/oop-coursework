using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Avalonia.Media.Imaging;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CourseWork.Models
{
    public class Book
    {
        private readonly ILazyLoader? _loader;
        private IList<Author> _authors = new List<Author>();
        private IList<Subject> _subjects = new List<Subject>();

        public Book()
        {
        }

        [UsedImplicitly]
        private Book(ILazyLoader loader)
        {
            _loader = loader;
        }

        public long Id { get; set; }
        public int NumberOfPages { get; set; }

        [Required] public string? Name { get; set; }

        public string? Publisher { get; set; }
        public IBitmap? Cover { get; set; }

        [NotMapped] public string? CoverUrl { get; init; }

        public bool IsPresent { get; set; } = true;
        public int PublishingYear { get; set; }

        public IList<Author> Authors
        {
            get => _loader.Load(this, ref _authors);
            init => _authors = value;
        }

        public IList<Subject> Subjects
        {
            get => _loader.Load(this, ref _subjects);
            init => _subjects = value;
        }
    }
}