using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class DesProj : MonoBehaviour
{
	// Token: 0x0600008D RID: 141 RVA: 0x00005BBC File Offset: 0x00003DBC
	private void Start()
	{
		this.rig.AddForce(base.transform.forward * -this.f, ForceMode.VelocityChange);
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00005BEC File Offset: 0x00003DEC
	private void Update()
	{
		base.transform.rotation = Quaternion.LookRotation(-this.rig.velocity);
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00005C1C File Offset: 0x00003E1C
	private void OnCollisionEnter(Collision other)
	{
		this.shards.transform.SetParent(base.transform.parent);
		this.piece.SetActive(false);
		this.shards.SetActive(true);
		foreach (Rigidbody rigidbody in this.rigs)
		{
			rigidbody.velocity = -other.impactForceSum;
		}
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("destructionSound"), base.transform.position, base.transform.rotation);
	}

	// Token: 0x04000086 RID: 134
	public GameObject piece;

	// Token: 0x04000087 RID: 135
	public GameObject shards;

	// Token: 0x04000088 RID: 136
	public Rigidbody[] rigs;

	// Token: 0x04000089 RID: 137
	public Rigidbody rig;

	// Token: 0x0400008A RID: 138
	public float f;

	// Token: 0x0400008B RID: 139
	public float t;
}
