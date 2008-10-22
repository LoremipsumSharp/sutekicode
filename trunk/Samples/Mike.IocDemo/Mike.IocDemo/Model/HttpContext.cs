namespace Mike.IocDemo.Model
{
    public class HttpContext
    {
        private static readonly HttpContext httpContext = new HttpContext();

        public static HttpContext Current
        {
            get
            {
                return httpContext;
            }
        }
    }
}