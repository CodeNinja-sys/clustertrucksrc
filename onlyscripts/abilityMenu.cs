using System;
using InControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000262 RID: 610
public class abilityMenu : MonoBehaviour
{
	// Token: 0x06000ECE RID: 3790 RVA: 0x0005FB1C File Offset: 0x0005DD1C
	private void Start()
	{
		this.CheckButtons();
		this.lastObject = base.gameObject;
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x0005FB30 File Offset: 0x0005DD30
	private void OnEnable()
	{
		this.infoText.text = "YOU CAN EQUIP ONE MOVEMENT AND ONE UTILITY ABILITY";
		this.nameText.text = "ABILITIES";
		if (!string.IsNullOrEmpty(info.CurrentMovementAbility))
		{
			this.usedM = this.MovementGrid.FindChild(info.CurrentMovementAbility);
			info.abilityName = ((!this.usedM) ? string.Empty : info.CurrentMovementAbility);
		}
		if (!string.IsNullOrEmpty(info.CurrentUtilityAbility))
		{
			this.usedU = ((!this.UtilityGrid.FindChild(info.CurrentUtilityAbility)) ? null : this.UtilityGrid.FindChild(info.CurrentUtilityAbility));
			info.utilityName = ((!this.usedU) ? string.Empty : info.CurrentUtilityAbility);
		}
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x0005FC10 File Offset: 0x0005DE10
	private void CheckButtons()
	{
		foreach (Button b in this.movementParent.GetComponentsInChildren<Button>())
		{
			this.CheckButton(b);
		}
		foreach (Button b2 in this.utilityParent.GetComponentsInChildren<Button>())
		{
			this.CheckButton(b2);
		}
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x0005FC7C File Offset: 0x0005DE7C
	private void CheckButton(Button b)
	{
		ColorBlock colors = default(ColorBlock);
		colors.pressedColor = new Color(1f, 1f, 1f, 1f);
		colors.colorMultiplier = 1f;
		if (b.GetComponent<abilityInfo>().Unlocked)
		{
			colors.normalColor = new Color(1f, 1f, 1f, 1f);
			colors.highlightedColor = new Color(1f, 1f, 1f, 0.7f);
			colors.disabledColor = new Color(1f, 1f, 1f, 1f);
			foreach (Text text in b.GetComponentsInChildren<Text>())
			{
				text.color = new Color(0.2f, 0.2f, 0.2f, 1f);
			}
			foreach (Image image in b.GetComponentsInChildren<Image>())
			{
				if (image.gameObject != b.gameObject)
				{
					image.color = new Color(0.2f, 0.2f, 0.2f, 1f);
				}
			}
		}
		else
		{
			colors.normalColor = new Color(0.4f, 0.4f, 0.4f, 0.5f);
			colors.highlightedColor = new Color(0.3f, 0.3f, 0.3f, 0.35f);
			colors.disabledColor = new Color(0.3f, 0.3f, 0.3f, 0.5f);
		}
		b.colors = colors;
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x0005FE38 File Offset: 0x0005E038
	private void Update()
	{
		this.Used++;
		if (this.usedM)
		{
			this.selector.enabled = true;
			this.selector.transform.position = this.usedM.position;
		}
		else
		{
			this.selector.enabled = false;
		}
		if (this.usedU)
		{
			this.selector2.enabled = true;
			this.selector2.transform.position = this.usedU.position;
		}
		else
		{
			this.selector2.enabled = false;
		}
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x0005FEE4 File Offset: 0x0005E0E4
	public void OnSubmit(BaseEventData p)
	{
		PointerEventData pointerEventData = p as PointerEventData;
		if (p.selectedObject.tag == "ability")
		{
			if (p.selectedObject.gameObject == this.lastObject)
			{
				if (p.selectedObject.GetComponent<abilityInfo>().Unlocked)
				{
					this.BuyOrEquip();
				}
			}
			else
			{
				this.selectedButton = p.selectedObject.GetComponent<abilityInfo>();
				this.myUI.changeAbility(this.selectedButton.infoField, this.selectedButton.AbilityCost.ToString());
			}
			InputDevice activeDevice = InputManager.ActiveDevice;
			if (activeDevice != null)
			{
				if (activeDevice.Action1.WasPressed)
				{
					if (this.pressedButtonController != null)
					{
						if (this.pressedButtonController == this.lastObject.GetComponent<abilityInfo>())
						{
							this.BuyOrEquip();
						}
						else
						{
							this.SelectAbilityController(this.selectedButton);
						}
					}
					else
					{
						this.SelectAbilityController(this.selectedButton);
					}
				}
				else
				{
					this.UnlockButton.GetComponentInChildren<Text>().text = "UNLOCK";
					this.pressedButtonController = null;
				}
			}
			this.lastObject = p.selectedObject.gameObject;
			if (this.selectedButton.Unlocked)
			{
				this.UnlockButton.GetComponent<Animator>().SetBool("active", false);
			}
			else
			{
				this.UnlockButton.GetComponent<Animator>().SetBool("active", true);
			}
		}
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x0006006C File Offset: 0x0005E26C
	private void SelectAbilityController(abilityInfo selectedButton)
	{
		if ((float)selectedButton.AbilityCost > pointsHandler.Points && !selectedButton.Unlocked)
		{
			Debug.Log("NOPE MOFO!");
			this.UnlockButton.GetComponent<Animator>().Play("unlockNope", -1, 0f);
			this.au.PlayOneShot(this.nope);
			Debug.Log("PLAY THE THING");
			return;
		}
		if (!selectedButton.Unlocked)
		{
			this.UnlockButton.GetComponent<Animator>().Play("unlockShake");
			this.UnlockButton.GetComponentInChildren<Text>().text = "CONFIRM?";
			this.pressedButtonController = selectedButton;
		}
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x00060114 File Offset: 0x0005E314
	public void BuyOrEquip()
	{
		if (this.Used < 10)
		{
			return;
		}
		this.Used = 0;
		if (this.selectedButton.infoField[0] == "name")
		{
			this.au.PlayOneShot(this.nope);
		}
		else
		{
			if ((float)this.selectedButton.AbilityCost > pointsHandler.Points && !this.selectedButton.Unlocked)
			{
				this.UnlockButton.GetComponent<Animator>().Play("unlockNope", -1, 0f);
				this.au.PlayOneShot(this.nope);
				return;
			}
			if (!this.selectedButton.Unlocked)
			{
				this.selectedButton.UnlockMe();
				pointsHandler.AddPoints((float)(-(float)this.selectedButton.AbilityCost));
			}
			this.CheckButtons();
			if (info.abilityName == this.selectedButton.infoField[0])
			{
				info.abilityName = string.Empty;
				PlayerPrefs.SetString(info.ABILITY_MOVEMENT_KEY, string.Empty);
				this.usedM = null;
			}
			else if (info.utilityName == this.selectedButton.infoField[0])
			{
				info.utilityName = string.Empty;
				PlayerPrefs.SetString(info.ABILITY_UTILITY_KEY, string.Empty);
				this.usedU = null;
			}
			else if (this.selectedButton.movement)
			{
				info.abilityName = this.selectedButton.infoField[0];
				PlayerPrefs.SetString(info.ABILITY_MOVEMENT_KEY, this.selectedButton.name);
				this.usedM = this.selectedButton.transform;
			}
			else
			{
				info.utilityName = this.selectedButton.infoField[0];
				PlayerPrefs.SetString(info.ABILITY_UTILITY_KEY, this.selectedButton.name);
				this.usedU = this.selectedButton.transform;
			}
			this.au.PlayOneShot(this.equip);
			if (this.selectedButton.Unlocked)
			{
				this.UnlockButton.GetComponent<Animator>().SetBool("active", false);
			}
		}
	}

	// Token: 0x04000B64 RID: 2916
	public Text nameText;

	// Token: 0x04000B65 RID: 2917
	public Text infoText;

	// Token: 0x04000B66 RID: 2918
	public UI_Slide myUI;

	// Token: 0x04000B67 RID: 2919
	public abilityInfo selectedButton;

	// Token: 0x04000B68 RID: 2920
	public abilityInfo pressedButtonController;

	// Token: 0x04000B69 RID: 2921
	public Transform usedM;

	// Token: 0x04000B6A RID: 2922
	public Transform usedU;

	// Token: 0x04000B6B RID: 2923
	public Image selector;

	// Token: 0x04000B6C RID: 2924
	public Image selector2;

	// Token: 0x04000B6D RID: 2925
	public AudioSource au;

	// Token: 0x04000B6E RID: 2926
	public AudioClip unlock;

	// Token: 0x04000B6F RID: 2927
	public AudioClip nope;

	// Token: 0x04000B70 RID: 2928
	public AudioClip equip;

	// Token: 0x04000B71 RID: 2929
	public Transform movementParent;

	// Token: 0x04000B72 RID: 2930
	public Transform utilityParent;

	// Token: 0x04000B73 RID: 2931
	public Button UnlockButton;

	// Token: 0x04000B74 RID: 2932
	[SerializeField]
	private Transform MovementGrid;

	// Token: 0x04000B75 RID: 2933
	[SerializeField]
	private Transform UtilityGrid;

	// Token: 0x04000B76 RID: 2934
	private GameObject lastObject;

	// Token: 0x04000B77 RID: 2935
	private int Used;
}
