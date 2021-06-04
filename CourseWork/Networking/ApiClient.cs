using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CourseWork.Models;

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
                Console.WriteLine($"Failed due to connectivity issue, server respond {e.StatusCode}");
            }
            catch (TaskCanceledException)
            {   
                Console.WriteLine("Operation timed out");
            }

            return null;
        }
    }
}