using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class EndingOrb : MonoBehaviour
{
	// Token: 0x06000099 RID: 153 RVA: 0x00005D5C File Offset: 0x00003F5C
	private void Start()
	{
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00005D60 File Offset: 0x00003F60
	private void Update()
	{
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00005D64 File Offset: 0x00003F64
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player")
		{
			PlayerPrefs.SetInt("BeatGame", 1);
			other.GetComponent<AddEffect>().FadeToWhite();
		}
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00005DA4 File Offset: 0x00003FA4
	public void ShowCredits()
	{
		this.credits.SetActive(true);
		this.evereyThingElse.SetActive(false);
	}

	// Token: 0x0400008E RID: 142
	public GameObject credits;

	// Token: 0x0400008F RID: 143
	public GameObject evereyThingElse;
}
