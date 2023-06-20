using System;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class bossMissile : MonoBehaviour
{
	// Token: 0x0600117D RID: 4477 RVA: 0x00071054 File Offset: 0x0006F254
	private void Start()
	{
		this.rig = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x00071064 File Offset: 0x0006F264
	private void FixedUpdate()
	{
		if (this.life > 25f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			UnityEngine.Object.Instantiate(Resources.Load("explosionSpawnerB"), base.transform.position, base.transform.rotation);
		}
		this.life += Time.deltaTime;
		this.counter += Time.deltaTime;
		if (this.counter > 1f)
		{
			base.GetComponent<Collider>().enabled = true;
			this.counter = -1000f;
		}
		if (!this.player)
		{
			this.player = UnityEngine.Object.FindObjectOfType<player>().transform;
			this.playerRig = this.player.GetComponent<Rigidbody>();
		}
		else
		{
			this.rig.AddForce(base.transform.forward * 50f, ForceMode.Acceleration);
			this.rig.velocity *= 0.992f;
			this.rig.angularVelocity *= 0.9f;
			Vector3 target = Vector3.Normalize(this.player.position + this.playerRig.velocity * 1.2f - Vector3.up - base.transform.position);
			if (this.life < 5f)
			{
				target = Vector3.Normalize(this.player.position + this.playerRig.velocity * 20f - Vector3.up - base.transform.position);
			}
			base.transform.rotation = Quaternion.LookRotation(Vector3.MoveTowards(base.transform.forward, target, 0.005f));
			if (Vector3.Distance(base.transform.position, this.player.position) > 100f)
			{
				base.transform.rotation = Quaternion.LookRotation(Vector3.MoveTowards(base.transform.forward, target, 0.05f));
				this.rig.AddForce(base.transform.forward * 150f, ForceMode.Acceleration);
				this.rig.AddForce(Vector3.up * 4f, ForceMode.Acceleration);
				this.rig.velocity *= 0.95f;
				this.rig.angularVelocity *= 0.9f;
			}
		}
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x00071308 File Offset: 0x0006F508
	private void OnCollisionStay(Collision other)
	{
		this.rig.AddForce(base.transform.forward * 250f, ForceMode.Acceleration);
	}

	// Token: 0x04000EA9 RID: 3753
	private Rigidbody rig;

	// Token: 0x04000EAA RID: 3754
	private Rigidbody playerRig;

	// Token: 0x04000EAB RID: 3755
	private Transform player;

	// Token: 0x04000EAC RID: 3756
	private float counter;

	// Token: 0x04000EAD RID: 3757
	private float life;
}
