using System;
using UnityEngine;

// Token: 0x02000135 RID: 309
public class IntroHandler : MonoBehaviour
{
	// Token: 0x060006B1 RID: 1713 RVA: 0x0002CD20 File Offset: 0x0002AF20
	private void Step()
	{
		if (this.i == 0)
		{
			this.au.PlayOneShot(this.sound1);
			this.shaker.SetShake(0.3f, 8f);
		}
		if (this.i == 1)
		{
			this.au.PlayOneShot(this.sound2);
			this.shaker.SetShake(0.3f, 8f);
		}
		if (this.i == 2)
		{
			this.au.PlayOneShot(this.sound3);
			this.shaker.SetShake(0.3f, 8f);
		}
		if (this.i == 3)
		{
			this.musicMan.StartFirstSong();
			this.bar.SetActive(true);
			this.intro.SetActive(false);
			this.au.PlayOneShot(this.sound4);
			this.shaker.SetShake(0.5f, 5f);
		}
		this.i++;
	}

	// Token: 0x040004BC RID: 1212
	public GameObject bar;

	// Token: 0x040004BD RID: 1213
	public GameObject intro;

	// Token: 0x040004BE RID: 1214
	public ScreenShake shaker;

	// Token: 0x040004BF RID: 1215
	private int i;

	// Token: 0x040004C0 RID: 1216
	public AudioSource au;

	// Token: 0x040004C1 RID: 1217
	public AudioClip sound1;

	// Token: 0x040004C2 RID: 1218
	public AudioClip sound2;

	// Token: 0x040004C3 RID: 1219
	public AudioClip sound3;

	// Token: 0x040004C4 RID: 1220
	public AudioClip sound4;

	// Token: 0x040004C5 RID: 1221
	public musicManager musicMan;
}
