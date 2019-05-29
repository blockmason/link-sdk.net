using Link.OAuth2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Link {
  public class Project {
    public static async Task<Project> Create(string clientId, string clientSecret) {
      Session session = await Session.Create(clientId, clientSecret);
      return new Project(session);
    }

    public static async Task<Project> Create(string clientId, string clientSecret, string baseURL) {
      Session session = await Session.Create(clientId, clientSecret, baseURL);
      return new Project(session);
    }

    private Session m_Session;

    public Project(Session session) {
      m_Session = session;
    }

    public Task<T> Post<T>(string path, object inputs) {
      return m_Session.SendWithBody<T>(HttpMethod.Post, $"/v1{path}", inputs);
    }

    public Task<T> Get<T>(string path, object inputs) {
      return m_Session.SendWithQuery<T>(HttpMethod.Get, $"/v1{path}", inputs);
    }

    public Task<T> Get<T>(string path) {
      return m_Session.Send<T>(HttpMethod.Get, $"/v1{path}");
    }
  }
}
