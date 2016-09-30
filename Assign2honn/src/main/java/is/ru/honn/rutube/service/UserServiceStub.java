package is.ru.honn.rutube.service;

import is.ru.honn.rutube.domain.*;
import is.ru.honn.rutube.exceptions.ServiceException;

import java.util.List;

/**
 * Created by Janus on 9/28/16.
 */
public class UserServiceStub implements UserService {

    List<User> users;

    public int addUser(User user) throws ServiceException {
        users.add(user);
        return user.userId;
    }

    public List<User> getUsers(){
        return users;
    }
}
