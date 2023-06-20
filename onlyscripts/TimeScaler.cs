using System;
using UnityEngine;

// Token: 0x02000112 RID: 274
public class TimeScaler : MonoBehaviour
{
	// Token: 0x060005E5 RID: 1509 RVA: 0x0002AA20 File Offset: 0x00028C20
	private void Start()
	{
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0002AA24 File Offset: 0x00028C24
	private void FixedUpdate()
	{
		Time.timeScale = this.mTimeScale;
	}

	// Token: 0x04000452 RID: 1106
	[SerializeField]
	private float mTimeScale = 1f;
}
