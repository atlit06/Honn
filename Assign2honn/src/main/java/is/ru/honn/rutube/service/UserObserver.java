package is.ru.honn.rutube.service;

import is.ru.honn.rutube.domain.User;

import java.util.Observable;
import java.util.Observer;

/**
 * Created by Janus on 10/4/16.
 */

/**
 * A class for observing user objects and printing them out
 */
public class UserObserver implements Observer {
    /**
     * Prints out a user
     * @param o the subject that was updated
     * @param arg the user object that was updated
     */
    public void update(Observable o, Object arg) {
        User user = (User)arg;
        System.out.println(user.toString());
    }
}
