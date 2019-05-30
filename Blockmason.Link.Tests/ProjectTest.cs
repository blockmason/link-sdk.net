using Xunit;
using Blockmason.Link;
using Blockmason.Link.OAuth;
using System.Collections.Generic;
using System.Net.Http;

namespace Blockmason.Link.Tests {
  public class ProjectTest {
    internal class MockHttpClient : HttpClient {
      public MockHttpClient() : base() {
      }

      public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) {
        return new HttpResponseMessage();
      }
    }

    [Fact]
    public async void CanBeConstructed() {
      Credential credential = new Credential();
      HttpClient httpClient = new HttpClient();
      string baseURL = "https://localhost:9000/not-a-live-server-dont-worry";
      Session session = new Session(httpClient, credential, baseURL);
      Project project = new Project(session);
      // Obviously, `project` will not be null, but we are really just testing whether it can be constructed at all without errors.
      Assert.NotNull(project);
    }
  }
}
