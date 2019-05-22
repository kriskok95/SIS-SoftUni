using System.Collections.Generic;
using SIS.HTTP.Common;
using SIS.HTTP.Sessions.Contracts;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private Dictionary<string, object> sessionParameters;

        public HttpSession(string id)
        {
            this.sessionParameters = new Dictionary<string, object>();
            this.Id = id;
        }

        public string Id { get; }
        public object GetParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            return this.sessionParameters[name];
        }

        public bool ContainsParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            return this.sessionParameters.ContainsKey(name);
        }

        public void AddParameter(string name, object parameter)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNull(parameter, nameof(parameter));

            this.sessionParameters[name] = parameter;
        }

        public void ClearParameters()
        {
            this.sessionParameters.Clear();
        }
    }
}
