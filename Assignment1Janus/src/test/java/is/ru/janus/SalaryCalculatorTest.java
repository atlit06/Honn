package is.ru.janus;

import junit.framework.*;

import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

/**
 * Created by Janus on 9/3/16.
 */
public class SalaryCalculatorTest
extends TestCase
{

    public void testCalculator() {

        // Keep the real System.out
        PrintStream oldOut = System.out;

        // Point our System.out into our byte array
        ByteArrayOutputStream consoleOut = new ByteArrayOutputStream();
        System.setOut(new PrintStream(consoleOut));



        // Create Testing Data
        SalaryCalculator calculator = new SalaryCalculator();


        calculator.calculatePay(35, 450);
        oldOut.print(consoleOut);
        assertEquals(true, consoleOut.toString().contentEquals("Error: Wage is too low!"));
        consoleOut.reset();

        calculator.calculatePay(47, 900);
        oldOut.print(consoleOut);
        assertEquals(true, consoleOut.toString().contentEquals("45450.0"));
        consoleOut.reset();

        calculator.calculatePay(73, 1500);
        oldOut.print(consoleOut);
        assertEquals(true, consoleOut.toString().contentEquals("Error: This employee is over his quota"));


    }
}
