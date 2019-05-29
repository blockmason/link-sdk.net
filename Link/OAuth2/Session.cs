using Link.OAuth2.Grant;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Link.OAuth2 {
  public class Session {
    public const string DEFAULT_BASE_URL = "https://api.block.mason.link";

    public static Task<Session> Create(string clientId, string clientSecret) {
      return Create(new HttpClient(), clientId, clientSecret);
    }

    public static Task<Session> Create(string clientId, string clientSecret, string baseURL) {
      return Create(new HttpClient(), clientId, clientSecret, baseURL);
    }

    public static Task<Session> Create(ClientCredentialsGrant grant) {
      return Create(new HttpClient(), grant);
    }

    public static Task<Session> Create(ClientCredentialsGrant grant, string baseURL) {
      return Create(new HttpClient(), grant, baseURL);
    }

    public static Task<Session> Create(HttpClient client, string clientId, string clientSecret) {
      return Create(client, clientId, clientSecret, DEFAULT_BASE_URL);
    }

    public static Task<Session> Create(HttpClient client, string clientId, string clientSecret, string baseURL) {
      return Create(client, new ClientCredentialsGrant(clientId, clientSecret), baseURL);
    }

    public static Task<Session> Create(HttpClient client, ClientCredentialsGrant grant) {
      return Create(client, grant, DEFAULT_BASE_URL);
    }

    public static Task<Session> Create(HttpClient client, ClientCredentialsGrant grant, string baseURL) {
      return Grant(client, grant.ToJSON(), baseURL);
    }

    private static Task<Session> Create(HttpClient client, RefreshTokenGrant grant, string baseURL) {
      return Grant(client, grant.ToJSON(), baseURL);
    }

    private static async Task<Session> Grant(HttpClient client, string requestJSON, string baseURL) {
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{baseURL}/oauth2/token");
      request.Content = new StringContent(requestJSON, Encoding.UTF8, "application/json");
      HttpResponseMessage response = await client.SendAsync(request);
      response.EnsureSuccessStatusCode();
      string responseContent = await response.Content.ReadAsStringAsync();
      return new Session(client, Credential.FromJSON(responseContent), baseURL);
    }

    private string m_BaseURL;
    private HttpClient m_Client;
    private Credential m_Credential;

    public Credential Credential { get { return m_Credential; } }

    public Session(HttpClient client, Credential credential) : this(client, credential, DEFAULT_BASE_URL) {
    }

    public Session(HttpClient client, Credential credential, string baseURL) {
      m_BaseURL = baseURL;
      m_Client = client;
      m_Credential = credential;
    }

    public Task<Session> Refresh() {
      return Create(m_Client, new RefreshTokenGrant(m_Credential.RefreshToken), m_BaseURL);
    }

    public Task<T> SendWithBody<T>(HttpMethod method, string path, object inputs) {
      HttpRequestMessage request = new HttpRequestMessage(method, $"{m_BaseURL}{path}");

      if (inputs != null) {
        request.Content = new StringContent(JsonConvert.SerializeObject(inputs), Encoding.UTF8, "application/json");
      }

      return Send<T>(request);
    }

    public Task<T> SendWithQuery<T>(HttpMethod method, string path, object inputs) {
      return Send<T>(new HttpRequestMessage(method, $"{m_BaseURL}{path}{ToQueryString(inputs)}"));
    }

    public Task<T> Send<T>(HttpMethod method, string path) {
      return Send<T>(new HttpRequestMessage(method, $"{m_BaseURL}{path}"));
    }

    private async Task<T> Send<T>(HttpRequestMessage request) {
      request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", m_Credential.AccessToken);
      HttpResponseMessage response = await m_Client.SendAsync(request);
      response.EnsureSuccessStatusCode();
      string responseContent = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<T>(responseContent);
    }

    private string ToQueryString(object keyValuePairs) {
      StringBuilder queryString = new StringBuilder();

      foreach (PropertyInfo property in keyValuePairs.GetType().GetProperties()) {
        object value = property.GetValue(keyValuePairs, null);

        if (value != null) {
          if (queryString.Length > 0) {
            queryString.Append("&");
          }

          queryString.AppendFormat("{0}={1}", Uri.EscapeDataString(property.Name), Uri.EscapeDataString(value.ToString()));
        }
      }

      if (queryString.Length > 0) {
        queryString.Insert(0, "?");
      }

      return queryString.ToString();
    }
  }
}
