package is.ru.honn.rutube.service;

import java.util.ArrayList;
import java.util.Observable;
import java.util.Observer;

/**
 * Created by Janus on 10/4/16.
 */

/**
 * A class for subjects that can have multiple observers
 */
public abstract class Subject extends Observable{
    //observers notified on update
    ArrayList<Observer> observers;
    //the updated entry
    protected Object entry;

    public Subject(){
        observers = new ArrayList<Observer>();
    }

    /**
     * A function that registers an observer
     * @param observer the observer to be registered
     */
    public void attach(Observer observer) {
        observers.add(observer);
    }

    /**
     * notifies all observers that the Subject has been updated
     * and sends the updated entry
     */
    public void notifyObservers(){
        for (Observer obs : observers) {
            obs.update(this, entry);
        }
    }

}
