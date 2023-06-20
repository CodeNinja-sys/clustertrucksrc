using System;

// Token: 0x020001F0 RID: 496
public class GhostRaceTime
{
	// Token: 0x06000BBA RID: 3002 RVA: 0x00048F70 File Offset: 0x00047170
	public GhostRaceTime(byte[] ghostData, string ghostName)
	{
		this.mGhostData = ghostData;
		this.mGhostName = ghostName;
	}

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00048F88 File Offset: 0x00047188
	public byte[] GhostData
	{
		get
		{
			return this.mGhostData;
		}
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00048F90 File Offset: 0x00047190
	public string GhostName
	{
		get
		{
			return this.mGhostName;
		}
	}

	// Token: 0x04000859 RID: 2137
	private byte[] mGhostData;

	// Token: 0x0400085A RID: 2138
	private string mGhostName;
}
