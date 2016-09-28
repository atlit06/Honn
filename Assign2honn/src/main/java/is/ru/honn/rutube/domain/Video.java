package is.ru.honn.rutube.domain;

import java.util.List;

/**
 * Created by steinn on 28/09/16.
 */
public class Video {
    private int videoId;
    private String title;
    private String description;
    private String src;
    private String type;
    private List<String> tags;
    public Video(
            int videoId,
            String title,
            String description,
            String src,
            String type,
            List<String> tags
    ) {
        this.videoId = videoId;
        this.title = title;
        this.description = description;
        this.src = src;
        this.type = type;
        this.tags = tags;
    }
    public int getVideoId() {
        return this.videoId;
    }
}
