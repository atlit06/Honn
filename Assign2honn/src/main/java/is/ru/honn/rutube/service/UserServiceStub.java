package is.ru.honn.rutube.service;

import is.ru.honn.rutube.domain.*;
import is.ru.honn.rutube.exceptions.*;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Observer;

/**
 * Created by Janus on 9/28/16.
 */
public class UserServiceStub extends Subject implements UserService {

    public List<User> users;

    public UserServiceStub() {
        users = new ArrayList<User>();
    }

    public UserServiceStub(ArrayList<Observer> obs)
    {

    }

    public int addUser(User user) throws ServiceException {
        for (User u : users) {
            if (u.userId == user.userId) {
                throw new ServiceException("This user already Exists!");
            }
        }
        users.add(user);
        entry = user;
        notifyObservers();
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
