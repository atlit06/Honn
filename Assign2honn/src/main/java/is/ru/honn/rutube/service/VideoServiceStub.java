package is.ru.honn.rutube.service;

import is.ru.honn.rutube.domain.Video;
import is.ru.honn.rutube.exceptions.ServiceException;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by steinn on 28/09/16.
 */
public class VideoServiceStub implements VideoService {
    private List<Video> videos;
    public VideoServiceStub() {
        this.videos = new ArrayList<Video>();
    }
    public Video getVideo(int videoId) {
        List<String> tags = new ArrayList<String>();
        tags.add("Tarzan");
        tags.add("Jane");
       return new Video(videoId, "Tarzan", "Man thinks he is monkey", "http://www.example.com", "video", tags);
    };
    public List<Video> getVideosbyUser(int userId) {
        List<Video> userVids = new ArrayList<Video>();
        List<String> tags = new ArrayList<String>();
        tags.add("Snakes");
        tags.add("Plane");
        userVids.add(new Video(1, "Snakes on a plane", "Snakes are on a plane", "http://vedur.is", "video", tags));
        return userVids;
    };
    public int addVideo(Video video, int userId) throws ServiceException {return 0;};
}
