using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class BackTrack : AbilityBaseClass
{
	// Token: 0x06000077 RID: 119 RVA: 0x000056B4 File Offset: 0x000038B4
	private void Start()
	{
	}

	// Token: 0x06000078 RID: 120 RVA: 0x000056B8 File Offset: 0x000038B8
	public override string getToolTip()
	{
		return this._toolTip;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x000056C0 File Offset: 0x000038C0
	public override string getToolTipController()
	{
		return this._toolTipController;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000056C8 File Offset: 0x000038C8
	private void FixedUpdate()
	{
		if (this.goingBack)
		{
			this.auSource.volume = Mathf.Lerp(this.auSource.volume, 1f, Time.deltaTime * 25f);
			info.extraAirTime = 1000f;
			if (this.truck)
			{
				this.mPlayer.rig.velocity = Vector3.Normalize(this.truck.position + Vector3.up * 2f - this.mPlayer.transform.position) * 70f;
				if (Vector3.Distance(base.transform.position, this.truck.position + Vector3.up * 2f) < 1.5f)
				{
					this.goingBack = false;
					this.mPlayer.rig.velocity = this.truck.velocity;
					foreach (Collider collider in this.mPlayer.GetComponentsInChildren<Collider>())
					{
						collider.enabled = true;
					}
					info.extraAirTime = 0f;
				}
			}
		}
		else
		{
			this.auSource.volume = Mathf.Lerp(this.auSource.volume, 0f, Time.deltaTime * 2f);
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00005838 File Offset: 0x00003A38
	private IEnumerator duck()
	{
		return null;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x0000583C File Offset: 0x00003A3C
	public override void Go()
	{
		car component = this.mPlayer.lastCar.GetComponent<car>();
		if (component)
		{
			this.goingBack = true;
			this.truck = component.secondRig;
			foreach (Collider collider in this.mPlayer.GetComponentsInChildren<Collider>())
			{
				collider.enabled = false;
			}
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x000058A4 File Offset: 0x00003AA4
	public override void Stop()
	{
	}

	// Token: 0x0400007F RID: 127
	[SerializeField]
	private player mPlayer;

	// Token: 0x04000080 RID: 128
	private bool goingBack;

	// Token: 0x04000081 RID: 129
	private Rigidbody truck;

	// Token: 0x04000082 RID: 130
	public AudioSource auSource;
}
