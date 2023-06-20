using System;
using UnityEngine;

// Token: 0x0200029C RID: 668
[AddComponentMenu("Camera-Control/Mouse Look")]
public class myMouseLook : MonoBehaviour
{
	// Token: 0x06000FF0 RID: 4080 RVA: 0x00067230 File Offset: 0x00065430
	private void Awake()
	{
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x00067234 File Offset: 0x00065434
	private void Update()
	{
		if (this.axes == myMouseLook.RotationAxes.MouseXAndY)
		{
			this.rotationX = base.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * this.sensitivityX;
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
			float num;
			if (this.rotationY > 360f - this.maximumY)
			{
				num = this.rotationY - 360f;
				Debug.Log(num + " same as: " + this.rotationY);
			}
			else
			{
				num = this.rotationY;
			}
			this.rotationY = Mathf.Clamp(num, this.minimumY, this.maximumY);
			base.transform.localEulerAngles = new Vector3(this.rotationY, this.rotationX, base.transform.eulerAngles.z);
		}
		else if (this.axes == myMouseLook.RotationAxes.MouseX)
		{
			Debug.Log(2);
			base.transform.Rotate(0f, Input.GetAxis("Mouse X") * this.sensitivityX, 0f);
		}
		else
		{
			Debug.Log(3);
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
			base.transform.localEulerAngles = new Vector3(-this.rotationY, base.transform.localEulerAngles.y, 0f);
		}
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x000673E8 File Offset: 0x000655E8
	public void reset()
	{
		this.rotationY = base.transform.localEulerAngles.x;
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x00067410 File Offset: 0x00065610
	private void Start()
	{
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = false;
		}
	}

	// Token: 0x04000CD0 RID: 3280
	public myMouseLook.RotationAxes axes;

	// Token: 0x04000CD1 RID: 3281
	public float sensitivityX = 15f;

	// Token: 0x04000CD2 RID: 3282
	public float sensitivityY = 15f;

	// Token: 0x04000CD3 RID: 3283
	public float minimumX = -360f;

	// Token: 0x04000CD4 RID: 3284
	public float maximumX = 360f;

	// Token: 0x04000CD5 RID: 3285
	public float minimumY = -90f;

	// Token: 0x04000CD6 RID: 3286
	public float maximumY = 90f;

	// Token: 0x04000CD7 RID: 3287
	private float rotationY;

	// Token: 0x04000CD8 RID: 3288
	private float rotationX;

	// Token: 0x0200029D RID: 669
	public enum RotationAxes
	{
		// Token: 0x04000CDA RID: 3290
		MouseXAndY,
		// Token: 0x04000CDB RID: 3291
		MouseX,
		// Token: 0x04000CDC RID: 3292
		MouseY
	}
}
