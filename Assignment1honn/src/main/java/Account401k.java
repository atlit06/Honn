/**
 * Created by steinn on 01/09/16.
 */

import sun.java2d.loops.TransformBlit;

import java.util.Calendar;
import java.util.Date;

/**
 * A 401k account component which can only be withdrawn from if the customer is at least 66 years old
 */
public class Account401k extends Account {
    // constructors
    public Account401k(
            int accountNumber,
            boolean accountStatus,
            String accountName,
            Customer accountOwner,
            double balance
    ) {
        this.accountNumber = accountNumber;
        this.accountStatus = accountStatus;
        this.accountOwner = accountOwner;
        this.accountName = accountName;
        this.balance = balance;
    }
    // getters
    public int getAccountNumber() {
        return this.accountNumber;
    }
    public boolean getAccountStatus() {
        return this.accountStatus;
    }
    public String getAccountName() {
        return this.accountName;
    }
    public double getBalance() { return this.balance; }
    public int getAccountOwnerId() {
        return this.accountOwner.getCustomerId();
    }

    /**
     * deposits into the account
     * @param amount the amount to be deposited
     * @return the balance of the account after the transaction
     * @throws TransactionException if the account is not active
     */
    public double deposit(double amount) throws TransactionException {
        if (amount <= 0) {
            throw new IllegalArgumentException("Amount must be more than 0");
        }
        if (!this.accountStatus) {
            throw new TransactionException("This account is not active");
        }
        this.balance += amount;
        return this.balance;
    }

    /**
     * withdraws a given amount from the account
     * @param amount the amount to be withdrawn
     * @return the balance of the account after the transaction
     * @throws TransactionException if the account is not active
     */
    public double withdraw(double amount) throws TransactionException {
        if (amount <= 0) {
            throw new IllegalArgumentException("Amount must be more than 0");
        }
        if (!this.accountStatus) {
            throw new TransactionException("This account is not active");
        }

        // setting up variables for age calculations
        Calendar birthDate = Calendar.getInstance();
        birthDate.setTime(this.accountOwner.getCustomerDateOfBirth());
        int yearOfBirth = birthDate.get(Calendar.YEAR);
        int monthOfBirth = birthDate.get(Calendar.MONTH);
        int dayOfBirth = birthDate.get(Calendar.DAY_OF_MONTH);

        Date now = new Date();
        Calendar today = Calendar.getInstance();
        today.setTime(now);

        // calculating age
        int ageOfAccountOwner = today.get(Calendar.YEAR) - yearOfBirth;
        if (monthOfBirth > today.get(Calendar.MONTH)) {
            ageOfAccountOwner -= 1;
        }
        if (monthOfBirth == today.get(Calendar.MONTH) && dayOfBirth > today.get(Calendar.DAY_OF_MONTH)) {
            ageOfAccountOwner -= 1;
        }

        if (ageOfAccountOwner < 66) {
            throw new TransactionException("Customer must be older than 65");
        }

        if (this.balance - amount < 0) {
            throw new TransactionException("The withdrawal amount exceeds the allowed account withdrawal limit");
        } else {
            this.balance -= amount;
            return this.balance;
        }
    }
}
