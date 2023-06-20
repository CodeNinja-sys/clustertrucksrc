using System;
using UnityEngine;

// Token: 0x0200028C RID: 652
public class lightFade : MonoBehaviour
{
	// Token: 0x06000FA7 RID: 4007 RVA: 0x00065A54 File Offset: 0x00063C54
	private void Awake()
	{
		this.myLight = base.GetComponent<Light>();
		this.intensity = this.myLight.intensity;
		this.myLight.intensity = 0f;
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x00065A84 File Offset: 0x00063C84
	private void Update()
	{
		if (this.goindUp)
		{
			this.myLight.intensity += Time.deltaTime * this.speed;
		}
		else
		{
			this.myLight.intensity -= Time.deltaTime * this.speed;
		}
		if (this.myLight.intensity >= this.intensity)
		{
			this.goindUp = false;
		}
	}

	// Token: 0x04000C85 RID: 3205
	public float speed;

	// Token: 0x04000C86 RID: 3206
	private float intensity;

	// Token: 0x04000C87 RID: 3207
	private Light myLight;

	// Token: 0x04000C88 RID: 3208
	private bool goindUp = true;
}
