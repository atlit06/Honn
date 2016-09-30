package is.ru.janus;

/**
 * Created by Janus on 9/4/16.
 */
public class AccountSavings
extends Account {

    private int accountTransactions;
    private int accountMaxTransactions;



    public AccountSavings (){
        accountNumber = 0;
        accountStatus = false;
        accountOwner = 0;
        accountName = "";
        accountBalance = 0;
        accountTransactions = 0;
        accountMaxTransactions = 0;
    }

    public AccountSavings (int accountNumber, boolean accountStatus, int accountOwner, String accountName, int accountBalance,
                           int transactions, int maxTransactions){
        this.accountNumber = accountNumber;
        this.accountStatus = accountStatus;
        this.accountOwner = accountOwner;
        this.accountName = accountName;
        this.accountBalance = accountBalance;
        this.accountTransactions = transactions;
        this.accountMaxTransactions = maxTransactions;
    }

    /**
     *
     * @param amount
     * @throws IllegalArgumentException
     */
    @Override
    public void withdraw(int amount) throws IllegalArgumentException {

        // If we've hit our transaction limit we print an error
        if(accountMaxTransactions <= accountTransactions) {
            System.out.println("Maximum Transactions Reached - Cannot Perform Action");
            return;
        }
        if(accountBalance < amount) {
            System.out.println("Amount is too high");
            return;
        }

        accountBalance -= amount;
        accountTransactions++;
    }

    /**
     *
     * @param amount
     * @throws IllegalArgumentException
     */
    @Override
    public void deposit(int amount) throws IllegalArgumentException {


        // If we've hit our transaction limit we print an error
        if(accountMaxTransactions <= accountTransactions) {
            System.out.println("Maximum Transactions Reached - Cannot Perform Action");
            return;
        }

        // Overflow protection
        long temp = accountBalance + amount;
        if (temp < accountBalance){
            throw new IllegalArgumentException("This amount will exceed this accounts limit;");
        }

        accountTransactions++;
        accountBalance += amount;
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
        accountMaxTransactions = amount;
    }
}
