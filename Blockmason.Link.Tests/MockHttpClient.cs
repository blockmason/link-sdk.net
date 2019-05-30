using System.Net.Http;
using System.Threading.Tasks;

namespace Blockmason.Link.Tests {
  public class MockHttpClient : HttpClient {
    public MockHttpClient() : base() {
    }

    public new Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) {
      return new Task<HttpResponseMessage>(() => {
        return new HttpResponseMessage();
      });
    }
  }
}
