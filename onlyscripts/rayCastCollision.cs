using System;
using UnityEngine;

// Token: 0x020002AB RID: 683
public class rayCastCollision : MonoBehaviour
{
	// Token: 0x0600104A RID: 4170 RVA: 0x0006A394 File Offset: 0x00068594
	private void Start()
	{
		this.prevPos = base.transform.position;
		this._player = base.GetComponent<player>();
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x0006A3B4 File Offset: 0x000685B4
	private void Update()
	{
		if (this._player.framesSinceStart > 3f)
		{
			Ray ray = new Ray(this.prevPos, (base.transform.position - this.prevPos).normalized);
			float num = Vector3.Distance(base.transform.position, this.prevPos);
			foreach (RaycastHit raycastHit in Physics.RaycastAll(ray, num, this.mask))
			{
				if (raycastHit.transform.tag == "kill")
				{
					if (raycastHit.collider.GetComponent<World>())
					{
						Debug.DrawLine(ray.origin, ray.origin + ray.direction * num, Color.green, 10000f);
					}
					else if (raycastHit.transform.GetComponent<IgnoreRaycast>() == null)
					{
						Debug.LogError("Death Hit: " + raycastHit.transform.name, raycastHit.transform);
						this._player.Die(0);
					}
				}
			}
		}
		this.prevPos = base.transform.position;
	}

	// Token: 0x04000D50 RID: 3408
	private Vector3 prevPos;

	// Token: 0x04000D51 RID: 3409
	private player _player;

	// Token: 0x04000D52 RID: 3410
	public LayerMask mask;
}
