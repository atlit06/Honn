package is.ru.honn.rutube.service;

import is.ru.honn.rutube.domain.Video;
import is.ru.honn.rutube.exceptions.ServiceException;

import java.util.List;

/**
 * Created by steinn on 28/09/16.
 */
public interface VideoService
{
    Video getVideo(int videoId);
    List<Video> getVideosbyUser(int userId);
    int addVideo(Video video, int userId) throws ServiceException;
}

