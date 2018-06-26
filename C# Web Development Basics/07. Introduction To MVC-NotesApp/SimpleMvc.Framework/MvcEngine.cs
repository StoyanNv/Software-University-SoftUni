namespace SimpleMvc.Framework
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Reflection;

    public class MvcEngine
    {
        public static void Run(WebServer.WebServer server, DbContext dbContext)
        {
            RegisterAssemblyName();
            RegisterControllersData();
            RegisterViewsData();
            RegisterModelsData();
            ConfigureDatabaseContext(dbContext);
            try
            {
                server.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ConfigureDatabaseContext(DbContext context)
        {
            using (context)
            {
                context.Database.Migrate();
            }
        }

        private static void RegisterModelsData()
        {
            MvcContext.Get.ModelsFolder = "Models";
        }

        private static void RegisterViewsData()
        {
            MvcContext.Get.ViewsFolder = "Views";
        }

        private static void RegisterControllersData()
        {
            MvcContext.Get.ControllersSuffix = "Controller";
            MvcContext.Get.ControllersFolder = "Controllers";
        }

        private static void RegisterAssemblyName()
        {
            MvcContext.Get.AssemblyName = Assembly.GetEntryAssembly()
                .GetName().Name;
        }
    }
}