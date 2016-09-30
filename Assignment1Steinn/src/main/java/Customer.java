import java.util.Date;

/**
 * Created by steinn on 01/09/16.
 */

/**
 * A Customer component for a bank
 */
public class Customer {
    // private member variables
    private int customerId;
    private String customerName;
    private Date customerDateOfBirth;
    private String customerAddress;

    // constructors
    public Customer (int customerID, String customerName, Date customerDateOfBirth, String customerAddress) {
        this.customerId = customerID;
        this.customerName = customerName;
        this.customerDateOfBirth = customerDateOfBirth;
        this.customerAddress = customerAddress;
    }

    // getters
    public int getCustomerId() { return this.customerId; }
    public Date getCustomerDateOfBirth() {
        return this.customerDateOfBirth;
    }
    public String getCustomerAddress() {
        return this.customerAddress;
    }
    public String getCustomerName() {
        return this.customerName;
    }
}
