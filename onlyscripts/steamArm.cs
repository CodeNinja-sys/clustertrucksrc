using System;
using UnityEngine;

// Token: 0x02000304 RID: 772
public class steamArm : MonoBehaviour
{
	// Token: 0x06001226 RID: 4646 RVA: 0x00073AB8 File Offset: 0x00071CB8
	private void Start()
	{
		this.duns = base.GetComponent<AudioSource>();
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x00073AC8 File Offset: 0x00071CC8
	private void Update()
	{
		this.cog.transform.localRotation = Quaternion.Euler(new Vector3(base.transform.localRotation.eulerAngles.x - 15f, 0f, 90f));
		if (this.isGoing && this.speed < this.moveSpeed)
		{
			this.speed += Time.deltaTime * 10f;
		}
		if (!this.isGoing)
		{
			this.counter += Time.deltaTime;
		}
		if (this.counter > this.waitBetween)
		{
			this.isGoing = true;
			this.counter = 0f;
		}
		Quaternion to = Quaternion.Euler(this.angles[this.currentAngle], base.transform.localRotation.eulerAngles.y, base.transform.localRotation.eulerAngles.z);
		base.transform.localRotation = Quaternion.RotateTowards(base.transform.localRotation, to, this.speed * Time.deltaTime);
		if (Quaternion.Angle(base.transform.localRotation, Quaternion.Euler(this.angles[this.currentAngle], base.transform.localRotation.eulerAngles.y, base.transform.localRotation.eulerAngles.z)) < 0.5f && this.isGoing)
		{
			if (this.speed > 0.5f)
			{
				Camera.main.GetComponent<cameraEffects>().SetShake(1f, Vector3.zero);
				if (this.duns != null)
				{
					this.duns.PlayOneShot(this.duns.clip);
				}
			}
			this.isGoing = false;
			this.speed = 0f;
			if (this.currentAngle + 1 >= this.angles.Length)
			{
				this.currentAngle = 0;
			}
			else
			{
				this.currentAngle++;
			}
		}
	}

	// Token: 0x04000F48 RID: 3912
	public float[] angles;

	// Token: 0x04000F49 RID: 3913
	public float waitBetween;

	// Token: 0x04000F4A RID: 3914
	public Transform cog;

	// Token: 0x04000F4B RID: 3915
	public float moveSpeed = 5f;

	// Token: 0x04000F4C RID: 3916
	private float counter;

	// Token: 0x04000F4D RID: 3917
	private int currentAngle;

	// Token: 0x04000F4E RID: 3918
	private bool isGoing;

	// Token: 0x04000F4F RID: 3919
	private float speed;

	// Token: 0x04000F50 RID: 3920
	public AudioSource duns;
}
