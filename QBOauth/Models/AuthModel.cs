using QBAuthManager.Models;
using QBEntity.QB;
using QBEntity.System;
using System.Net;

namespace QBOauth.Models
{
    public class AuthModel
    {
        public TokenBaerer Tokens { get; set; }

        public string Json { get; set; }

        public string Vender { get; set; }

        public Customer Customer { get; set; }

        public HttpStatusCode Status { get; set; }
    }
}