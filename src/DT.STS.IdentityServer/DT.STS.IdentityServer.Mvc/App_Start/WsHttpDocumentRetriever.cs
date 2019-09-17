using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Mvc.App_Start
{
    class WsHttpDocumentRetriever : IDocumentRetriever
    {
        private readonly HttpClient _httpClient;

        public WsHttpDocumentRetriever()
            : this(new HttpClient())
        {
        }

        public WsHttpDocumentRetriever(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException("httpClient");
        }

        public async Task<string> GetDocumentAsync(string address, CancellationToken cancel)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentNullException("address");
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(address, cancel).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new IOException("Unable to get document from: " + address, ex);
            }
        }
    }
}