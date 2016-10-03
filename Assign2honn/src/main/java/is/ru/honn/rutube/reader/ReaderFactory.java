package is.ru.honn.rutube.reader;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

/**
 * Created by steinn on 30/09/16.
 */
public class ReaderFactory {
    private ApplicationContext context;

    /**
     * Gets an instance of the Reader interface
     * @param type type of Reader to be returned
     * @return An instance of a class that implements Reader
     * @throws ReaderException  if the reader class type is not recognized by the system
     */
    public static Reader getReader(String type) throws ReaderException {
        ApplicationContext context = new
                ClassPathXmlApplicationContext("reader.xml");
        if (type.equals("videoReader")) {
            Reader videoReader = (VideoReader) context.getBean(type);
            return videoReader;
        } else if (type.equals("userReader")) {
            Reader userReader = (UserReader) context.getBean(type);
            return userReader;
        } else {
            throw new ReaderException("Reader class not recognized");
        }
    }
}
