using System;
using System.Reactive;
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
        private const string UnknownCoverIconFile = "ic-unknown-cover.png";
        private const double CoverAspectRatio = 1.5d; // height to width

        private static readonly WeakReference<IImage> UnknownCover;

        public static readonly AvaloniaProperty<long> IdProperty =
            AvaloniaProperty.RegisterDirect<BookItem, long>(nameof(Id), o => o.Id, (o, v) => o.Id = v);

        public static readonly AvaloniaProperty<string> TitleProperty =
            AvaloniaProperty.RegisterDirect<BookItem, string>(nameof(Title), o => o.Title, (o, v) => o.Title = v);

        public static readonly AvaloniaProperty<string> SubtitleProperty =
            AvaloniaProperty.RegisterDirect<BookItem, string>(nameof(Subtitle), o => o.Subtitle,
                (o, v) => o.Subtitle = v);

        public static readonly AvaloniaProperty<IImage> CoverProperty =
            AvaloniaProperty.RegisterDirect<BookItem, IImage>(nameof(Cover), o => o.Cover,
                (o, v) => o.Cover = v);

        private readonly Image _coverView;
        private readonly TextBlock _subtitleView;
        private readonly TextBlock _titleView;
        private string _subtitle = "";
        private string _title = "";

        static BookItem()
        {
            UnknownCover = new WeakReference<IImage>(GetUnknownIcon());
        }

        public BookItem()
        {
            InitializeComponent();
            _coverView = this.FindControlStrict<Image>("Cover");
            _titleView = this.FindControlStrict<TextBlock>("Title");
            _subtitleView = this.FindControlStrict<TextBlock>("Subtitle");
        }

        public long Id { get; set; }

        public string Title
        {
            get => _title;
            set => SetTitle(value);
        }

        public string Subtitle
        {
            get => _subtitle;
            set => SetSubtitle(value);
        }

        public IImage Cover
        {
            get => _coverView.Source;
            set => _coverView.Source = value;
        }

        private static Bitmap GetUnknownIcon()
        {
            return new(AssetLoader.GetResourceAsStream(UnknownCoverIconFile));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            WidthProperty.Changed.Subscribe(Observer.Create<AvaloniaPropertyChangedEventArgs>(prop =>
            {
                if (!ReferenceEquals(prop.Sender, this)) return;
                var value = (double) (prop.NewValue ?? 0);
                _coverView.Height = value * CoverAspectRatio;
            }));
        }

        private void SetTitle(string value)
        {
            _titleView.Text = value;
            SetAndRaise(TitleProperty, ref _title, value);
        }

        private void SetSubtitle(string value)
        {
            _subtitleView.Text = value;
            SetAndRaise(SubtitleProperty, ref _subtitle, value);
        }

        public override void Render(DrawingContext context)
        {
            if (_coverView.Source is null)
            {
                IImage cover;
                if (UnknownCover.TryGetTarget(out var unknown))
                {
                    Console.WriteLine("Reused cached default cover");
                    cover = unknown;
                }
                else
                {
                    var temp = GetUnknownIcon();
                    UnknownCover.SetTarget(temp);
                    cover = temp;
                }

                _coverView.Source = cover;
            }

            base.Render(context);
        }
    }
}