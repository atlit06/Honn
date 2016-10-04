package is.ru.honn.rutube.reader;

import is.ru.honn.rutube.domain.User;
import is.ru.honn.rutube.domain.Video;
import is.ru.honn.rutube.exceptions.ServiceException;
import is.ruframework.process.RuProcessRunner;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Iterator;
import java.util.List;

public class UserReader extends AbstractReader implements ReadHandler {

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
      final int[] position = {0};
    jUsers.stream().forEach(jUser1 -> {
      JSONObject jUser = (JSONObject) jUser1;
      int userId = getInt(jUser, "userId");
        User user = null;
        try {
            user = new User(
            userId,
            (String) jUser.get("firstName"),
            (String) jUser.get("lastName"),
            (String) jUser.get("email"),
            (String) jUser.get("displayName"),
            (String) jUser.get("birthdate")
            );
        } catch (ServiceException e) {
            e.printStackTrace();
        }

      JSONArray jVideos = (JSONArray) jUser.get("videos");
      Object jvids = videoReader.parse(jVideos.toString());
      List<Video> videos = (List<Video>) jvids;
      user.setVideos(videos);
      readHandler.read(position[0], user);
      position[0]++;
      users.add(user);
    });

    return users;
  }

  public void read(int count, Object user) {
      User usr = (User)user;
      System.out.println(usr.displayName);
      System.out.println(count);
      return;
  }

  public void testRead() {
      ReaderFactory factory = new ReaderFactory();
      Reader reader;
      try {
          reader = factory.getReader("userReader");
      } catch (ReaderException r) {
          System.out.println(r.getMessage());
          return;
      }
      reader.setReadHandler(this);
      reader.setURI("http://mockaroo.com/f13b8200/download?count=1&key=e79a3650");
      try {
          Object nextUser = reader.read();
      } catch (ReaderException e) {
          e.printStackTrace();
          return;
      }
  }

  public static void main(String args[]) throws Exception{
      String[] a = {"process.xml"};
      RuProcessRunner.main(a);


      /*
    VideoReader videoReader = new VideoReader();
    UserReader userReader = new UserReader(videoReader);
    ClientRequest clientRequest = new ClientRequest();
    String content = clientRequest.getRequest("http://mockaroo.com/f13b8200/download?count=1&key=e79a3650");
    List<User> users = (List<User>)userReader.parse(content);
      ReaderFactory factory = new ReaderFactory();

      UserReader reader;
      try {
          reader = (UserReader)factory.getReader("userReader");
          reader.testRead();
      } catch (ReaderException r) {
          System.out.println(r.getMessage());
          return;
      }
    */

  }

}
