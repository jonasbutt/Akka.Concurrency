using System;
using System.Net.Http;
using Akka.Actor;

namespace Akka.Concurrency.Test
{
    public class DownloadActor : ReceiveActor
    {
        public DownloadActor()
        {
            this.Receive<Uri>(async uri =>
            {
                var httpClient = new HttpClient();
                var result = await httpClient.GetStringAsync(uri);
                this.Sender.Tell(result, this.Self);
            });
        }
    }
}