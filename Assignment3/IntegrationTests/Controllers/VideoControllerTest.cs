using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;
using Assignment3.Services;
using Assignment3.Services.DataAccess;
using Assignment3.Services.Entities;
using Assignment3.Services.Exceptions;
using Assignment3.Models;
using Newtonsoft.Json.Linq;

namespace Assignment3.IntegrationTests
{
    public class VideoControllerTest
    {
        public TestServer server { get; }
        public HttpClient client { get; }

        public VideoControllerTest() {
            var builder = new WebHostBuilder()
            .UseKestrel()
            .UseStartup<TestStartup>();
            server = new TestServer(builder);
            client = server.CreateClient();
        }


        [Fact]
        public async void getAllVideosTest()
        {
            
           // Start by logging in a user
           var signupCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
             new KeyValuePair<string, string>("email", "steinn@steinn.is"),
             new KeyValuePair<string, string>("fullName", "Steinn Ellidi")
           });
           var signup = await client.PostAsync("/api/account/signup", signupCredentials);
           var loginCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
           });
           var login = await client.PostAsync("/api/account/login", loginCredentials);
           var loginContents = await login.Content.ReadAsStringAsync();
           JObject jsonParsedLogin = JObject.Parse(loginContents);
           // setting access token
           string accessToken = jsonParsedLogin["accessToken"].ToString();
           client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);


           // create a channel to add into
           var channel = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("title", "steinn")
           });
           var newChannel = await client.PostAsync("/api/video/channel", channel);
           Assert.Equal(HttpStatusCode.Created, newChannel.StatusCode);

           // create two videos in that channel
           var video = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("title", "steinn"),
             new KeyValuePair<string, string>("source", "steinn")
           });
           var newVideo = await client.PostAsync("/api/video/channel/1", video);
           Assert.Equal(HttpStatusCode.Created, newVideo.StatusCode);
           var video2 = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("title", "steinn2"),
             new KeyValuePair<string, string>("source", "steinn2")
           });
           var newVideo2 = await client.PostAsync("/api/video/channel/1", video2);
           Assert.Equal(HttpStatusCode.Created, newVideo2.StatusCode);

           // test the response for 2 items
           var response = await client.GetAsync("/api/video/videos");
           var contents = await response.Content.ReadAsStringAsync();
           
           JArray jsonParsed = JArray.Parse(contents);
           Assert.Equal(jsonParsed.Count, 2);
        }

        [Fact]
        public async void getVideosInchannelTest() {
           // Start by logging in a user
           var signupCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
             new KeyValuePair<string, string>("email", "steinn@steinn.is"),
             new KeyValuePair<string, string>("fullName", "Steinn Ellidi")
           });
           var signup = await client.PostAsync("/api/account/signup", signupCredentials);
           var loginCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
           });
           var login = await client.PostAsync("/api/account/login", loginCredentials);
           var loginContents = await login.Content.ReadAsStringAsync();
           JObject jsonParsedLogin = JObject.Parse(loginContents);
           // setting access token
           string accessToken = jsonParsedLogin["accessToken"].ToString();
           client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);


           // create two channels to add into
           var channel1 = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("title", "steinn")
           });
           var newChannel1 = await client.PostAsync("/api/video/channel", channel1);
           Assert.Equal(HttpStatusCode.Created, newChannel1.StatusCode);

           var channel2 = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("title", "steinn2")
           });
           var newChannel2 = await client.PostAsync("/api/video/channel", channel2);
           Assert.Equal(HttpStatusCode.Created, newChannel2.StatusCode);

           // create two videos in channel 1
           var video = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("title", "steinn"),
             new KeyValuePair<string, string>("source", "steinn")
           });
           var newVideo = await client.PostAsync("/api/video/channel/1", video);
           Assert.Equal(HttpStatusCode.Created, newVideo.StatusCode);
           var video2 = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("title", "steinn2"),
             new KeyValuePair<string, string>("source", "steinn2")
           });
           var newVideo2 = await client.PostAsync("/api/video/channel/1", video2);
           Assert.Equal(HttpStatusCode.Created, newVideo2.StatusCode);

           // create one video in channel 2
           var video3 = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("title", "steinn3"),
             new KeyValuePair<string, string>("source", "steinn3")
           });
           var newVideo3 = await client.PostAsync("/api/video/channel/2", video3);
           Assert.Equal(HttpStatusCode.Created, newVideo3.StatusCode);

           // test the response for channel 1 for 2 items
           var channel1response = await client.GetAsync("/api/video/channel/1");
           var channel1contents = await channel1response.Content.ReadAsStringAsync();
           
           JObject channel1Parsed = JObject.Parse(channel1contents);
           JArray channel1Arr = (JArray)channel1Parsed["videos"];
           Assert.Equal(channel1Arr.Count, 2);

           // test the response for channel 1 for 1 item
           var channel2response = await client.GetAsync("/api/video/channel/2");
           var channel2contents = await channel2response.Content.ReadAsStringAsync();
           
           JObject channel2Parsed = JObject.Parse(channel2contents);
           JArray channel2Arr = (JArray)channel2Parsed["videos"];
           Assert.Equal(channel2Arr.Count, 1);
        }

        [Fact]
        public async void deleteVideoTest()
        {
            
           // Start by logging in a user
           var signupCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
             new KeyValuePair<string, string>("email", "steinn@steinn.is"),
             new KeyValuePair<string, string>("fullName", "Steinn Ellidi")
           });
           var signup = await client.PostAsync("/api/account/signup", signupCredentials);
           var loginCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
           });
           var login = await client.PostAsync("/api/account/login", loginCredentials);
           var loginContents = await login.Content.ReadAsStringAsync();
           JObject jsonParsedLogin = JObject.Parse(loginContents);
           // setting access token
           string accessToken = jsonParsedLogin["accessToken"].ToString();
           client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);


           // create a channel to add into
           var channel = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("title", "steinn")
           });
           var newChannel = await client.PostAsync("/api/video/channel", channel);
           Assert.Equal(HttpStatusCode.Created, newChannel.StatusCode);

           // create two videos in that channel
           var video = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("title", "steinn"),
             new KeyValuePair<string, string>("source", "steinn")
           });
           var newVideo = await client.PostAsync("/api/video/channel/1", video);
           Assert.Equal(HttpStatusCode.Created, newVideo.StatusCode);
           var video2 = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("title", "steinn2"),
             new KeyValuePair<string, string>("source", "steinn2")
           });
           var newVideo2 = await client.PostAsync("/api/video/channel/1", video2);
           Assert.Equal(HttpStatusCode.Created, newVideo2.StatusCode);

           // test the response for 2 items
           var response = await client.GetAsync("/api/video/videos");
           var contents = await response.Content.ReadAsStringAsync();
           
           JArray jsonParsed = JArray.Parse(contents);
           Assert.Equal(jsonParsed.Count, 2);

           // delete one of the videos
           var deleteResponse = await client.DeleteAsync("/api/video/1");
           Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

           // test the response for 1 item
           var responseAfterDelete = await client.GetAsync("/api/video/videos");
           var contentsAfterDelete = await responseAfterDelete.Content.ReadAsStringAsync();
           
           JArray jsonParsedAfterDelete = JArray.Parse(contentsAfterDelete);
           Assert.Equal(jsonParsedAfterDelete.Count, 1);
        }
    }
}