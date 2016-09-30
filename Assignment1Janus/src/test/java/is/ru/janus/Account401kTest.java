package is.ru.janus;


import org.joda.time.LocalDate;
import org.junit.Assert;
import org.junit.Test;

import java.io.ByteArrayOutputStream;
import java.io.InvalidClassException;
import java.io.PrintStream;

/**
 * Created by Janus on 9/5/16.
 */
public class Account401kTest{

    @Test(expected = IllegalAccessError.class)
    public void testWithdrawAgeException() {
        Account401k account = new Account401k(1, true, 1, "Janus", 5000, new Customer(1,"Janus",new LocalDate(1994,8,30),"Dofraberg 21"));
        account.withdraw(55000);
    }

    @Test(expected = IllegalArgumentException.class)
    public void testWithdrawAmountException() {
        Account401k account = new Account401k(1, true, 1, "Janus", 5000, new Customer(1,"Janus",new LocalDate(1924,8,30),"Dofraberg 21"));
        account.withdraw(55000);
    }

    @Test
    public void testWithdrawLegal() {
        Account401k account = new Account401k(1, true, 1, "Janus", 5000, new Customer(1,"Janus",new LocalDate(1924,8,30),"Dofraberg 21"));
        account.withdraw(500);
        Assert.assertEquals(account.accountBalance, 4500L);
    }

    @Test
    public void testDepositLegal() {
        Account401k account = new Account401k(1, true, 1, "Janus", 5000, new Customer(1,"Janus",new LocalDate(1924,8,30),"Dofraberg 21"));
        account.deposit(500);
        Assert.assertEquals(account.accountBalance, 5500L);
    }

    @Test(expected = IllegalArgumentException.class)
    public void testDepositOverflow() {
        Account401k account = new Account401k(1, true, 1, "Janus", Integer.MAX_VALUE-50, new Customer(1,"Janus",new LocalDate(1924,8,30),"Dofraberg 21"));
        account.deposit(5000);
    }


}
