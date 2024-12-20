namespace Zopoesht2Migration.DbContexts
{
    public static class DbConfigurations
    {
        public static string Zopoesht2Context { get; set; } = "Server=localhost;Port=5432;User Id=postgres;Password=!QAZ2wsx#EDC;Database=zopoesht2";

        public static string Zopoesht1Context { get; set; } = "Data Source=.;Initial Catalog=MosvEcologicalDamage;User ID=sa;Password=!QAZ2wsx#EDC;TrustServerCertificate=True;";

        public static string Zopoesht1ProdContext { get; set; } = "Data Source=.;Initial Catalog=MosvEcologicalDamageProd;User ID=sa;Password=!QAZ2wsx#EDC;TrustServerCertificate=True;";

    }
}
