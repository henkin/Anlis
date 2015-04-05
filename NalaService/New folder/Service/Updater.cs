using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class UpdaterService
    {
        // http://henkin.us:51501/guestAuth/repository/download/Nala_Nala/lastSuccessful/Nala.Server.zip

        public UpdaterService(string projectName)
        {
            
        }

        public void BeginPollingForUpdate()
        {
            while (true)
            {
                int? latestVersion = GetLatestVersion();
                
                Thread.Sleep(1000);
            }
        }

        public int? GetLatestVersion()
        {
            
        }

        public static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
    }
}
