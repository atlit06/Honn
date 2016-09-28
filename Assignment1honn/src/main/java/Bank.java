import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;

import java.io.BufferedWriter;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.Date;

/**
 * Created by steinn on 01/09/16.
 */

/**
 * A component representing a bank with accounts and customers
 * @author Steinn Ellidi Petursson
 */
public class Bank {
    // private member variables
    private ArrayList<Account> accounts;
    private ArrayList<Customer> customers;

    // constructors
    public Bank() {
        accounts = new ArrayList<Account>();
        customers = new ArrayList<Customer>();
    }
    public Bank(String path) {
        accounts = new ArrayList<Account>();
        customers = new ArrayList<Customer>();
        populateBankFromFile(path);
    }

    /**
     * Adds an account to the bank
     * @param account the account to be added
     */
    public void addAccount(Account account) {
        accounts.add(account);
    }

    /**
     * Adds a customer to the bank
     * @param customer the customer to be added
     */
    public void addCustomer(Customer customer) {
        customers.add(customer);
    }

    /**
     * @return An ArrayList af all the banks accounts
     */
    public ArrayList<Account> getAccounts() {
        return accounts;
    }

    /**
     * @return An ArrayList of all the banks customers
     */
    public ArrayList<Customer> getCustomers() {
        return customers;
    }

    /**
     * Gets a customer from the bank by ID
     * @param ID ID of the customer
     * @return A customer with the corresponding ID or null if no customer has the ID
     */
    public Customer getCustomerByID(int ID) {
        for (int i = 0; i < this.customers.size(); i++) {
            if (this.customers.get(i).getCustomerId() == ID) {
                return this.customers.get(i);
            }
        }
        return null;
    }
    /**
     * prints out all accounts within the bank
     */
    public void printAccounts() {
        System.out.format("%-15s %-15s %-15s %-15s %-15s%n", "Number", "Status", "Name", "Owner", "Balance");
        for (Account account: accounts) {
            String accountStatus = account.accountStatus? "active" : "inactive";
            System.out.format(
                    "%-15d %-15s %-15s %-15d %-15.2f%n",
                    account.accountNumber,
                    accountStatus,
                    account.accountName,
                    account.accountOwner.getCustomerId(),
                    account.balance
            );
        }
    }

    /**
     * Prints all active accounts of a customer
     * @param accountOwnerId the Id of the owner of the accounts
     */
    public void printActiveAccounts(int accountOwnerId) throws AccountException {
        Customer owner = null;
        for (Customer customer: this.customers) {
            if (customer.getCustomerId() == accountOwnerId) {
                owner = customer;
            }
        }
        if (owner == null) {
            throw new AccountException("No customer with this ID can be found");
        }
        System.out.format("Printing out all active accounts for %s%n", owner.getCustomerName());
        System.out.format("%-15s %-15s %-15s%n", "Number", "Name", "Balance");
        for (Account account: accounts) {
            if (account.accountStatus == true && account.accountOwner.getCustomerId() == accountOwnerId) {
                System.out.format(
                        "%-15d %-15s %-15.2f%n",
                        account.accountNumber,
                        account.accountName,
                        account.balance
                );
            }
        }

    }

    /**
     * A function which populates the bank accounts list and customers
     */
    public void generateData() {
        Customer testCustomer1 = new Customer(1, "Steinn", new Date("1931/5/19"), "Menntavegur 1");
        this.addCustomer(testCustomer1);
        Customer testCustomer2 = new Customer(2, "Siggi", new Date("1991/7/21"), "Grandi 2");
        this.addCustomer(testCustomer2);
        Account checkingAccount1 = new CheckingAccount(1, true, "checkingAccount1", testCustomer1, 2000, 0, 0, 0);
        this.addAccount(checkingAccount1);
        Account checkingAccount2 = new CheckingAccount(2, false, "checkingAccount2", testCustomer2, 3000, 0, 0, 0);
        this.addAccount(checkingAccount2);
        Account account401k1 = new Account401k(1, true, "401kAccount1", testCustomer1, 2000);
        this.addAccount(account401k1);
        Account account401k2 = new Account401k(1, true, "401kAccount2", testCustomer2, 2000);
        this.addAccount(account401k2);
        Account savingsAccount1 = new SavingsAccount(1, true, "savingsAccount1", testCustomer1, 2000, 2);
        this.addAccount(savingsAccount1);
        Account savingsAccount2 = new SavingsAccount(1, true, "savingsAccount2", testCustomer2, 2000, 2);
        this.addAccount(savingsAccount2);
    };

    /**
     * A method to convert all account objects to json objects
     * @return a JSONArray containing all accounts formatted as JSONObject
     */
    private JSONArray convertAccountsToJSON() {
        ArrayList<Account> accounts = this.accounts;
        JSONArray accountsJSON = new JSONArray();
        for (Account account: accounts) {
            JSONObject accountJSON = new JSONObject();
            Class className = account.getClass();
            if (className.getName().equals("CheckingAccount")) {
                CheckingAccount checkingAccount = (CheckingAccount) account;
                accountJSON.put("objectType", "CheckingAccount");
                accountJSON.put("accountName", checkingAccount.getAccountName());
                accountJSON.put("accountNumber", checkingAccount.getAccountNumber());
                accountJSON.put("accountStatus", checkingAccount.getAccountStatus());
                accountJSON.put("accountOwnerID", checkingAccount.getAccountOwnerId());
                accountJSON.put("balance", checkingAccount.getBalance());
                accountJSON.put("overdraft", checkingAccount.getOverdraft());
                accountJSON.put("freeTransactions", checkingAccount.getFreeTransactions());
                accountJSON.put("transactionFee", checkingAccount.getTransactionFee());
                accountsJSON.add(accountJSON);
            } else if (className.getName().equals("SavingsAccount")) {
                SavingsAccount savingsAccount = (SavingsAccount) account;
                accountJSON.put("objectType", "SavingsAccount");
                accountJSON.put("accountName", savingsAccount.getAccountName());
                accountJSON.put("accountNumber", savingsAccount.getAccountNumber());
                accountJSON.put("accountStatus", savingsAccount.getAccountStatus());
                accountJSON.put("accountOwnerID", savingsAccount.getAccountOwnerId());
                accountJSON.put("balance", savingsAccount.getBalance());
                accountJSON.put("monthlyTransactions", savingsAccount.getMonthlyTransactions());
                accountJSON.put("transactionsLeft", savingsAccount.getTransactionsLeft());
                accountJSON.put("currentTransactionMonth", savingsAccount.getCurrentTransactionMonth());
                accountsJSON.add(accountJSON);
            } else if (className.getName().equals("Account401k")) {
                Account401k account401k = (Account401k) account;
                accountJSON.put("objectType", "Account401k");
                accountJSON.put("accountName", account401k.getAccountName());
                accountJSON.put("accountNumber", account401k.getAccountNumber());
                accountJSON.put("accountStatus", account401k.getAccountStatus());
                accountJSON.put("accountOwnerID", account401k.getAccountOwnerId());
                accountJSON.put("balance", account401k.getBalance());
                accountsJSON.add(accountJSON);
            }
        }
        return accountsJSON;
    }

    /**
     * A method to convert all customer objects to json objects
     * @return a JSONArray containing all customers formatted as JSONObject
     */
    private JSONArray convertCustomersToJSON() {
        JSONArray customersJSON = new JSONArray();
        ArrayList<Customer> customers = this.customers;
        for (Customer customer: customers) {
            JSONObject customerJSON = new JSONObject();
            customerJSON.put("objectType", "Customer");
            customerJSON.put("customerID", customer.getCustomerId());
            customerJSON.put("customerName", customer.getCustomerName());
            customerJSON.put("customerDateOfBirth", customer.getCustomerDateOfBirth().toString());
            customerJSON.put("customerAddress", customer.getCustomerAddress());
            customersJSON.add(customerJSON);
        }
        return customersJSON;
    }

    /**
     * A method to convert all objects in bank to json string
     * @return a String containing JSON formatted objects
     */
    private String convertDataToJSONString() {
        JSONArray objects = convertAccountsToJSON();
        JSONArray customers = convertCustomersToJSON();
        for (int i = 0; i < customers.size(); i++) {
            objects.add(customers.get(i));
        }
        return objects.toString();
    }

    /**
     * Writes all account objects and customer objects of bank
     * into a file in JSON format
     * @param filePath path to the which should be written
     */
    private void writeDataToFile(String filePath) {
        try {
            Path path = Paths.get(filePath);
            BufferedWriter writer = Files.newBufferedWriter(path);
            writer.write("{\"objects\" : ");
            writer.write(this.convertDataToJSONString());
            writer.write("}\n");
            writer.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    /**
     * Reads contents of a file
     * @param path the path to the file
     * @return the contents of the file represented in a string
     * @throws RequestException if the file can not be found
     */
    private String getFileContent (String path) throws RequestException {
        try {
            byte[] contents = Files.readAllBytes(Paths.get(path));
            return new String(contents);
        } catch (IOException e) {
            throw new RequestException(e);
        }
    }

    /**
     * Reads all bank objects from a file and converts into a JSON array
     * @param path the path to the file
     * @return A JSON array containing all Customers and Accounts declared in the file
     */
    private JSONArray readBankObjectsFromFile(String path) {
        try {
            String contents = getFileContent(path);
            JSONObject values =  (JSONObject) JSONValue.parse(contents);
            JSONArray returnValues = (JSONArray) values.get("objects");
            return returnValues;
        } catch (RequestException e) {
            e.printStackTrace();
        }
        return new JSONArray();
    }

    /**
     * Reads all customer objects from a JSON array
     * @param customersJSON JSON array containing customer objects
     */
    private void addCustomersFromJSONArray(JSONArray customersJSON) {
        for (int i = 0; i < customersJSON.size(); i++) {
            JSONObject customerJSON = (JSONObject)customersJSON.get(i);
            if (((String)customerJSON.get("objectType")).equals("Customer") == true) {
                int customerID = ((Long)customerJSON.get("customerID")).intValue();
                Customer customer = new Customer(
                        customerID,
                        (String) customerJSON.get("customerName"),
                        (Date) new Date((String) customerJSON.get("customerDateOfBirth")),
                        (String) customerJSON.get("customerAddress")
                );
                this.customers.add(customer);
            }
        }
    }

    /**
     * Reads all Account objects from a JSON array
     * @param accountsJSON JSON array containing account objects
     */
    private void addAccountsFromJSONArray(JSONArray accountsJSON) {
        for (int i = 0; i < accountsJSON.size(); i++) {
            JSONObject accountJSON = (JSONObject) accountsJSON.get(i);
            if (((String)accountJSON.get("objectType")).equals("CheckingAccount") == true) {
                int accountNumber = ((Long) accountJSON.get("accountNumber")).intValue();
                boolean accountStatus = (Boolean) accountJSON.get("accountStatus");
                String accountName = (String) accountJSON.get("accountName");
                int accountOwnerID = ((Long) accountJSON.get("accountOwnerID")).intValue();
                Customer accountOwner = this.getCustomerByID(accountOwnerID);
                double balance = (Double) accountJSON.get("balance");
                double overdraft = (Double) accountJSON.get("overdraft");
                int freeTransactions = ((Long) accountJSON.get("freeTransactions")).intValue();
                double transactionFee = (Double) accountJSON.get("balance");
                this.accounts.add(
                        new CheckingAccount(
                                accountNumber,
                                accountStatus,
                                accountName,
                                accountOwner,
                                balance,
                                overdraft,
                                freeTransactions,
                                transactionFee
                        )
                );
            } else if (((String)accountJSON.get("objectType")).equals("SavingsAccount") == true) {
                int accountNumber = ((Long) accountJSON.get("accountNumber")).intValue();
                boolean accountStatus = (Boolean) accountJSON.get("accountStatus");
                String accountName = (String) accountJSON.get("accountName");
                int accountOwnerID = ((Long) accountJSON.get("accountOwnerID")).intValue();
                Customer accountOwner = this.getCustomerByID(accountOwnerID);
                double balance = (Double) accountJSON.get("balance");
                int monthlyTransactions = ((Long) accountJSON.get("monthlyTransactions")).intValue();
                this.accounts.add(
                        new SavingsAccount(
                                accountNumber,
                                accountStatus,
                                accountName,
                                accountOwner,
                                balance,
                                monthlyTransactions
                        )
                );
            } else if (((String)accountJSON.get("objectType")).equals("Account401k") == true) {
                int accountNumber = ((Long) accountJSON.get("accountNumber")).intValue();
                boolean accountStatus = (Boolean) accountJSON.get("accountStatus");
                String accountName = (String) accountJSON.get("accountName");
                int accountOwnerID = ((Long) accountJSON.get("accountOwnerID")).intValue();
                Customer accountOwner = this.getCustomerByID(accountOwnerID);
                double balance = (Double) accountJSON.get("balance");
                this.accounts.add(
                        new Account401k(
                                accountNumber,
                                accountStatus,
                                accountName,
                                accountOwner,
                                balance
                        )
                );
            }

        }
    }

    /**
     * Populates the bank with Account and Customer objects found in a file
     * @param path path to the file
     */
    private void populateBankFromFile(String path) {
        JSONArray objects = this.readBankObjectsFromFile(path);
        this.addCustomersFromJSONArray(objects);
        this.addAccountsFromJSONArray(objects);
    }

    public static void main(String[] args) {
        Bank bank = new Bank();
        if (args.length > 0) {
            System.out.println(args[0]);
            bank = new Bank(args[0]);
        }
        // Uncomment the following code to populate bank with dummy data
        /*
        bank.generateData();
        */
        // Uncomment the following code to write account and customer objects to file
        /*
        bank.writeDataToFile("PATH_TO_FILE");
        */
        // Uncomment the following code to print out all accounts
        /*
        bank.printAccounts();
        */
        // Uncomment the following code to print out all active accounts of customer with ID 2
        /*
        try {
            bank.printActiveAccounts(2);
        } catch (AccountException e) {
            System.out.println(e);
        }
        */
    }
}
