using System;

namespace InControl
{
	// Token: 0x0200006D RID: 109
	public class Logger
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002CD RID: 717 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		// (remove) Token: 0x060002CE RID: 718 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		public static event Logger.LogMessageHandler OnLogMessage;

		// Token: 0x060002CF RID: 719 RVA: 0x0000E7F4 File Offset: 0x0000C9F4
		public static void LogInfo(string text)
		{
			if (Logger.OnLogMessage != null)
			{
				LogMessage message = new LogMessage
				{
					text = text,
					type = LogMessageType.Info
				};
				Logger.OnLogMessage(message);
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000E834 File Offset: 0x0000CA34
		public static void LogWarning(string text)
		{
			if (Logger.OnLogMessage != null)
			{
				LogMessage message = new LogMessage
				{
					text = text,
					type = LogMessageType.Warning
				};
				Logger.OnLogMessage(message);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000E874 File Offset: 0x0000CA74
		public static void LogError(string text)
		{
			if (Logger.OnLogMessage != null)
			{
				LogMessage message = new LogMessage
				{
					text = text,
					type = LogMessageType.Error
				};
				Logger.OnLogMessage(message);
			}
		}

		// Token: 0x02000313 RID: 787
		// (Invoke) Token: 0x0600125A RID: 4698
		public delegate void LogMessageHandler(LogMessage message);
	}
}
