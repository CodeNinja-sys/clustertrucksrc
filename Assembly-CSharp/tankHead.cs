using System;
using UnityEngine;

// Token: 0x020002BE RID: 702
public class tankHead : MonoBehaviour
{
	// Token: 0x060010C4 RID: 4292 RVA: 0x0006DA58 File Offset: 0x0006BC58
	private void Start()
	{
		this.rig = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x0006DA68 File Offset: 0x0006BC68
	private void Update()
	{
		if (this.player == null)
		{
			this.player = GameObject.Find("player").transform.FindChild("hitbox");
		}
		else
		{
			Vector3 vector = this.rig.transform.InverseTransformPoint(this.player.position);
			this.rig.AddTorque(Time.deltaTime * this.rig.transform.up * 2000f * vector.x);
			this.rig.angularVelocity *= 0.9f;
			if (this.cd > 2f && vector.x < 0.2f)
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("rocket"), this.gun.position, this.gun.rotation);
				this.cd = 0f;
			}
			this.cd += Time.deltaTime;
		}
	}

	// Token: 0x04000DDF RID: 3551
	private Transform player;

	// Token: 0x04000DE0 RID: 3552
	private Rigidbody rig;

	// Token: 0x04000DE1 RID: 3553
	public Transform gun;

	// Token: 0x04000DE2 RID: 3554
	private float cd = 5f;
}
