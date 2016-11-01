using System;
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
    public class VideoServiceTest
    {   
        // Declaring mock classes
        public class TokenServiceMock : ITokenService
        {
            public bool validateUserToken(string token, int userID){
                return true;
            }
            public string createUserToken(int userID, string username){
                return "test";
            }
            public string getUsernameFromTokenString(string tokenString){
                return "test";
            }
        }

        public class AccountMapperMock : IAccountDataMapper
        {
            public User findUserByUsername(string username) {
                return new User {
                    id = 1,
                    username = "test"
                };
            }
            public void createUser(User user) {
                return;
            }
            public void updateUserPassword(string username, string password) {
                return;
            }
            public void deleteUser(int userID) {
                return;
            }
        }

        // Mock class for add video
        public class VideoMapperMock : IVideoDataMapper
        {
            public int callCount;
            public int addVideoCallCount;
            public int deleteVideoCallCount;
            public List<Video> returnVideos;
            public Video returnVideo;
            public Channel returnChannel;
            public VideoMapperMock() {
                callCount = 0;
                addVideoCallCount = 0;
                deleteVideoCallCount = 0;
                returnChannel = null;
                returnVideos = null;
                returnVideo = null;
            }

            public int addChannel(Channel ch) {
                return 0;
            }

            public List<Video> getAllVideos()
            {
                callCount += 1;
                return returnVideos;
            }
            public List<Video> getAllVideosInChannel(int channelID) {
                return returnVideos;
            }
            public Channel getChannelById(int id) {
                return returnChannel;
            }
            public int addVideo(Video id) {
                addVideoCallCount += 1;
                return 1;
            }
            public void deleteVideoById(int id) {
                deleteVideoCallCount += 1;
                return;
            }
            public Video getVideoById(int id) {
                return returnVideo;
            }

        }

        /// <summary>
        /// Test for the getAllVideos function. 
        /// </summary>
        [Fact]
        public void getAllVideosTest() {
            VideoMapperMock mapper = new VideoMapperMock();
            mapper.returnVideos = new List<Video>();
            VideoService service = new VideoService(new TokenServiceMock(), mapper, new AccountMapperMock());
            service.getAllVideos("test");
            Assert.Equal(mapper.callCount, 1);
        }

        [Fact]
        public void getAllVideosByChannelTest() {
            VideoMapperMock mapper = new VideoMapperMock();
            mapper.returnVideos = new List<Video>();
            VideoService service = new VideoService(new TokenServiceMock(), mapper, new AccountMapperMock());
            Exception ex = Assert.Throws<AppObjectNotFoundException>( () => service.getAllVideosByChannel("test", 2));
            mapper.returnChannel = new Channel {
                ID = 1,
                title = "test",
            };
            ChannelVideosDTO vidsInChannel = service.getAllVideosByChannel("test", 2);
            Assert.Equal(vidsInChannel.channelID, 1);
            Assert.Equal(vidsInChannel.title, "test");
        }

        [Fact]
        public void addChannelVideoTest() {
            VideoMapperMock mapper = new VideoMapperMock();
            mapper.returnVideos = new List<Video>();
            VideoService service = new VideoService(new TokenServiceMock(), mapper, new AccountMapperMock());
            VideoDTO vid = new VideoDTO {
                title = "test"
            };
            Exception ex = Assert.Throws<InvalidParametersException>( () => service.addChannelVideo("test", 2, vid));
            vid.source = "testing";
            Exception ex2 = Assert.Throws<AppObjectNotFoundException>( () => service.addChannelVideo("test", 2, vid));
            mapper.returnChannel = new Channel {
                ID = 1,
                title = "test",
            };
            service.addChannelVideo("test", 2, vid);
            Assert.Equal(mapper.addVideoCallCount, 1);
        }

        [Fact]
        public void deleteVideoTest() {
            VideoMapperMock mapper = new VideoMapperMock();
            mapper.returnVideos = new List<Video>();
            VideoService service = new VideoService(new TokenServiceMock(), mapper, new AccountMapperMock());
            Exception ex = Assert.Throws<AppObjectNotFoundException>( () => service.deleteVideo("test", 2));
            mapper.returnVideo = new Video {
                channelId = 1,
                creator = 5
            };
            Exception ex2 = Assert.Throws<AppValidationException>( () => service.deleteVideo("test", 2));
            mapper.returnVideo = new Video {
                channelId = 1,
                creator = 1
            };
            service.deleteVideo("test", 2);
            Assert.Equal(mapper.deleteVideoCallCount, 1);
        }
    }
}