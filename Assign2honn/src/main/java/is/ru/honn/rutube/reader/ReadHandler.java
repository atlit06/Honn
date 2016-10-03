package is.ru.honn.rutube.reader;

/**
 * Created by steinn on 29/09/16.
 */

public interface ReadHandler
{
    /**
     * Interface function for handling objects read by Reader
     * @param count Number of objects read so far
     * @param object object to be handled by read
     */
    void read(int count, Object object);
}
