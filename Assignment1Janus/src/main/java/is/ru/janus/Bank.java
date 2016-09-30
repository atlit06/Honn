package is.ru.janus;

import java.util.ArrayList;

/**
 * Created by Janus on 9/6/16.
 */
public class Bank {

    private ArrayList<Account> accounts;
    private ArrayList<Customer> customers;

    public Bank() {
        accounts = new ArrayList<Account>();
        customers = new ArrayList<Customer>();
    }

    /**
     *
     * @param accounts
     * @param customers
     */
    public Bank(ArrayList<Account> accounts, ArrayList<Customer> customers) {
        this.accounts = accounts;
        this.customers = customers;
    }

    /**
     *
     * @param customer
     */
    public void add(Customer customer) {
        customers.add(customer);
    }

    public void printAccounts() {

        for (Account a : accounts) {

            ArrayList<String> aList = a.getValues();

            System.out.format("%-25s %s %s %s %s %s \n", aList.get(0), aList.get(1), aList.get(2), aList.get(3), aList.get(4));

        }


    }

    public static void main(String[] args) {
        Bank b = new Bank();
    }
}