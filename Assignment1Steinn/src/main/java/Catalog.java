import java.util.ArrayList;

/**
 * Created by steinn on 05/09/16.
 */

/**
 * Catalog component to store video information
 */
public class Catalog {
    // private member variables
    private String catalog;
    private ArrayList<CatalogContent> content;

    // constructors
    public Catalog(String catalog, ArrayList<CatalogContent> content) {
        this.catalog = catalog;
        this.content = content;
    }

    /**
     * Function that pretty prints the contents of the Catalog object
     */
    public void printCatalog() {
        System.out.format("Catalog: %s%n%n", this.catalog);
        System.out.format("%-25s %-25s %-25s\n", "Title", "Type", "Author");
        System.out.println("-------------------------------------------------------------------------------------");
        for(CatalogContent content: this.content) {
            System.out.format("%-25s %-25s %-25s\n", content.getTitle(), content.getType(), content.getAuthor());
        }
    }
}
