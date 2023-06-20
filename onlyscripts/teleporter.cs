using System;
using UnityEngine;

// Token: 0x020002BF RID: 703
public class teleporter : MonoBehaviour
{
	// Token: 0x060010C7 RID: 4295 RVA: 0x0006DB90 File Offset: 0x0006BD90
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
		{
			float num = Mathf.Abs(other.transform.position.z - this.to.position.z);
			other.transform.root.position = new Vector3(other.transform.root.position.x, other.transform.root.position.y, other.transform.root.position.z - num);
		}
	}

	// Token: 0x04000DE3 RID: 3555
	public Transform to;
}
