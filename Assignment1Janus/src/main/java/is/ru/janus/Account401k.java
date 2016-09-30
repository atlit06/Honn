package is.ru.janus;

import org.joda.time.LocalDate;
import org.joda.time.Years;


/**
 * Created by Janus on 9/4/16.
 */
public class Account401k
extends Account {

    private Customer accountCustomer;

    public Account401k (){
        accountNumber = 0;
        accountStatus = false;
        accountOwner = 0;
        accountName = "";
        accountBalance = 0;
        accountCustomer = new Customer();
    }

    /**
     *
     * @param accountNumber
     * @param accountStatus
     * @param accountOwner
     * @param accountName
     * @param accountBalance
     * @param accountCustomer
     */
    public Account401k(int accountNumber, boolean accountStatus, int accountOwner, String accountName, int accountBalance,
                       Customer accountCustomer ) {
        this.accountNumber = accountNumber;
        this.accountStatus = accountStatus;
        this.accountOwner = accountOwner;
        this.accountName = accountName;
        this.accountBalance = accountBalance;
        this.accountCustomer = accountCustomer;
    }

    /**
     *
     * @param amount
     */
    @Override
    public void withdraw(int amount) {

        LocalDate now = new LocalDate();
        Years age = Years.yearsBetween(accountCustomer.customerDateOfBirth, now);

        if (age.getYears() < 65) {
            System.out.println("Customer is too young");
            System.out.println(age.getYears());
            throw new IllegalAccessError();
        }

        if (accountBalance < amount) {
            throw new IllegalArgumentException("Not enough money!");
        }
        accountBalance -= amount;
    }

    /**
     *
     * @param amount
     */
    @Override
    public void deposit(int amount) {

        long temp = accountBalance + amount;
        if (temp < accountBalance){
            throw new IllegalArgumentException("This amount will exceed this accounts limit;");
        }

        accountBalance += amount;
    }
}
