package is.ru.janus;

import org.junit.Assert;
import org.junit.Test;

/**
 * Created by Janus on 9/5/16.
 */
public class AccountCheckingTest {

    @Test
    public void testWithdrawLegal() {
        AccountChecking account = new AccountChecking(1, true, 1,"Eydsla", 500, 10, 12, 200, 5000);

        account.withdraw(500);
        account.withdraw(500);
        Assert.assertEquals(account.accountBalance, -500);
    }

    @Test
    public void testWithdrawlWithdrawlFee() {
        AccountChecking account = new AccountChecking(1, true, 1,"Eydsla", 500, 12, 12, 200, 1000);

        account.withdraw(500);
        account.withdraw(500);
        Assert.assertEquals(account.accountBalance, -900);
    }

    @Test(expected = IllegalArgumentException.class)
    public void testWithdrawlWithdrawlFeeOverdraw() {
        AccountChecking account = new AccountChecking(1, true, 1,"Eydsla", 500, 12, 12, 200, 800);

        account.withdraw(500);
        account.withdraw(500);
    }

    @Test(expected = IllegalArgumentException.class)
    public void testDeposit(){
        AccountChecking account = new AccountChecking(1, true, 1,"Eydsla", Integer.MAX_VALUE, 12, 12, 200, 800);

        account.deposit(500);
    }



}
