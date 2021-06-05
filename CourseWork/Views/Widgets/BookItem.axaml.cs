using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CourseWork.Utils;

namespace CourseWork.Views.Widgets
{
    public class BookItem : UserControl
    {
        private string _title = "";
        private string _subtitle = "";
        private IImage? _cover;

        private readonly Image _coverView;
        private readonly TextBlock _titleView;
        private readonly TextBlock _subtitleView;

        private static readonly WeakReference<IImage> UnknownCover;
        private const string UnknownCoverIconFile = "ic-unknown-cover.png";

        static BookItem()
        {
            UnknownCover = new WeakReference<IImage>(GetUnknownIcon());
        }

        private static Bitmap GetUnknownIcon()
        {
            return new(AssetLoader.GetResourceAsStream(UnknownCoverIconFile));
        }

        public BookItem()
        {
            InitializeComponent();
            _coverView = this.FindControlStrict<Image>("Cover");
            _titleView = this.FindControlStrict<TextBlock>("Title");
            _subtitleView = this.FindControlStrict<TextBlock>("Subtitle");
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
            set => SetTitle(value);
        }

        private void SetTitle(string value)
        {
            _titleView.Text = value;
            SetAndRaise(TitleProperty, ref _title, value);
        }

        public string Subtitle
        {
            get => _subtitle;
            set => SetSubtitle(value);
        }

        private void SetSubtitle(string value)
        {
            _subtitleView.Text = value;
            SetAndRaise(SubtitleProperty, ref _subtitle, value);
        }

        public IImage Cover
        {
            get
            {
                if (_cover is not null) return _cover;
                if (UnknownCover.TryGetTarget(out var cover))
                {
                    Console.WriteLine("Reused cached default cover");
                    return cover;
                }

                var temp = GetUnknownIcon();
                UnknownCover.SetTarget(temp);
                return temp;

            }
            set => LoadCover(value);
        }

        private void LoadCover(IImage value)
        {
            _coverView.Source = value;
            SetAndRaise(CoverProperty, ref _cover, value);
        }
    }
}