package is.ru.honn.rutube.test;

import is.ru.honn.rutube.exceptions.ServiceException;
import is.ru.honn.rutube.service.UserService;
import is.ru.honn.rutube.service.UserServiceStub;
import is.ru.honn.rutube.domain.User;
import is.ru.honn.rutube.domain.Video;

import is.ru.honn.rutube.service.VideoServiceStub;
import org.junit.Assert;
import org.junit.Test;

import java.util.Arrays;
import java.util.List;

/**
 * Created by Janus on 9/29/16.
 */
public class TestVideoService {
        /*  Add video, userId, title and src
            Add video that fails
            Check if getting videos that do not exist works correctly
        */

        @Test
        public void testAdd() throws ServiceException{
            UserServiceStub userService = new UserServiceStub();
            VideoServiceStub videoService = new VideoServiceStub(userService);


            User a = new User(0, "Janus", "Kristjansson", "jth@365.is", "Royal", "2003-07-17");
            User b = new User(1, "Janus", "Kristjansson", "jth@365.is", "Cookie", "1998-01-13");
            User c = new User(2, "Janus", "Kristjansson", "jth@365.is", "Smarties", "1994-04-24");

            Video va = new Video(0,
                                "The Avengers",
                                "Superheroes!",
                                "https://www.youtube.com/watch?v=t46cHwwVolA",
                                "Movie",
                                Arrays.asList("Avengers","Super Heroes"));
            Video vb = new Video(1,
                                "Scream Queens",
                                "Drama & Stuff",
                                "https://www.youtube.com/watch?v=i6_LwI-hUms",
                                "TV Show",
                                Arrays.asList("Drama","Teens"));

            userService.addUser(a);
            userService.addUser(b);
            userService.addUser(c);
            videoService.addVideo(va, 0);
            videoService.addVideo(vb, 1);

            Assert.assertEquals(va, videoService.getVideo(0));


        }

        @Test(expected = ServiceException.class)
        public void testAddFail() throws ServiceException{
            UserServiceStub userService = new UserServiceStub();
            VideoServiceStub videoService = new VideoServiceStub(userService);


            User a = new User(0, "Janus", "Kristjansson", "jth@365.is", "Royal", "2003-07-17");
            User b = new User(1, "Janus", "Kristjansson", "jth@365.is", "Cookie", "1998-01-13");
            User c = new User(2, "Janus", "Kristjansson", "jth@365.is", "Smarties", "1994-04-24");
            userService.addUser(a);
            userService.addUser(b);

            Video va = new Video(0,
                    "The Avengers",
                    "Superheroes!",
                    "https://www.youtube.com/watch?v=t46cHwwVolA",
                    "Movie",
                    Arrays.asList("Avengers","Super Heroes"));
            Video vb = new Video(1,
                    "Scream Queens",
                    "Drama & Stuff",
                    "https://www.youtube.com/watch?v=i6_LwI-hUms",
                    "TV Show",
                    Arrays.asList("Drama","Teens"));

            videoService.addVideo(va, 1);
            videoService.addVideo(va, 0);
        }

        @Test
        public void testGetNonExistingVideo() throws ServiceException{
            UserServiceStub userService = new UserServiceStub();
            VideoServiceStub videoService = new VideoServiceStub(userService);


            User a = new User(0, "Janus", "Kristjansson", "jth@365.is", "Royal", "2003-07-17");
            User b = new User(1, "Janus", "Kristjansson", "jth@365.is", "Cookie", "1998-01-13");
            User c = new User(2, "Janus", "Kristjansson", "jth@365.is", "Smarties", "1994-04-24");

            Video va = new Video(0,
                    "The Avengers",
                    "Superheroes!",
                    "https://www.youtube.com/watch?v=t46cHwwVolA",
                    "Movie",
                    Arrays.asList("Avengers","Super Heroes"));
            Video vb = new Video(1,
                    "Scream Queens",
                    "Drama & Stuff",
                    "https://www.youtube.com/watch?v=i6_LwI-hUms",
                    "TV Show",
                    Arrays.asList("Drama","Teens"));

            userService.addUser(a);
            userService.addUser(b);
            userService.addUser(c);

            videoService.addVideo(va, 1);
            videoService.addVideo(vb, 0);


            Assert.assertEquals(null, videoService.getVideo(234));
        }
}
