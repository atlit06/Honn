import SalaryCalculator;

/**
 * Created by steinn on 29/08/16.
 */

class SalaryCalculatorTest extends groovy.util.GroovyTestCase {
    void testTotalPay() {
        SalaryCalculator calculator = new SalaryCalculator();
        assert calculator.totalPay(450, 35) == "Base salary must be over 500 isk.";
        assert calculator.totalPay(900, 47) == "45450.0";
        assert calculator.totalPay(1500, 73) == "Hours worked must be under 60";
        return;
    }
}
