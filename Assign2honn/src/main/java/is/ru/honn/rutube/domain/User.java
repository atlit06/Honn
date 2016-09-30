package is.ru.honn.rutube.domain;


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
    public List<String> videos;

    public User(int userId, String firstName, String lastName, String email, String displayName, Date birthDate, List<String> videos) {
        this.userId = userId;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.displayName = displayName;
        this.birthDate = birthDate;
        this.videos = videos;
    }

    public void addVideo(){

    }


}
