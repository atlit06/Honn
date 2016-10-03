package is.ru.honn.rutube.domain;


import is.ru.honn.rutube.exceptions.ServiceException;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * Created by Janus on 9/28/16.
 */
public class User {

    public int userId;
    public String firstName;
    public String lastName;
    public String email;
    public String displayName;
    public Date birthDate;
    public List<Video> videos;


    public User(int userId, String firstName, String lastName, String email, String displayName, String birthDate) throws ServiceException {
        this.userId = userId;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.displayName = displayName;
        DateFormat format = new SimpleDateFormat("y-M-d");
        try {
            this.birthDate = format.parse(birthDate);
        } catch (Exception e) {
            throw new ServiceException("Couldnt Parse String To Date");
        }
        this.videos = new ArrayList<Video>();
    }


    public void setVideos(List<Video> videos) {
        this.videos = videos;
    }
}
