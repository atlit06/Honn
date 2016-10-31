using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using System.Net.Http;
using Xunit;
using Assignment3.Services;
using Assignment3.Services.DataAccess;
using Assignment3.Services.Entities;
using Assignment3.Services.Exceptions;
using Assignment3.Models;

namespace Assignment3.IntegrationTests
{
    public class AccountControllerTest
    {
        public TestServer server { get; }
        public HttpClient client { get; }
        public AccountControllerTest() {
            var builder = new WebHostBuilder().UseStartup<Startup>();
            server = new TestServer(builder);
            client = server.CreateClient();
        }
    }
    [Fact]
    public async void TestVisitRoot() {
        var response = await client.GetAsync("/");
        response.EnsureSuccessStatusCode();
    }
}