package is.ru.honn.rutube.reader;

import org.json.simple.JSONObject;

/**
 * Created by steinn on 29/09/16.
 */
public abstract class AbstractReader implements Reader {
    private String URI;
    private ReadHandler readHandler;
    public Object read() {

        return new Object();
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
