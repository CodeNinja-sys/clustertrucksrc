using System;
using UnityEngine;

// Token: 0x0200024C RID: 588
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
	// Token: 0x06000E5A RID: 3674 RVA: 0x0005D824 File Offset: 0x0005BA24
	private void Awake()
	{
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x0005D828 File Offset: 0x0005BA28
	private void Update()
	{
		if (levelEditorManager.Instance().IsBusy)
		{
			return;
		}
		if (CameraMovement._currentMovementType == CameraMovement.CameraMovementType.topDown)
		{
			if (!this._setRot)
			{
				this._rotationBefore = base.transform.rotation;
				this._setRot = true;
			}
			base.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
		}
		else if (CameraMovement._currentMovementType == CameraMovement.CameraMovementType.freeLook && this._setRot)
		{
			base.transform.rotation = this._rotationBefore;
			this._setRot = false;
			return;
		}
		if (!levelEditorManager.canLook)
		{
			return;
		}
		if (CameraMovement._currentMovementType == CameraMovement.CameraMovementType.freeLook)
		{
			if (this.axes == MouseLook.RotationAxes.MouseXAndY)
			{
				this.rotationX = base.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * this.sensitivityX;
				if ((Input.GetAxis("Mouse X") > 0f || Input.GetAxis("Mouse Y") > 0f) && TutorialHandler.Instance.SpecialEvents[3].gameObject.activeInHierarchy)
				{
					this._mouseMoveTutorialTimer += Time.deltaTime;
				}
				if (this._mouseMoveTutorialTimer > 0.5f)
				{
					TutorialHandler.Instance.SpecialEvents[3].Clicked();
				}
				this.rotationY = base.transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * this.sensitivityY;
				float value;
				if (this.rotationY > 360f - this.maximumY)
				{
					value = this.rotationY - 360f;
				}
				else
				{
					value = this.rotationY;
				}
				this.rotationY = Mathf.Clamp(value, this.minimumY, this.maximumY);
				base.transform.localEulerAngles = new Vector3(this.rotationY, this.rotationX, base.transform.eulerAngles.z);
			}
			else if (this.axes == MouseLook.RotationAxes.MouseX)
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
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x0005DAE8 File Offset: 0x0005BCE8
	public void reset()
	{
		this.rotationY = base.transform.localEulerAngles.x;
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x0005DB10 File Offset: 0x0005BD10
	private void Start()
	{
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = false;
		}
	}

	// Token: 0x04000AF6 RID: 2806
	private Quaternion _rotationBefore = Quaternion.identity;

	// Token: 0x04000AF7 RID: 2807
	private bool _setRot;

	// Token: 0x04000AF8 RID: 2808
	public MouseLook.RotationAxes axes;

	// Token: 0x04000AF9 RID: 2809
	public float sensitivityX = 15f;

	// Token: 0x04000AFA RID: 2810
	public float sensitivityY = 15f;

	// Token: 0x04000AFB RID: 2811
	public float minimumX = -360f;

	// Token: 0x04000AFC RID: 2812
	public float maximumX = 360f;

	// Token: 0x04000AFD RID: 2813
	public float minimumY = -90f;

	// Token: 0x04000AFE RID: 2814
	public float maximumY = 90f;

	// Token: 0x04000AFF RID: 2815
	private float rotationY;

	// Token: 0x04000B00 RID: 2816
	private float rotationX;

	// Token: 0x04000B01 RID: 2817
	private float _mouseMoveTutorialTimer;

	// Token: 0x0200024D RID: 589
	public enum RotationAxes
	{
		// Token: 0x04000B03 RID: 2819
		MouseXAndY,
		// Token: 0x04000B04 RID: 2820
		MouseX,
		// Token: 0x04000B05 RID: 2821
		MouseY
	}
}
