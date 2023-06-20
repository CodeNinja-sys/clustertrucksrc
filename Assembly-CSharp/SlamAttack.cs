using System;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public class SlamAttack : MonoBehaviour
{
	// Token: 0x06001106 RID: 4358 RVA: 0x0006E938 File Offset: 0x0006CB38
	private void Start()
	{
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x0006E93C File Offset: 0x0006CB3C
	private void Slam()
	{
		foreach (car car in UnityEngine.Object.FindObjectsOfType<car>())
		{
			if (car.secondRig && Vector3.Distance(this.hitPos.position, car.secondRig.position) < 20f)
			{
				car.secondRig.AddExplosionForce(this.slamForce, this.hitPos.position, 20f, 1f, ForceMode.VelocityChange);
			}
		}
	}

	// Token: 0x04000E1E RID: 3614
	public Transform hitPos;

	// Token: 0x04000E1F RID: 3615
	public float slamForce = 10f;
}
