using System.Net.Http;
using System.Threading.Tasks;

namespace Blockmason.Link.Tests {
  public class MockHttpClient : HttpClient {
    public MockHttpClient() : base() {
    }

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) {
      return new HttpResponseMessage();
    }
  }
}
