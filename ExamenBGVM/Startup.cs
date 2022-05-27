using Microsoft.AspNetCore.Builder;

namespace ExamenBGVM
{
    public class Startup
    {
        internal static ILoggerFactory LogFactory { get; set; }

        public void Configure(IApplicationBuilder app, ILoggerFactory logFactory)
        {
            LogFactory = logFactory;
        }
    }
}
