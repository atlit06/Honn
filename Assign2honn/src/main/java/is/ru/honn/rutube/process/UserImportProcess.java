package is.ru.honn.rutube.process;

import is.ru.honn.rutube.domain.User;
import is.ru.honn.rutube.exceptions.ServiceException;
import is.ru.honn.rutube.process.*;
import is.ru.honn.rutube.reader.*;
import is.ru.honn.rutube.service.*;

import is.ruframework.process.RuAbstractProcess;
import is.ruframework.process.RuProcessContext;
import org.springframework.context.ApplicationContext;
import org.springframework.context.MessageSource;
import org.springframework.context.support.FileSystemXmlApplicationContext;

import java.util.List;
import java.util.Locale;

/**
 * Created by Janus on 10/3/16.
 */
public class UserImportProcess extends RuAbstractProcess implements ReadHandler {

    private UserServiceStub userService;
    private UserReader reader;
    private MessageSource msg;


    public void startProcess() {

        String startProcessMessageEn = msg.getMessage("process.start",
                new Object[] {getProcessContext().getProcessName()}, Locale.ENGLISH);
        System.out.println(startProcessMessageEn);
        String startProcessMessageIs = msg.getMessage("process.start",
                new Object[] {getProcessContext().getProcessName()}, new Locale("is"));
        System.out.println(startProcessMessageIs);


        try {
            reader.setURI(getProcessContext().getImportURL());
            reader.setReadHandler(this);
            reader.read();
        }catch (Exception e) {
            System.out.print("Unable to run function Start Process in UserImport Process");
        }



    }

    public void beforeProcess() {


        ApplicationContext context = new FileSystemXmlApplicationContext("classpath:app.xml");
        msg = (MessageSource)context.getBean("messageSource");

        String beforeProcessMessageEn = msg.getMessage("process.before",
                new Object[] {getProcessContext().getProcessName()}, Locale.ENGLISH);
        System.out.println(beforeProcessMessageEn);
        String beforeProcessMessageIs = msg.getMessage("process.before",
                new Object[] {getProcessContext().getProcessName()}, new Locale("is"));
        System.out.println(beforeProcessMessageIs);



        userService = (UserServiceStub)context.getBean("userService");
        UserObserver obs = new UserObserver();
        userService.attach(obs);



        reader = (UserReader)context.getBean("userReader");






    }

    public void afterProcess() {

        String afterProcessMessageEn = msg.getMessage("process.after",
                new Object[] {getProcessContext().getProcessName()}, Locale.ENGLISH);
        System.out.println(afterProcessMessageEn);

        String afterProcessMessageIs = msg.getMessage("process.after",
                new Object[] {getProcessContext().getProcessName()}, new Locale("is"));
        System.out.println(afterProcessMessageIs);


    }


    public void read(int count, Object object) {
        try {
            userService.addUser((User)object);
        } catch (ServiceException e) {
            System.out.print("Unable to add user inside read method in UserImportProcess\nError Returned: " + e.getMessage());
        }
    }
}
