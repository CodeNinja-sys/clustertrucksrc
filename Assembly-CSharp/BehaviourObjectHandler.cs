using System;
using UnityEngine;

// Token: 0x020001E1 RID: 481
public class BehaviourObjectHandler : MonoBehaviour
{
	// Token: 0x1700023B RID: 571
	// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00046B88 File Offset: 0x00044D88
	// (set) Token: 0x06000B5A RID: 2906 RVA: 0x00046B90 File Offset: 0x00044D90
	public ObjectBehaviourContainer[] Behaviours
	{
		get
		{
			return this._behaviours;
		}
		set
		{
			this._behaviours = value;
		}
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x00046B9C File Offset: 0x00044D9C
	public void Init(ObjectBehaviourContainer[] behaviours)
	{
		Debug.Log("Initalizing Behaviours for: " + base.name);
		this._behaviours = behaviours.SortByWeight();
		for (int i = 0; i < this._behaviours.Length; i++)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Index: ",
				i,
				" Adding: ",
				behaviours[i].BehaviourType.ToString(),
				" to: ",
				base.gameObject.name
			}));
			this.addBehaviour(behaviours[i]);
		}
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x00046C40 File Offset: 0x00044E40
	private void addBehaviour(ObjectBehaviourContainer behaviour)
	{
		switch (behaviour.BehaviourType)
		{
		case BehaviourToolLogic.BehaviourTypes.Spin:
			if (!base.gameObject.GetComponent<spin>())
			{
				base.gameObject.AddComponent<spin>().Initalize(behaviour.Params);
			}
			break;
		case BehaviourToolLogic.BehaviourTypes.RigidBody:
		{
			base.gameObject.isStatic = false;
			Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
			if (rigidbody.GetComponent<MeshCollider>())
			{
				rigidbody.isKinematic = !rigidbody.GetComponent<MeshCollider>().convex;
			}
			break;
		}
		case BehaviourToolLogic.BehaviourTypes.NoGravity:
		{
			base.gameObject.isStatic = false;
			Rigidbody[] array = new Rigidbody[1];
			if (base.GetComponentsInChildren<Rigidbody>().Length > 0)
			{
				array = base.GetComponentsInChildren<Rigidbody>();
			}
			else
			{
				array[0] = base.gameObject.AddComponent<Rigidbody>();
			}
			foreach (Rigidbody rigidbody2 in array)
			{
				if (rigidbody2.GetComponent<MeshCollider>())
				{
					rigidbody2.isKinematic = !rigidbody2.GetComponent<MeshCollider>().convex;
				}
				rigidbody2.useGravity = false;
			}
			break;
		}
		default:
			throw new Exception(behaviour.BehaviourType + " Is not Set!");
		}
	}

	// Token: 0x04000810 RID: 2064
	private ObjectBehaviourContainer[] _behaviours;
}
