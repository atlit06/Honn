package is.ru.honn.rutube.reader;

import is.ru.honn.rutube.domain.User;
import is.ru.honn.rutube.domain.Video;
import is.ru.honn.rutube.exceptions.ServiceException;
import is.ru.honn.rutube.service.UserObserver;
import is.ru.honn.rutube.service.UserService;
import is.ru.honn.rutube.service.UserServiceStub;
import is.ruframework.process.RuProcessRunner;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;

import java.util.*;

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

      UserServiceStub userService = new UserServiceStub();
      UserObserver obs = new UserObserver();
      userService.attach(obs);
      User a = new User(0, "Janus", "Kristjansson", "jth@365.is", "Royal", "2003-07-17");
      User b = new User(1, "Janus", "Kristjansson", "jth@365.is", "Cookie", "1998-01-13");
      User c = new User(2, "Janus", "Kristjansson", "jth@365.is", "Smarties", "1994-04-24");

      userService.addUser(a);
      userService.addUser(b);
      userService.addUser(c);


      /*
      String[] a = {"process.xml"};
      RuProcessRunner.main(a);
      */


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
