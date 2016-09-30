package is.ru.honn.rutube.reader;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;
/**
 * Created by steinn on 30/09/16.
 */
public class ReaderFactory {
    private ApplicationContext context;
    public ReaderFactory() {
       this.context = new
                ClassPathXmlApplicationContext("/reader.xml");
    }
    public Reader getReader(String type) {
        if (type.equals("videoReader")) {
            return (VideoReader) context.getBean(type);
        } else if (type.equals("userReader")) {
            return (UserReader) context.getBean(type);
        }
    }
}
