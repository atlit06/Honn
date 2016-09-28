package is.ru.honn.rutube.service;


import java.util.Date;
import java.util.List;

/**
 * Created by Janus on 9/28/16.
 */
public class User {

    private int userId;
    private String firstName;
    private String lastName;
    private String email;
    private String displayName;
    private Date birthDate;
    private List<String> videos;

    public User(int userId, String firstName, String lastName, String email, String displayName, Date birthDate, List<String> videos) {
        this.userId = userId;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.displayName = displayName;
        this.birthDate = birthDate;
        this.videos = videos;
    }

    public int getUserId() {
        return userId;
    }

    public String getFirstName() {
        return firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public String getEmail() {
        return email;
    }

    public String getDisplayName() {
        return displayName;
    }

    public Date getBirthDate() {
        return birthDate;
    }

    public List<String> getVideos() {
        return videos;
    }


}
