using System;
using UnityEngine;

// Token: 0x020002B4 RID: 692
public class shooter : MonoBehaviour
{
	// Token: 0x0600106F RID: 4207 RVA: 0x0006AF04 File Offset: 0x00069104
	private void Start()
	{
		if (this.cd != 99f && this.someRand)
		{
			this.counter = this.cd - 2f + UnityEngine.Random.Range(0f, this.cd * 0.3f);
		}
		else
		{
			this.counter = 100f;
		}
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x0006AF68 File Offset: 0x00069168
	private void Update()
	{
		this.wait -= Time.deltaTime;
		if (this.wait < 0f)
		{
			this.counter += Time.deltaTime;
		}
		if (this.counter > this.cd && this.ammo > 0 && this.wait < 0f)
		{
			if (this.forceScript != null)
			{
				this.forceScript.addForce(100f);
			}
			Vector3 vector = new Vector3(UnityEngine.Random.Range(-this.spread, this.spread), UnityEngine.Random.Range(-this.spread, this.spread), UnityEngine.Random.Range(-this.spread, this.spread));
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(this.projectile), base.transform.position, base.transform.rotation);
			gameObject.transform.Rotate(vector);
			if (this.someRand)
			{
				this.counter = UnityEngine.Random.Range(0f, this.cd * 0.3f);
			}
			else
			{
				this.counter = 0f;
			}
			this.ammo--;
			if (this.projectile == "truck" && !this.ignoreTruckDisable)
			{
				gameObject.GetComponent<car>().enabled = false;
			}
			if (this.forceToAllChildren > 0f)
			{
				foreach (Rigidbody rigidbody in gameObject.GetComponentsInChildren<Rigidbody>())
				{
					if (rigidbody.tag == "car")
					{
						rigidbody.GetComponent<carCheckDamage>().SetImmunity(true);
						rigidbody.AddForce(Vector3.Normalize(base.transform.forward + vector) * this.forceToAllChildren, ForceMode.VelocityChange);
						rigidbody.AddTorque(rigidbody.transform.right * 4f, ForceMode.VelocityChange);
					}
					else
					{
						rigidbody.AddForce(Vector3.Normalize(base.transform.forward + vector) * this.forceToAllChildren, ForceMode.VelocityChange);
					}
					if (this.randomTorque > 0f)
					{
						rigidbody.AddTorque(new Vector3(UnityEngine.Random.Range(-this.randomTorque, this.randomTorque), UnityEngine.Random.Range(-this.randomTorque, this.randomTorque), UnityEngine.Random.Range(-this.randomTorque, this.randomTorque)), ForceMode.VelocityChange);
					}
				}
			}
			foreach (ParticleSystem particleSystem in this.parts)
			{
				particleSystem.Play();
			}
			if (this.sound != null)
			{
				this.sound.Play();
			}
		}
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x0006B240 File Offset: 0x00069440
	public void sendInfo(float[] info)
	{
		this.cd = info[0];
		if (info.Length > 1)
		{
			this.spread = info[1];
		}
		if (info.Length > 2)
		{
			this.ammo = (int)info[2];
		}
		if (info.Length > 3)
		{
			this.forceToAllChildren = info[3];
		}
		if (info.Length > 4)
		{
			this.wait = info[4];
		}
	}

	// Token: 0x04000D79 RID: 3449
	public float wait;

	// Token: 0x04000D7A RID: 3450
	public int ammo = 100;

	// Token: 0x04000D7B RID: 3451
	public float cd;

	// Token: 0x04000D7C RID: 3452
	public string projectile = string.Empty;

	// Token: 0x04000D7D RID: 3453
	private float counter;

	// Token: 0x04000D7E RID: 3454
	public float spread;

	// Token: 0x04000D7F RID: 3455
	public Vector3 rot;

	// Token: 0x04000D80 RID: 3456
	public removeForceAndLerpBack forceScript;

	// Token: 0x04000D81 RID: 3457
	public float forceToAllChildren;

	// Token: 0x04000D82 RID: 3458
	public ParticleSystem[] parts;

	// Token: 0x04000D83 RID: 3459
	public AudioSource sound;

	// Token: 0x04000D84 RID: 3460
	public float randomTorque;

	// Token: 0x04000D85 RID: 3461
	public bool ignoreTruckDisable;

	// Token: 0x04000D86 RID: 3462
	public bool someRand = true;
}
