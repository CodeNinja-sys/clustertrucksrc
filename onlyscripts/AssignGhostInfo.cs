using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000013 RID: 19
public class AssignGhostInfo : MonoBehaviour
{
	// Token: 0x06000073 RID: 115 RVA: 0x000055CC File Offset: 0x000037CC
	public void Init()
	{
		base.gameObject.SetActive(true);
		if (info.P_GhostRaceInfo == null)
		{
			this.NameText.text = "No Ghost Found for your current settings!";
			return;
		}
		string mRacerName = info.P_GhostRaceInfo.mRacerName;
		float mRaceTime = info.P_GhostRaceInfo.mRaceTime;
		string text = mRaceTime.ToString("F3").Replace('.', ':');
		this.NameText.text = mRacerName;
		this.TimeText.text = text;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00005644 File Offset: 0x00003844
	public void Reset()
	{
		this.NameText.text = string.Empty;
		this.TimeText.text = string.Empty;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00005680 File Offset: 0x00003880
	private IEnumerator DisableAfterSeconds(float time)
	{
		yield return new WaitForSeconds(time);
		this.Reset();
		yield break;
	}

	// Token: 0x0400007D RID: 125
	[SerializeField]
	private Text NameText;

	// Token: 0x0400007E RID: 126
	[SerializeField]
	private Text TimeText;
}
