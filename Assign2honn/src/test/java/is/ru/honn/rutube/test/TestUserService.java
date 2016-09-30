package is.ru.honn.rutube.test;

import java.awt.peer.SystemTrayPeer;
import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.List;

import is.ru.honn.rutube.domain.User;
import is.ru.honn.rutube.service.UserService;
import is.ru.honn.rutube.service.UserServiceStub;
import is.ru.honn.rutube.exceptions.*;

import is.ru.honn.rutube.service.VideoService;
import is.ru.honn.rutube.service.VideoServiceStub;
import org.junit.Assert;
import org.junit.Test;

/**
 * Created by Janus on 9/29/16.
 */
public class TestUserService {

    @Test
    public void testAdd() throws ServiceException {
        UserServiceStub userService = new UserServiceStub();
        VideoServiceStub videoService = new VideoServiceStub(userService);

        User a = new User(0, "Janus", "Kristjansson", "jth@365.is", "Royal", "2003-07-17");
        User b = new User(1, "Janus", "Kristjansson", "jth@365.is", "Cookie", "1998-01-13");
        User c = new User(2, "Janus", "Kristjansson", "jth@365.is", "Smarties", "1994-04-24");

        userService.addUser(a);
        userService.addUser(b);
        userService.addUser(c);

        Assert.assertEquals(userService.getUsers(), Arrays.asList(a, b, c));
    }

    @Test(expected = ServiceException.class)
    public void testAddFail() throws ServiceException {

        UserServiceStub userService = new UserServiceStub();
        VideoServiceStub videoServiceStub = new VideoServiceStub(userService);


        User a = new User(0, "Janus", "Kristjansson", "jth@365.is", "Royal", "2003-07-17");
        User b = new User(1, "Janus", "Kristjansson", "jth@365.is", "Cookie", "1998-01-13");
        User c = new User(2, "Janus", "Kristjansson", "jth@365.is", "Smarties", "1994-04-24");


        userService.addUser(a);
        userService.addUser(a);
    }


    @Test
    public void testGet() throws ServiceException{
        UserServiceStub userService = new UserServiceStub();
        VideoServiceStub videoService = new VideoServiceStub(userService);


        User a = new User(0, "Janus", "Kristjansson", "jth@365.is", "Royal", "2003-07-17");
        User b = new User(1, "Janus", "Kristjansson", "jth@365.is", "Cookie", "1998-01-13");
        User c = new User(2, "Janus", "Kristjansson", "jth@365.is", "Smarties", "1994-04-24");


        Assert.assertEquals(Arrays.asList(), userService.getUsers());

        userService.addUser(a);
        userService.addUser(b);
        userService.addUser(c);

        Assert.assertEquals(Arrays.asList(a,b,c), userService.getUsers());
    }

    @Test
    public void testGetFail() throws ServiceException {

        UserServiceStub userService = new UserServiceStub();
        VideoServiceStub videoService = new VideoServiceStub(userService);


        User a = new User(0, "Janus", "Kristjansson", "jth@365.is", "Royal", "2003-07-17");
        User b = new User(1, "Janus", "Kristjansson", "jth@365.is", "Cookie", "1998-01-13");
        User c = new User(2, "Janus", "Kristjansson", "jth@365.is", "Smarties", "1994-04-24");

        userService.addUser(a);
        userService.addUser(b);
        userService.addUser(c);

        Assert.assertEquals(null, userService.getUser(23));
    }
}
