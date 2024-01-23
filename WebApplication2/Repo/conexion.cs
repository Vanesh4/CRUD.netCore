namespace WebApplication2.Repo
{
	public class conexion
	{
		private String cadenaConAPP { get; }
		private String cadenaConCinco { get; }
		public conexion()
		{
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
			cadenaConAPP = builder.GetSection("ConnectionStrings:ConnectionApp").Value;
			cadenaConCinco = builder.GetSection("ConnectionStrings:ConnectionCinco").Value;
		}
		public string getCadenaConAPP(){ return cadenaConAPP; }
		public string getCincaConCinco() {  return cadenaConCinco; }
    }
}
