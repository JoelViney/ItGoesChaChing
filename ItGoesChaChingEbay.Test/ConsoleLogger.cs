using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay.Test
{
	public class ConsoleLogger : ILogger
	{
		public void Log(LogLevel logLevel, string message, System.Exception exception)
		{
			Log(logLevel, message + "\r\n" + exception.ToString());
		}

		public void Log(LogLevel logLevel, string message, params object[] args)
		{
			string str = System.String.Format(message, args);
			Log(logLevel, str);
		}

		public void Log(LogLevel logLevel, string message)
		{
			System.Console.WriteLine("[{0}] {1}", logLevel.ToString(), message);
		}
	}
}
