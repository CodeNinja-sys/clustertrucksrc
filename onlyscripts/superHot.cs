using System;
using UnityEngine;

// Token: 0x020002BD RID: 701
public class superHot : MonoBehaviour
{
	// Token: 0x060010C1 RID: 4289 RVA: 0x0006D77C File Offset: 0x0006B97C
	private void OnEnable()
	{
		this.p = UnityEngine.Object.FindObjectOfType<player>();
		foreach (Transform transform in UnityEngine.Object.FindObjectsOfType<Transform>())
		{
			if (transform.parent == null)
			{
				foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>(true))
				{
					try
					{
						if (renderer.sharedMaterial.name.ToLower().Contains("tesla"))
						{
							renderer.material = this.illumi;
						}
						else if (renderer.gameObject.tag != "car" && !renderer.gameObject.name.Contains("goal"))
						{
							if (!renderer.GetComponent<ParticleSystem>())
							{
								try
								{
									if (renderer.GetComponent<Rigidbody>() || renderer.transform.parent.GetComponent<Rigidbody>())
									{
										renderer.material = this.black;
									}
									else
									{
										renderer.material = this.white;
									}
								}
								catch
								{
								}
							}
						}
						else
						{
							renderer.material = this.red;
						}
					}
					catch
					{
					}
				}
			}
		}
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x0006D904 File Offset: 0x0006BB04
	private void Update()
	{
		this.t += Time.unscaledDeltaTime;
		if (this.p.getWalkingstate() || this.p.getRunningState() || this.p.lastGrounded > 0.1f || !this.p.hasTouchedGround || this.t < 0.5f || Input.GetKey(KeyCode.Mouse0))
		{
			this.movement = Mathf.Lerp(this.movement, 1f, Time.unscaledDeltaTime * 5f);
		}
		else
		{
			this.movement = Mathf.Lerp(this.movement, 0.02f, Time.unscaledDeltaTime * 5f);
		}
		this.mouseLook = Mathf.Lerp(this.mouseLook, Mathf.Clamp((Mathf.Abs(this.p._yValue) + Mathf.Abs(this.p._xValue)) * 1f, 0f, 0.3f), Time.unscaledDeltaTime * 20f);
		info.superTime = Mathf.Clamp(this.mouseLook + this.movement, 0f, 1f);
	}

	// Token: 0x04000DD7 RID: 3543
	private player p;

	// Token: 0x04000DD8 RID: 3544
	private float movement;

	// Token: 0x04000DD9 RID: 3545
	private float mouseLook;

	// Token: 0x04000DDA RID: 3546
	public Material red;

	// Token: 0x04000DDB RID: 3547
	public Material white;

	// Token: 0x04000DDC RID: 3548
	public Material black;

	// Token: 0x04000DDD RID: 3549
	public Material illumi;

	// Token: 0x04000DDE RID: 3550
	private float t;
}
