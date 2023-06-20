using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E1 RID: 737
public class TrucksoluteZero : AbilityBaseClass
{
	// Token: 0x06001154 RID: 4436 RVA: 0x000708E4 File Offset: 0x0006EAE4
	public override string getToolTip()
	{
		return this._toolTip;
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x000708EC File Offset: 0x0006EAEC
	public override string getToolTipController()
	{
		return this._toolTipController;
	}

	// Token: 0x06001156 RID: 4438 RVA: 0x000708F4 File Offset: 0x0006EAF4
	private void Start()
	{
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x000708F8 File Offset: 0x0006EAF8
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			this.Go();
		}
		if (Input.GetKeyUp(KeyCode.B))
		{
			this.Stop();
		}
		if (this.isGoing)
		{
		}
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x0007093C File Offset: 0x0006EB3C
	public override void Stop()
	{
		if (!this.isGoing)
		{
			return;
		}
		Debug.Log("TrucksoluteZero STOP");
		this.isGoing = false;
		for (int i = 0; i < this.mTruckStates.Count; i++)
		{
			this.mTruckStates[i].unFreeze();
		}
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x00070994 File Offset: 0x0006EB94
	public override void Go()
	{
		Debug.Log("TrucksoluteZero GO");
		if (this.isGoing)
		{
			return;
		}
		this.isGoing = true;
		this.mTruckStates = new List<TrucksoluteZero.RigidState>();
		car[] array = UnityEngine.Object.FindObjectsOfType<car>();
		for (int i = 0; i < array.Length; i++)
		{
			Rigidbody[] componentsInChildren = array[i].GetComponentsInChildren<Rigidbody>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				this.mTruckStates.Add(new TrucksoluteZero.RigidState(componentsInChildren[j]));
			}
		}
	}

	// Token: 0x04000E73 RID: 3699
	private bool isGoing;

	// Token: 0x04000E74 RID: 3700
	private List<TrucksoluteZero.RigidState> mTruckStates = new List<TrucksoluteZero.RigidState>();

	// Token: 0x020002E2 RID: 738
	private class RigidState
	{
		// Token: 0x0600115A RID: 4442 RVA: 0x00070A14 File Offset: 0x0006EC14
		public RigidState(Rigidbody rigid)
		{
			this.mRigidbody = rigid;
			this.mDamageCheck = this.mRigidbody.GetComponent<carCheckDamage>();
			this.mBeforeColdVelocity = this.mRigidbody.velocity;
			this.mBeforeColdAngularVelocity = this.mRigidbody.angularVelocity;
			this.mRigidbody.velocity = Vector3.zero;
			this.mRigidbody.angularVelocity = Vector3.zero;
			this.mRigidbody.constraints = RigidbodyConstraints.FreezeAll;
			this.mRigidbody.useGravity = false;
			this.mRigidbody.isKinematic = true;
			this.mDamageCheck.SetImmunity(false);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00070AB4 File Offset: 0x0006ECB4
		public void unFreeze()
		{
			if (this.mRigidbody == null)
			{
				return;
			}
			this.mRigidbody.constraints = RigidbodyConstraints.None;
			this.mRigidbody.isKinematic = false;
			this.mRigidbody.velocity = this.mBeforeColdVelocity;
			this.mRigidbody.angularVelocity = this.mBeforeColdAngularVelocity;
			this.mRigidbody.useGravity = true;
			this.mDamageCheck.SetImmunity(false);
		}

		// Token: 0x04000E75 RID: 3701
		private carCheckDamage mDamageCheck;

		// Token: 0x04000E76 RID: 3702
		public Rigidbody mRigidbody;

		// Token: 0x04000E77 RID: 3703
		public Vector3 mBeforeColdVelocity;

		// Token: 0x04000E78 RID: 3704
		public Vector3 mBeforeColdAngularVelocity;
	}
}
