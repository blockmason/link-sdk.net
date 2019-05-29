using Newtonsoft.Json;
using System;

namespace Blockmason.Link.OAuth2.Grant {
  public class RefreshTokenGrant {
    public class Contract {
      public string grant_type;
      public string refresh_token;

      public Contract() : this("") {
      }

      public Contract(string refreshToken) {
        refresh_token = refreshToken;
      }
    }

    private string m_RefreshToken;

    public RefreshTokenGrant(string refreshToken) {
      m_RefreshToken = refreshToken;
    }

    public string ToJSON() {
      Contract contract = new Contract(m_RefreshToken);
      return JsonConvert.SerializeObject(contract);
    }
  }
}
