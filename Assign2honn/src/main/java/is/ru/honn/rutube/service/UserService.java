package is.ru.honn.rutube.service;

import is.ru.honn.rutube.domain.*;
import is.ru.honn.rutube.exceptions.*;
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
