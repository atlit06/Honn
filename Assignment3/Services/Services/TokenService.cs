using System;
using Assignment3.Services.DataAccess;
using Assignment3.Services.Entities;
using Newtonsoft.Json.Linq;

namespace Assignment3.Services
{
    public class TokenService : ITokenService {
        private  ITokenDataMapper _mapper;

        public TokenService(ITokenDataMapper mapper) {
            _mapper = mapper;
        }

        private static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public bool validateUserToken(string token, int userID) {
            Token userToken = _mapper.getTokenByUserID(userID);
            if (userToken == null) {
                return false;
            }
            DateTime expiry = Convert.ToDateTime(userToken.expires);
            if (DateTime.Compare(DateTime.Now, expiry) > 0) {
                return false;
            }
            if (userToken.token != token) {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Creates a base64 encoded string based on the username and the current datetime,
        /// tokens are valid for 24 hours.
        /// </summary>
        /// <param name="userID">ID of the user</param>
        /// <param name="username">the users username</param>
        /// <returns>the newly created user token</returns>
        public string createUserToken(int userID, string username) {
            string tokenString = "{ \"username\": \"" + username +"\", \"time\": \"" + DateTime.Now.ToString() + "\" }";
            string tokenStringBase64 = Base64Encode(tokenString);
            System.TimeSpan duration = new System.TimeSpan(1, 0, 0, 0);
            string expires = (DateTime.Now.Add(duration)).ToString();
            Token newToken = new Token {
                userID = userID,
                token = tokenStringBase64,
                expires = expires
            };
            _mapper.createOrUpdateUserToken(newToken);
            return tokenStringBase64;
        }

        public string getUsernameFromTokenString(string tokenString) {
            byte[] data = Convert.FromBase64String(tokenString);
            string decodedString = System.Text.Encoding.UTF8.GetString(data);
            JObject jsonParsed = JObject.Parse(decodedString);
            return jsonParsed["username"].ToString();

        }
    }
}
