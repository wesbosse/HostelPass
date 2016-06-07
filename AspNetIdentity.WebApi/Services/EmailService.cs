using Microsoft.AspNet.Identity;
using RestSharp;
using RestSharp.Authenticators;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace AspNetIdentity.WebApi.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await SendSimpleMessage(message);
        }

        public async Task<IRestResponse> SendSimpleMessage(IdentityMessage message)
        {
            return await Task.Run(() =>
            {
                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator =
                        new HttpBasicAuthenticator("api",
                                                   "key-e37313e7bee6c5a04bf229a77c894368");
                RestRequest request = new RestRequest();
                request.AddParameter("domain",
                                     "mg.hostl.xyz", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "Account Services <accountservices@hostl.xyz>");
                request.AddParameter("to", message.Destination);
                request.AddParameter("subject", message.Subject);
                request.AddParameter("text", message.Body);
                request.Method = Method.POST;
                return client.Execute(request);
            });
            
        }
    }
}