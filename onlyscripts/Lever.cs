using System;
using InControl;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class Lever : MonoBehaviour
{
	// Token: 0x06000E19 RID: 3609 RVA: 0x0005C2D8 File Offset: 0x0005A4D8
	private void Awake()
	{
		for (int i = 0; i < this.targets.Length; i++)
		{
			this.targets[i].GetComponent<spawnObject>().GetLever(base.transform, i);
		}
		this.armMat.material.EnableKeyword("_EMISSION");
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x0005C32C File Offset: 0x0005A52C
	private void Update()
	{
		if (this.player != null)
		{
			Color color = new Color(this.value, this.value, this.value);
			if (Vector3.Distance(base.transform.position, this.player.position) < 7f && this.arm.angularVelocity.magnitude < 1f)
			{
				this.armMat.material.SetColor("_EmissionColor", color);
				this.value = Mathf.Lerp(this.value, 0.5f, this.speed * Time.deltaTime);
			}
			else
			{
				this.armMat.material.SetColor("_EmissionColor", color);
				this.value = Mathf.Lerp(this.value, 0f, this.speed * Time.deltaTime);
			}
			if (this.value > 0.1f && (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.E) || InputManager.ActiveDevice.Action3))
			{
				this.Pull();
				this.value = 1f;
			}
		}
		else
		{
			try
			{
				this.player = UnityEngine.Object.FindObjectOfType<player>().transform;
			}
			catch
			{
			}
		}
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x0005C4A8 File Offset: 0x0005A6A8
	private void FixedUpdate()
	{
		this.arm.angularVelocity *= 0.95f;
		float num = 200f;
		if (!this.down)
		{
			if (this.arm.transform.localRotation.eulerAngles.y < 225f)
			{
				this.arm.AddTorque(this.arm.transform.up * num);
			}
			else
			{
				this.arm.angularVelocity *= 0.7f;
			}
		}
		else if (this.arm.transform.localRotation.eulerAngles.y > 135f)
		{
			this.arm.AddTorque(this.arm.transform.up * -num);
		}
		else
		{
			this.arm.angularVelocity *= 0.7f;
		}
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x0005C5C0 File Offset: 0x0005A7C0
	public void GetTarget(Transform tran, int i)
	{
		this.targets[i] = tran;
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x0005C5CC File Offset: 0x0005A7CC
	private void Pull()
	{
		Camera.main.GetComponent<cameraEffects>().SetShake(0.7f, Vector3.zero);
		foreach (Transform transform in this.targets)
		{
			transform.SendMessage("Pull");
		}
		this.down = !this.down;
	}

	// Token: 0x04000AB8 RID: 2744
	public Rigidbody arm;

	// Token: 0x04000AB9 RID: 2745
	private Transform player;

	// Token: 0x04000ABA RID: 2746
	public Light myLight;

	// Token: 0x04000ABB RID: 2747
	public Transform[] targets;

	// Token: 0x04000ABC RID: 2748
	public Renderer armMat;

	// Token: 0x04000ABD RID: 2749
	private float value;

	// Token: 0x04000ABE RID: 2750
	private float speed = 10f;

	// Token: 0x04000ABF RID: 2751
	private bool down;
}
