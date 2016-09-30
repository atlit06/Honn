package is.ru.honn.rutube.service;

import is.ru.honn.rutube.domain.*;
<<<<<<< HEAD
import is.ru.honn.rutube.exceptions.ServiceException;

=======
import is.ru.honn.rutube.exceptions.*;
>>>>>>> 4e61b1af6a8ba3176a9917e444bb21fa23a98c31
import java.util.List;

/**
 * Created by Janus on 9/28/16.
 */
public interface UserService
{
    int addUser(User user) throws ServiceException;
    User getUser(int userId);
    List<User> getUsers();
}
