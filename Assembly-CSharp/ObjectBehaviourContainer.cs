using System;
using Newtonsoft.Json;

// Token: 0x0200024E RID: 590
public class ObjectBehaviourContainer
{
	// Token: 0x06000E5E RID: 3678 RVA: 0x0005DB30 File Offset: 0x0005BD30
	public ObjectBehaviourContainer(int Behaviour, string[] Params, int Weight = 0)
	{
		this._behaviour = Behaviour;
		this._params = Params;
		this._weight = Weight;
	}

	// Token: 0x1700028E RID: 654
	// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0005DB50 File Offset: 0x0005BD50
	// (set) Token: 0x06000E60 RID: 3680 RVA: 0x0005DB58 File Offset: 0x0005BD58
	public string[] Params
	{
		get
		{
			return this._params;
		}
		set
		{
			this._params = value;
		}
	}

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0005DB64 File Offset: 0x0005BD64
	// (set) Token: 0x06000E62 RID: 3682 RVA: 0x0005DB6C File Offset: 0x0005BD6C
	public int Behaviour
	{
		get
		{
			return this._behaviour;
		}
		set
		{
			this._behaviour = value;
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0005DB78 File Offset: 0x0005BD78
	[JsonIgnore]
	public BehaviourToolLogic.BehaviourTypes BehaviourType
	{
		get
		{
			return (BehaviourToolLogic.BehaviourTypes)this._behaviour;
		}
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0005DB80 File Offset: 0x0005BD80
	[JsonIgnore]
	public int Weight
	{
		get
		{
			return this._weight;
		}
	}

	// Token: 0x04000B06 RID: 2822
	private string[] _params;

	// Token: 0x04000B07 RID: 2823
	private int _behaviour;

	// Token: 0x04000B08 RID: 2824
	private int _weight;
}
