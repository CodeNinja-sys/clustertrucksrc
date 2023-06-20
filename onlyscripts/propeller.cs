using System;
using UnityEngine;

// Token: 0x020002A9 RID: 681
public class propeller : MonoBehaviour
{
	// Token: 0x0600103F RID: 4159 RVA: 0x00069DA8 File Offset: 0x00067FA8
	private void Start()
	{
		this.rig = base.GetComponent<Rigidbody>();
		this.rig.isKinematic = false;
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x00069DC4 File Offset: 0x00067FC4
	private void Update()
	{
		Vector3 a = Vector3.Normalize(this.positions[this.currentTarget] - base.transform.position);
		if (Vector3.Distance(base.transform.position, this.positions[this.currentTarget]) < 5f)
		{
			this.currentTarget++;
			if (this.currentTarget >= this.positions.Length)
			{
				this.currentTarget = 0;
			}
		}
		this.rig.AddForce(a * Mathf.Clamp(Time.deltaTime, 0f, 0.1f) * this.speed, ForceMode.Acceleration);
		this.rig.AddForce(Vector3.up * Mathf.Clamp(Time.deltaTime, 0f, 0.1f) * 600f, ForceMode.Acceleration);
	}

	// Token: 0x06001041 RID: 4161 RVA: 0x00069EBC File Offset: 0x000680BC
	public void sendInfo2(float[] info)
	{
		this.speed = info[0];
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x00069EC8 File Offset: 0x000680C8
	public void sendTransforms(Vector3[] vectors)
	{
		this.positions = vectors;
	}

	// Token: 0x04000D3C RID: 3388
	private Vector3[] positions;

	// Token: 0x04000D3D RID: 3389
	private int currentTarget;

	// Token: 0x04000D3E RID: 3390
	private Rigidbody rig;

	// Token: 0x04000D3F RID: 3391
	public float speed = 2000f;
}
