using System;
using UnityEngine;

// Token: 0x0200027B RID: 635
public class explosion : MonoBehaviour
{
	// Token: 0x06000F3F RID: 3903 RVA: 0x000636A8 File Offset: 0x000618A8
	private void Start()
	{
		Camera.main.GetComponent<cameraEffects>().SetAll(this.shake, base.transform.position);
		foreach (Collider collider in Physics.OverlapSphere(base.transform.position, this.range, this.mask))
		{
			Rigidbody component = collider.transform.parent.GetComponent<Rigidbody>();
			component.AddExplosionForce(this.force, base.transform.position, this.range, this.up, ForceMode.Impulse);
		}
	}

	// Token: 0x04000C0F RID: 3087
	public float shake = 0.5f;

	// Token: 0x04000C10 RID: 3088
	public float force = 5f;

	// Token: 0x04000C11 RID: 3089
	public float up = 5f;

	// Token: 0x04000C12 RID: 3090
	public float range = 20f;

	// Token: 0x04000C13 RID: 3091
	private float life;

	// Token: 0x04000C14 RID: 3092
	public LayerMask mask;

	// Token: 0x04000C15 RID: 3093
	private MeshRenderer rend;
}
