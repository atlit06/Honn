package is.ru.honn.rutube.reader;

import is.ru.honn.domain.User;
import is.ru.honn.domain.Video;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;

import java.util.ArrayList;
import java.util.List;

public class UserReader {

  private VideoReader videoReader;

  public UserReader(VideoReader videoReader){
    this.videoReader = videoReader;
  }


  /**
   *
   * Walks through the json String and maps it to lists of users, which all contain 1 or more videos.
   *
   * @param content Json string with list of users, see http://mockaroo.com/f13b8200/download?count=1&key=e79a3650
   * @return Object which is a list of users.
   */
  public Object parse(String content) {

    //root object
    JSONObject jsonObject = (JSONObject) JSONValue.parse(content);

    // Get apiResults, this is an array so get the first (and only) item
    JSONArray apiResults = (JSONArray) jsonObject.get("apiResults");
    JSONObject jTmp = (JSONObject) apiResults.get(0);

    JSONArray jUsers = (JSONArray) jTmp.get("users");
    List<User> users = new ArrayList<>();

    jUsers.stream().forEach(jUser1 -> {
      JSONObject jUser = (JSONObject) jUser1;
      int userId = getInt(jUser, "userId");
      User user = new User(
      userId,
      (String) jUser.get("firstName"),
      (String) jUser.get("lastName"),
      (String) jUser.get("email"),
      (String) jUser.get("displayName"),
      (String) jUser.get("birthdate"));

      JSONArray jVideos = (JSONArray) jUser.get("videos");
      Object jvids = videoReader.parse(jVideos.toString());
      List<Video> videos = (List<Video>) jvids;
      user.setVideos(videos);

      users.add(user);
    });

    return users;
  }

  /**
   *
   * @param jParent Json parent containing an integer field.
   * @param name name of the integer field
   * @return int value of the json int in the jParent object.
   */
  protected int getInt(JSONObject jParent, String name)
  {
    if(jParent == null)
      return 0;
    Long value = (Long)jParent.get(name);
    if(value == null)
      return 0;
    return value.intValue();
  }

  public static void main(String args[]){

    VideoReader videoReader = new VideoReader();
    UserReader userReader = new UserReader(videoReader);
    ClientRequest clientRequest = new ClientRequest();
    String content = clientRequest.getRequest("http://mockaroo.com/f13b8200/download?count=1&key=e79a3650");
    List<User> users = (List<User>)userReader.parse(content);

  }

}
