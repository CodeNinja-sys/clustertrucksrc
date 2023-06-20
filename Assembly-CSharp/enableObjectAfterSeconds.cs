using System;
using UnityEngine;

// Token: 0x02000278 RID: 632
public class enableObjectAfterSeconds : MonoBehaviour
{
	// Token: 0x06000F31 RID: 3889 RVA: 0x0006327C File Offset: 0x0006147C
	private void Start()
	{
		this.secondsLeft += UnityEngine.Random.Range(-this.spread, this.spread);
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x000632A0 File Offset: 0x000614A0
	private void Update()
	{
		this.secondsLeft -= Time.deltaTime;
		if (this.secondsLeft < 0f)
		{
			this.obj.SetActive(true);
		}
		else if (this.secondsLeft < 0.5f && this.blink)
		{
			this.blinkTimer -= Time.deltaTime;
			if (this.blinkTimer < 0f)
			{
				this.blinkTimer = UnityEngine.Random.Range(0.5f, 0.1f);
				this.obj.SetActive(!this.obj.activeInHierarchy);
			}
		}
	}

	// Token: 0x04000C00 RID: 3072
	public float secondsLeft = 1f;

	// Token: 0x04000C01 RID: 3073
	public float spread = 0.5f;

	// Token: 0x04000C02 RID: 3074
	public bool blink;

	// Token: 0x04000C03 RID: 3075
	private float blinkTimer;

	// Token: 0x04000C04 RID: 3076
	public GameObject obj;
}
