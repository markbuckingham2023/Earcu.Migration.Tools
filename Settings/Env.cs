namespace Settings
{
    public static class Env
    {
        public static string RackSpaceConnectionString = @"Data Source = 192.168.100.60; Initial Catalog = DBMaster; User Id = dbmaster; Password=hErman4Munster;";

        //public static string RackSpaceConnectionString = @"Data Source=localhost;Initial Catalog=DBMaster;User Id=sa;Password=Password123;";
         
        //public static string AWSConnectionString = @"Data Source=localhost;Initial Catalog=DBMaster;User Id=sa;Password=Password123;";
        public static string AWSConnectionString = @"Data Source=live-poc-db-4.cfrf2wualswa.eu-west-1.rds.amazonaws.com;Initial Catalog=DBMaster;User Id=Admin;Password=Jrj6QGA-GrYnZqvJ4sD.;";

        
        public static string Secret = "kweRGW235sv23freha";
        public static string OutputFolder = @"c:\temp\dataimport\";
        public static string InputFolder = @"c:\temp\dataimport\";

    }
}
