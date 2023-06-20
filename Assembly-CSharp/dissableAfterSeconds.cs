using System;
using UnityEngine;

// Token: 0x020002ED RID: 749
public class dissableAfterSeconds : MonoBehaviour
{
	// Token: 0x06001181 RID: 4481 RVA: 0x00071340 File Offset: 0x0006F540
	private void OnEnable()
	{
		this.counter = 0f;
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x00071350 File Offset: 0x0006F550
	private void Update()
	{
		this.counter += Time.unscaledDeltaTime;
		if (this.counter > 6f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000EAE RID: 3758
	private float counter;
}
