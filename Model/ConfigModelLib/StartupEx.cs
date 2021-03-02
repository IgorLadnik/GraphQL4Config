using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using ConfigModelLib.Mutation;
using ConfigModelLib.Query;
using ConfigModelLib.Type;
using ConfigModelLib.Type.Input;
using ConfigModelLib.Type.Output;

namespace PersonModelLib
{
    public static class StartupEx
    {
        public static void AddPersonModelServices(this IServiceCollection services) 
        {
            // Types
            services.AddTransient<SourceType>();
            services.AddTransient<LoaderType>();
            services.AddTransient<ConfigurationManagerType>();

            // Input Types
            services.AddTransient<SourceInputType>();
            services.AddTransient<LoaderInputType>();
            services.AddTransient<ConfigurationManagerInputType>();

            // Output Types
            services.AddTransient<ConfigurationManagerOutputType>();

            // Queries
            services.AddTransient<ConfigurationManagerQuery>();
            services.AddTransient<RootQuery>();

            // Mutations
            services.AddTransient<ConfigurationManagerMutation>();
            services.AddTransient<RootMutation>();
        }
    }

    public static class TraceHelper 
    {
        public static int instance = 0;

        public static string Out(string fieldName, int instance) =>
            $"{fieldName}  instance = {instance},  thread = {Thread.CurrentThread.ManagedThreadId}  ";
    }
}
