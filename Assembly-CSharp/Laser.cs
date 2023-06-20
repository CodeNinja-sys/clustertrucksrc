using System;
using UnityEngine;

// Token: 0x020001F5 RID: 501
public class Laser : MonoBehaviour
{
	// Token: 0x06000BCF RID: 3023 RVA: 0x000492A0 File Offset: 0x000474A0
	private void Start()
	{
		this.myLigt = this.parts.GetComponent<Light>();
		this.parts.emission.enabled = false;
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x000492D4 File Offset: 0x000474D4
	private void Update()
	{
		RaycastHit raycastHit;
		Physics.Raycast(new Ray(this.holder.position, this.holder.forward), out raycastHit, 5000f);
		if (raycastHit.collider != null && this.cares)
		{
			if (this.holder.gameObject.activeInHierarchy)
			{
				this.parts.transform.position = raycastHit.point + raycastHit.normal * 0.07f;
				this.holder.localScale = new Vector3(1f, 1f, Vector3.Distance(this.holder.position, raycastHit.point));
				this.parts.emission.enabled = true;
			}
		}
		else
		{
			this.parts.emission.enabled = false;
			if (this.maxLength == 0f)
			{
				this.holder.localScale = new Vector3(1f, 1f, 1000f);
			}
			else
			{
				this.holder.localScale = new Vector3(1f, 1f, this.maxLength);
			}
		}
		if (raycastHit.collider && raycastHit.transform.tag == "Player")
		{
			GameManager gameManager = (GameManager)UnityEngine.Object.FindObjectOfType(typeof(GameManager));
			raycastHit.transform.GetComponent<player>().Die(2);
		}
		Physics.SphereCast(new Ray(this.holder.position, this.holder.forward), 1f, out raycastHit, 100f);
	}

	// Token: 0x04000866 RID: 2150
	public ParticleSystem parts;

	// Token: 0x04000867 RID: 2151
	public Transform holder;

	// Token: 0x04000868 RID: 2152
	private Light myLigt;

	// Token: 0x04000869 RID: 2153
	public bool cares = true;

	// Token: 0x0400086A RID: 2154
	public float maxLength;
}
