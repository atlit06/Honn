using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Assignment3.Services;
using Assignment3.UnitTests.MockDataMappers;
using Assignment3.Services.DataAccess;
using Assignment3.Services.Entities;
using Assignment3.Services.Exceptions;
using Assignment3.Models;

namespace Assignment3.UnitTests
{
    public class UserServiceTest
    {
            
        


        [Fact]
        public void updateUsernameTest() {
            MockTokenService tokenService = new MockTokenService();
            MockUserDataMapper mockMapper = new MockUserDataMapper();
            IUserService _service = new UserService(tokenService, mockMapper);
            

            mockMapper.Users.Add(new User{  
                id = 1,
                username = "janus",
                email = "jth@365.is",
                password = "qwerty",
                fullName = "Janus Þór"
            });
            


           _service.updateUserName(new UpdateUsernameDTO {  
                username = "janus",
                email = "jth@365.is",
                password = "qwerty",
                fullName = "Janus Þór",
                accessToken = tokenService.createUserToken(1, "janus"),
                newUsername = "test"
            });

            Assert.Equal("test", mockMapper.Users.First().username);
        }

        [Fact]
        public void addFavouriteVideoTest() {
            MockTokenService tokenService = new MockTokenService();
            MockUserDataMapper mockMapper = new MockUserDataMapper();
            IUserService _service = new UserService(tokenService, mockMapper);
            

            mockMapper.Users.Add(new User{  
                id = 1,
                username = "janus",
                email = "jth@365.is",
                password = "qwerty",
                fullName = "Janus Þór"
            });
            Video v = new Video {id = 1,
                title = "testing",
                source = "testing",
                creator  = 1,
                channelId = 0};
            mockMapper.Videos.Add(v);
            var vDTO = new VideoDTO {id = 1,
                title = "testing",
                source = "testing",
                creator  = 1,
                channelId = 0};
            var authDTO = new AuthorizedUserDTO{
                username = "janus",
                email = "jth@365.is",
                password = "qwerty",
                fullName = "Janus Þór",
                accessToken = "1"
            };

            _service.addFavouriteVideo(authDTO, vDTO);

            Assert.Equal(1, mockMapper.Favourites.Count());
            Assert.Equal(1, mockMapper.Favourites[0].userId);
            Assert.Equal(1, mockMapper.Favourites[0].videoId);
        }
        
        [Fact]
        public void addFriendTest() {
            MockTokenService tokenService = new MockTokenService();
            MockUserDataMapper mockMapper = new MockUserDataMapper();
            IUserService _service = new UserService(tokenService, mockMapper);
            

            mockMapper.Users.Add(new User{  
                id = 1,
                username = "janus",
                email = "jth@365.is",
                password = "qwerty",
                fullName = "Janus Þór"
            });
            mockMapper.Users.Add(new User{  
                id = 2,
                username = "steinn",
                email = "st@st.is",
                password = "qwerty",
                fullName = "Steinn"
            });

            var authDTO = new FriendDTO{
                username = "janus",
                email = "jth@365.is",
                password = "qwerty",
                fullName = "Janus Þór",
                accessToken = "1",
                friendUsername = "steinn"
            };

            _service.addFriend(authDTO);

            Assert.Equal(1, mockMapper.Friends.Count());
        }


        [Fact]
        public void getFavVidsTest(){
            MockTokenService tokenService = new MockTokenService();
            MockUserDataMapper mockMapper = new MockUserDataMapper();
            IUserService _service = new UserService(tokenService, mockMapper);
            

            mockMapper.Users.Add(new User{  
                id = 1,
                username = "janus",
                email = "jth@365.is",
                password = "qwerty",
                fullName = "Janus Þór"
            });

            Video v1 = new Video {id = 1,
                title = "testing",
                source = "testing",
                creator  = 1,
                channelId = 0};
            Video v2 = new Video {id = 2,
                title = "testing2",
                source = "testing2",
                creator  = 1,
                channelId = 0};
                
            Video v3 = new Video {id = 3,
                title = "testing3",
                source = "testing3",
                creator  = 1,
                channelId = 0};
            
            mockMapper.Videos.Add(v1);
            mockMapper.Videos.Add(v2);
            mockMapper.Videos.Add(v3);

            var vDTO1 = new VideoDTO {id = 1,
                title = "testing",
                source = "testing",
                creator  = 1,
                channelId = 0};
            var vDTO2 = new VideoDTO {id = 2,
                title = "testing2",
                source = "testing2",
                creator  = 1,
                channelId = 0};
            var vDTO3 = new VideoDTO {id = 3,
                title = "testing3",
                source = "testing3",
                creator  = 1,
                channelId = 0};
            
            var authDTO = new AuthorizedUserDTO{
                username = "janus",
                email = "jth@365.is",
                password = "qwerty",
                fullName = "Janus Þór",
                accessToken = "1"
            };
            _service.addFavouriteVideo(authDTO, vDTO1);
            _service.addFavouriteVideo(authDTO, vDTO2);
            _service.addFavouriteVideo(authDTO, vDTO3);


            List<VideoDTO> verifyData = (from v in mockMapper.Videos
                            select new VideoDTO {
                            id = v.id,
                            title = v.title,
                            source = v.source,
                            creator = v.creator,
                            channelId = v.channelId}).ToList();
            List<VideoDTO> testData =  _service.getFavouriteVideos(authDTO);
            
            Assert.Equal(mockMapper.Favourites.Count(), 3);

            Assert.Equal(verifyData.Count(), testData.Count());
            for(int i = 0; i < 0; i++){
                Assert.Equal(verifyData[i].id, testData[i].id);
                Assert.Equal(verifyData[i].title, testData[i].title);
                Assert.Equal(verifyData[i].source, testData[i].source);
                Assert.Equal(verifyData[i].creator, testData[i].creator);
                Assert.Equal(verifyData[i].channelId, testData[i].channelId);
            }
        }

        [Fact]
        public void getFriendsTest(){
            MockTokenService tokenService = new MockTokenService();
            MockUserDataMapper mockMapper = new MockUserDataMapper();
            IUserService _service = new UserService(tokenService, mockMapper);
            
            string name = "test";
            string mailAddition = "@test.is";
            string password = "test";
            string fullname = "Test Testson The ";
            List<FriendDTO> DTOfriends = new List<FriendDTO>();

            // Fill USER datalist and friendDTO datalist
            for(int i = 0; i < 8; i++){
                User u = new User{
                    id = i,
                    username = name += i.ToString(),
                    email = name += i.ToString() + mailAddition,
                    password = password += i.ToString(),
                    fullName = fullname += i.ToString()
                };
                mockMapper.Users.Add(u);
                if(i > 0){
                    var authDTO = new FriendDTO{
                    username = u.username,
                    email = u.email,
                    password = u.password,
                    fullName = u.fullName,
                    accessToken = "asd",
                    friendUsername = mockMapper.Users[i-1].username};
                    DTOfriends.Add(authDTO);
                }
            }

            foreach (FriendDTO friend in DTOfriends)
            {
                _service.addFriend(friend);
            }

            Assert.Equal(mockMapper.Friends.Count(), 7);

        }
    
    }
}