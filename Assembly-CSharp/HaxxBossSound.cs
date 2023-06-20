using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class HaxxBossSound : Singleton<HaxxBossSound>
{
	// Token: 0x06000129 RID: 297 RVA: 0x0000813C File Offset: 0x0000633C
	private void Awake()
	{
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00008140 File Offset: 0x00006340
	public void PlayBossMusic()
	{
		HaxxBossSound.mDead = false;
		Debug.LogError("haxx awake");
		if (HaxxBossSound.mMusic == null)
		{
			HaxxBossSound.mMusic = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("FinalBossMusicThingy"));
		}
		HaxxBossSound.mMusic.transform.parent = base.transform;
		this.KillMe();
	}

	// Token: 0x0600012B RID: 299 RVA: 0x000081A4 File Offset: 0x000063A4
	private void Update()
	{
		this.KillMe();
	}

	// Token: 0x0600012C RID: 300 RVA: 0x000081AC File Offset: 0x000063AC
	public void ForceKill()
	{
		Debug.LogError("died");
		HaxxBossSound.mDead = true;
		UnityEngine.Object.Destroy(HaxxBossSound.mMusic);
	}

	// Token: 0x0600012D RID: 301 RVA: 0x000081C8 File Offset: 0x000063C8
	private void KillMe()
	{
		if ((info.currentLevel != 90 || Manager.Instance().IsMenuActive) && !HaxxBossSound.mDead)
		{
			this.ForceKill();
		}
	}

	// Token: 0x040000DB RID: 219
	private static GameObject mMusic;

	// Token: 0x040000DC RID: 220
	private static bool mDead;
}
