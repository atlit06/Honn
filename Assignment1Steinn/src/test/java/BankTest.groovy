import org.junit.After
import org.junit.Before

/**
 * Created by steinn on 01/09/16.
 */
class BankTest extends GroovyTestCase {
    void testAddAccount() {
        Customer testCustomer = new Customer(1, "Steinn", new Date(1991, 5, 19), "Menntavegur 1");
        Bank bank = new Bank();
        assert bank.getAccounts().isEmpty() == true;
        Account testAccount1 = new CheckingAccount(1, true, "test", testCustomer, 20000);
        bank.addAccount(testAccount1);
        assert bank.getAccounts().isEmpty() == false;
        assert bank.getAccounts().contains(testAccount1) == true;
        assert bank.getAccounts().size() == 1;
        Account testAccount2 = new CheckingAccount(2, false, "test2", testCustomer, 10000);
        bank.addAccount(testAccount2);
        assert bank.getAccounts().contains(testAccount1) == true;
        assert bank.getAccounts().contains(testAccount2) == true;
        assert bank.getAccounts().size() == 2;
    }

    void testAddCustomer() {
        Bank bank = new Bank();
        assert bank.getCustomers().isEmpty() == true;
        Customer testCustomer1 = new Customer(1, "Steinn", new Date(1991, 5, 19), "Menntavegur 1");
        bank.addCustomer(testCustomer1);
        assert bank.getCustomers().isEmpty() == false;
        assert bank.getCustomers().contains(testCustomer1) == true;
        assert bank.getCustomers().size() == 1;
        Customer testCustomer2 = new Customer(1, "Steinn2", new Date(1990, 5, 19), "Menntavegur 1");
        bank.addCustomer(testCustomer2);
        assert bank.getCustomers().isEmpty() == false;
        assert bank.getCustomers().contains(testCustomer1) == true;
        assert bank.getCustomers().contains(testCustomer2) == true;
        assert bank.getCustomers().size() == 2;


    }

    void testPrintAccounts() {
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        Bank bank = new Bank();
        Customer testCustomer1 = new Customer(1, "Steinn", new Date(), "Menntavegur 1");
        Customer testCustomer2 = new Customer(2, "Siggi", new Date(), "Menntavegur 1");
        Account testAccount1 = new CheckingAccount(1, true, "test", testCustomer1, 2000, 0, 0, 0);
        bank.addAccount(testAccount1);
        Account testAccount2 = new CheckingAccount(2, false, "test2", testCustomer2, 3000, 0, 0, 0);
        bank.addAccount(testAccount2);
        bank.printAccounts();
        assertEquals("Number          Status          Name            Owner           Balance        \n" +
                "1               active          test            1               2000.00        \n" +
                "2               inactive        test2           2               3000.00        \n", outContent.toString());
        outContent.reset();
        bank = new Bank();
        bank.printAccounts();
        assertEquals("Number          Status          Name            Owner           Balance        \n"
                , outContent.toString());
        System.setOut(null);
    }
    void testPrintActiveAccounts() {
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        Bank bank = new Bank();
        Customer testCustomer1 = new Customer(1, "Steinn", new Date(), "Menntavegur 1");
        Customer testCustomer2 = new Customer(2, "Siggi", new Date(), "Menntavegur 1");
        bank.addCustomer(testCustomer1);
        bank.addCustomer(testCustomer2);
        Account testAccount1 = new CheckingAccount(1, true, "test", testCustomer1, 2000, 0, 0, 0);
        bank.addAccount(testAccount1);
        Account testAccount2 = new CheckingAccount(2, false, "test2", testCustomer2, 3000, 0, 0, 0);
        bank.addAccount(testAccount2);
        Account testAccount3 = new CheckingAccount(3, false, "test2", testCustomer2, 3000, 0, 0, 0);
        bank.addAccount(testAccount3);
        bank.printActiveAccounts(1);
        assertEquals("Printing out all active accounts for Steinn\n" +
                "Number          Name            Balance        \n" +
                "1               test            2000.00        \n", outContent.toString());
        System.setOut(null);
        String accountException = shouldFail {
            bank.printActiveAccounts(5);
        }
        assert accountException.equals("No customer with this ID can be found");
    }
}
