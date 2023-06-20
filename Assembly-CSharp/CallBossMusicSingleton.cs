using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class CallBossMusicSingleton : MonoBehaviour
{
	// Token: 0x0600007F RID: 127 RVA: 0x000058B0 File Offset: 0x00003AB0
	private void Start()
	{
		Debug.LogError("CallBossMusic1");
		Singleton<HaxxBossSound>.Instance.PlayBossMusic();
	}

	// Token: 0x06000080 RID: 128 RVA: 0x000058C8 File Offset: 0x00003AC8
	private void Update()
	{
	}
}
