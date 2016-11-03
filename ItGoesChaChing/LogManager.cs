using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing
{
    public static class LogManager
    {
        public static ILogger GetLogger()
        {
            var stack = new StackTrace();
            var frame = stack.GetFrame(1);

            return new Logger(frame.GetMethod().DeclaringType);
        }
    }
}
