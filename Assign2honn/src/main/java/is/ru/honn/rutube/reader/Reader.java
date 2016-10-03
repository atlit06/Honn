package is.ru.honn.rutube.reader;

/**
 * Created by steinn on 29/09/16.
 */
public interface Reader {
    /**
     * Reads a string from a URI, parses it to objects feeds them to a ReadHandler and returns them
     * @return List of objects read
     * @throws ReaderException if either URI or readHandler are not set
     */
    Object read() throws ReaderException;

    /**
     * Parses a string to an Object
     * @param content the string to be parsed
     * @return the parsed object
     */
    Object parse(String content);

    /**
     * Sets the URI for the String which will be read
     * @param URI URI to be called to get the content
     */
    void setURI(String URI);

    /**
     * sets the readHandler for the callback in read
     * @param readHandler the readHandler which will process the read objects
     */
    void setReadHandler(ReadHandler readHandler);
}
