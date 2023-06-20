using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class AbilitySelector : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x000020F4 File Offset: 0x000002F4
	private void OnEnable()
	{
		if (info.abilityName != string.Empty)
		{
			this.SelectAbility();
		}
		if (info.utilityName != string.Empty)
		{
			this.SelectAbility();
		}
		helpText.Instance().Init();
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002140 File Offset: 0x00000340
	private void Update()
	{
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002144 File Offset: 0x00000344
	private void disableAll()
	{
		if (this.movementManager.myAbility)
		{
			this.movementManager.myAbility.gameObject.SetActive(false);
		}
		if (this.movementManager.energySlider)
		{
			this.movementManager.energySlider.transform.parent.gameObject.SetActive(false);
		}
		this.movementManager.myAbility = null;
		if (this.utilityManager.myAbility)
		{
			this.utilityManager.myAbility.gameObject.SetActive(false);
		}
		if (this.utilityManager.energySlider)
		{
			this.utilityManager.energySlider.transform.parent.gameObject.SetActive(false);
		}
		this.utilityManager.myAbility = null;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000222C File Offset: 0x0000042C
	public void SelectAbility()
	{
		this.disableAll();
		bool flag = false;
		bool flag2 = false;
		string abilityName = info.abilityName;
		string utilityName = info.utilityName;
		if (utilityName == "trucksolute zero")
		{
			this.utilityManager.myAbility = this.a_TrucksoluteZero;
			flag2 = true;
		}
		if (abilityName == "trucker flip")
		{
			this.movementManager.myAbility = this.a_TruckFlip;
		}
		if (utilityName == "Time slow")
		{
			this.utilityManager.myAbility = this.a_TimeWatch;
			flag2 = true;
		}
		if (utilityName == "Epic mode")
		{
			this.utilityManager.myAbility = this.a_Epic;
			flag2 = true;
		}
		if (utilityName == "freeze")
		{
			this.utilityManager.myAbility = this.freeze;
			flag2 = true;
		}
		if (utilityName == "Portable truck")
		{
			this.utilityManager.myAbility = this.emergencyTruck;
			flag2 = true;
		}
		if (utilityName == "Truck cannon")
		{
			this.utilityManager.myAbility = this.truckCannon;
			flag2 = true;
		}
		if (abilityName == "Grappling hook")
		{
			this.movementManager.myAbility = this.grappling;
			flag = true;
		}
		if (abilityName == "truck boost")
		{
			this.movementManager.myAbility = this.a_truckBoost;
			flag = true;
		}
		if (abilityName == "back truck")
		{
			this.movementManager.myAbility = this.a_backTruck;
			flag = true;
		}
		if (abilityName == "Double jump")
		{
			this.movementManager.myAbility = this.a_DoubleJump;
		}
		if (abilityName == "air dash")
		{
			this.movementManager.myAbility = this.a_AirDash;
			flag = true;
		}
		if (abilityName == "jetpack")
		{
			this.movementManager.myAbility = this.a_JetPack;
			flag = true;
		}
		if (abilityName == "levitation")
		{
			this.movementManager.myAbility = this.a_levitate;
			flag = true;
		}
		if (abilityName == "surfing shoes")
		{
			this.movementManager.myAbility = this.a_powerLegs;
		}
		if (flag2 && PlayerPrefs.GetInt(utilityName + info.ABILITY_UTILITY_KEY, 0) != 1)
		{
			helpText.Instance().SetUtilityToolTip(this.utilityManager.myAbility);
		}
		if (flag && PlayerPrefs.GetInt(abilityName + info.ABILITY_MOVEMENT_KEY, 0) != 1)
		{
			helpText.Instance().SetMovementToolTip(this.movementManager.myAbility);
		}
		this.movementManager.Activate();
		this.utilityManager.Activate();
	}

	// Token: 0x04000001 RID: 1
	public abilityManager movementManager;

	// Token: 0x04000002 RID: 2
	public abilityManager utilityManager;

	// Token: 0x04000003 RID: 3
	public timeWatch a_TimeWatch;

	// Token: 0x04000004 RID: 4
	public moveMentAbility a_DoubleJump;

	// Token: 0x04000005 RID: 5
	public moveMentAbility a_AirDash;

	// Token: 0x04000006 RID: 6
	public moveMentAbility a_JetPack;

	// Token: 0x04000007 RID: 7
	public moveMentAbility a_levitate;

	// Token: 0x04000008 RID: 8
	public BackTrack a_backTruck;

	// Token: 0x04000009 RID: 9
	public TruckBooster a_truckBoost;

	// Token: 0x0400000A RID: 10
	public grapplingHook grappling;

	// Token: 0x0400000B RID: 11
	public weapon freeze;

	// Token: 0x0400000C RID: 12
	public weapon emergencyTruck;

	// Token: 0x0400000D RID: 13
	public weapon truckCannon;

	// Token: 0x0400000E RID: 14
	public epic a_Epic;

	// Token: 0x0400000F RID: 15
	public superHot a_SuperHot;

	// Token: 0x04000010 RID: 16
	public abilityOther a_powerLegs;

	// Token: 0x04000011 RID: 17
	public TrucksoluteZero a_TrucksoluteZero;

	// Token: 0x04000012 RID: 18
	public TruckerFlip a_TruckFlip;
}
