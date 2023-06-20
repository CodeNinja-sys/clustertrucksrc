using System;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class RotationFix : MonoBehaviour
{
	// Token: 0x06000885 RID: 2181 RVA: 0x00037E4C File Offset: 0x0003604C
	private void Update()
	{
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(base.transform.localPosition.x, base.transform.localRotation.eulerAngles.y * 0.14f + this.plus + 2f, base.transform.localPosition.z), Time.deltaTime * 1f);
	}

	// Token: 0x040006BB RID: 1723
	public float plus = 12f;
}
