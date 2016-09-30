/**
 * Created by steinn on 01/09/16.
 */

import java.util.Calendar;
import java.util.Date;

/**
 * A savings account component for customers of a bank
 */
public class SavingsAccount extends Account {
    // private member variables
    // number of monthly transactions left
    private int monthlyTransactions;
    // The current transactions month
    private int currentTransactionMonth;
    // Transactions left in the current month
    private int transactionsLeft;

    // constructors
    public SavingsAccount(
            int accountNumber,
            boolean accountStatus,
            String accountName,
            Customer accountOwner,
            double balance,
            int monthlyTransactions
    ) {
        this.accountNumber = accountNumber;
        this.accountStatus = accountStatus;
        this.accountOwner = accountOwner;
        this.accountName = accountName;
        this.balance = balance;
        this.monthlyTransactions = monthlyTransactions;
        this.transactionsLeft = monthlyTransactions;
        Date currentDate = new Date();
        Calendar cal = Calendar.getInstance();
        cal.setTime(currentDate);
        int currentTransactionMonth = cal.get(Calendar.MONTH);
        this.currentTransactionMonth = currentTransactionMonth;
    }

    // getters
    public int getAccountNumber() { return this.accountNumber; }
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
    public int getMonthlyTransactions() { return this.monthlyTransactions; }
    public int getTransactionsLeft() { return this.transactionsLeft; }
    public int getCurrentTransactionMonth() { return this.currentTransactionMonth; }

    /**
     * Deposits a given amount to the account
     * @param amount the amount to be deposited
     * @return the balance of the account after the deposit
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
     * Withdraws a given amount from the account
     * @param amount the amount to be withdrawn
     * @return the balance of the account after withdrawal
     * @throws TransactionException if the withdrawal amount exceeds the balance, or the withdrawal limit has been reached
     * @throws TransactionException if the account is not active
     */
    public double withdraw(double amount) throws TransactionException {
        if (amount <= 0) {
            throw new IllegalArgumentException("Amount must be more than 0");
        }
        if (!this.accountStatus) {
            throw new TransactionException("This account is not active");
        }
        if (!this.accountStatus) {
            throw new TransactionException("This account is not active");
        }

        if (this.balance - amount < 0) {
            throw new TransactionException("The withdrawal amount exceeds the allowed account withdrawal limit");
        }

        // reset transactionsLeft if needed
        Calendar cal = Calendar.getInstance();
        cal.setTime(new Date());
        if (cal.get(Calendar.MONTH) != this.currentTransactionMonth) {
            this.transactionsLeft = this.monthlyTransactions;
            this.currentTransactionMonth = cal.get(Calendar.MONTH);
        }

        if (this.transactionsLeft > 0) {
            this.transactionsLeft -= 1;
            this.balance -= amount;
            return this.balance;
        } else {
            System.err.println("The withdrawal limit has been reached for this month");
            throw new TransactionException("The withdrawal limit has been reached for this month");
        }
    }
}
