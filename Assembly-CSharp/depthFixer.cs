using System;
using UnityEngine;

// Token: 0x02000272 RID: 626
public class depthFixer : MonoBehaviour
{
	// Token: 0x06000F1A RID: 3866 RVA: 0x000624A0 File Offset: 0x000606A0
	private void Start()
	{
	}

	// Token: 0x06000F1B RID: 3867 RVA: 0x000624A4 File Offset: 0x000606A4
	private void Update()
	{
		RaycastHit raycastHit;
		if (Physics.SphereCast(base.transform.position + Vector3.up * 3f, 3f, base.transform.forward, out raycastHit, 300f, this.mask))
		{
			this.target.position = Vector3.Lerp(this.target.position, raycastHit.point, Time.unscaledDeltaTime * 10f);
			this.dof.enabled = true;
			Debug.Log(raycastHit.transform.name + raycastHit.transform.gameObject.layer);
		}
		else
		{
			this.dof.enabled = false;
		}
	}

	// Token: 0x04000BDE RID: 3038
	public Transform target;

	// Token: 0x04000BDF RID: 3039
	public LayerMask mask;

	// Token: 0x04000BE0 RID: 3040
	public MonoBehaviour dof;
}
