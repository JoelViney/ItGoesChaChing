using NLog;
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
		private readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

		#region Constructors...

		public Logger()
		{

		}

		#endregion

		public void Log(LogLevel logType, string message, System.Exception exception)
		{
			this._logger.Log(ConvertToNLog(logType), message, exception);
		}

		public void Log(LogLevel logType, string message, params object[] args)
		{
			this._logger.Log(ConvertToNLog(logType), args);
		}

		public void Log(LogLevel logType, string message)
		{
			this._logger.Log(ConvertToNLog(logType), message);
		}

		private NLog.LogLevel ConvertToNLog(LogLevel logType)
		{ 
			switch (logType)
			{
				case LogLevel.Debug: return NLog.LogLevel.Debug;
				case LogLevel.Info: return NLog.LogLevel.Info;
			}
			return NLog.LogLevel.Error;
		}

		#region Ebay.ILogger.Log...
		
		void Ebay.ILogger.Log(Ebay.LogLevel logLevel, string message)
		{
			this._logger.Log(ConvertToNLog(logLevel), message);
		}

		void Ebay.ILogger.Log(Ebay.LogLevel logLevel, string message, Exception exception)
		{
			this._logger.Log(ConvertToNLog(logLevel), message, exception);
		}

		void Ebay.ILogger.Log(Ebay.LogLevel logLevel, string message, params object[] args)
		{
			this._logger.Log(ConvertToNLog(logLevel), message, args);
		}

		private NLog.LogLevel ConvertToNLog(Ebay.LogLevel logType)
		{
			switch (logType)
			{
				case Ebay.LogLevel.Debug: return NLog.LogLevel.Debug;
				case Ebay.LogLevel.Info: return NLog.LogLevel.Info;
			}
			return NLog.LogLevel.Error;
		}

		#endregion

	}

}
