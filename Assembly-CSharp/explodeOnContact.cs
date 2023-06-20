using System;
using UnityEngine;

// Token: 0x0200027A RID: 634
public class explodeOnContact : MonoBehaviour
{
	// Token: 0x06000F3D RID: 3901 RVA: 0x0006361C File Offset: 0x0006181C
	private void OnCollisionEnter(Collision other)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(this.effect), base.transform.position, Quaternion.LookRotation(base.transform.forward));
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04000C0E RID: 3086
	public string effect = string.Empty;
}
