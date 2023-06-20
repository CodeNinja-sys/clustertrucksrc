using System;
using UnityEngine;

// Token: 0x020002B2 RID: 690
public class shakeObject : MonoBehaviour
{
	// Token: 0x06001068 RID: 4200 RVA: 0x0006AD0C File Offset: 0x00068F0C
	private void Start()
	{
		if (this.otherPos)
		{
			this.pos = base.transform.localPosition;
		}
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x0006AD2C File Offset: 0x00068F2C
	private void Update()
	{
		this.Shake();
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x0006AD34 File Offset: 0x00068F34
	private void Shake()
	{
		if (this.shake > 0f)
		{
			if (this.shakeCd > 0.01f)
			{
				this.pos = new Vector3(UnityEngine.Random.Range(-this.shake, this.shake) * this.multiplier.x, UnityEngine.Random.Range(-this.shake, this.shake) * this.multiplier.y, UnityEngine.Random.Range(-this.shake, this.shake) * this.multiplier.z);
				this.shakeCd = 0f;
			}
			if (this.hard)
			{
				base.transform.localPosition = this.pos;
			}
			else
			{
				base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.pos, Time.deltaTime * 30f);
			}
			this.shakeCd += Time.deltaTime;
			this.shake -= Time.deltaTime * this.decrease;
		}
		else
		{
			this.shake = 0f;
		}
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x0006AE5C File Offset: 0x0006905C
	public void SetShake(float f)
	{
		if (f > this.shake)
		{
			this.shake = f;
		}
	}

	// Token: 0x04000D71 RID: 3441
	public Vector3 multiplier = new Vector3(1f, 1f, 1f);

	// Token: 0x04000D72 RID: 3442
	public bool hard;

	// Token: 0x04000D73 RID: 3443
	public float shake;

	// Token: 0x04000D74 RID: 3444
	public float amount = 1f;

	// Token: 0x04000D75 RID: 3445
	public float decrease = 10f;

	// Token: 0x04000D76 RID: 3446
	private float shakeCd;

	// Token: 0x04000D77 RID: 3447
	private Vector3 pos = Vector3.zero;

	// Token: 0x04000D78 RID: 3448
	public bool otherPos;
}
