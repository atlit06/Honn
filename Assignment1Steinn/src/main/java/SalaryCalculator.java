/**
 * Created by steinn on 29/08/16.
 */

/**
 * A component which calculates salary
 * @author Steinn Ellidi Petursson
 */
public class SalaryCalculator {
    /**
     * A method which calculates the monthly salary of an employee
     * @param salary hourly salary of an employee
     * @param hoursWorked number of hours an employee has worked in a month
     * @return A string representation of the total salary of an employee in one month
     */
    public static String totalPay (int salary, int hoursWorked) {
        if (salary < 500) {
            return "Base salary must be over 500 isk.";
        }
        if (hoursWorked > 60) {
            return "Hours worked must be under 60";
        }
        if (hoursWorked < 0) {
            return "Hours worked must be over 0";
        }
        if (hoursWorked <= 40) {
            return Double.toString(hoursWorked * salary);
        } else {
            double regularSalary = salary * 40;
            int bonusHours = hoursWorked - 40;
            double bonusSalary = salary * bonusHours * 1.5;
            double totalSalary = regularSalary + bonusSalary;
            return Double.toString(totalSalary);
        }
    }
}
