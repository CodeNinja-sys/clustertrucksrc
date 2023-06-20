using System;
using UnityEngine;

// Token: 0x020002C7 RID: 711
public class wheelForce : MonoBehaviour
{
	// Token: 0x060010EA RID: 4330 RVA: 0x0006E3AC File Offset: 0x0006C5AC
	private void Start()
	{
		this.myCar = base.transform.root.GetComponent<car>();
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x0006E3C4 File Offset: 0x0006C5C4
	private void Update()
	{
		RaycastHit raycastHit;
		if (Time.timeScale > 0f && Physics.Raycast(new Ray(base.transform.position, base.transform.forward), out raycastHit, 0.4f))
		{
			float multi = 1f;
			if (raycastHit.collider.tag.Contains("speed"))
			{
				speedRoad component = raycastHit.collider.GetComponent<speedRoad>();
				multi = component.speedBonus;
			}
			this.myCar.AddForceToMainRig(multi);
		}
	}

	// Token: 0x04000E0B RID: 3595
	private car myCar;
}
