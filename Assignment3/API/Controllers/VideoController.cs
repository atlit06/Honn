using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assignment3.Models;
using Assignment3.Services;
using Assignment3.Services.Exceptions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class VideoController : Controller
    {
        private readonly IVideoService _videoService;
        public VideoController(IVideoService videoService) {
            _videoService = videoService;
        }

        /// <summary>
        /// Returns a list of all videos in RuTube.
        /// Example response:
        /// [
        ///   {
        ///     "id": 1,
        ///     "title": "test",
        ///     "source": "test.is/test",
        ///     "creator": 1,
        ///     "channelId": 2
        ///   }
        /// ]  
        /// </summary>
        /// <returns>A list of videos</returns>
        [HttpGet]
        [Route("videos")]
        public IActionResult getAllVideos()
        {
            try
            {
                string accessToken = Request.Headers["Authorization"];
                List<VideoDTO> videos = _videoService.getAllVideos(accessToken);
                return Ok(videos);
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (AppObjectNotFoundException e) {
                return NotFound(e.Message);
            }
            catch (AppValidationException) {
                return Unauthorized();
            }

        }

        /// <summary>
        /// Returns a list of all videos in the a given channel.
        /// Example response:
        /// {
        ///   "channelID": 2,
        ///   "title": "tests",
        ///   "videos": [
        ///     {
        ///       "id": 1,
        ///       "title": "test",
        ///       "source": "test.is/test",
        ///       "creator": 1,
        ///       "channelId": 2
        ///     }
        ///   ] 
        /// } 
        /// </summary>
        /// <returns>A list of videos</returns>
        [HttpGet]
        [Route("channel/{channelID:int}", Name="getVideosInChannel")]
        public IActionResult getVideosInChannel(int channelID)
        {
            try
            {
                string accessToken = Request.Headers["Authorization"];
                ChannelVideosDTO videos = _videoService.getAllVideosByChannel(accessToken, channelID);
                return Ok(videos);
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (AppObjectNotFoundException e) {
                return NotFound(e.Message);
            }
            catch (AppValidationException) {
                return Unauthorized();
            }

        }

        /// <summary>
        /// Adds a new video to a channel.
        /// Example request:
        /// {
        ///     "title": "Scrubs",
        ///     "source": "www.example.com" 
        /// }  
        /// Example response:
        /// {
        ///   "id": 1,
        ///   "title": "Scrubs",
        ///   "source": "www.example.com",
        ///   "channelId": 2,
        ///   "creator": 1
        /// }
        /// </summary>
        /// <param name="channelID">id of the channel</param>
        /// <param name="vid">video object to be created</param>
        /// <returns>The newly created object along with a location header pointing to it.</returns>
        [HttpPost]
        [Route("channel/{channel:int}")]
        public IActionResult postVideoInChannel(int channel, VideoDTO vid) {
            try
            {
                string accessToken = Request.Headers["Authorization"];
                VideoDTO video = _videoService.addChannelVideo(accessToken, channel, vid);
                var location = Url.Link("getVideosInChannel", new { channelID = channel});
                return Created(location, video);
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (AppObjectNotFoundException e) {
                return NotFound(e.Message);
            }
            catch (AppValidationException) {
                return Unauthorized();
            }

        }

        /// <summary>
        /// Delete a video by id 
        /// </summary>
        /// <param name="videoID">id of the video to be deleted</param>
        /// <returns>200 Ok for successful deletion</returns>
        [HttpDelete]
        [Route("{videoID:int}")]
        public IActionResult deleteVideo(int videoID) {
            try
            {
                string accessToken = Request.Headers["Authorization"];
                _videoService.deleteVideo(accessToken, videoID);
                return Ok();
            }
            catch (InvalidParametersException e) {
                return BadRequest(e.Message);
            }
            catch (AppObjectNotFoundException e) {
                return NotFound(e.Message);
            }
            catch (AppValidationException) {
                return Unauthorized();
            }
        }
    }

}
