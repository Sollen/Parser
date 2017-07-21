using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Core
{
    class HtmlLoader
    {
        readonly HttpClient _client;

        readonly string _url;

        public HtmlLoader(IParserSettings settings)
        {

            _client = new HttpClient();
            _url = $"{settings.BaseUrl}/{settings.Prefix}/";

        }

        public async Task<string> GetSourceById(int id)
        {

            var currentUrl = _url.Replace("{CurrentId}", id.ToString());
            var response = await _client.GetAsync(currentUrl);
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {

                source = await response.Content.ReadAsStringAsync();

            }

            return source;
        }


    }
}
