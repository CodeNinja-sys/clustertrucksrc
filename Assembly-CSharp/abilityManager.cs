using System;
using InControl;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000261 RID: 609
public class abilityManager : MonoBehaviour
{
	// Token: 0x06000EC7 RID: 3783 RVA: 0x0005EFFC File Offset: 0x0005D1FC
	private void Start()
	{
		this.myPlayer = UnityEngine.Object.FindObjectOfType<player>();
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x0005F00C File Offset: 0x0005D20C
	public void Activate()
	{
		if (!this.second)
		{
			info.extraAirTime = 0f;
		}
		if (!this.myAbility)
		{
			return;
		}
		if (!this.second)
		{
			info.extraAirTime = this.myAbility.extraAirTime;
		}
		foreach (Transform transform in this.dissableThese.GetComponentsInChildren<Transform>())
		{
			if (transform.transform.parent == this.dissableThese && this.second != transform.GetComponent<AbilityTag>().movement)
			{
				transform.gameObject.SetActive(false);
			}
		}
		foreach (Transform transform2 in this.andThese.GetComponentsInChildren<Transform>())
		{
			if (transform2.transform.parent == this.andThese && this.second != transform2.GetComponent<AbilityTag>().movement)
			{
				transform2.gameObject.SetActive(false);
			}
		}
		this.counter = 0f;
		if (this.myAbility.uiStuff != null)
		{
			this.myAbility.uiStuff.SetActive(true);
		}
		this.useTime = this.myAbility.useTime;
		this.rechargeTime = this.myAbility.rechargeTime;
		this.charges = this.myAbility.charges;
		this.myAbility.gameObject.SetActive(true);
		if (this.energySlider)
		{
			this.energySlider.transform.parent.gameObject.SetActive(true);
			if (this.myAbility.myType == AbilityBaseClass.type.energy)
			{
				this.energySlider.value = this.energy;
				this.energySlider.maxValue = 1f;
			}
			if (this.myAbility.myType == AbilityBaseClass.type.cooldown)
			{
				this.energySlider.value = this.rechargeTime;
				this.energySlider.maxValue = this.rechargeTime;
			}
			this.SliderHandle = this.energySlider.handleRect.GetComponent<Image>();
			this.SliderFill = this.energySlider.fillRect.GetComponent<Image>();
			this.SliderBackground = this.energySlider.transform.FindChild("Background").GetComponent<Image>();
		}
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x0005F278 File Offset: 0x0005D478
	private void Update()
	{
		if (!info.playing)
		{
			return;
		}
		this.counter += 1f;
		if (this.counter < 10f)
		{
			return;
		}
		this.tapFloat += Time.deltaTime;
		bool flag = false;
		if ((((Input.GetKey(KeyCode.Mouse0) || InputManager.ActiveDevice.LeftBumper.IsPressed) && this.myAbility.canHold) || Input.GetKeyDown(KeyCode.Mouse0) || InputManager.ActiveDevice.LeftBumper.WasPressed) && !this.second)
		{
			flag = true;
		}
		if ((Input.GetKeyUp(KeyCode.Mouse0) || InputManager.ActiveDevice.LeftBumper.WasReleased) && !this.second)
		{
			this.wantToStop = true;
		}
		if ((((Input.GetKey(KeyCode.Mouse1) || InputManager.ActiveDevice.RightBumper.IsPressed) && this.myAbility.canHold) || Input.GetKeyDown(KeyCode.Mouse1) || InputManager.ActiveDevice.RightBumper.WasPressed) && this.second)
		{
			flag = true;
		}
		if ((Input.GetKeyUp(KeyCode.Mouse1) || InputManager.ActiveDevice.RightBumper.WasReleased) && this.second)
		{
			this.wantToStop = true;
		}
		if (this.myAbility == null)
		{
			return;
		}
		if ((this.myAbility.hasToBeGrounded && (!this.play.hasTouchedGround || this.play.lastGrounded > 0.1f)) || info.paused || this.play.frozen)
		{
			flag = false;
		}
		this.UpdateSlider();
		this.cd += Time.deltaTime;
		bool flag2 = false;
		if (!info.playing || info.paused || !info.playing)
		{
			flag2 = true;
		}
		if (this.myAbility.myType == AbilityBaseClass.type.energy)
		{
			if (flag && !flag2)
			{
				this.myAbility.Go();
				this.isGoing = true;
				this.tapFloat = 0f;
				this.wantToStop = false;
			}
			if ((this.wantToStop && this.tapFloat > this.myAbility.overTime) || this.energy < 0f || flag2)
			{
				this.myAbility.Stop();
				this.isGoing = false;
			}
			if (this.isGoing)
			{
				this.energy -= Time.deltaTime / this.useTime;
				this.cd = 0f;
			}
			else if (this.energy < 1f && this.cd > 1f)
			{
				this.energy += Time.deltaTime / this.rechargeTime;
			}
		}
		if (this.myAbility.myType == AbilityBaseClass.type.cooldown)
		{
			if (this.cd > this.rechargeTime)
			{
				this.canUse = true;
			}
			if (flag && this.canUse)
			{
				this.myAbility.Go();
				this.canUse = false;
				this.cd = 0f;
			}
		}
		if (this.myAbility.myType == AbilityBaseClass.type.oncePerJump)
		{
			if (this.myPlayer.hasTouchedGround)
			{
				this.canUse = true;
				this.cd = 0f;
			}
			if ((Input.GetKeyDown(KeyCode.Space) || InputManager.ActiveDevice.Action1.WasPressed) && !this.myPlayer.hasTouchedGround && this.canUse && this.cd > 0.1f)
			{
				this.myAbility.Go();
				this.canUse = false;
			}
		}
		if (this.myAbility.myType == AbilityBaseClass.type.replaceJump && (Input.GetKey(KeyCode.Space) || InputManager.ActiveDevice.Action1.IsPressed) && this.myPlayer.hasTouchedGround)
		{
			this.myAbility.Go();
		}
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0005F6C4 File Offset: 0x0005D8C4
	public void OnEnable()
	{
		if (info.abilityName == string.Empty && !this.second)
		{
			return;
		}
		if (info.utilityName == string.Empty && this.second)
		{
			return;
		}
		this.cd = 100f;
		this.myAbility.Stop();
		this.isGoing = false;
		this.energy = 1f;
		if (this.energySlider)
		{
			this.energySlider.value = 1f;
		}
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x0005F75C File Offset: 0x0005D95C
	private void UpdateSlider()
	{
		if (this.myAbility.myType == AbilityBaseClass.type.energy)
		{
			this.energySlider.value = Mathf.Lerp(this.energySlider.value, this.energy, Time.deltaTime * 4f);
			if (this.energy < 1f)
			{
				this.LerpToColor(true);
			}
			else
			{
				this.LerpToColor(false);
			}
		}
		if (this.myAbility.myType == AbilityBaseClass.type.cooldown)
		{
			float num = 4f;
			if (this.cd < 0.5f)
			{
				num = 15f;
			}
			this.energySlider.value = Mathf.Lerp(this.energySlider.value, this.cd, Time.unscaledDeltaTime * num);
			if (this.cd < this.rechargeTime)
			{
				this.LerpToColor(true);
			}
			else
			{
				this.LerpToColor(false);
			}
		}
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x0005F844 File Offset: 0x0005DA44
	private void LerpToColor(bool toColor)
	{
		float num = 5f;
		if (toColor)
		{
			this.SliderHandle.color = new Color(this.SliderHandle.color.r, this.SliderHandle.color.g, this.SliderHandle.color.b, Mathf.Lerp(this.SliderHandle.color.a, 1f, Time.unscaledDeltaTime * num));
			this.SliderFill.color = new Color(this.SliderFill.color.r, this.SliderFill.color.g, this.SliderFill.color.b, Mathf.Lerp(this.SliderFill.color.a, 1f, Time.unscaledDeltaTime * num));
			this.SliderBackground.color = new Color(this.SliderBackground.color.r, this.SliderBackground.color.g, this.SliderBackground.color.b, Mathf.Lerp(this.SliderBackground.color.a, 0.5f, Time.unscaledDeltaTime * num * 0.7f));
		}
		else
		{
			this.SliderHandle.color = new Color(this.SliderHandle.color.r, this.SliderHandle.color.g, this.SliderHandle.color.b, Mathf.Lerp(this.SliderHandle.color.a, 0f, Time.unscaledDeltaTime * num));
			this.SliderFill.color = new Color(this.SliderFill.color.r, this.SliderFill.color.g, this.SliderFill.color.b, Mathf.Lerp(this.SliderFill.color.a, 0f, Time.unscaledDeltaTime * num));
			this.SliderBackground.color = new Color(this.SliderBackground.color.r, this.SliderBackground.color.g, this.SliderBackground.color.b, Mathf.Lerp(this.SliderBackground.color.a, 0f, Time.unscaledDeltaTime * num * 0.7f));
		}
	}

	// Token: 0x04000B50 RID: 2896
	public AbilityBaseClass myAbility;

	// Token: 0x04000B51 RID: 2897
	private player myPlayer;

	// Token: 0x04000B52 RID: 2898
	private float energy = 1f;

	// Token: 0x04000B53 RID: 2899
	private Image SliderHandle;

	// Token: 0x04000B54 RID: 2900
	private Image SliderFill;

	// Token: 0x04000B55 RID: 2901
	private Image SliderBackground;

	// Token: 0x04000B56 RID: 2902
	public Slider energySlider;

	// Token: 0x04000B57 RID: 2903
	private bool isGoing;

	// Token: 0x04000B58 RID: 2904
	public player play;

	// Token: 0x04000B59 RID: 2905
	private float useTime = 2f;

	// Token: 0x04000B5A RID: 2906
	private float rechargeTime = 2f;

	// Token: 0x04000B5B RID: 2907
	private int charges = 3;

	// Token: 0x04000B5C RID: 2908
	public float cd = 100f;

	// Token: 0x04000B5D RID: 2909
	private bool canUse = true;

	// Token: 0x04000B5E RID: 2910
	public bool second;

	// Token: 0x04000B5F RID: 2911
	private float tapFloat = 1f;

	// Token: 0x04000B60 RID: 2912
	private bool wantToStop;

	// Token: 0x04000B61 RID: 2913
	private float counter;

	// Token: 0x04000B62 RID: 2914
	public Transform dissableThese;

	// Token: 0x04000B63 RID: 2915
	public Transform andThese;
}
