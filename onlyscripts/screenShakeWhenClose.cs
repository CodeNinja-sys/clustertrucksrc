using System;
using UnityEngine;

// Token: 0x020002AF RID: 687
public class screenShakeWhenClose : MonoBehaviour
{
	// Token: 0x06001059 RID: 4185 RVA: 0x0006A974 File Offset: 0x00068B74
	private void Start()
	{
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x0006A978 File Offset: 0x00068B78
	private void Update()
	{
		if (this.cam == null && Camera.main != null)
		{
			this.cam = Camera.main.GetComponent<cameraEffects>();
		}
		if (this.cam == null)
		{
			return;
		}
		this.tran = this.cam.transform;
		int num = this.distance;
		float num2 = Mathf.Clamp(((float)num - Vector3.Distance(base.transform.position, this.tran.position)) / (float)num, 0f, 1f);
		if (this.cam.shake < num2)
		{
			this.cam.shake = num2;
		}
	}

	// Token: 0x04000D63 RID: 3427
	private cameraEffects cam;

	// Token: 0x04000D64 RID: 3428
	private Transform tran;

	// Token: 0x04000D65 RID: 3429
	public int distance = 200;
}
