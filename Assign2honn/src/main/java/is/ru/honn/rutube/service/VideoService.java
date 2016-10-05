package is.ru.honn.rutube.service;

import is.ru.honn.rutube.domain.Video;
import is.ru.honn.rutube.exceptions.ServiceException;

import java.util.List;

/**
 * Created by steinn on 28/09/16.
 */
public interface VideoService
{
    /**
     * gets a video by id
     * @param videoId the id of the video
     * @return a video object
     */
    Video getVideo(int videoId);

    /**
     * gets all videos of a user
     * @param userId the id of the user
     * @return a list of Videos
     */
    List<Video> getVideosbyUser(int userId);

    /**
     * adds a new video to a user
     * @param video the video to be added
     * @param userId the id of the user that the video will be added to
     * @return the video id
     * @throws ServiceException if the user can not be found.
     */
    int addVideo(Video video, int userId) throws ServiceException;
}

