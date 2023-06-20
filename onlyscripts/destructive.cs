using System;
using UnityEngine;

// Token: 0x02000273 RID: 627
public class destructive : MonoBehaviour
{
	// Token: 0x06000F1D RID: 3869 RVA: 0x00062594 File Offset: 0x00060794
	private void Start()
	{
		this.moveScript = base.GetComponent<moveBackAndForth>();
		this.rig = base.GetComponent<Rigidbody>();
		this.explosionMask = 8192;
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x000625CC File Offset: 0x000607CC
	private void FixedUpdate()
	{
		this.topSpeed *= 0.9f;
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x000625E4 File Offset: 0x000607E4
	private void Update()
	{
		if (this.moveScript != null)
		{
			this.rig.constraints = RigidbodyConstraints.None;
			this.rig.useGravity = true;
			if (!this.rig.isKinematic && this.topSpeed.magnitude < this.rig.velocity.magnitude)
			{
				this.topSpeed = this.rig.velocity;
			}
		}
		else if (this.topSpeed.magnitude < this.rig.velocity.magnitude)
		{
			this.topSpeed = this.rig.velocity;
		}
		if (this.holders.Length > 0)
		{
			bool flag = false;
			bool flag2 = true;
			for (int i = 0; i < this.holders.Length; i++)
			{
				if (this.holders[i].activeSelf)
				{
					flag = true;
				}
				else
				{
					flag2 = false;
				}
			}
			if (!flag || (this.needAllHolders && !flag2))
			{
				this.Explode(Vector3.zero);
			}
		}
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x00062700 File Offset: 0x00060900
	private void OnCollisionEnter(Collision other)
	{
		if (other == null)
		{
			return;
		}
		if (other.rigidbody == null)
		{
			return;
		}
		if (other.rigidbody.transform.tag == "car")
		{
			if (!this.rig.isKinematic && this.topSpeed.magnitude > this.neededForce)
			{
				Camera.main.GetComponent<cameraEffects>().SetShake(0.7f, base.transform.position);
				this.Explode(other.transform.position);
			}
			if (other.rigidbody != null)
			{
				car component = other.transform.root.gameObject.GetComponent<car>();
				if (component != null)
				{
					float num = Vector3.Angle(component.myVelocity, new Vector3(base.transform.position.x, 0f, base.transform.position.z) - new Vector3(other.transform.position.x, 0f, other.transform.position.z));
					float num2 = Mathf.Clamp(num / 90f, 0f, 1f);
					if ((1f - num2) * component.myVelocity.magnitude > this.neededForce * 2f)
					{
						this.Explode(other.transform.position);
					}
				}
				else
				{
					float magnitude = other.rigidbody.velocity.magnitude;
					if (magnitude > this.neededForce)
					{
						this.Explode(other.transform.position);
					}
				}
			}
		}
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x000628C8 File Offset: 0x00060AC8
	private void Explode(Vector3 pos)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("destructionSound"), base.transform.position, base.transform.rotation);
		foreach (Transform transform in base.GetComponentsInChildren<Transform>(true))
		{
			if (transform.parent == base.transform)
			{
				transform.parent = base.transform.root;
				transform.gameObject.SetActive(true);
				Rigidbody component = transform.GetComponent<Rigidbody>();
				if (this.moveScript != null && component)
				{
					component.velocity = this.moveScript.vel;
				}
				if (!this.rig.isKinematic)
				{
					component.velocity = this.topSpeed;
				}
				if (transform.childCount > 0)
				{
					transform.gameObject.AddComponent<destructivePiece>();
					if (Vector3.Distance(transform.position, pos) < 15f && pos != Vector3.zero)
					{
						foreach (Transform transform2 in transform.gameObject.GetComponentsInChildren<Transform>(true))
						{
							transform2.parent = base.transform.root;
							transform2.gameObject.SetActive(true);
							transform.gameObject.SetActive(false);
							component = transform2.GetComponent<Rigidbody>();
							if (this.moveScript != null && component)
							{
								component.velocity = this.moveScript.vel;
							}
							if (!this.rig.isKinematic)
							{
								component.velocity = this.topSpeed;
							}
						}
					}
				}
			}
		}
		base.gameObject.SetActive(false);
		Camera.main.GetComponent<cameraEffects>().SetShake(0.5f, pos);
		foreach (Collider collider in Physics.OverlapSphere(pos, 10f, this.explosionMask))
		{
			Rigidbody component2 = collider.GetComponent<Rigidbody>();
			if (component2)
			{
				component2.AddExplosionForce(50f, pos, 10f, 1f, ForceMode.Impulse);
			}
		}
	}

	// Token: 0x04000BE1 RID: 3041
	public GameObject[] holders;

	// Token: 0x04000BE2 RID: 3042
	public bool needAllHolders;

	// Token: 0x04000BE3 RID: 3043
	private moveBackAndForth moveScript;

	// Token: 0x04000BE4 RID: 3044
	private Rigidbody rig;

	// Token: 0x04000BE5 RID: 3045
	private Vector3 topSpeed = Vector3.zero;

	// Token: 0x04000BE6 RID: 3046
	public LayerMask explosionMask;

	// Token: 0x04000BE7 RID: 3047
	public float neededForce = 5f;
}
