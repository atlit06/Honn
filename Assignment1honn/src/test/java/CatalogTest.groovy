/**
 * Created by steinn on 05/09/16.
 */
class CatalogTest extends GroovyTestCase {
    void testPrintCatalog() {
        ArrayList<CatalogContent> testList = new ArrayList<CatalogContent>();
        testList.add(new CatalogContent("Title1", "Type1", "Author1"));
        testList.add(new CatalogContent("Title2", "Type2", "Author2"));
        String title = "test1";
        Catalog testCatalog = new Catalog(title, testList);
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        testCatalog.printCatalog();
        assertEquals("Catalog: test1\n" +
                "\n" +
                "Title                     Type                      Author                   \n" +
                "-------------------------------------------------------------------------------------\n" +
                "Title1                    Type1                     Author1                  \n" +
                "Title2                    Type2                     Author2                  \n", outContent.toString());
        System.setOut(null);
    }
}
