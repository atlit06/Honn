package is.ru.janus;


/**
 * Created by Janus on 9/4/16.
 */
public class AccountChecking
extends Account {

    private int accountTransactions;
    private int accountFreeTransactions;
    private int accountTransactionFee;
    private int accountOverdrawLimit;



    public AccountChecking (){
        accountNumber = 0;
        accountStatus = false;
        accountOwner = 0;
        accountName = "";
        accountBalance = 0;
        accountTransactions = 0;
        accountFreeTransactions = 0;
        accountTransactionFee = 0;
        accountOverdrawLimit = 0;
    }

    /**
     *
     * @param accountNumber
     * @param accountStatus
     * @param accountOwner
     * @param accountName
     * @param accountBalance
     * @param transactions
     * @param freeTransactions
     * @param transactionFee
     * @param overdrawLimit
     */
    public AccountChecking(int accountNumber, boolean accountStatus, int accountOwner, String accountName, int accountBalance,
                           int transactions, int freeTransactions, int transactionFee, int overdrawLimit){
        this.accountNumber = accountNumber;
        this.accountStatus = accountStatus;
        this.accountOwner = accountOwner;
        this.accountName = accountName;
        this.accountBalance = accountBalance;
        this.accountTransactions = transactions;
        this.accountFreeTransactions = freeTransactions;
        this.accountTransactionFee = transactionFee;
        this.accountOverdrawLimit = overdrawLimit;
    }

    /**
     *
     * @param amount
     * @throws IllegalArgumentException
     */
    @Override
    public void withdraw(int amount) throws IllegalArgumentException {

        // Create withdrawl fee to be used in the formulas below
        int withdrawlFee = 0;
        if (accountTransactions >= accountFreeTransactions){
            withdrawlFee = accountTransactionFee;
        }

        // Check if we have the balance to withdraw the money
        // Else we throw an Exception
        if(amount + withdrawlFee > accountBalance + accountOverdrawLimit) {
            throw new IllegalArgumentException("Amount is too high");
        }

        accountBalance -= amount+withdrawlFee;
    }

    /**
     *
     * @param amount
     * @throws IllegalArgumentException
     */
    @Override
    public void deposit(int amount) throws IllegalArgumentException {

        long temp = accountBalance + amount;
        if (temp < accountBalance){
            throw new IllegalArgumentException("This amount will exceed this accounts limit;");
        }

        accountBalance += amount;
    }

    /**
     *
     * @param amount
     * @throws IllegalArgumentException
     */
    public void changeOverdrawLimit(int amount) throws IllegalArgumentException {
        accountOverdrawLimit -= amount;
        if (accountOverdrawLimit < 0) {
            accountOverdrawLimit += amount;
            throw new IllegalArgumentException("Overdraw must be 0 or higher");
        }
    }

    /**
     *
     * @param amount
     * @throws IllegalArgumentException
     */
    public void changeTransactionLimit(int amount) throws IllegalArgumentException {
        if (amount < 0) {
            throw new IllegalArgumentException("Free Transactions Must Be Zero or Higher");
        }
        accountFreeTransactions = amount;
    }


    /**
     *
     * @param amount
     * @throws IllegalArgumentException
     */
    public void changeTransactionFee(int amount) throws IllegalArgumentException {
        if (amount < 0) {
            throw new IllegalArgumentException("Transaction Fee Must Be Zero or Higher");
        }
        accountTransactionFee = amount;
    }

}
