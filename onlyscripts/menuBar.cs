using System;
using InControl;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000293 RID: 659
public class menuBar : MonoBehaviour
{
	// Token: 0x06000FC9 RID: 4041 RVA: 0x00066520 File Offset: 0x00064720
	private void Start()
	{
	}

	// Token: 0x06000FCA RID: 4042 RVA: 0x00066524 File Offset: 0x00064724
	private void Update()
	{
		InputDevice activeDevice = InputManager.ActiveDevice;
		if ((Input.GetKeyDown(KeyCode.Escape) || (activeDevice && activeDevice.Action2.WasPressed)) && this.firstObject.GetComponent<MenuInfo>() != null)
		{
			this.ChangeMenu(this.firstObject.GetComponent<MenuInfo>().back);
		}
		Vector3 vector = Vector3.zero;
		if (this.currentlyLeft)
		{
			vector = Vector3.Lerp(base.transform.position, new Vector3(-this.pos, 0f, 0f), Time.deltaTime * this.speed);
		}
		else
		{
			vector = Vector3.Lerp(base.transform.position, new Vector3(this.pos, 0f, 0f), Time.deltaTime * this.speed);
		}
		float num = Vector3.Distance(base.transform.position, vector) * 20f;
		base.transform.position = vector;
		this.bar.localScale = new Vector3(num + 1f, 1f, 1f);
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x00066650 File Offset: 0x00064850
	public void ChangeMenuShowbuild()
	{
		Manager.Instance().NewGame();
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x0006665C File Offset: 0x0006485C
	public void ChangeMenu(GameObject go)
	{
		this.soundPlayer.Swoosh();
		if (go != this.firstObject)
		{
			if (this.secondObject != null)
			{
				this.secondObject.SetActive(false);
				this.secondObject.transform.SetParent(base.transform.parent, true);
			}
			this.secondObject = this.firstObject;
			this.secondObject.SendMessage("DisableMenu", SendMessageOptions.DontRequireReceiver);
			foreach (Button button in this.secondObject.GetComponentsInChildren<Button>())
			{
				button.interactable = false;
			}
			foreach (Scrollbar scrollbar in this.secondObject.GetComponentsInChildren<Scrollbar>())
			{
				scrollbar.interactable = false;
			}
			foreach (Slider slider in this.secondObject.GetComponentsInChildren<Slider>())
			{
				slider.interactable = false;
			}
			this.firstObject = go;
			this.firstObject.SetActive(true);
			go.SendMessage("EnableMenu", SendMessageOptions.DontRequireReceiver);
			foreach (Button button2 in this.firstObject.GetComponentsInChildren<Button>())
			{
				if (!button2.GetComponent<DontActivateMe>())
				{
					button2.interactable = true;
				}
			}
			foreach (Scrollbar scrollbar2 in this.firstObject.GetComponentsInChildren<Scrollbar>())
			{
				scrollbar2.interactable = true;
			}
			foreach (Slider slider2 in this.firstObject.GetComponentsInChildren<Slider>())
			{
				slider2.interactable = true;
			}
			this.currentlyLeft = !this.currentlyLeft;
			if (!this.currentlyLeft)
			{
				this.firstObject.transform.SetParent(this.leftMask, true);
			}
			else
			{
				this.firstObject.transform.SetParent(this.rightMask, true);
			}
		}
	}

	// Token: 0x04000C98 RID: 3224
	private bool currentlyLeft;

	// Token: 0x04000C99 RID: 3225
	private float speed = 4f;

	// Token: 0x04000C9A RID: 3226
	private float pos = 2.5f;

	// Token: 0x04000C9B RID: 3227
	public Transform leftMask;

	// Token: 0x04000C9C RID: 3228
	public Transform rightMask;

	// Token: 0x04000C9D RID: 3229
	public Transform bar;

	// Token: 0x04000C9E RID: 3230
	public GameObject firstObject;

	// Token: 0x04000C9F RID: 3231
	public GameObject secondObject;

	// Token: 0x04000CA0 RID: 3232
	public GameObject main;

	// Token: 0x04000CA1 RID: 3233
	public GameObject abMenu;

	// Token: 0x04000CA2 RID: 3234
	public bool hasStarted;

	// Token: 0x04000CA3 RID: 3235
	public AudioSource au;

	// Token: 0x04000CA4 RID: 3236
	public menuSound soundPlayer;
}
