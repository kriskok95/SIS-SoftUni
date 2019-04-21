using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;

namespace SIS.MvcFramework.Routing
{
    public class HttpGetAttribute : HttpAttribute
    {
        public HttpGetAttribute(string path)
            :base(path)
        {
            
        }

        public override HttpRequestMethod Method => HttpRequestMethod.Get;
    }
}
