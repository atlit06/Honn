package is.ru.honn.rutube.reader;

/**
 * Created by steinn on 29/09/16.
 */
public interface Reader {
    Object read() throws ReaderException;
    Object parse(String content);
    void setURI(String URI);
    void setReadHandler(ReadHandler readHandler);
}
