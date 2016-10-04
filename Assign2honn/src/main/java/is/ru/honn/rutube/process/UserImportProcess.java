package is.ru.honn.rutube.process;

import is.ru.honn.rutube.domain.User;
import is.ru.honn.rutube.exceptions.ServiceException;
import is.ru.honn.rutube.process.*;
import is.ru.honn.rutube.reader.*;
import is.ru.honn.rutube.service.*;

import is.ruframework.process.RuAbstractProcess;
import is.ruframework.process.RuProcessContext;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.FileSystemXmlApplicationContext;

import java.util.List;

/**
 * Created by Janus on 10/3/16.
 */
public class UserImportProcess extends RuAbstractProcess implements ReadHandler {
    /*
    ----RuAbstract Process----
    private RuProcessContext processContext;
    private String contextFile;
    private String[] parameters;
    Logger log = Logger.getLogger(this.getClass().getName());

    ----RUProcessContext----
    private String processName;
    private String processClass;
    private String importFile;
    private String importURL;
    private String dataSourceFile;
    private Map params;
     */

    private UserService userService;
    private UserReader reader;



    // NEEED TO FIX EXCEPTION CATCHING
    public void startProcess() {
        try {
            reader.setURI(getProcessContext().getImportURL());
            reader.setReadHandler(this);
            reader.read();
        }catch (Exception e) {
            System.out.print("Some shit went wrong");
        }
    }

    public void beforeProcess() {
        ApplicationContext context = new FileSystemXmlApplicationContext("classpath:app.xml");
        userService = (UserService)context.getBean("userService");
        reader = (UserReader)context.getBean("userReader");







    }

    public void afterProcess() {
        List<User> users = userService.getUsers();
        for (User u: users) {
            System.out.println(u.displayName);
        }
    }


    public void read(int count, Object object) {
        try {
            userService.addUser((User)object);
        } catch (ServiceException e) {
            e.printStackTrace();
        }
    }
}
