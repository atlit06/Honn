/**
 * Created by steinn on 01/09/16.
 */

/**
 * A checking account component for customers of a bank which allows
 * overdraft.
 */
public class CheckingAccount extends Account {
    // private member variables
    /**
     * Overdraft amount of the account
     */
    private double overdraft;
    /**
     * Number of free transactions left
     */
    private int freeTransactions;
    /**
     * Transaction fee when withdrawing from the account.
     */
    private double transactionFee;

    // constructors
    public CheckingAccount(
            int accountNumber,
            boolean accountStatus,
            String accountName,
            Customer accountOwner,
            double balance,
            double overdraft,
            int freeTransactions,
            double transactionFee
    ) {
        this.accountNumber = accountNumber;
        this.accountStatus = accountStatus;
        this.accountOwner = accountOwner;
        this.accountName = accountName;
        this.balance = balance;
        this.overdraft = overdraft;
        this.freeTransactions = freeTransactions;
        this.transactionFee = transactionFee;
    }
    public CheckingAccount(
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
        this.overdraft = 0;
        this.freeTransactions = 5;
        this.transactionFee = 100;
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
    public int getAccountOwnerId() {
        return this.accountOwner.getCustomerId();
    }
    public double getBalance() {
        return this.balance;
    }
    public double getOverdraft() { return this.overdraft; }
    public int getFreeTransactions() {
        return this.freeTransactions;
    }
    public double getTransactionFee() {
        return this.transactionFee;
    }

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
     * @return the balance of the account after the withdrawal
     * @throws TransactionException if the amount to withdraw exceeds the allowed overdraft
     * @throws TransactionException if the account is not active
     */
    public double withdraw(double amount) throws TransactionException {
        if (amount <= 0) {
            throw new IllegalArgumentException("Amount must be more than 0");
        }
        if (!this.accountStatus) {
            throw new TransactionException("This account is not active");
        }
        if (this.freeTransactions == 0) {
            amount += this.transactionFee;
        } else {
            this.freeTransactions -= 1;
        }
        // The withdrawal amount exceeds the overdraft and the transaction
        // can not be completed
        if (this.balance - amount < (-overdraft)) {
            throw new TransactionException("The withdrawal amount exceeds the allowed overdraft of the account.");
        } else {
            this.balance = this.balance - amount;
        }
        return this.balance;
    }
}
