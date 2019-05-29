using Newtonsoft.Json;
using System;

namespace Blockmason.Link.OAuth2.Grant {
  public class ClientCredentialsGrant {
    public class Contract {
      public string client_id;
      public string client_secret;
      public string grant_type;

      public Contract() : this("", "") {
      }

      public Contract(string clientId, string clientSecret) {
        client_id = clientId;
        client_secret = clientSecret;
        grant_type = "client_credentials";
      }
    }

    private string m_ClientId;
    private string m_ClientSecret;

    public ClientCredentialsGrant(string clientId, string clientSecret) {
      m_ClientId = clientId;
      m_ClientSecret = clientSecret;
    }

    public string ToJSON() {
      Contract contract = new Contract(m_ClientId, m_ClientSecret);
      return JsonConvert.SerializeObject(contract);
    }
  }
}
