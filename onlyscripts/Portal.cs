using System;
using UnityEngine;

// Token: 0x02000179 RID: 377
public class Portal : MonoBehaviour
{
	// Token: 0x06000873 RID: 2163 RVA: 0x00037B98 File Offset: 0x00035D98
	private void Start()
	{
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x00037B9C File Offset: 0x00035D9C
	private void Update()
	{
		Vector3 vector = Vector3.Normalize(-(this.player.position - base.transform.transform.position));
		Debug.DrawRay(this.player.position, vector, Color.red);
		Vector3 dir = Vector3.Reflect(vector, base.transform.forward);
		Debug.DrawRay(this.otherPortal.position, dir, Color.green);
	}

	// Token: 0x040006B6 RID: 1718
	public Transform otherPortal;

	// Token: 0x040006B7 RID: 1719
	public Transform cam;

	// Token: 0x040006B8 RID: 1720
	public Transform player;
}
