namespace CardOrderApp.Core
{
    public class Options
    {
        public static string ConnectionString
        {
            get
            {
                return "Server = localhost; Port = 5432; User Id = postgres; Password = root; Database = orderCard;";
                //return Environment.GetEnvironmentVariable("Игорь");
            }
        }
    }
}
