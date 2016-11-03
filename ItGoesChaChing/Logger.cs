
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing
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

	/// <summary>
	/// Defines the interface required for the Logger.
	/// </summary>
	public interface ILogger
	{
		void Log(LogLevel logLevel, string message);
		void Log(LogLevel logLevel, string message, System.Exception exception);
		void Log(LogLevel logLevel, string message, params object[] args);
	}

	//
	// If we ever need to get the calling method etc... --> http://stackoverflow.com/questions/15356626/setting-up-c-sharp-solution-with-multiple-projects-using-nlog-in-visual-studio
	//
	/// <summary></summary>
	public sealed class Logger : ILogger, ItGoesChaChing.Ebay.ILogger
	{
		private Type _type;
		private log4net.ILog _logger;

		#region Constructors...

		public Logger(Type type)
		{
			this._type = type; 
			_logger = log4net.LogManager.GetLogger(type);
		}

		#endregion

		public void Log(LogLevel logLevel, string message, System.Exception exception)
		{
			_logger.Logger.Log(this._type, ConvertToNLog(logLevel), message, exception);
		}

		public void Log(LogLevel logLevel, string message, params object[] args)
		{
			_logger.Logger.Log(this._type, ConvertToNLog(logLevel), FormatMessage(message, args), null);
		}

		public void Log(LogLevel logLevel, string message)
		{
			_logger.Logger.Log(this._type, ConvertToNLog(logLevel), message, null);
		}

		#region Ebay.ILogger.Log...

		void Ebay.ILogger.Log(Ebay.LogLevel logLevel, string message, Exception exception)
		{
			_logger.Logger.Log(this._type, ConvertToNLog(logLevel), message, exception);
		}

		void Ebay.ILogger.Log(Ebay.LogLevel logLevel, string message, params object[] args)
		{
			_logger.Logger.Log(this._type, ConvertToNLog(logLevel), FormatMessage(message, args), null);
		}
		
		void Ebay.ILogger.Log(Ebay.LogLevel logLevel, string message)
		{
			_logger.Logger.Log(this._type, ConvertToNLog(logLevel), message, null);
		}

		private string FormatMessage(string message, params object[] args)
		{ 
			try
			{
				string result = String.Format(message, args);
				return result;
			}
			catch(Exception ex)
			{
				return ex.ToString();
			}
		}

		private log4net.Core.Level ConvertToNLog(LogLevel logType)
		{
			switch (logType)
			{
				case LogLevel.Debug: return log4net.Core.Level.Debug;
				case LogLevel.Info: return log4net.Core.Level.Info;
			}
			return log4net.Core.Level.Error;
		}

		private log4net.Core.Level ConvertToNLog(Ebay.LogLevel logType)
		{
			switch (logType)
			{
				case Ebay.LogLevel.Debug: return log4net.Core.Level.Debug;
				case Ebay.LogLevel.Info: return log4net.Core.Level.Info;
			}
			return log4net.Core.Level.Error;
		}

		#endregion

	}

}
