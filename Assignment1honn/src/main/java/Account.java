/**
 * Created by steinn on 01/09/16.
 */

/**
 * Represents the general structure of Accounts
 * within a bank
 */
public abstract class Account {
    // member variables
    /**
     * The number of the account (the account ID)
     */
    int accountNumber;
    /**
     * Tells if the account is active or inactive,
     * active if true, inactive if false
     */
    boolean accountStatus;
    String accountName;
    Customer accountOwner;
    double balance;

    /**
     * A method to withdraw money from the account
     * @param amount the amount to be withdrawn
     * @return the account balance after the transaction
     * @throws TransactionException if something went wrong when processing the transaction
     */
    abstract double withdraw(double amount) throws TransactionException;

    /**
     * A method to deposit money to the account
     * @param amount the amount to be deposited
     * @return the account balance after the transaction
     * @throws TransactionException if something went wrong when processing the transaction
     */
    abstract double deposit(double amount) throws TransactionException;
}
