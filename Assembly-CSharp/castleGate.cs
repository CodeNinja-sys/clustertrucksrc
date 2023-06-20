using System;
using UnityEngine;

// Token: 0x0200026F RID: 623
public class castleGate : MonoBehaviour
{
	// Token: 0x06000F0F RID: 3855 RVA: 0x0006216C File Offset: 0x0006036C
	private void Start()
	{
		if (this.startUp)
		{
			this.rig.transform.Translate(Vector3.up * 20f, Space.World);
		}
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x0006219C File Offset: 0x0006039C
	private void Update()
	{
		this.waitTime -= Time.deltaTime;
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x000621B0 File Offset: 0x000603B0
	private void FixedUpdate()
	{
		if (this.waitTime < 0f)
		{
			if (this.goingUp)
			{
				if (this.rig.transform.localPosition.z < 20f)
				{
					this.rig.AddForce(Vector3.up * this.speed, ForceMode.VelocityChange);
				}
				else
				{
					this.rig.velocity *= 0.7f;
				}
			}
			if (!this.goingUp)
			{
				if (this.rig.transform.localPosition.z > this.speed / 5f)
				{
					this.rig.AddForce(-Vector3.up * this.speed, ForceMode.VelocityChange);
				}
				else
				{
					this.rig.velocity *= 0.7f;
				}
			}
		}
		this.rig.velocity *= 0.95f;
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x000622C8 File Offset: 0x000604C8
	private void Pull()
	{
		this.goingUp = !this.goingUp;
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x000622DC File Offset: 0x000604DC
	private void sendInfo(float[] info)
	{
		if (info[0] == 1f)
		{
			this.startUp = true;
		}
		if (info[1] == 1f)
		{
			this.goingUp = true;
		}
		this.waitTime = info[2];
		this.speed = info[3];
	}

	// Token: 0x04000BD2 RID: 3026
	public Rigidbody rig;

	// Token: 0x04000BD3 RID: 3027
	private bool startUp;

	// Token: 0x04000BD4 RID: 3028
	private bool goingUp;

	// Token: 0x04000BD5 RID: 3029
	private float speed = 25f;

	// Token: 0x04000BD6 RID: 3030
	private float waitTime;
}
