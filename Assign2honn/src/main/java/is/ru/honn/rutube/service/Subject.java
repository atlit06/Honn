package is.ru.honn.rutube.service;

import java.util.ArrayList;
import java.util.Observable;
import java.util.Observer;

/**
 * Created by Janus on 10/4/16.
 */
public abstract class Subject extends Observable{

    ArrayList<Observer> observers;
    protected Object entry;

    public Subject(){
        observers = new ArrayList<Observer>();
    }

    public void attach(Observer observer) {
        observers.add(observer);
    }

    public void notifyObservers(){
        for (Observer obs : observers) {
            obs.update(this, entry);
        }
    }

}
