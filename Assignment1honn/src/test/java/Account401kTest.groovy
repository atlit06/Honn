/**
 * Created by steinn on 03/09/16.
 */
class Account401kTest extends GroovyTestCase {
    void testDeposit() {
        Customer testCustomer = new Customer(1, "Steinn", new Date(1931, 5, 19), "Menntavegur 1");
        Account testAccount = new Account401k(1, true, "test", testCustomer, 2000);
        String amountException = shouldFail {
            testAccount.deposit(-1);
        }
        assert amountException.equals("Amount must be more than 0");
        double newBalance = testAccount.deposit(1000);
        assert newBalance == 3000;
    }

    void testWithdraw() {
        Customer testCustomer = new Customer(1, "Steinn", new Date("1931/5/19"), "Menntavegur 1");
        Account testAccount = new Account401k(1, true, "test", testCustomer, 2000);
        String amountException = shouldFail {
            testAccount.withdraw(-1);
        }
        assert amountException.equals("Amount must be more than 0");
        String withdrawalException = shouldFail {
            testAccount.withdraw(2001);
        }
        assert withdrawalException.equals("The withdrawal amount exceeds the allowed account withdrawal limit");
        assert 0 == testAccount.withdraw(2000);
        Customer testCustomer2 = new Customer(1, "Steinn", new Date("1971/5/19"), "Menntavegur 1");
        Account testAccount2 = new Account401k(1, true, "test", testCustomer2, 2000);
        withdrawalException = shouldFail {
            testAccount2.withdraw(200);
        }
        assert withdrawalException.equals("Customer must be older than 65");


    }
}
