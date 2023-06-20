using System;
using UnityEngine;

// Token: 0x02000274 RID: 628
public class destructivePiece : MonoBehaviour
{
	// Token: 0x06000F23 RID: 3875 RVA: 0x00062B3C File Offset: 0x00060D3C
	private void Start()
	{
		this.rig = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x00062B4C File Offset: 0x00060D4C
	private void Update()
	{
		if (this.lastAngVel == Vector3.zero)
		{
			this.lastAngVel = this.rig.angularVelocity;
		}
		if (this.lastVel == Vector3.zero)
		{
			this.lastVel = this.rig.velocity;
		}
		Vector3 vector = this.rig.velocity - this.lastVel;
		Vector3 vector2 = this.rig.angularVelocity - this.lastAngVel;
		float num = Mathf.Clamp(Time.timeScale, 0.7f, 1f);
		if (!this.used)
		{
			if (vector.magnitude > 5f * num || vector2.magnitude > 4f * num)
			{
				this.used = true;
				this.Explode();
			}
		}
		else
		{
			base.gameObject.SetActive(false);
		}
		this.lastVel = this.rig.velocity;
		this.lastAngVel = this.rig.angularVelocity;
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x00062C5C File Offset: 0x00060E5C
	private void Explode()
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("destructionSoundSmall"), base.transform.position, base.transform.rotation);
		Camera.main.GetComponent<cameraEffects>().SetShake(0.75f, base.transform.position);
		foreach (Transform transform in base.GetComponentsInChildren<Transform>(true))
		{
			Rigidbody component = transform.GetComponent<Rigidbody>();
			if (component != null)
			{
				component.velocity = this.rig.velocity;
				component.angularVelocity = this.rig.angularVelocity;
			}
			transform.parent = base.transform.parent;
			transform.gameObject.SetActive(true);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000BE8 RID: 3048
	private Rigidbody rig;

	// Token: 0x04000BE9 RID: 3049
	private Vector3 lastVel = Vector3.zero;

	// Token: 0x04000BEA RID: 3050
	private Vector3 lastAngVel = Vector3.zero;

	// Token: 0x04000BEB RID: 3051
	private bool used;

	// Token: 0x04000BEC RID: 3052
	private float counter;
}
