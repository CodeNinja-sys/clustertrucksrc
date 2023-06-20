using System;
using InControl;
using UnityEngine;

// Token: 0x020001E8 RID: 488
public class CameraMovementRotation : MonoBehaviour
{
	// Token: 0x06000B78 RID: 2936 RVA: 0x000478CC File Offset: 0x00045ACC
	private void Start()
	{
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x000478D0 File Offset: 0x00045AD0
	private void Update()
	{
		if (Input.GetButton("Forward") || InputManager.ActiveDevice.LeftStickY.Value > 0.3f)
		{
		}
		if (Input.GetButton("Back") || InputManager.ActiveDevice.LeftStickY.Value < -0.3f)
		{
		}
		if (Input.GetButton("Left") || InputManager.ActiveDevice.LeftStickX.Value < -0.3f)
		{
			this.rot += Vector3.forward * Time.deltaTime * 0.1f;
		}
		if (Input.GetButton("Right") || InputManager.ActiveDevice.LeftStickX.Value > 0.3f)
		{
			this.rot -= Vector3.forward * Time.deltaTime * 0.1f;
		}
		if (Input.GetButton("Jump") || InputManager.ActiveDevice.Action1.IsPressed)
		{
		}
		base.transform.localRotation = Quaternion.Euler(this.rot);
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x00047A0C File Offset: 0x00045C0C
	private void FixedUpdate()
	{
		this.rot *= 0.97f;
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x00047A24 File Offset: 0x00045C24
	public void addRot(Vector3 sentRot)
	{
		this.rot += sentRot;
	}

	// Token: 0x04000823 RID: 2083
	private Vector3 rot = Vector3.zero;
}
