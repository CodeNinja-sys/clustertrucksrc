using System;
using UnityEngine;

// Token: 0x020002A8 RID: 680
public class prop : MonoBehaviour
{
	// Token: 0x0600103B RID: 4155 RVA: 0x00069C64 File Offset: 0x00067E64
	private void Awake()
	{
		foreach (Transform transform in base.GetComponentsInChildren<Transform>(true))
		{
			transform.gameObject.AddComponent<RemoveOnMapChange>();
		}
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x00069CA0 File Offset: 0x00067EA0
	private void OnCollisionEnter(Collision other)
	{
		if (other.rigidbody != null)
		{
			float magnitude = other.rigidbody.velocity.magnitude;
			if (magnitude > 2f)
			{
				this.Explode(other.transform.position);
			}
		}
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x00069CF0 File Offset: 0x00067EF0
	private void Explode(Vector3 pos)
	{
		foreach (Transform transform in base.GetComponentsInChildren<Transform>(true))
		{
			if (transform.parent == base.transform)
			{
				if (transform.root == base.transform)
				{
					transform.parent = null;
				}
				else
				{
					transform.parent = base.transform.root;
				}
				transform.gameObject.SetActive(true);
			}
		}
		base.gameObject.SetActive(false);
		Camera.main.GetComponent<cameraEffects>().SetShake(0.3f, pos);
	}
}
