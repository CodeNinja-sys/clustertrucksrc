using System;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class PlaybackViewCam : MonoBehaviour
{
	// Token: 0x06000621 RID: 1569 RVA: 0x0002B7CC File Offset: 0x000299CC
	private void Start()
	{
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x0002B7D0 File Offset: 0x000299D0
	private void ApplyVelocity()
	{
		Vector3 vector = this.mVelocity;
		if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
		{
			vector *= this.mSprintMul;
		}
		vector = vector * Mathf.Clamp01(Time.deltaTime) * this.mMoveSpeed;
		base.transform.Translate(vector, Space.World);
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0002B82C File Offset: 0x00029A2C
	private void Drag()
	{
		this.mVelocity *= 1f - this.mDrag;
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0002B84C File Offset: 0x00029A4C
	private void Input()
	{
		float d = 0f;
		float d2 = 0f;
		float d3 = 0f;
		if (UnityEngine.Input.GetKey(KeyCode.W))
		{
			d = -1f;
		}
		if (UnityEngine.Input.GetKey(KeyCode.S))
		{
			d = 1f;
		}
		if (UnityEngine.Input.GetKey(KeyCode.D))
		{
			d2 = -1f;
		}
		if (UnityEngine.Input.GetKey(KeyCode.A))
		{
			d2 = 1f;
		}
		if (UnityEngine.Input.GetKey(KeyCode.E))
		{
			d3 = 1f;
		}
		if (UnityEngine.Input.GetKey(KeyCode.Q))
		{
			d3 = -1f;
		}
		Vector3 a = d * base.transform.forward;
		Vector3 b = d2 * base.transform.right;
		Vector3 b2 = d3 * base.transform.up;
		Vector3 a2 = a + b + b2;
		this.mVelocity += a2 * this.mAccelerationSpeed;
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0002B940 File Offset: 0x00029B40
	private void Update()
	{
		this.Input();
		this.ApplyVelocity();
		this.Drag();
	}

	// Token: 0x04000472 RID: 1138
	[SerializeField]
	private float mMoveSpeed = 16f;

	// Token: 0x04000473 RID: 1139
	[SerializeField]
	private float mSprintMul = 2f;

	// Token: 0x04000474 RID: 1140
	[SerializeField]
	private float mAccelerationSpeed = 1f;

	// Token: 0x04000475 RID: 1141
	[Range(0.01f, 1f)]
	public float mDrag = 0.1f;

	// Token: 0x04000476 RID: 1142
	private Vector3 mVelocity = new Vector3(0f, 0f, 0f);
}
