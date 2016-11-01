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
    public class AccountControllerTest
    {
        public TestServer server { get; }
        public HttpClient client { get; }
        public AccountControllerTest() {
            var builder = new WebHostBuilder()
            .UseKestrel()
            .UseStartup<TestStartup>();
            server = new TestServer(builder);
            client = server.CreateClient();
        }

        [Fact]
        public async void TestSignup() {
           // Test a succesfull registration
           var content = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
             new KeyValuePair<string, string>("email", "steinn@steinn.is"),
             new KeyValuePair<string, string>("fullName", "Steinn Ellidi")
           });
           var response = await client.PostAsync("/api/account/signup", content);
           Assert.Equal(HttpStatusCode.Created, response.StatusCode);

           // Test a duplicate registration
           var dupContent = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
             new KeyValuePair<string, string>("email", "steinn@steinn.is"),
             new KeyValuePair<string, string>("fullName", "Steinn Ellidi")
           });
           var dupResponse = await client.PostAsync("/api/account/signup", dupContent);
           Assert.Equal(HttpStatusCode.BadRequest, dupResponse.StatusCode);

           // Test a registration with missing parameters
           var illegalContent = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
             new KeyValuePair<string, string>("email", "steinn@steinn.is"),
           });
           var illegalResponse = await client.PostAsync("/api/account/signup", illegalContent);
           Assert.Equal(HttpStatusCode.BadRequest, illegalResponse.StatusCode);
        }

        [Fact]
        public async void TestLogin() {
           // Testing a succesfull login
           var signupCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
             new KeyValuePair<string, string>("email", "steinn@steinn.is"),
             new KeyValuePair<string, string>("fullName", "Steinn Ellidi")
           });
           var response = await client.PostAsync("/api/account/signup", signupCredentials);
           Assert.Equal(HttpStatusCode.Created, response.StatusCode);

           var loginCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
           });
           var login = await client.PostAsync("/api/account/login", loginCredentials);
           var contents = await login.Content.ReadAsStringAsync();
           JObject jsonParsed = JObject.Parse(contents);
           Assert.Equal(HttpStatusCode.OK, login.StatusCode);
           Assert.NotNull(jsonParsed["accessToken"]);
           Assert.Equal(jsonParsed["fullName"], "Steinn Ellidi");
           Assert.Equal(jsonParsed["username"], "steinn");

           // Testing login with no password
           var badCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn2"),
           });
           var noPass = await client.PostAsync("/api/account/login", badCredentials);
           Assert.Equal(HttpStatusCode.BadRequest, noPass.StatusCode);

           // Testing login with a user not in the system
           var badUsernameCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn235235"),
             new KeyValuePair<string, string>("password", "steinn"),
           });
           var noUser = await client.PostAsync("/api/account/login", badUsernameCredentials);
           Assert.Equal(HttpStatusCode.NotFound, noUser.StatusCode);

           // Testing login with a user with an invalid password
           var invalidPassCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "invalid"),
           });
           var invalidPass = await client.PostAsync("/api/account/login", invalidPassCredentials);
           Assert.Equal(HttpStatusCode.Unauthorized, invalidPass.StatusCode);

        }

        public async void loginUser() {
           // Start by logging in a user
           var signupCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
             new KeyValuePair<string, string>("email", "steinn@steinn.is"),
             new KeyValuePair<string, string>("fullName", "Steinn Ellidi")
           });
           var response = await client.PostAsync("/api/account/signup", signupCredentials);
           var loginCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
           });
           var login = await client.PostAsync("/api/account/login", loginCredentials);
           var contents = await login.Content.ReadAsStringAsync();
           JObject jsonParsed = JObject.Parse(contents);
           // setting access token
           string accessToken = jsonParsed["accessToken"].ToString();
           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
        [Fact]
        public async void TestUpdatePassword() {
           // Start by logging in a user
           var signupCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
             new KeyValuePair<string, string>("email", "steinn@steinn.is"),
             new KeyValuePair<string, string>("fullName", "Steinn Ellidi")
           });
           var response = await client.PostAsync("/api/account/signup", signupCredentials);
           var loginCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
           });
           var login = await client.PostAsync("/api/account/login", loginCredentials);
           var contents = await login.Content.ReadAsStringAsync();
           JObject jsonParsed = JObject.Parse(contents);
           // setting access token
           string accessToken = jsonParsed["accessToken"].ToString();
           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

           // Successfully updating credentials
           var updatePasswordCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("newPassword", "steinn2")
           });
           var update = await client.PutAsync("/api/account/updatePassword", updatePasswordCredentials);
           Assert.Equal(HttpStatusCode.OK, update.StatusCode);

           // Try updating with a bad token
           var failedUpdate = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
           });
           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "teeat");
           var noUpdate = await client.PutAsync("/api/account/updatePassword", failedUpdate);
           Assert.Equal(HttpStatusCode.Unauthorized, noUpdate.StatusCode);
        }
        [Fact]
        public async void deleteUser() {
           // Start by logging in a user
           var signupCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
             new KeyValuePair<string, string>("email", "steinn@steinn.is"),
             new KeyValuePair<string, string>("fullName", "Steinn Ellidi")
           });
           var response = await client.PostAsync("/api/account/signup", signupCredentials);
           var loginCredentials = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
           });
           var login = await client.PostAsync("/api/account/login", loginCredentials);
           var contents = await login.Content.ReadAsStringAsync();
           JObject jsonParsed = JObject.Parse(contents);
           // setting access token
           string accessToken = jsonParsed["accessToken"].ToString();
           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            // Delete an existing user
            var deleteCredentials = new FormUrlEncodedContent(new[]
            {
              new KeyValuePair<string, string>("username", "steinn"),
              new KeyValuePair<string, string>("password", "steinn"),
            });
           var deleteOk = await client.PostAsync("/api/account/deleteUser", deleteCredentials);
           Assert.Equal(HttpStatusCode.OK, deleteOk.StatusCode);

           // Delete a nonexisting user
           var deleteCredentials2 = new FormUrlEncodedContent(new[]
           {
             new KeyValuePair<string, string>("username", "steinn"),
             new KeyValuePair<string, string>("password", "steinn"),
           });
           var response2 = await client.PostAsync("/api/account/deleteUser", deleteCredentials2);
           Assert.Equal(HttpStatusCode.NotFound, response2.StatusCode);


        }
    }
}