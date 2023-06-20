using System;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x020001EB RID: 491
public class EventInfo
{
	// Token: 0x06000B85 RID: 2949 RVA: 0x00047CC4 File Offset: 0x00045EC4
	[JsonConstructor]
	public EventInfo(object parameters)
	{
		Debug.Log("New JSON Event! ");
		this.eventParameters = (parameters as string[]);
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x00047CE4 File Offset: 0x00045EE4
	public EventInfo(string eventType, string[] parameters)
	{
		this.eventParameters = parameters;
		this.eventParameters[0] = this.checkEventType(eventType).ToString();
		Debug.Log(string.Concat(new object[]
		{
			"New Event! Trigger: ",
			this.TriggerEvent.ToString(),
			" Type: ",
			this.EventType.ToString(),
			"  EntityIndex: ",
			this.EntityIndex
		}), (this.EntityBlock != null) ? this.EntityBlock.go : null);
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x00047D90 File Offset: 0x00045F90
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x00047D98 File Offset: 0x00045F98
	public override string ToString()
	{
		return "EventInfo: Trigger: " + this.TriggerEvent.ToString() + "  Target: " + this.EntityBlock.id;
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x00047DD0 File Offset: 0x00045FD0
	public override bool Equals(object obj)
	{
		EventInfo eventInfo = null;
		try
		{
			eventInfo = (obj as EventInfo);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
		}
		return eventInfo != null && (eventInfo.EntityIndex == this.EntityIndex && eventInfo.EventType == this.EventType && eventInfo.TriggerEventIndex == this.TriggerEventIndex);
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00047E58 File Offset: 0x00046058
	[JsonIgnore]
	public string[] Parameters
	{
		get
		{
			if (this.eventParameters.Length >= 5)
			{
				return this.eventParameters.FromToIndex(5, this.eventParameters.Length);
			}
			return null;
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06000B8B RID: 2955 RVA: 0x00047E8C File Offset: 0x0004608C
	[JsonIgnore]
	public EventToolLogic.EventTypes EventType
	{
		get
		{
			return (EventToolLogic.EventTypes)((int)Enum.Parse(typeof(EventToolLogic.EventTypes), this.eventParameters[0]));
		}
	}

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06000B8C RID: 2956 RVA: 0x00047EAC File Offset: 0x000460AC
	[JsonIgnore]
	public EventToolLogic.TriggerEvents TriggerEvent
	{
		get
		{
			return (EventToolLogic.TriggerEvents)((int)Enum.Parse(typeof(EventToolLogic.TriggerEvents), this.eventParameters[1]));
		}
	}

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x06000B8D RID: 2957 RVA: 0x00047ECC File Offset: 0x000460CC
	[JsonIgnore]
	public int TriggerEventIndex
	{
		get
		{
			return int.Parse(this.eventParameters[1]);
		}
	}

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00047EDC File Offset: 0x000460DC
	[JsonIgnore]
	public float Delay
	{
		get
		{
			return float.Parse(this.eventParameters[3]);
		}
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00047EEC File Offset: 0x000460EC
	[JsonIgnore]
	public bool OnlyOnce
	{
		get
		{
			return int.Parse(this.eventParameters[4]) == 1;
		}
	}

	// Token: 0x17000244 RID: 580
	// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00047F00 File Offset: 0x00046100
	[JsonIgnore]
	public int EntityIndex
	{
		get
		{
			return int.Parse(this.eventParameters[2]);
		}
	}

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00047F10 File Offset: 0x00046110
	[JsonIgnore]
	public levelEditorManager.Block EntityBlock
	{
		get
		{
			int num = int.Parse(this.eventParameters[2]);
			if (num == levelEditorManager.EVENT_THATREFERENCE)
			{
				return new levelEditorManager.Block
				{
					Index = levelEditorManager.EVENT_THATREFERENCE
				};
			}
			if (num == levelEditorManager.EVENT_SELFREFERENCE)
			{
				return new levelEditorManager.Block
				{
					Index = levelEditorManager.EVENT_SELFREFERENCE
				};
			}
			foreach (levelEditorManager.Block block in levelEditorManager.Instance().getActiveMap.tiles)
			{
				Debug.Log("Current Map Object: ", block.go);
				if (block.Index == num)
				{
					Debug.Log("Returning Object: " + block.id, block.go);
					return block;
				}
			}
			Debug.LogError("Cannot find block with index: " + num);
			return null;
		}
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x0004801C File Offset: 0x0004621C
	public void SetNewInfo(string eventType, string triggerEvent, int entityIndex, string[] parameters = null)
	{
		this.SetEventType(eventType);
		Debug.Log("New Type: " + eventType + " New Trigger Event: " + triggerEvent);
		this.eventParameters[1] = ((int)Enum.Parse(typeof(EventToolLogic.TriggerEvents), triggerEvent)).ToString();
		this.eventParameters[2] = entityIndex.ToString();
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x0004807C File Offset: 0x0004627C
	public void SetNewInfo(float delay, bool onlyOnce)
	{
		this.eventParameters[3] = ((int)delay * 100).ToString();
		this.eventParameters[4] = Convert.ToInt32(onlyOnce).ToString();
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x000480B4 File Offset: 0x000462B4
	public void SetNewParameters(string[] Parameters)
	{
		for (int i = 0; i < Parameters.Length; i++)
		{
			this.eventParameters[5 + i] = Parameters[i];
		}
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x000480E4 File Offset: 0x000462E4
	private void SetEventType(string type)
	{
		this.eventParameters[0] = this.checkEventType(type).ToString();
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00048108 File Offset: 0x00046308
	private int checkEventType(string checker)
	{
		int result;
		try
		{
			object obj = Enum.Parse(typeof(EventToolLogic.EventTypes), checker, true);
			result = (int)obj;
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
			result = -1;
		}
		return result;
	}

	// Token: 0x0400082B RID: 2091
	public string[] eventParameters;
}
