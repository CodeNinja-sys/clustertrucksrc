using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029E RID: 670
public class noGrav : MonoBehaviour
{
	// Token: 0x06000FF5 RID: 4085 RVA: 0x00067444 File Offset: 0x00065644
	private void Update()
	{
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x00067448 File Offset: 0x00065648
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "car" && !this.targets.Contains(other.transform.root))
		{
			this.targets.Add(other.transform.root);
			foreach (Rigidbody rigidbody in other.transform.root.GetComponentsInChildren<Rigidbody>())
			{
				rigidbody.useGravity = false;
			}
		}
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x000674CC File Offset: 0x000656CC
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "car" && this.targets.Contains(other.transform.root) && this.leave)
		{
			this.targets.Remove(other.transform.root);
			foreach (Rigidbody rigidbody in other.transform.root.GetComponentsInChildren<Rigidbody>())
			{
				rigidbody.useGravity = true;
			}
		}
	}

	// Token: 0x04000CDD RID: 3293
	private List<Transform> targets = new List<Transform>();

	// Token: 0x04000CDE RID: 3294
	public bool leave;
}
