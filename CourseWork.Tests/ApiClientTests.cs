using System.Drawing;
using System.IO;
using CourseWork.Networking;
using NUnit.Framework;

namespace CourseWork.Tests
{
    [TestFixture]
    public class ApiClientTests
    {
        private readonly ApiClient _client = new();

        [Test]
        public void DownloadImage_ValidUrlPasses_NoExceptionsOccur()
        {
            const string url = "https://citizengo.org/sites/default/files/images/test.png";
            Assert.That(async () =>
            {
                var result = await _client.DownloadImage(url);
                var bitmap = Image.FromStream(new MemoryStream(result));
                bitmap.Save("tmp.png");
            }, Throws.Nothing);
        }
    }
}