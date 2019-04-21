using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;

namespace SIS.MvcFramework.Routing
{
    public class HttpPostAttribute : HttpAttribute
    {
        public HttpPostAttribute(string path)
            :base(path)
        {
        }

        public override HttpRequestMethod Method => HttpRequestMethod.Post;
    }
}
