using System;
using UnityEngine;

// Token: 0x0200028B RID: 651
public class life : MonoBehaviour
{
	// Token: 0x06000FA4 RID: 4004 RVA: 0x00065A10 File Offset: 0x00063C10
	private void Start()
	{
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x00065A14 File Offset: 0x00063C14
	private void Update()
	{
		this.i -= Time.deltaTime;
		if (this.i < 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000C84 RID: 3204
	public float i = 5f;
}
