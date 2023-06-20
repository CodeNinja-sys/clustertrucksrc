using System;
using Steamworks;

// Token: 0x02000289 RID: 649
public class leaderboardScore
{
	// Token: 0x06000F9D RID: 3997 RVA: 0x00065978 File Offset: 0x00063B78
	public leaderboardScore(LeaderboardEntry_t _l, int[] _det)
	{
		this.m_entryDetails = _l;
		this.m_detals = _det;
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x00065990 File Offset: 0x00063B90
	public LeaderboardEntry_t getEntry()
	{
		return this.m_entryDetails;
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x00065998 File Offset: 0x00063B98
	public int[] getDetails()
	{
		return this.m_detals;
	}

	// Token: 0x04000C80 RID: 3200
	private LeaderboardEntry_t m_entryDetails;

	// Token: 0x04000C81 RID: 3201
	private int[] m_detals;
}
