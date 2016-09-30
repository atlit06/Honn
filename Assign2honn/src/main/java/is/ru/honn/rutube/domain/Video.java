package is.ru.honn.rutube.domain;

import java.util.List;

/**
 * Created by steinn on 28/09/16.
 */
public class Video {

    public int videoId;
    public String title;
    public String description;
    public String src;
    public String type;
    public List<String> tags;

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

}
