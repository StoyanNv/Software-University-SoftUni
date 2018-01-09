using System;
using TeamBuilder.App.Core;
using TeamBuilder.Data;

namespace TeamBuilder.App
{
    class StartUp
    {
        static void Main()
        {
            RessetDatabase();
            var engine = new Engine();
            engine.Run();
        }

        static void RessetDatabase()
        {
            using (var db = new TeamBuilderContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
