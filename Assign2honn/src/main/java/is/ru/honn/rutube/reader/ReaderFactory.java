package is.ru.honn.rutube.reader;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import java.io.File;

/**
 * Created by steinn on 30/09/16.
 */
public class ReaderFactory {
    private ApplicationContext context;
    public ReaderFactory() {
        File f = new File("/reader.xml");
        System.out.println("Exist test: " + f.exists());
       this.context = new
                ClassPathXmlApplicationContext("/reader.xml");
    }
    public Reader getReader(String type) throws ReaderException {
        if (type.equals("videoReader")) {
            return (VideoReader) context.getBean(type);
        } else if (type.equals("userReader")) {
            return (UserReader) context.getBean(type);
        } else {
            throw new ReaderException("Context not defined");
        }
    }
}
