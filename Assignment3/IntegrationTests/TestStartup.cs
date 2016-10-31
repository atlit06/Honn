namespace Assign3.IntegrationTests
{
    public class TestStartup : Startup
    {
        //Setting up in memory database
        public override void SetUpDataBase(IServiceCollection services)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            services
            .AddEntityFrameworkSqlite()
            .AddDbContext<CmsDbContext>(
                options => options.UseSqlite(connection)
            );
        }
    }
}