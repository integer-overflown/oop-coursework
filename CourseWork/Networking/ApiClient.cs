using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CourseWork.Models;
using JetBrains.Annotations;

namespace CourseWork.Networking
{
    public class ApiClient
    {
        private const string QueryByIsbnTemplateUrl =
            "https://openlibrary.org/api/books?bibkeys=ISBN:{0}&jscmd=data&format=json";

        private readonly HttpClient _client = new();
        private readonly IParser<Book> _parser = new BookJsonInfoParser();

        public async Task<Book?> QueryBookByIsbnAsync(string isbn)
        {
            var url = string.Format(QueryByIsbnTemplateUrl, isbn);
            try
            {
                var response = await _client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                var book = _parser.Parse(responseString);

                if (book?.CoverUrl is null) return book;

                var buf = await DownloadImage(book.CoverUrl);
                book.Cover = new Bitmap(new MemoryStream(buf));

                return book;
            }
            catch (HttpRequestException e)
            {
                var message = e.StatusCode is null
                    ? "Couldn't communicate with the server."
                    : $"Failed due to connectivity issue, server respond {e.StatusCode}.";
                throw new ApiClientException(message, e);
            }
            catch (TaskCanceledException e)
            {
                throw new ApiClientException("Operation timed out.", e);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                throw new ApiClientException("Unknown error occured.");
            }
        }

        public async Task<IBitmap> DownloadImageBitmap(string url)
        {
            await using var response = await _client.GetStreamAsync(url);
            return new Bitmap(response);
        }

        public async Task<byte[]> DownloadImage(string url)
        {
            return await _client.GetByteArrayAsync(url);
        }
    }

    public class ApiClientException : Exception
    {
        public ApiClientException()
        {
        }

        public ApiClientException([CanBeNull] string? message) : base(message)
        {
        }

        public ApiClientException([CanBeNull] string? message, [CanBeNull] Exception? innerException) : base(message,
            innerException)
        {
        }
    }
}