using System;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class motherTruck : MonoBehaviour
{
	// Token: 0x060011DC RID: 4572 RVA: 0x00072D7C File Offset: 0x00070F7C
	private void Start()
	{
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x00072D80 File Offset: 0x00070F80
	private void Update()
	{
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x00072D84 File Offset: 0x00070F84
	private void SpawnMissile()
	{
		UnityEngine.Object.Instantiate(Resources.Load("motherMissile"), this.missile.position, this.missile.rotation);
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x00072DB8 File Offset: 0x00070FB8
	private void setShake()
	{
		Camera.main.GetComponent<cameraEffects>().SetShake(1f, Vector3.zero);
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x00072DD4 File Offset: 0x00070FD4
	private void setShakeHalf()
	{
		Camera.main.GetComponent<cameraEffects>().SetShake(0.5f, Vector3.zero);
	}

	// Token: 0x04000EF7 RID: 3831
	public Transform missile;
}
