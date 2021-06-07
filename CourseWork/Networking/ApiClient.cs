using System;
using System.Net.Http;
using System.Threading.Tasks;
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
                return _parser.Parse(responseString);
            }
            catch (HttpRequestException e)
            {
                throw new ApiClientException($"Failed due to connectivity issue, server respond {e.StatusCode}.", e);
            }
            catch (TaskCanceledException e)
            {
                throw new ApiClientException("Operation timed out.", e);
            }
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