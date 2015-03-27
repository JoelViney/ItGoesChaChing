using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Ebay
{
	/// <summary>
	/// Definitions for the types of logs sent to Log.Message()
	/// </summary>
	public enum LogLevel
	{
		/// <summary>Used to display debug messages.</summary>
		Debug,
		/// <summary>Used to display informational messages.</summary>
		Info,
		/// <summary>Used to display an error in the code.</summary>
		Error,
	};

	/// <summary>Defines the interface required for the Logger.</summary>
	public interface ILogger
	{
		void Log(LogLevel logLevel, string message);
		void Log(LogLevel logLevel, string message, System.Exception exception);
		void Log(LogLevel logLevel, string message, params object[] args);
	}
}
