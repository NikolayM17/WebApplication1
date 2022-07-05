namespace WebApplication1.Service
{
	public static class Extensions
	{
		public static string CutController(this string str) => str.Replace("Controller", "");
	}
}
