using Newtonsoft.Json;

namespace Blockmason.Link.OAuth2 {
  public class Credential {
    public class Contract {
      public string access_token;
      public int expires_in;
      public int issued_at;
      public string refresh_token;
      public string scope;
      public string token_type;
    }

    public static Credential FromJSON(string json) {
      Credential credential = new Credential();

      Contract contract = JsonConvert.DeserializeObject<Contract>(json);

      credential.AccessToken = contract.access_token;
      credential.ExpiresIn = contract.expires_in;
      credential.IssuedAt = contract.issued_at;
      credential.RefreshToken = contract.refresh_token;
      credential.Scope = contract.scope;
      credential.TokenType = contract.token_type;

      return credential;
    }

    private string m_AccessToken;
    private int m_ExpiresIn;
    private int m_IssuedAt;
    private string m_RefreshToken;
    private string m_Scope;
    private string m_TokenType;

    public string AccessToken {
      get { return m_AccessToken; }
      set { m_AccessToken = value; }
    }

    public int ExpiresIn {
      get { return m_ExpiresIn; }
      set { m_ExpiresIn = value; }
    }

    public int ExpiresAt {
      get { return m_IssuedAt + m_ExpiresIn; }
    }

    public int IssuedAt {
      get { return m_IssuedAt; }
      set { m_IssuedAt = value; }
    }

    public string RefreshToken {
      get { return m_RefreshToken; }
      set { m_RefreshToken = value; }
    }

    public string Scope {
      get { return m_Scope; }
      set { m_Scope = value; }
    }

    public string TokenType {
      get { return m_TokenType; }
      set { m_TokenType = value; }
    }

    public Credential() {
    }
  }
}
