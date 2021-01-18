using System.Runtime.CompilerServices;

namespace ItSerwis_Merge_v2
{
    public class LogHelper
    {
        public static log4net.ILog GetLogger([CallerFilePath] string filename = "")
        {
            return log4net.LogManager.GetLogger(filename);
        }
    }
}