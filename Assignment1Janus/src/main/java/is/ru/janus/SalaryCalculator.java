package is.ru.janus;

/**
 * Created by Janus on 9/3/16.
 */
public class SalaryCalculator {

    public SalaryCalculator(){

    }

    /**
     *
     * @param Hours
     * @param Wage
     */
    public void calculatePay(int Hours, int Wage){

        if (Wage < 500){
            System.out.print("Error: Wage is too low!");
            return;
        }

        if (Hours > 60){
            System.out.print("Error: This employee is over his quota");
            return;
        }

        if (Hours > 40){
            System.out.print((40*Wage)+((Hours-40)*Wage*1.5));
        } else {
            System.out.print(Hours*Wage);
        }
    }
}
