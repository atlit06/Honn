package is.ru.honn.rutube.service;

import is.ru.honn.rutube.domain.User;

import java.util.Observable;
import java.util.Observer;

/**
 * Created by Janus on 10/4/16.
 */
public class UserObserver implements Observer {

    public void update(Observable o, Object arg) {
        User user = (User)arg;
        System.out.println(user.toString());
    }
}
