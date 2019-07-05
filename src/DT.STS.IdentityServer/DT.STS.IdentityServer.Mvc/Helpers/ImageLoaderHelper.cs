using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Mvc.Helpers
{
    public static class ImageLoaderHelper
    {
        public static async Task<byte[]> GetImageToBytesFromUriAsync(Uri uri)
        {
            var handler = new HttpClientHandler();
            handler.DefaultProxyCredentials = CredentialCache.DefaultCredentials;
            using (HttpClient client = new HttpClient(handler))
            {             
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "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");
                HttpResponseMessage response = await client.GetAsync(uri);
                byte[] content = await response.Content.ReadAsByteArrayAsync();         
                return content;
            }
        }
    }
}