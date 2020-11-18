using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace LoadTest
{
    public class LoadTestOptions
    {
        public string LoadTestUri { get; set; }
        public string LoadTestFilePath { get; set; }
        public int ConcurrentClientCount { get; set; }
        public int Duration { get; set; }
        public bool RandomizeFileName { get; set; }

    }
}
