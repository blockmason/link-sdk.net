using Xunit;
using Blockmason.Link;
using Blockmason.Link.OAuth2;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blockmason.Link.Tests {
  public class ProjectTest {
    [Fact]
    public async void CanBeConstructed() {
      Credential credential = new Credential();
      HttpClient httpClient = new MockHttpClient();
      string baseURL = "https://localhost:9000/not-a-live-server-dont-worry";
      Session session = new Session(httpClient, credential, baseURL);
      Project project = new Project(session);
      // Obviously, `project` will not be null, but we are really just testing whether it can be constructed at all without errors.
      Assert.NotNull(project);
    }
  }
}
