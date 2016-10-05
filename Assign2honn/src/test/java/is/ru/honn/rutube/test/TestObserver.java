package is.ru.honn.rutube.test;

import is.ru.honn.rutube.domain.User;
import is.ru.honn.rutube.exceptions.ServiceException;
import is.ru.honn.rutube.service.UserObserver;
import is.ru.honn.rutube.service.UserServiceStub;
import org.junit.Assert;
import org.junit.Test;

import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

/**
 * Created by Janus on 9/29/16.
 */
public class TestObserver {

    @Test
    public void testObserver() throws ServiceException {

        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        PrintStream ps = new PrintStream(baos);

        PrintStream old = System.out;

        System.setOut(ps);

        UserServiceStub userService = new UserServiceStub();
        UserObserver obs = new UserObserver();
        userService.attach(obs);
        User a = new User(0, "Janus", "Kristjansson", "jth@365.is", "Royal", "2003-07-17");
        User b = new User(1, "Janus", "Kristjansson", "jth@365.is", "Cookie", "1998-01-13");
        User c = new User(2, "Janus", "Kristjansson", "jth@365.is", "Smarties", "1994-04-24");

        userService.addUser(a);
        userService.addUser(b);
        userService.addUser(c);

        String user = (a.toString() + "\n" + b.toString() + "\n" + c.toString() + "\n");

        Assert.assertEquals(user, baos.toString());
    }
}
