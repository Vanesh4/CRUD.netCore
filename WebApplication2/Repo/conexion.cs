namespace WebApplication2.Repo
{
	public class conexion
	{
		private String cadenaCon { get; set; }
		public conexion()
		{
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
			cadenaCon = builder.GetSection("ConnectionStrings:Connection").Value;
		}
		public string getCadenaCon()
		{
			return cadenaCon;
		}
	}
}
