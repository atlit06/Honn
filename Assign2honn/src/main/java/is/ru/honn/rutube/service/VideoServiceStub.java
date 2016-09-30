package is.ru.honn.rutube.service;

import is.ru.honn.rutube.domain.Video;
import is.ru.honn.rutube.domain.User;
import is.ru.honn.rutube.exceptions.ServiceException;
import is.ru.honn.rutube.service.UserService;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by steinn on 28/09/16.
 */
public class VideoServiceStub implements VideoService {

    private UserServiceStub userService;

    public VideoServiceStub(UserServiceStub userService) {
        this.userService = userService;
    }

    public Video getVideo(int videoId) {
        for(User user : userService.users) {
            for(Video vid : user.videos) {
                if (vid.videoId == videoId){
                    return vid;
                }
            }
        }
        return null;
    }

    public List<Video> getVideosbyUser(int userId) {
        for (User user : userService.users) {
            if (user.userId == userId) {
                return user.videos;
            }
        }

        return null;
    }

    public int addVideo(Video video, int userId) throws ServiceException {

        if (getVideo(video.videoId) != null){
            throw new ServiceException("This videoId Exists Already!");
        }

        for (User user : userService.users) {
            if (user.userId == userId) {
                user.videos.add(video);
                return video.videoId;
            }
        }

        throw new ServiceException("User Not Found!");
    }
}
