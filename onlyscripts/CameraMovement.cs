using System;
using UnityEngine;

// Token: 0x020001E6 RID: 486
public class CameraMovement : MonoBehaviour
{
	// Token: 0x06000B72 RID: 2930 RVA: 0x00047184 File Offset: 0x00045384
	private void Awake()
	{
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x00047188 File Offset: 0x00045388
	private void Start()
	{
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x0004718C File Offset: 0x0004538C
	private void Update()
	{
		if (levelEditorManager.Instance().IsBusy)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.C) && levelEditorManager.Instance().ListenForHotkeys)
		{
			this.switchCameraMovementType();
		}
		if (Input.GetKey(KeyCode.LeftShift))
		{
			this.speed = 20f;
		}
		else
		{
			this.speed = 10f;
		}
		if (Input.GetMouseButtonDown(2))
		{
		}
		if (Input.GetMouseButton(2))
		{
			Debug.Log("Holding Middle Mouse");
			base.transform.Translate(new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y")));
		}
		if (levelEditorManager.canLook && CameraMovement._currentMovementType == CameraMovement.CameraMovementType.freeLook)
		{
			if (Input.GetKey(KeyCode.A))
			{
				this.movementAcc += -base.transform.right * Time.deltaTime * this.speed;
				if (TutorialHandler.Instance.SpecialEvents[4].gameObject.activeInHierarchy)
				{
					this._WASDTutorialTimer += Time.deltaTime;
				}
			}
			if (Input.GetKey(KeyCode.D))
			{
				this.movementAcc += base.transform.right * Time.deltaTime * this.speed;
				if (TutorialHandler.Instance.SpecialEvents[4].gameObject.activeInHierarchy)
				{
					this._WASDTutorialTimer += Time.deltaTime;
				}
			}
			if (Input.GetKey(KeyCode.W))
			{
				this.movementAcc += new Vector3(base.transform.forward.x, 0f, base.transform.forward.z) * Time.deltaTime * this.speed;
				if (TutorialHandler.Instance.SpecialEvents[4].gameObject.activeInHierarchy)
				{
					this._WASDTutorialTimer += Time.deltaTime;
				}
			}
			if (Input.GetKey(KeyCode.S))
			{
				this.movementAcc += -new Vector3(base.transform.forward.x, 0f, base.transform.forward.z) * Time.deltaTime * this.speed;
				if (TutorialHandler.Instance.SpecialEvents[4].gameObject.activeInHierarchy)
				{
					this._WASDTutorialTimer += Time.deltaTime;
				}
			}
			if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.E))
			{
				this.movementAcc += Vector3.up * Time.deltaTime * this.speed;
				if (TutorialHandler.Instance.SpecialEvents[5].gameObject.activeInHierarchy)
				{
					this._QETutorialTimer += Time.deltaTime;
				}
			}
			if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Q))
			{
				this.movementAcc += -Vector3.up * Time.deltaTime * this.speed;
				if (TutorialHandler.Instance.SpecialEvents[5].gameObject.activeInHierarchy)
				{
					this._QETutorialTimer += Time.deltaTime;
				}
			}
			base.transform.Translate(this.movementAcc, Space.World);
			if (this._WASDTutorialTimer >= 0.5f)
			{
				TutorialHandler.Instance.SpecialEvents[4].Clicked();
			}
			if (this._QETutorialTimer >= 0.5f)
			{
				TutorialHandler.Instance.SpecialEvents[5].Clicked();
			}
			if (Input.GetMouseButton(0))
			{
			}
		}
		else if (levelEditorManager.canLook && CameraMovement._currentMovementType == CameraMovement.CameraMovementType.topDown)
		{
			if (Input.GetKey(KeyCode.A))
			{
				this.movementAcc += -Vector3.right * Time.deltaTime * this.speed;
			}
			if (Input.GetKey(KeyCode.D))
			{
				this.movementAcc += Vector3.right * Time.deltaTime * this.speed;
			}
			if (Input.GetKey(KeyCode.W))
			{
				this.movementAcc += new Vector3(Vector3.forward.x, 0f, Vector3.forward.z) * Time.deltaTime * this.speed;
			}
			if (Input.GetKey(KeyCode.S))
			{
				this.movementAcc += -new Vector3(Vector3.forward.x, 0f, Vector3.forward.z) * Time.deltaTime * this.speed;
			}
			if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.E))
			{
				this.movementAcc += Vector3.up * Time.deltaTime * this.speed;
			}
			if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Q))
			{
				this.movementAcc += -Vector3.up * Time.deltaTime * this.speed;
			}
			base.transform.Translate(this.movementAcc, Space.World);
			if (Input.GetMouseButton(1))
			{
				Debug.Log("Holding Rigjht Mouse");
				base.transform.Translate(new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y")));
			}
		}
		if (levelEditorManager.Instance().getTargetedGameObject() != null)
		{
			return;
		}
		float d = Input.GetAxis("Mouse ScrollWheel") * (5f + -this._camera.transform.localPosition.z / 20f);
		this._camera.transform.Translate(this._camera.transform.forward * d * 10f, Space.World);
		if (CameraMovement._currentMovementType == CameraMovement.CameraMovementType.freeLook)
		{
			this._camera.transform.localPosition = new Vector3(0f, 0f, Mathf.Clamp(this._camera.transform.localPosition.z, float.NegativeInfinity, -2f));
		}
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x0004787C File Offset: 0x00045A7C
	private void FixedUpdate()
	{
		this.movementAcc *= this.friction;
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x00047898 File Offset: 0x00045A98
	public void switchCameraMovementType()
	{
		if (CameraMovement._currentMovementType == CameraMovement.CameraMovementType.freeLook)
		{
			CameraMovement._currentMovementType = CameraMovement.CameraMovementType.topDown;
		}
		else
		{
			CameraMovement._currentMovementType = CameraMovement.CameraMovementType.freeLook;
		}
	}

	// Token: 0x04000818 RID: 2072
	public static CameraMovement.CameraMovementType _currentMovementType;

	// Token: 0x04000819 RID: 2073
	private Vector3 direction;

	// Token: 0x0400081A RID: 2074
	private float speed = 20f;

	// Token: 0x0400081B RID: 2075
	private float friction = 0.7f;

	// Token: 0x0400081C RID: 2076
	private Vector3 movementAcc = new Vector3(0f, 0f, 0f);

	// Token: 0x0400081D RID: 2077
	public GameObject _camera;

	// Token: 0x0400081E RID: 2078
	private float _WASDTutorialTimer;

	// Token: 0x0400081F RID: 2079
	private float _QETutorialTimer;

	// Token: 0x020001E7 RID: 487
	public enum CameraMovementType
	{
		// Token: 0x04000821 RID: 2081
		freeLook,
		// Token: 0x04000822 RID: 2082
		topDown
	}
}
