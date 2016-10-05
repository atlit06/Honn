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

    /**
     * List of users that the service stores
     */
    public List<User> users;

    /**
     * Constructor To Initialize the User List
     */
    public UserServiceStub() {
        users = new ArrayList<User>();
    }

    /**
     * Adds a user
     * @param user
     * @return Userid of the User added
     * @throws ServiceException
     */
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


    /**
     * Gets a list of all users
     * @return Either a empty list or list with users
     */
    public List<User> getUsers(){
        return users;
    }

    /**
     * Get a specific user by userId
     * @param userId
     * @return Null if no user exists or a User object
     */
    public User getUser(int userId) {
        for (User u : users) {
            if (u.userId == userId) {
                return u;
            }
        }
        return null;
    }
}
