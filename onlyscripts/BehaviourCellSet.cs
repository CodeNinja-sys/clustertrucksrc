using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F6 RID: 502
public class BehaviourCellSet : MonoBehaviour
{
	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x000494A4 File Offset: 0x000476A4
	public string[] Params
	{
		get
		{
			return this._params;
		}
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x000494AC File Offset: 0x000476AC
	public int Behaviour
	{
		get
		{
			return (int)Enum.Parse(typeof(BehaviourToolLogic.BehaviourTypes), this.NameText.text);
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x000494D0 File Offset: 0x000476D0
	public BehaviourToolLogic.BehaviourTypes BehaviourType
	{
		get
		{
			return (BehaviourToolLogic.BehaviourTypes)((int)Enum.Parse(typeof(BehaviourToolLogic.BehaviourTypes), this.NameText.text));
		}
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x000494F4 File Offset: 0x000476F4
	public void Init(string type)
	{
		this.NameText.text = type;
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x00049504 File Offset: 0x00047704
	public void setParams(string[] Params)
	{
		this._params = Params;
	}

	// Token: 0x0400086B RID: 2155
	[SerializeField]
	private string[] _params;

	// Token: 0x0400086C RID: 2156
	public Text NameText;
}
