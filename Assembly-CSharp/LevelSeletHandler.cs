using System;
using InControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000242 RID: 578
public class LevelSeletHandler : MonoBehaviour
{
	// Token: 0x06000E0E RID: 3598 RVA: 0x0005BBB8 File Offset: 0x00059DB8
	private void Start()
	{
		if (info.completedLevels > 0)
		{
			this.ChangeWorld(info.currentlyPlayedWorld - 1);
			char[] array = info.currentLevel.ToString().ToCharArray();
			int levelNr = (info.currentLevel % 10 != 0) ? ((array.Length <= 1) ? int.Parse(array[0].ToString()) : int.Parse(array[1].ToString())) : 10;
			this.MoveToButton(true, levelNr);
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(this.buttons.transform.FindChild(levelNr.ToString()).gameObject);
		}
		else
		{
			this.ChangeWorld(0);
			EventSystem.current.SetSelectedGameObject(null);
			EventSystem.current.SetSelectedGameObject(this.buttons.transform.FindChild(1.ToString()).gameObject);
		}
		this.rightAnim.Play("rightAppear");
		this.select.position = this.firstLevel.position;
		this.setButtons();
		Debug.Log(info.currentLevel);
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x0005BCE8 File Offset: 0x00059EE8
	private void OpenBeatWorld()
	{
		this.select.position = this.lastLevel.position;
		this.selectTarget = this.lastLevel.position;
		EventSystem.current.SetSelectedGameObject(null);
		EventSystem.current.SetSelectedGameObject(this.lastLevel.gameObject);
		this.setButtons();
		this.beatWorld = false;
		this.rightAnim.Play("rightAppear");
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x0005BD5C File Offset: 0x00059F5C
	private void Update()
	{
		InputDevice activeDevice = InputManager.ActiveDevice;
		if (Input.GetKeyDown(KeyCode.Escape) || (activeDevice && activeDevice.Action2.WasPressed))
		{
			Manager.Instance().OpenMenu();
			return;
		}
		if (this.selectTarget != Vector3.zero)
		{
			this.select.position = Vector3.Lerp(this.select.position, this.selectTarget, Time.deltaTime * 10f);
		}
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x0005BDE8 File Offset: 0x00059FE8
	public void OnSubmit(BaseEventData b)
	{
		if (b.selectedObject.transform.tag == "levelButton")
		{
			if (int.Parse(b.selectedObject.name) + info.currentWorld * 10 <= info.completedLevels + 1)
			{
				if (this._currentlySelectedLevel == null)
				{
					this._currentlySelectedLevel = b.selectedObject;
				}
				else if (this._currentlySelectedLevel == b.selectedObject)
				{
					Manager.Instance().Play();
				}
				else
				{
					this._currentlySelectedLevel = b.selectedObject;
				}
				this.selectTarget = b.selectedObject.transform.position;
				info.currentLevel = int.Parse(b.selectedObject.name) + info.currentWorld * 10;
			}
			else
			{
				this.selectShake.SetShake(10f);
			}
		}
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x0005BED8 File Offset: 0x0005A0D8
	private void OnEnable()
	{
		if (this.beatWorld)
		{
			this.OpenBeatWorld();
			return;
		}
		this.Start();
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x0005BEF4 File Offset: 0x0005A0F4
	public void setButtons()
	{
		foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("levelButton"))
		{
			if (int.Parse(gameObject.name) + info.currentWorld * 10 <= info.completedLevels)
			{
				gameObject.transform.FindChild("box").gameObject.SetActive(true);
				gameObject.transform.FindChild("road").GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
				gameObject.transform.FindChild("box").GetComponent<blink>().enabled = false;
				gameObject.transform.FindChild("box").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
			}
			else if (int.Parse(gameObject.name) + info.currentWorld * 10 <= info.completedLevels + 1)
			{
				gameObject.transform.FindChild("road").GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.2f);
				gameObject.transform.FindChild("box").gameObject.SetActive(true);
				gameObject.transform.FindChild("box").GetComponent<blink>().enabled = true;
			}
			else
			{
				gameObject.transform.FindChild("box").gameObject.SetActive(false);
				gameObject.transform.FindChild("road").GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.2f);
			}
		}
		this.right.SetActive(false);
		this.left.SetActive(false);
		if (info.currentProgressWorld > info.currentWorld + 1)
		{
			this.right.SetActive(true);
		}
		if (info.currentWorld > 0)
		{
			this.left.SetActive(true);
		}
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x0005C10C File Offset: 0x0005A30C
	private void ChangeInfo()
	{
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x0005C110 File Offset: 0x0005A310
	public void MoveWorld(bool forward)
	{
		if (forward)
		{
			this.ChangeWorld(info.currentWorld + 1);
			info.currentLevel = (info.currentWorld + 1) * 10 - 9;
			this.selectTarget = this.firstLevel.position;
			this.select.position = this.firstLevel.position;
			UnityEngine.Object.FindObjectOfType<EventSystem>().SetSelectedGameObject(null);
			UnityEngine.Object.FindObjectOfType<EventSystem>().SetSelectedGameObject(this.buttons.transform.FindChild("1").gameObject);
		}
		else
		{
			this.ChangeWorld(info.currentWorld - 1);
			info.currentLevel = (info.currentWorld + 1) * 10;
			this.selectTarget = this.lastLevel.position;
			this.select.position = this.lastLevel.position;
			UnityEngine.Object.FindObjectOfType<EventSystem>().SetSelectedGameObject(null);
			UnityEngine.Object.FindObjectOfType<EventSystem>().SetSelectedGameObject(this.buttons.transform.FindChild("10").gameObject);
		}
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0005C210 File Offset: 0x0005A410
	public void ChangeWorld(int i)
	{
		this._currentlySelectedLevel = null;
		info.currentWorld = i;
		foreach (GameObject gameObject in this.worlds)
		{
			gameObject.SetActive(false);
		}
		this.worlds[info.currentWorld].SetActive(true);
		this.selectTarget = this.firstLevel.position;
		this.setButtons();
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x0005C27C File Offset: 0x0005A47C
	private void MoveToButton(bool hard, int levelNr)
	{
		if (hard)
		{
			Vector3 position = this.buttons.transform.FindChild(levelNr.ToString()).position;
			this.select.position = position;
			this.selectTarget = position;
		}
	}

	// Token: 0x04000AA9 RID: 2729
	public Transform select;

	// Token: 0x04000AAA RID: 2730
	public Transform firstLevel;

	// Token: 0x04000AAB RID: 2731
	public Transform lastLevel;

	// Token: 0x04000AAC RID: 2732
	private GameObject _currentlySelectedLevel;

	// Token: 0x04000AAD RID: 2733
	private Vector3 selectTarget;

	// Token: 0x04000AAE RID: 2734
	public bool beatWorld;

	// Token: 0x04000AAF RID: 2735
	public shakeObject selectShake;

	// Token: 0x04000AB0 RID: 2736
	public GameObject[] worlds;

	// Token: 0x04000AB1 RID: 2737
	public Text worldText;

	// Token: 0x04000AB2 RID: 2738
	public Text worldTextUnder;

	// Token: 0x04000AB3 RID: 2739
	public GameObject left;

	// Token: 0x04000AB4 RID: 2740
	public GameObject right;

	// Token: 0x04000AB5 RID: 2741
	public GameObject buttons;

	// Token: 0x04000AB6 RID: 2742
	public Animator rightAnim;

	// Token: 0x04000AB7 RID: 2743
	private float counter;
}
