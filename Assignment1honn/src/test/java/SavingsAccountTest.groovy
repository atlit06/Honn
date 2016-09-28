/**
 * Created by steinn on 03/09/16.
 */
class SavingsAccountTest extends GroovyTestCase {
    void testDeposit() {
        Customer testCustomer = new Customer(1, "Steinn", new Date(1991, 5, 19), "Menntavegur 1");
        Account testAccount = new SavingsAccount(1, true, "test", testCustomer, 2000, 2);
        String amountException = shouldFail {
            testAccount.deposit(-1);
        }
        assert amountException.equals("Amount must be more than 0");
        double newBalance = testAccount.deposit(1000);
        assert newBalance == 3000;
    }
    void testWithdraw() {
        Customer testCustomer = new Customer(1, "Steinn", new Date(1991, 5, 19), "Menntavegur 1");
        Account testAccount = new SavingsAccount(1, true, "test", testCustomer, 2000, 2);
        String amountException = shouldFail {
            testAccount.withdraw(-1);
        }
        assert amountException.equals("Amount must be more than 0");

        String withdrawalException = shouldFail {
            testAccount.withdraw(10000);
        }
        assert withdrawalException.equals("The withdrawal amount exceeds the allowed account withdrawal limit");

        Account testAccount2 = new SavingsAccount(1, true, "test", testCustomer, 2000, 2);
        double newBalance = testAccount2.withdraw(100);
        assert newBalance == 1900;
        newBalance = testAccount2.withdraw(100);
        assert newBalance == 1800;

        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setErr(new PrintStream(outContent));
        withdrawalException = shouldFail {
            testAccount2.withdraw(100);
        }
        assert withdrawalException.equals("The withdrawal limit has been reached for this month");
        assertEquals("The withdrawal limit has been reached for this month\n", outContent.toString());
        System.setErr(null);
    }
}
