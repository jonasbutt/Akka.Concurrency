using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akka.Concurrency.Test
{
    [TestClass]
    public class ConcurrencyHelperIntegrationTests
    {
        const int NumberOfInstances = 4;

        IConcurrencyHelper concurrencyHelper;
        Uri[] webPagesToDownload;

        [TestInitialize]
        public void InitializeTest()
        {
            this.concurrencyHelper = new ConcurrencyHelper();

            this.webPagesToDownload =
                new[]
                {
                    new Uri("http://www.microsoft.com"),
                    new Uri("http://www.google.com"),
                    new Uri("http://www.apple.com"),
                    new Uri("http://www.avanade.com"),
                    new Uri("http://www.accenture.com"),
                    new Uri("http://www.linkedin.com"),
                    new Uri("http://www.facebook.com"),
                    new Uri("http://www.twitter.com"),
                    new Uri("http://www.amazon.com"),
                    new Uri("http://www.ebay.com"),
                    new Uri("http://www.dx.com"),
                    new Uri("http://www.alibaba.com")
                };
        }

        [TestMethod]
        public async Task CanRunConcurrentlyAsync()
        {
            var results = await this.concurrencyHelper.RunConcurrentlyAsync<DownloadActor, Uri>(this.webPagesToDownload, NumberOfInstances);
            Assert.IsTrue(results.Cast<string>().Any());
        }

        [TestMethod]
        public void CanRunConcurrently()
        {
            var results = this.concurrencyHelper.RunConcurrently<DownloadActor, Uri>(this.webPagesToDownload, NumberOfInstances);
            Assert.IsTrue(results.Cast<string>().Any());
        }

        [TestMethod]
        public async Task CanRunConcurrentlyAsync_WithResultType()
        {
            var results = await this.concurrencyHelper.RunConcurrentlyAsync<DownloadActor, Uri, string>(this.webPagesToDownload, NumberOfInstances);
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public void CanRunConcurrently_WithResultType()
        {
            var results = this.concurrencyHelper.RunConcurrently<DownloadActor, Uri, string>(this.webPagesToDownload, NumberOfInstances);
            Assert.IsTrue(results.Any());
        }
    }
}