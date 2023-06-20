using System;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class TonemapTrigger : MonoBehaviour
{
	// Token: 0x0600113A RID: 4410 RVA: 0x000703EC File Offset: 0x0006E5EC
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player")
		{
			Camera.main.GetComponent<TonemapHandler>().SetTone(this.tone);
		}
	}

	// Token: 0x04000E6A RID: 3690
	public float tone = 1f;
}
