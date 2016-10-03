package is.ru.honn.rutube.reader;

import org.json.simple.JSONObject;

import java.util.Iterator;
import java.util.List;

/**
 * Created by steinn on 29/09/16.
 */
public abstract class AbstractReader implements Reader {
    private String URI;
    protected ReadHandler readHandler;
    private ClientRequest clientRequest;
    /**
     * Reads a string from a URI, parses it to objects feeds them to a ReadHandler and returns them
     * @return List of objects read
     * @throws ReaderException if either URI or readHandler are not set
     */
    public Object read() throws ReaderException {
        if (this.readHandler == null) {
            throw new ReaderException("ReadHandler is not set");
        }
        if (this.URI == null) {
            throw new ReaderException("URI is not set");
        }
        if (this.clientRequest == null) {
            this.clientRequest = new ClientRequest();
        }
        String content = this.clientRequest.getRequest(this.URI);
        List objects = (List) this.parse(content);
        return objects;
    };
    /**
     * Sets the URI for the String which will be read
     * @param URI URI to be called to get the content
     */
    public void setURI(String URI) {
        this.URI = URI;
    };
    /**
     * sets the readHandler for the callback in read
     * @param readHandler the readHandler which will process the read objects
     */
    public void setReadHandler(ReadHandler readHandler) {
        this.readHandler = readHandler;
    };

    /**
     *
     * @param jParent Json parent containing an integer field.
     * @param name name of the integer field
     * @return int value of the json int in the jParent object.
     */
    protected int getInt(JSONObject jParent, String name)
    {
        if(jParent == null)
            return 0;
        Long value = (Long)jParent.get(name);
        if(value == null)
            return 0;
        return value.intValue();
    }
    /**
     * Parses a string to an Object
     * @param content the string to be parsed
     * @return the parsed object
     */
    public abstract Object parse (String content);
}
