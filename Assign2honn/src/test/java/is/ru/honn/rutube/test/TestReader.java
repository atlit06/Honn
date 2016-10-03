package is.ru.honn.rutube.test;

import is.ru.honn.rutube.domain.User;
import is.ru.honn.rutube.reader.*;
import org.junit.Assert;
import org.junit.Test;

import java.util.ArrayList;

/**
 * Created by Janus on 9/29/16.
 */
public class TestReader implements ReadHandler {
    public void read(int count, Object object) {
        return;
    }
    @Test
    public void testFactoryVideoReader() {
        ReaderFactory factory = new ReaderFactory();
        try {
            // check if we can cast the Reader returned to VideoReader
            Reader reader = (VideoReader)factory.getReader("videoReader");
        } catch (ReaderException e) {
            Assert.assertEquals(true, false);
        }
    }
    @Test
    public void testFactoryUserReader() {
        ReaderFactory factory = new ReaderFactory();
        try {
            // check if we can cast the Reader returned to VideoReader
            Reader reader = (UserReader)factory.getReader("userReader");
        } catch (ReaderException e) {
            Assert.assertEquals(true, false);
        }
    }
    @Test(expected = ReaderException.class)
    public void testFactoryBogusReader() throws ReaderException {
        ReaderFactory factory = new ReaderFactory();
        Reader reader = factory.getReader("bogusReader");
    }
    @Test(expected = ReaderException.class)
    public void NoURISet() throws ReaderException {
        ReaderFactory factory = new ReaderFactory();
        UserReader reader = (UserReader)factory.getReader("userReader");
        reader.setReadHandler(reader);
        reader.read();
    }
    @Test(expected = ReaderException.class)
    public void noReadHandlerSet() throws ReaderException {
        ReaderFactory factory = new ReaderFactory();
        UserReader reader = (UserReader)factory.getReader("userReader");
        reader.setURI("test");
        reader.read();
    }
    @Test
    public void readFromURI() throws ReaderException{
        ReaderFactory factory = new ReaderFactory();
        UserReader reader = (UserReader)factory.getReader("userReader");
        reader.setURI("http://mockaroo.com/f13b8200/download?count=1&key=e79a3650");
        reader.setReadHandler(this);
        ArrayList<User> users  = (ArrayList<User>) reader.read();
        Assert.assertEquals(users.size(), 50);
    }
}
