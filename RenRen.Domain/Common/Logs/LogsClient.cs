using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RenRen.Domain.Common.Logs
{
    public class LogsClient
    {
        public HttpClient Client { get; }

        public LogsClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://logsene-receiver.eu.sematext.com");
            Client = client;
        }

        public async Task WriteLogsAsync(Content content)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(content));
            await Client.PostAsync("/487570d1-b985-4c88-bee1-b09f61398beb/example", httpContent);
            httpContent.Dispose();
        }
    }

    public class Content
    {
        public string Message { get; set; }
    }
}
