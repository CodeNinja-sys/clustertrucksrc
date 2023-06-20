using System;
using UnityEngine;

// Token: 0x020002B1 RID: 689
public class sendMessageForward : MonoBehaviour
{
	// Token: 0x06001061 RID: 4193 RVA: 0x0006AC64 File Offset: 0x00068E64
	private void Start()
	{
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x0006AC68 File Offset: 0x00068E68
	private void Update()
	{
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x0006AC6C File Offset: 0x00068E6C
	public void sendInfo(float[] info)
	{
		this.info1Obj.SendMessage("sendInfo", info);
	}

	// Token: 0x06001064 RID: 4196 RVA: 0x0006AC80 File Offset: 0x00068E80
	public void sendInfo2(float[] info2)
	{
		this.info2Obj.SendMessage("sendInfo2", info2);
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x0006AC94 File Offset: 0x00068E94
	public void sendTransforms(Vector3[] transforms)
	{
		this.transformsObj.SendMessage("sendTransforms", transforms);
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x0006ACA8 File Offset: 0x00068EA8
	public void sendVectors(Vector3[] vectors)
	{
		this.vectorsObj.SendMessage("sendTransforms", vectors);
	}

	// Token: 0x04000D6D RID: 3437
	public Transform info1Obj;

	// Token: 0x04000D6E RID: 3438
	public Transform info2Obj;

	// Token: 0x04000D6F RID: 3439
	public Transform transformsObj;

	// Token: 0x04000D70 RID: 3440
	public Transform vectorsObj;
}
