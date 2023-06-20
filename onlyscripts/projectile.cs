using System;
using UnityEngine;

// Token: 0x020002A6 RID: 678
public class projectile : MonoBehaviour
{
	// Token: 0x06001032 RID: 4146 RVA: 0x00069704 File Offset: 0x00067904
	private void Start()
	{
		this.rig = base.gameObject.GetComponent<Rigidbody>();
		this.rig.AddForce(this.forwardForce * base.transform.forward, ForceMode.VelocityChange);
		this.rig.AddForce(this.upForce * Vector3.up, ForceMode.VelocityChange);
		Camera.main.GetComponent<cameraEffects>().SetShake(0.2f, base.transform.position);
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x00069780 File Offset: 0x00067980
	private void Update()
	{
		this.life += Time.deltaTime;
		if (this.life > 5f && this.done)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		Ray ray = new Ray(base.transform.position, Vector3.Normalize(this.rig.velocity));
		RaycastHit other;
		if (Physics.Raycast(ray, out other, 3f))
		{
			base.transform.position = other.point;
			if (other.transform.root.gameObject.layer != base.gameObject.layer)
			{
				this.Hit(other);
			}
		}
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x00069838 File Offset: 0x00067A38
	private void Hit(RaycastHit other)
	{
		if (!this.done)
		{
			this.CheckEffects(other);
			if (other.transform.gameObject.layer != 13)
			{
				if (other.transform.gameObject.GetComponent<Rigidbody>() != null)
				{
					other.transform.gameObject.GetComponent<Rigidbody>().AddForce(this.attackForce * base.transform.forward, ForceMode.VelocityChange);
				}
				else if (other.transform.parent.gameObject.GetComponent<Rigidbody>() != null)
				{
					other.transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(this.attackForce * base.transform.forward, ForceMode.VelocityChange);
				}
			}
			Vector3 vector = Vector3.zero;
			Quaternion rotation = base.transform.rotation;
			if (this.normalSpawn > 0f)
			{
				vector = other.normal * this.normalSpawn;
				Vector3 b = Vector3.Normalize(Vector3.Project(base.transform.forward, other.normal));
				rotation = Quaternion.LookRotation(base.transform.forward - b);
			}
			if (this.effect != string.Empty)
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(this.effect), base.transform.position + vector * 1f, rotation);
				if (this.effect == "Truck")
				{
					car component = gameObject.GetComponent<car>();
					component.mainRig.mass = 2f;
					component.secondRig.mass = 2f;
					component.enabled = false;
				}
			}
			if (this.effect2 != string.Empty)
			{
				UnityEngine.Object.Instantiate(Resources.Load(this.effect2), base.transform.position + vector, rotation);
			}
			this.done = true;
			foreach (ParticleSystem particleSystem in base.transform.GetComponentsInChildren<ParticleSystem>())
			{
				particleSystem.enableEmission = false;
			}
			foreach (MeshRenderer meshRenderer in base.transform.GetComponentsInChildren<MeshRenderer>())
			{
				meshRenderer.enabled = false;
			}
			foreach (Collider collider in base.transform.GetComponentsInChildren<Collider>())
			{
				collider.enabled = false;
			}
		}
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x00069AE8 File Offset: 0x00067CE8
	private void Freeze(Rigidbody other)
	{
		other.velocity *= 0f;
		other.angularVelocity *= 0f;
		other.useGravity = false;
		other.isKinematic = true;
		Transform transform = other.transform;
		if (other.tag == "car")
		{
			transform = other.transform.parent;
			other.GetComponent<carCheckDamage>().SetImmunity(true);
		}
		foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>())
		{
			renderer.material.color = new Color(0.6f, 1f, 1f);
		}
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x00069BA4 File Offset: 0x00067DA4
	private void CheckEffects(RaycastHit other)
	{
		Rigidbody rigidbody = other.rigidbody;
		if (rigidbody && this.freeze)
		{
			this.Freeze(rigidbody);
		}
	}

	// Token: 0x04000D2F RID: 3375
	public float forwardForce = 100f;

	// Token: 0x04000D30 RID: 3376
	public float upForce;

	// Token: 0x04000D31 RID: 3377
	public float attackForce;

	// Token: 0x04000D32 RID: 3378
	public string effect = string.Empty;

	// Token: 0x04000D33 RID: 3379
	public string effect2 = string.Empty;

	// Token: 0x04000D34 RID: 3380
	public float normalSpawn;

	// Token: 0x04000D35 RID: 3381
	private Rigidbody rig;

	// Token: 0x04000D36 RID: 3382
	private bool done;

	// Token: 0x04000D37 RID: 3383
	private float life;

	// Token: 0x04000D38 RID: 3384
	public bool freeze;
}
