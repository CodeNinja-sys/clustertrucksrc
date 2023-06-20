using System;
using InControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002B0 RID: 688
public class selectMe : MonoBehaviour
{
	// Token: 0x0600105D RID: 4189 RVA: 0x0006AA3C File Offset: 0x00068C3C
	private void Start()
	{
		this.mouseCast = base.transform.root.GetComponentInChildren<GraphicRaycaster>();
		this.bar = UnityEngine.Object.FindObjectOfType<menuBar>();
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x0006AA6C File Offset: 0x00068C6C
	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Mouse0) || Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.1f)
		{
			selectMe.mouseMovement = 1f;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			this.mouseCast.enabled = true;
		}
		if (InputManager.ActiveDevice.LeftStick.Vector.magnitude > 0.1f || InputManager.ActiveDevice.AnyButton.WasPressed)
		{
			this.controlMovement = true;
			Cursor.visible = false;
			this.mouseCast.enabled = false;
			if (EventSystem.current.currentSelectedGameObject == null)
			{
				this.active = false;
			}
		}
		if (!this.active || (this.controlMovement && !this.hasUsedControl))
		{
			if (selectMe.mouseMovement < 0f)
			{
				bool flag = true;
				if (this.bar && this.bar.secondObject == base.gameObject)
				{
					flag = false;
				}
				if (flag)
				{
					if ((!this.dontOverTurn || EventSystem.current.currentSelectedGameObject == null) && (!base.GetComponent<Button>() || base.GetComponent<Button>().interactable))
					{
						EventSystem.current.SetSelectedGameObject(null);
						EventSystem.current.SetSelectedGameObject(base.gameObject);
					}
					this.controlMovement = false;
					this.hasUsedControl = true;
					Cursor.visible = false;
				}
			}
			this.active = true;
		}
		selectMe.mouseMovement -= Time.unscaledDeltaTime;
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x0006AC44 File Offset: 0x00068E44
	private void OnDisable()
	{
		this.controlMovement = false;
		this.active = false;
		this.hasUsedControl = false;
	}

	// Token: 0x04000D66 RID: 3430
	private bool active;

	// Token: 0x04000D67 RID: 3431
	private bool controlMovement;

	// Token: 0x04000D68 RID: 3432
	private bool hasUsedControl;

	// Token: 0x04000D69 RID: 3433
	public static float mouseMovement;

	// Token: 0x04000D6A RID: 3434
	private menuBar bar;

	// Token: 0x04000D6B RID: 3435
	private GraphicRaycaster mouseCast;

	// Token: 0x04000D6C RID: 3436
	public bool dontOverTurn;
}
