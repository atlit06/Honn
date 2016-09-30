/**
 * Created by steinn on 05/09/16.
 */

/**
 * A component for storing relevant information of a Catalog
 */
public class CatalogContent {
    // private member variables
    private String title;
    private String type;
    private String author;

    // constructors
    public CatalogContent(String title, String type, String author) {
        this.title = title;
        this.type = type;
        this.author = author;
    }

    // getters
    public String getTitle() { return this.title; }
    public String getType() { return this.type; }
    public String getAuthor() { return this.author; }
}
