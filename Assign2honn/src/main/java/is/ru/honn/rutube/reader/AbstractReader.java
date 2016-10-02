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
        Iterator i = objects.iterator();
        int position = 0;
        while (i.hasNext()) {
            Object next = i.next();
            readHandler.read(position, next);
            position = position + 1;
        }
        return objects;
    };
    public void setURI(String URI) {
        this.URI = URI;
    };
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
    public abstract Object parse (String content);
}
