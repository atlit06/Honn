package is.ru.janus;


import com.sun.org.apache.xpath.internal.operations.Bool;

import java.util.ArrayList;

/**
 * Created by Janus on 9/3/16.
 */
public abstract class Account {

    protected int accountNumber;
    protected boolean accountStatus;
    protected int accountOwner;
    protected String accountName;
    protected int accountBalance;

    public Account(){
        accountNumber = 0;
        accountStatus = false;
        accountOwner = 0;
        accountName = "";
        accountBalance = 0;

    }


    /**
     *
     * @param accountNumber
     * @param accountStatus
     * @param accountName
     * @param accountOwner
     * @param accountBalance
     */
    public Account(int accountNumber, boolean accountStatus, String accountName, int accountOwner, int accountBalance){
        this.accountNumber = accountNumber;
        this.accountStatus = accountStatus;
        this.accountOwner = accountOwner;
        this.accountName = accountName;
        this.accountBalance = accountBalance;
    }

    public abstract void deposit(int amount) throws Exception;
    public abstract void withdraw(int amount) throws Exception;

    /**
     *
     * @return
     */
    public ArrayList<String> getValues() {
        ArrayList<String> retVal = new ArrayList<String>();
        retVal.add(Integer.toString(accountNumber));
        retVal.add(Boolean.toString(accountStatus));
        retVal.add(Integer.toString(accountOwner));
        retVal.add(accountName);
        retVal.add(Integer.toString(accountBalance));

        return retVal;
    }
}
