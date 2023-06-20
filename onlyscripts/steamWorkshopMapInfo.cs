using System;
using System.IO;
using Steamworks;
using UnityEngine;

// Token: 0x020002BB RID: 699
public class steamWorkshopMapInfo
{
	// Token: 0x06001092 RID: 4242 RVA: 0x0006BE80 File Offset: 0x0006A080
	public steamWorkshopMapInfo(SteamUGCDetails_t _det, uint _subs)
	{
		this.m_details = _det;
		this.m_subs = _subs;
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x0006BE98 File Offset: 0x0006A098
	public steamWorkshopMapInfo(string _title, string _desc, string[] _tags, string _changeNotes, string _pID, FileInfo _map)
	{
		this.m_title = _title;
		this.m_desc = _desc;
		this.m_tags = _tags;
		this.m_changeNotes = _changeNotes;
		this.m_pID = _pID;
		this.m_mapFile = _map;
		Debug.Log("Pubish ID: " + this.m_pID);
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x0006BEF0 File Offset: 0x0006A0F0
	public SteamUGCDetails_t getDetails()
	{
		return this.m_details;
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x0006BEF8 File Offset: 0x0006A0F8
	public uint getSubs()
	{
		return this.m_subs;
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x0006BF00 File Offset: 0x0006A100
	public string getTitle()
	{
		return this.m_title;
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x0006BF08 File Offset: 0x0006A108
	public string getDescription()
	{
		return this.m_desc;
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x0006BF10 File Offset: 0x0006A110
	public string[] getTags()
	{
		return this.m_tags;
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x0006BF18 File Offset: 0x0006A118
	public string getChangeNotes()
	{
		return this.m_changeNotes;
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x0006BF20 File Offset: 0x0006A120
	public string getPublishID()
	{
		return this.m_pID;
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x0006BF28 File Offset: 0x0006A128
	public FileInfo getMapFile()
	{
		return this.m_mapFile;
	}

	// Token: 0x04000DAD RID: 3501
	private SteamUGCDetails_t m_details;

	// Token: 0x04000DAE RID: 3502
	private FileInfo m_mapFile;

	// Token: 0x04000DAF RID: 3503
	private uint m_subs;

	// Token: 0x04000DB0 RID: 3504
	private string m_title;

	// Token: 0x04000DB1 RID: 3505
	private string m_desc;

	// Token: 0x04000DB2 RID: 3506
	private string m_changeNotes;

	// Token: 0x04000DB3 RID: 3507
	private string m_pID;

	// Token: 0x04000DB4 RID: 3508
	private string[] m_tags;
}
