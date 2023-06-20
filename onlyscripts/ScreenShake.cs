using System;
using UnityEngine;

// Token: 0x020001DD RID: 477
public class ScreenShake : MonoBehaviour
{
	// Token: 0x06000B4C RID: 2892 RVA: 0x00046958 File Offset: 0x00044B58
	private void Update()
	{
		this.Shake();
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00046960 File Offset: 0x00044B60
	private void Shake()
	{
		if (this.shake > 0f)
		{
			this.rot = new Vector3(UnityEngine.Random.Range(-this.shake, this.shake), UnityEngine.Random.Range(-this.shake, this.shake), UnityEngine.Random.Range(-this.shake, this.shake));
			Camera.main.transform.localRotation = Quaternion.Euler(this.rot);
			this.shake -= Time.deltaTime * this.decrease;
		}
		else
		{
			this.shake = 0f;
		}
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00046A04 File Offset: 0x00044C04
	public void SetShake(float f, float d)
	{
		this.decrease = d;
		f *= 5f;
		if (f > this.shake)
		{
			this.shake = f;
		}
	}

	// Token: 0x040007EE RID: 2030
	private float shake;

	// Token: 0x040007EF RID: 2031
	public float amount = 1f;

	// Token: 0x040007F0 RID: 2032
	public float decrease = 3f;

	// Token: 0x040007F1 RID: 2033
	private float shakeCd;

	// Token: 0x040007F2 RID: 2034
	private float uiShakeCd;

	// Token: 0x040007F3 RID: 2035
	private Vector3 rot = Vector3.zero;

	// Token: 0x040007F4 RID: 2036
	private Vector3 pos = Vector3.zero;
}
