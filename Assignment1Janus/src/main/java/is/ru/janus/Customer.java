package is.ru.janus;

import org.joda.time.LocalDate;
import org.joda.time.LocalDateTime;

/**
 * Created by Janus on 9/4/16.
 */
public class Customer {
    protected int customerId;
    protected String customerName;
    protected LocalDate customerDateOfBirth;
    protected String customerAddress;

    public Customer() {
        customerId = 0;
        customerName = "";
        customerDateOfBirth = new LocalDate(1990, 1, 1);
        customerAddress = "";
    }

    /**
     *
     * @param customerId
     * @param customerName
     * @param customerDateOfBirth
     * @param customerAddress
     */
    public Customer (int customerId, String customerName, LocalDate customerDateOfBirth, String customerAddress) {
        this.customerId = customerId;
        this.customerName = customerName;
        this.customerDateOfBirth = customerDateOfBirth;
        this.customerAddress = customerAddress;
    }


    /**
     *
     * @param newAddress
     */
    public void changeAddress (String newAddress) {
        customerAddress = newAddress;
    }
}
