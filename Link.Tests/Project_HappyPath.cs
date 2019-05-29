using Xunit;
using Link;
using System.Collections.Generic;

namespace Link.UnitTests {
  public class Project_HappyPath {
    [Fact]
    public async void CanBeConstructed() {
      Project project = await Project.Create("...", "...");

      await project.Post<object>("/setOwner", new { asset = "example", owner = "0x1000200030004000500060007000800090009999" });

      Dictionary<string, string> ownerOf = await project.Get<Dictionary<string, string>>("/ownerOf", new { value = "example" });

      Assert.Matches("^0x1000200030004000500060007000800090009999$", ownerOf["result"]);
    }
  }
}
