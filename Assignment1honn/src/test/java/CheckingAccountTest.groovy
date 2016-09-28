import org.junit.rules.ExpectedException

/**
 * Created by steinn on 03/09/16.
 */
class CheckingAccountTest extends GroovyTestCase {
    void testDeposit() {
        Customer testCustomer = new Customer(1, "Steinn", new Date(), "Menntavegur 1");
        Account testAccount = new CheckingAccount(1, true, "test", testCustomer, 2000, 0, 0, 0);
        String amountException = shouldFail {
            testAccount.deposit(-1);
        }
        assert amountException.equals("Amount must be more than 0");
        double newBalance = testAccount.deposit(1000);
        assert newBalance == 3000;
    }

    void testWithdraw() {
        Customer testCustomer = new Customer(1, "Steinn", new Date(), "Menntavegur 1");
        Account testAccount = new CheckingAccount(1, true, "test", testCustomer, 2000, 1000, 0, 0);
        String amountException = shouldFail {
            testAccount.withdraw(-1);
        }
        assert amountException.equals("Amount must be more than 0");

        double newBalance = testAccount.withdraw(3000);
        assert newBalance == -1000;

        String withdrawalException = shouldFail {
            testAccount.withdraw(1);
        }
        assert withdrawalException.equals("The withdrawal amount exceeds the allowed overdraft of the account.");

        Account testAccount2 = new CheckingAccount(1, true, "test", testCustomer, 2000, 1000, 2, 100);
        newBalance = testAccount2.withdraw(100);
        assert newBalance == 1900;
        newBalance = testAccount2.withdraw(100);
        assert newBalance == 1800;
        newBalance = testAccount2.withdraw(100);
        assert newBalance == 1600;


    }
}
