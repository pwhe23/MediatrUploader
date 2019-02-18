using System;
using System.IO;
using MediatorUploader.Domain;

namespace MediatorUploader.Web
{
    public class AppContext : IAppContext
    {
        public string MapDataPath(string path)
        {
            var folder = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/");
            if (folder == null)
                throw new ApplicationException("App_Data folder not found");

            if (path == null)
            {
                path = "";
            }

            path = path.Replace('/', '\\');

            return Path.Combine(folder, path);
        }
    };
}
