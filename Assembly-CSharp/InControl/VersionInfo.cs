using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000FB RID: 251
	public struct VersionInfo : IComparable<VersionInfo>
	{
		// Token: 0x06000579 RID: 1401 RVA: 0x00028868 File Offset: 0x00026A68
		public VersionInfo(int major, int minor, int patch, int build)
		{
			this.Major = major;
			this.Minor = minor;
			this.Patch = patch;
			this.Build = build;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00028888 File Offset: 0x00026A88
		public static VersionInfo InControlVersion()
		{
			return new VersionInfo
			{
				Major = 1,
				Minor = 5,
				Patch = 12,
				Build = 6556
			};
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000288C4 File Offset: 0x00026AC4
		internal static VersionInfo UnityVersion()
		{
			Match match = Regex.Match(Application.unityVersion, "^(\\d+)\\.(\\d+)\\.(\\d+)");
			int build = 0;
			return new VersionInfo
			{
				Major = Convert.ToInt32(match.Groups[1].Value),
				Minor = Convert.ToInt32(match.Groups[2].Value),
				Patch = Convert.ToInt32(match.Groups[3].Value),
				Build = build
			};
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0002894C File Offset: 0x00026B4C
		public int CompareTo(VersionInfo other)
		{
			if (this.Major < other.Major)
			{
				return -1;
			}
			if (this.Major > other.Major)
			{
				return 1;
			}
			if (this.Minor < other.Minor)
			{
				return -1;
			}
			if (this.Minor > other.Minor)
			{
				return 1;
			}
			if (this.Patch < other.Patch)
			{
				return -1;
			}
			if (this.Patch > other.Patch)
			{
				return 1;
			}
			if (this.Build < other.Build)
			{
				return -1;
			}
			if (this.Build > other.Build)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000289FC File Offset: 0x00026BFC
		public override bool Equals(object other)
		{
			return other is VersionInfo && this == (VersionInfo)other;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00028A1C File Offset: 0x00026C1C
		public override int GetHashCode()
		{
			return this.Major.GetHashCode() ^ this.Minor.GetHashCode() ^ this.Patch.GetHashCode() ^ this.Build.GetHashCode();
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00028A58 File Offset: 0x00026C58
		public override string ToString()
		{
			if (this.Build == 0)
			{
				return string.Format("{0}.{1}.{2}", this.Major, this.Minor, this.Patch);
			}
			return string.Format("{0}.{1}.{2} build {3}", new object[]
			{
				this.Major,
				this.Minor,
				this.Patch,
				this.Build
			});
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00028AE4 File Offset: 0x00026CE4
		public string ToShortString()
		{
			if (this.Build == 0)
			{
				return string.Format("{0}.{1}.{2}", this.Major, this.Minor, this.Patch);
			}
			return string.Format("{0}.{1}.{2}b{3}", new object[]
			{
				this.Major,
				this.Minor,
				this.Patch,
				this.Build
			});
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00028B70 File Offset: 0x00026D70
		public static bool operator ==(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) == 0;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00028B80 File Offset: 0x00026D80
		public static bool operator !=(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) != 0;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00028B90 File Offset: 0x00026D90
		public static bool operator <=(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) <= 0;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00028BA0 File Offset: 0x00026DA0
		public static bool operator >=(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) >= 0;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00028BB0 File Offset: 0x00026DB0
		public static bool operator <(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) < 0;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00028BC0 File Offset: 0x00026DC0
		public static bool operator >(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) > 0;
		}

		// Token: 0x040003F8 RID: 1016
		public int Major;

		// Token: 0x040003F9 RID: 1017
		public int Minor;

		// Token: 0x040003FA RID: 1018
		public int Patch;

		// Token: 0x040003FB RID: 1019
		public int Build;
	}
}
