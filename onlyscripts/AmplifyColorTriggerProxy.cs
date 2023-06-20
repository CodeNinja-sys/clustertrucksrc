using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[AddComponentMenu("")]
public class AmplifyColorTriggerProxy : MonoBehaviour
{
	// Token: 0x0600002D RID: 45 RVA: 0x000039F0 File Offset: 0x00001BF0
	private void Start()
	{
		this.sphereCollider = base.GetComponent<SphereCollider>();
		this.sphereCollider.radius = 0.01f;
		this.sphereCollider.isTrigger = true;
		this.rigidBody = base.GetComponent<Rigidbody>();
		this.rigidBody.useGravity = false;
		this.rigidBody.isKinematic = true;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00003A4C File Offset: 0x00001C4C
	private void LateUpdate()
	{
		base.transform.position = this.Reference.position;
		base.transform.rotation = this.Reference.rotation;
	}

	// Token: 0x0400004A RID: 74
	public Transform Reference;

	// Token: 0x0400004B RID: 75
	public AmplifyColorBase OwnerEffect;

	// Token: 0x0400004C RID: 76
	private SphereCollider sphereCollider;

	// Token: 0x0400004D RID: 77
	private Rigidbody rigidBody;
}
