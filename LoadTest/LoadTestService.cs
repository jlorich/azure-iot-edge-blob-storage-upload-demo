using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Options;
using System.Timers;

namespace LoadTest
{
    public class LoadTestService
    {
        private byte[] _LoadTestFile;

        IHttpClientFactory _httpClientFactory;

        private LoadTestOptions _options;
        private int _uploadCount = 0;
        private static object _countLock = new object();

        public LoadTestService(IHttpClientFactory httpClientFactory, IOptions<LoadTestOptions> options) {
            _options = options.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task BeginTest() {
            var tasks = new List<Task>();
            var cts = new CancellationTokenSource();

            for(var i = 0; i < _options.ConcurrentClientCount; i++)
            {
                tasks.Add(Task.Run(async () => await BeginTestClient(cts.Token)));
            }
            
            var timer = new System.Timers.Timer(5000);
            timer.Elapsed += new ElapsedEventHandler(OnTimerElapsedEvent);
            timer.Enabled = true;

            var duration = TimeSpan.FromSeconds(_options.Duration);
            cts.CancelAfter(duration);
            await Task.WhenAll(tasks.ToArray());
            
            timer.Enabled = false;

            Console.WriteLine($"{_uploadCount} Files in {_options.Duration} seconds");
        }

        private void OnTimerElapsedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine($"{_uploadCount} Files processed");
        }

        public async Task BeginTestClient(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) {
                var client = _httpClientFactory.CreateClient();

                HttpContent bytesContent = new ByteArrayContent(ReadTestFile());
                
                string uri = _options.LoadTestUri;

                if (_options.RandomizeFileName) {
                    uri = uri.Replace("{fileName}", Guid.NewGuid().ToString().Replace("-", ""));
                }

                var response = await client.PostAsync(uri, bytesContent);

                if (response.StatusCode == HttpStatusCode.Created) {
                    lock(_countLock) {
                        _uploadCount++;
                    }
                }
            }
        }

        public byte[] ReadTestFile() {
            if (_LoadTestFile == null) {
                _LoadTestFile = File.ReadAllBytes(_options.LoadTestFilePath);
            }

            return _LoadTestFile;
        }
    }
}
