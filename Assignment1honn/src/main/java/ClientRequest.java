import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;

import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.core.Response;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;

/**
 * Created by steinn on 03/09/16.
 */

/**
 * A component that parses request from files and urls and either prints them or
 * creates objects from them.
 */
public class ClientRequest {
    /**
     * Reads contents of a file
     * @param path the path to the file
     * @return the contents of the file represented in a string
     * @throws RequestException if the file can not be found
     */
    public String getFileContent (String path) throws RequestException {
        try {
            byte[] contents = Files.readAllBytes(Paths.get(path));
            return new String(contents);
        } catch (IOException e) {
            throw new RequestException(e);
        }
    }

    /**
     * Sends a request to a server and parses the content of the response to a string
     * @param url the url of the request
     * @return a String value of the request response
     */
    public String getRequest(String url) {
        Client client = ClientBuilder.newClient();
        Response response = client.target(url).request().get();
        String json = response.readEntity(String.class);
        return json;
    }

    /**
     * Parses a string of json objects and prints them out to stdout
     * @param s the string to be parsed
     */
    public void parseAndPrint (String s) {
        JSONObject jsonObject = (JSONObject) JSONValue.parse(s);
        System.out.println(jsonObject.get("status"));
        System.out.println(jsonObject.get("recordCount"));
        System.out.println(jsonObject.get("catalog_name"));
        JSONArray catalog = (JSONArray) jsonObject.get("catalog");
        for (int i = 0; i < catalog.size(); i++) {
            JSONObject obj = (JSONObject) catalog.get(i);
            System.out.print(obj.get("title"));
            System.out.print(", ");
            System.out.print(obj.get("type"));
            System.out.print(", ");
            System.out.println(obj.get("author_name"));
        }
        return;
    }

    /**
     * Parses a Catalog object given from a url
     * @param s the url string
     * @return A Catalog object
     */
    public Catalog parseVideo(String s) {
        String response = getRequest(s);
        JSONObject responseObject = (JSONObject) JSONValue.parse(response);
        String catalogName = (String)responseObject.get("catalog_name");
        ArrayList<CatalogContent> content = new ArrayList<CatalogContent>();
        JSONArray catalog = (JSONArray) responseObject.get("catalog");
        for (int i = 0; i < catalog.size(); i++) {
            JSONObject catalogInstance = (JSONObject) catalog.get(i);
            CatalogContent contentItem = new CatalogContent(
                    (String) catalogInstance.get("title"),
                    (String) catalogInstance.get("type"),
                    (String) catalogInstance.get("author_name")
            );
            content.add(contentItem);
        }
        return new Catalog(catalogName, content);
    }

    public static void main(String[] args) {
        ClientRequest request = new ClientRequest();
        // uncomment to parse and print catalog from file
        /*
        try {
            String contents = request.getFileContent("PATH_TO_FILE");
            request.parseAndPrint(contents);

        } catch (RequestException e) {
            e.printStackTrace();
        }
        */

        // uncomment to parse and print catalog from webpage
        /*
        String json = request.getRequest("https://www.mockaroo.com/e97aedd0/download?count=1&key=e79a3650");
        request.parseAndPrint(json);
        */

        // uncomment to parse catalog from webpage and create a catalog class
        // which can then pretty print the catalog contents
        /*
        Catalog cat = request.parseVideo("https://www.mockaroo.com/e97aedd0/download?count=1&key=e79a3650");
        cat.printCatalog();
        */
    }
}
