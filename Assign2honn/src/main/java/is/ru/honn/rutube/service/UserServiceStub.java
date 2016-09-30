package is.ru.honn.rutube.service;

import is.ru.honn.rutube.domain.*;
<<<<<<< HEAD
import is.ru.honn.rutube.exceptions.ServiceException;

=======
import is.ru.honn.rutube.exceptions.*;

import java.util.ArrayList;
>>>>>>> 4e61b1af6a8ba3176a9917e444bb21fa23a98c31
import java.util.List;

/**
 * Created by Janus on 9/28/16.
 */
public class UserServiceStub implements UserService {

    public List<User> users;

    public UserServiceStub() {
        users = new ArrayList<User>();
    }

    public int addUser(User user) throws ServiceException {
<<<<<<< HEAD
=======
        for (User u : users) {
            if (u.userId == user.userId) {
                throw new ServiceException("This user already Exists!");
            }
        }
>>>>>>> 4e61b1af6a8ba3176a9917e444bb21fa23a98c31
        users.add(user);
        return user.userId;
    }

    public List<User> getUsers(){
        return users;
    }

    public User getUser(int userId) {
        for (User u : users) {
            if (u.userId == userId) {
                return u;
            }
        }
        return null;
    }
}
