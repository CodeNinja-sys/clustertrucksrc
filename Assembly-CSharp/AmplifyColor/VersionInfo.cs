using System;
using UnityEngine;

namespace AmplifyColor
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public class VersionInfo
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00003C48 File Offset: 0x00001E48
		private VersionInfo()
		{
			this.m_major = 1;
			this.m_minor = 4;
			this.m_release = 4;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003C68 File Offset: 0x00001E68
		private VersionInfo(byte major, byte minor, byte release)
		{
			this.m_major = (int)major;
			this.m_minor = (int)minor;
			this.m_release = (int)release;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public static string StaticToString()
		{
			return string.Format("{0}.{1}.{2}", 1, 4, 4) + VersionInfo.StageSuffix + VersionInfo.TrialSuffix;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}", this.m_major, this.m_minor, this.m_release) + VersionInfo.StageSuffix + VersionInfo.TrialSuffix;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003D18 File Offset: 0x00001F18
		public int Number
		{
			get
			{
				return this.m_major * 100 + this.m_minor * 10 + this.m_release;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003D34 File Offset: 0x00001F34
		public static VersionInfo Current()
		{
			return new VersionInfo(1, 4, 4);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003D40 File Offset: 0x00001F40
		public static bool Matches(VersionInfo version)
		{
			return version.m_major == 1 && version.m_minor == 4 && 4 == version.m_release;
		}

		// Token: 0x04000053 RID: 83
		public const byte Major = 1;

		// Token: 0x04000054 RID: 84
		public const byte Minor = 4;

		// Token: 0x04000055 RID: 85
		public const byte Release = 4;

		// Token: 0x04000056 RID: 86
		private static string StageSuffix = "_dev007";

		// Token: 0x04000057 RID: 87
		private static string TrialSuffix = string.Empty;

		// Token: 0x04000058 RID: 88
		[SerializeField]
		private int m_major;

		// Token: 0x04000059 RID: 89
		[SerializeField]
		private int m_minor;

		// Token: 0x0400005A RID: 90
		[SerializeField]
		private int m_release;
	}
}
