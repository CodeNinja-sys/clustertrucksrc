using System;
using UnityEngine;

// Token: 0x02000267 RID: 615
public class addTorque : MonoBehaviour
{
	// Token: 0x06000EE3 RID: 3811 RVA: 0x00060768 File Offset: 0x0005E968
	private void Start()
	{
		this.rig = base.gameObject.GetComponent<Rigidbody>();
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x0006077C File Offset: 0x0005E97C
	private void FixedUpdate()
	{
		this.rig.AddTorque(this.dir);
		this.rig.angularVelocity *= 0.95f;
	}

	// Token: 0x04000B82 RID: 2946
	private Rigidbody rig;

	// Token: 0x04000B83 RID: 2947
	public Vector3 dir = new Vector3(0f, 0f, 0f);
}
