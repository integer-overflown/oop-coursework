using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace CourseWork.Views.Widgets
{
    public class BookItem : UserControl
    {
        private string _title;
        private string _subtitle;
        private IImage _cover;

        public BookItem()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public static readonly AvaloniaProperty<string> TitleProperty =
            AvaloniaProperty.RegisterDirect<BookItem, string>(nameof(Title), o => o.Title, (o, v) => o.Title = v);

        public static readonly AvaloniaProperty<string> SubtitleProperty =
            AvaloniaProperty.RegisterDirect<BookItem, string>(nameof(Subtitle), o => o.Subtitle,
                (o, v) => o.Subtitle = v);
        
        public static readonly AvaloniaProperty<IImage> CoverProperty =
            AvaloniaProperty.RegisterDirect<BookItem, IImage>(nameof(Cover), o => o.Cover,
                (o, v) => o.Cover = v);

        public string Title
        {
            get => _title;
            set => SetAndRaise(TitleProperty, ref _title, value);
        }

        public string Subtitle
        {
            get => _subtitle;
            set => SetAndRaise(SubtitleProperty, ref _subtitle, value);
        }

        public IImage Cover
        {
            get => _cover;
            set => SetAndRaise(CoverProperty, ref _cover, value);
        }
    }
}