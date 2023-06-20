using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020001FD RID: 509
public class EventToolLogic : MonoBehaviour
{
	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0004AD88 File Offset: 0x00048F88
	private levelEditorManager.Block _currentBlock
	{
		get
		{
			return (this._currentBlocks != null) ? this._currentBlocks[0] : null;
		}
	}

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0004ADA4 File Offset: 0x00048FA4
	private EventInfo[] CurrentEvents
	{
		get
		{
			List<EventInfo> list = new List<EventInfo>();
			foreach (EventToolLogic.EventWrapper eventWrapper in this._eventObjects)
			{
				list.Add(eventWrapper.EventInfo);
			}
			return list.ToArray();
		}
	}

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0004AE1C File Offset: 0x0004901C
	public static bool Initalized
	{
		get
		{
			return EventToolLogic._initialized;
		}
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0004AE24 File Offset: 0x00049024
	public static EventToolLogic Instance
	{
		get
		{
			return EventToolLogic._instance;
		}
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x0004AE2C File Offset: 0x0004902C
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x0004AE34 File Offset: 0x00049034
	public void Init()
	{
		if (EventToolLogic._instance)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		EventToolLogic._instance = this;
		Debug.Log("New Event Instance!");
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x0004AE68 File Offset: 0x00049068
	private void Start()
	{
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x0004AE6C File Offset: 0x0004906C
	private void OnEnable()
	{
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x0004AE70 File Offset: 0x00049070
	private void Update()
	{
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x0004AE74 File Offset: 0x00049074
	public void Init(GameObject[] objects)
	{
		if (EventToolLogic._initialized)
		{
			return;
		}
		EventToolLogic._initialized = true;
		this.Clear();
		this._currentBlocks = objects.ToBlockList().ToArray();
		List<string> list = new List<string>();
		this.EventType_Dropdown.AddOptions(this.getEventTypesForBlock(this._currentBlock));
		levelEditorManager.Block block = null;
		if (this._currentBlock.type != levelEditorManager.blockType.beventsblock)
		{
			list.Add("THIS_" + this._currentBlock.Index);
			block = this._currentBlock;
		}
		else
		{
			list.Add("THAT_" + levelEditorManager.EVENT_THATREFERENCE.ToString());
			block = new levelEditorManager.Block
			{
				Index = levelEditorManager.EVENT_THATREFERENCE
			};
		}
		this._currentSelectedObjectIndex = block.Index;
		foreach (levelEditorManager.Block block2 in levelEditorManager.Instance().getCurrentMap.tiles)
		{
			if (block2.id.Split(new char[]
			{
				'_'
			}).Length > 1 && block2.Index != this._currentBlock.Index)
			{
				list.Add(block2.id.Split(new char[]
				{
					'_'
				})[1] + "_" + block2.Index);
				if (block == null)
				{
					block = block2;
				}
			}
		}
		if (list.Count > 0)
		{
			this.Entity_DropDown.AddOptions(list);
			this.Entity_DropDown.GetComponentInChildren<Text>().text = this.Entity_DropDown.options[0].text.Split(new char[]
			{
				'_'
			})[0];
		}
		list.Clear();
		if (block != null)
		{
			this.EventToTrigger_Dropdown.AddOptions(this.getEventTriggersForBlock(block));
		}
		if (this._currentBlock.EventInfo != null)
		{
			foreach (EventInfo newEvent in this._currentBlock.EventInfo)
			{
				this.AddEvent(newEvent);
			}
		}
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x0004B0C4 File Offset: 0x000492C4
	public void OnClickDone()
	{
		Debug.Log("Done!");
		if (!EventToolLogic.Initalized)
		{
			return;
		}
		if (this._currentBlock != null)
		{
			foreach (EventInfo eventInfo in this.CurrentEvents)
			{
				Debug.Log(eventInfo.EntityBlock.id + " Index: " + eventInfo.EntityIndex);
			}
			for (int j = 0; j < this._currentBlocks.Length; j++)
			{
				this._currentBlocks[j].setObjectEvents(this.CurrentEvents);
			}
			this.Clear();
			this._currentBlocks = null;
		}
		EventToolLogic._initialized = false;
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x0004B174 File Offset: 0x00049374
	public void OnClickCancel()
	{
		Debug.Log("Done!");
		this._currentBlocks = null;
		this.Clear();
		EventToolLogic._initialized = false;
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x0004B194 File Offset: 0x00049394
	public void AddEvent(EventInfo newEvent)
	{
		Debug.Log("Adding Event!");
		if (this._currentEventInfo != null)
		{
			this._currentEventInfo.EventCellSet.GetComponentInChildren<Image>().color = Color.white;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Event_cell);
		gameObject.transform.SetParent(this.Grid);
		Debug.Log("Trigger Event: " + newEvent.TriggerEvent);
		string eventType = newEvent.EventType.ToString();
		string eventToTrigger = newEvent.TriggerEvent.ToString();
		string value;
		if (newEvent.EntityIndex == levelEditorManager.EVENT_THATREFERENCE)
		{
			value = "THAT";
		}
		else if (newEvent.EntityIndex == levelEditorManager.EVENT_SELFREFERENCE)
		{
			value = "THIS";
		}
		else
		{
			value = ((newEvent.EntityBlock.id.Split(new char[]
			{
				'_'
			}).Length <= 1) ? "THIS" : newEvent.EntityBlock.id.Split(new char[]
			{
				'_'
			})[1]);
		}
		KeyValuePair<int, string> entity = new KeyValuePair<int, string>(newEvent.EntityIndex, value);
		string[] parameters = newEvent.Parameters;
		float delay = newEvent.Delay / 100f;
		bool onlyOnce = newEvent.OnlyOnce;
		gameObject.GetComponent<Event_CellSet>().ChangeValues(eventType, eventToTrigger, entity, parameters);
		gameObject.GetComponent<Event_CellSet>().ChangeValues(delay, onlyOnce);
		EventToolLogic.EventWrapper item = new EventToolLogic.EventWrapper(newEvent, gameObject.GetComponent<Event_CellSet>());
		this._eventObjects.Add(item);
		Debug.Log("Adding: ", gameObject);
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x0004B324 File Offset: 0x00049524
	public void AddEvent()
	{
		if (this.Entity_DropDown.options.Count <= 0 || this.EventToTrigger_Dropdown.options.Count <= 0)
		{
			DisplayManager.Instance().DisplayMessage("Target or Event cannot be empty!");
			return;
		}
		Debug.Log("Adding Event!");
		string[] paramsFromGrid = this.getParamsFromGrid();
		string[] array = new string[Enum.GetValues(typeof(EventToolLogic.EventParameterStructure)).Length - 1 + ((paramsFromGrid != null) ? paramsFromGrid.Length : 0)];
		array[0] = ((int)Enum.Parse(typeof(EventToolLogic.EventTypes), this.EventType_Dropdown.options[this.EventType_Dropdown.value].text)).ToString();
		array[1] = ((int)Enum.Parse(typeof(EventToolLogic.TriggerEvents), this.EventToTrigger_Dropdown.options[this.EventToTrigger_Dropdown.value].text)).ToString();
		array[2] = ((!(this.Entity_DropDown.captionText.text.Split(new char[]
		{
			'_'
		})[0] == "THAT")) ? ((!(this.Entity_DropDown.captionText.text.Split(new char[]
		{
			'_'
		})[0] == "THIS")) ? this._currentSelectedObjectIndex.ToString() : levelEditorManager.EVENT_SELFREFERENCE.ToString()) : levelEditorManager.EVENT_THATREFERENCE.ToString());
		array[3] = ((!string.IsNullOrEmpty(this.delay_Inputfield.text)) ? (float.Parse(this.delay_Inputfield.text) * 100f).ToString() : 0.ToString());
		array[4] = Convert.ToInt32(this.fireOnlyOnce_Toggle.isOn).ToString();
		for (int i = 0; i < paramsFromGrid.Length; i++)
		{
			array[5 + i] = paramsFromGrid[i];
		}
		EventInfo newEventinfo = new EventInfo(this.EventType_Dropdown.options[this.EventType_Dropdown.value].text, array);
		if (this._eventObjects.ConvertAll<EventInfo>(new Converter<EventToolLogic.EventWrapper, EventInfo>(this.EventWrapperToEventInfo)).Find((EventInfo _event) => _event.Equals(newEventinfo)) != null)
		{
			Debug.LogError("Already containing an event!!");
			DisplayManager.Instance().DisplayMessage("Already containing an event!!");
			return;
		}
		if (this._currentEventInfo != null)
		{
			this._currentEventInfo.EventCellSet.GetComponentInChildren<Image>().color = Color.white;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Event_cell);
		gameObject.transform.SetParent(this.Grid);
		Debug.Log("DropDown Value! " + this.EventToTrigger_Dropdown.value);
		string text = this.EventType_Dropdown.options[this.EventType_Dropdown.value].text;
		string text2 = this.EventToTrigger_Dropdown.options[this.EventToTrigger_Dropdown.value].text;
		KeyValuePair<int, string> entity = new KeyValuePair<int, string>(this.Entity_DropDown.value, this.Entity_DropDown.options[this.Entity_DropDown.value].text);
		string[] parameters = paramsFromGrid;
		KeyValuePair<float, string> keyValuePair = new KeyValuePair<float, string>((!string.IsNullOrEmpty(this.delay_Inputfield.text)) ? float.Parse(this.delay_Inputfield.text) : 0f, "Delay");
		KeyValuePair<int, string> keyValuePair2 = new KeyValuePair<int, string>(Convert.ToInt32(this.fireOnlyOnce_Toggle.isOn), "OnlyOnce");
		gameObject.GetComponent<Event_CellSet>().ChangeValues(text, text2, entity, parameters);
		gameObject.GetComponent<Event_CellSet>().ChangeValues(keyValuePair.Key, keyValuePair2.Key == 1);
		EventToolLogic.EventWrapper eventWrapper = new EventToolLogic.EventWrapper(newEventinfo, gameObject.GetComponent<Event_CellSet>());
		this._eventObjects.Add(eventWrapper);
		Debug.Log(string.Concat(new object[]
		{
			"New Event: ",
			eventWrapper.EventInfo.EntityBlock.id,
			" Index: ",
			eventWrapper.EventInfo.EntityIndex
		}));
		Debug.Log("Adding: ", gameObject);
		this.EventScrollBar.value = 0f;
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x0004B7A0 File Offset: 0x000499A0
	public void RemoveEvent()
	{
		Debug.Log("Removing Event!");
		if (this._currentEventInfo != null)
		{
			UnityEngine.Object.Destroy(this._currentEventInfo.EventCellSet.gameObject);
			this._eventObjects.Remove(this._currentEventInfo);
			this._currentEventInfo = null;
			if (this._eventObjects.Count > 0)
			{
				this._currentEventInfo = this._eventObjects[this._eventObjects.Count - 1];
				this._currentEventInfo.EventCellSet.GetComponentInChildren<Image>().color = Color.gray;
			}
		}
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x0004B83C File Offset: 0x00049A3C
	public void ChangeCurrentSelectedEvent(Event_CellSet clicked)
	{
		foreach (EventToolLogic.EventWrapper eventWrapper in this._eventObjects)
		{
			if (eventWrapper.EventCellSet == clicked)
			{
				if (this._currentEventInfo != null)
				{
					this._currentEventInfo.EventCellSet.GetComponentInChildren<Image>().color = Color.white;
				}
				this._currentEventInfo = eventWrapper;
				this.EventToTrigger_Dropdown.ClearOptions();
				this.EventToTrigger_Dropdown.AddOptions(this.getEventTriggersForBlock(this._currentEventInfo.EventInfo.EntityBlock));
				clicked.GetComponentInChildren<Image>().color = Color.gray;
				Debug.Log("Succesful!", clicked);
				this.settingFlag = true;
				this.EventType_Dropdown.value = this._currentEventInfo.EventCellSet.GetProperty(1).Key;
				this.EventType_Dropdown.RefreshShownValue();
				this.EventToTrigger_Dropdown.value = this._currentEventInfo.EventCellSet.GetProperty(2).Key;
				this.EventToTrigger_Dropdown.RefreshShownValue();
				this.Entity_DropDown.value = this._currentEventInfo.EventCellSet.GetProperty(3).Key;
				this.Entity_DropDown.RefreshShownValue();
				this.Entity_DropDown.captionText.text = this.Entity_DropDown.captionText.text.Split(new char[]
				{
					'_'
				})[0];
				this.delay_Inputfield.text = this._currentEventInfo.EventCellSet.Delay.ToString("F2");
				this.fireOnlyOnce_Toggle.isOn = this._currentEventInfo.EventCellSet.OnlyOnce;
				this.settingFlag = false;
				return;
			}
		}
		Debug.LogError("Cannot find EventInfo!", clicked);
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x0004BA44 File Offset: 0x00049C44
	public void OnSubmit(BaseEventData b)
	{
		if (b.selectedObject.tag == "EventCell")
		{
			this.ChangeCurrentSelectedEvent(b.selectedObject.GetComponentInParent<Event_CellSet>());
		}
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x0004BA7C File Offset: 0x00049C7C
	public void ChangeSelectedEntity(levelEditorManager.Block Block)
	{
		if (Block != null)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Change Selected Entity! ",
				Block.id,
				" Index: ",
				Block.Index
			}));
			if (Block != this._currentBlock)
			{
				this._currentSelectedObjectIndex = Block.Index;
			}
		}
		else
		{
			this._currentSelectedObjectIndex = levelEditorManager.EVENT_THATREFERENCE;
		}
		this.Entity_DropDown.captionText.text = this.Entity_DropDown.captionText.text.Split(new char[]
		{
			'_'
		})[0];
		this.EventToTrigger_Dropdown.ClearOptions();
		this.EventToTrigger_Dropdown.AddOptions(this.getEventTriggersForBlock(Block));
		this.EventToTrigger_Dropdown.RefreshShownValue();
		if (this.EventToTrigger_Dropdown.value > this.EventToTrigger_Dropdown.options.Count - 1)
		{
			this.EventToTrigger_Dropdown.value = this.EventToTrigger_Dropdown.options.Count - 1;
		}
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x0004BB88 File Offset: 0x00049D88
	public void DropDownValueChanged()
	{
		if (this._currentEventInfo == null || this.settingFlag)
		{
			return;
		}
		this.Entity_DropDown.captionText.text = this.Entity_DropDown.captionText.text.Split(new char[]
		{
			'_'
		})[0];
		string text = this.EventType_Dropdown.options[this.EventType_Dropdown.value].text;
		string text2 = this.EventToTrigger_Dropdown.options[this.EventToTrigger_Dropdown.value].text;
		KeyValuePair<int, string> entity = new KeyValuePair<int, string>(this.Entity_DropDown.value, this.Entity_DropDown.options[this.Entity_DropDown.value].text);
		string[] paramsFromGrid = this.getParamsFromGrid();
		this._currentEventInfo.EventCellSet.ChangeValues(text, text2, entity, paramsFromGrid);
		Debug.Log("New EventToTrigger: " + this.EventToTrigger_Dropdown.options[this.EventToTrigger_Dropdown.value].text);
		int entityIndex = (!(this.Entity_DropDown.captionText.text.Split(new char[]
		{
			'_'
		})[0] == "THAT")) ? ((!(this.Entity_DropDown.captionText.text.Split(new char[]
		{
			'_'
		})[0] == "THIS")) ? this._currentSelectedObjectIndex : levelEditorManager.EVENT_SELFREFERENCE) : levelEditorManager.EVENT_THATREFERENCE;
		this._currentEventInfo.EventInfo.SetNewInfo(text, text2, entityIndex, paramsFromGrid);
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x0004BD30 File Offset: 0x00049F30
	public void OnlyOnceToggleChanged()
	{
		if (this._currentEventInfo == null || this.settingFlag)
		{
			return;
		}
		this._currentEventInfo.EventCellSet.ChangeValues(float.Parse(this.delay_Inputfield.text), this.fireOnlyOnce_Toggle.isOn);
		this._currentEventInfo.EventInfo.SetNewInfo(float.Parse(this.delay_Inputfield.text), this.fireOnlyOnce_Toggle.isOn);
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x0004BDAC File Offset: 0x00049FAC
	public void DelayTextInputChanged()
	{
		if (this._currentEventInfo == null || this.settingFlag)
		{
			return;
		}
		this._currentEventInfo.EventCellSet.ChangeValues(float.Parse(this.delay_Inputfield.text), this.fireOnlyOnce_Toggle.isOn);
		this._currentEventInfo.EventInfo.SetNewInfo(float.Parse(this.delay_Inputfield.text), this.fireOnlyOnce_Toggle.isOn);
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x0004BE28 File Offset: 0x0004A028
	public void EvenTriggerDropDownChanged()
	{
		this.ClearParameterGrid();
		this.PopulateParameterGridFor(this.EventToTrigger_Dropdown.options[this.EventToTrigger_Dropdown.value].text);
		this.DropDownValueChanged();
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x0004BE68 File Offset: 0x0004A068
	private void PopulateParameterGridFor(string TriggerEvent)
	{
		switch ((int)Enum.Parse(typeof(EventToolLogic.TriggerEvents), TriggerEvent))
		{
		case 0:
			break;
		case 1:
			break;
		case 2:
			break;
		case 3:
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.VectorCell);
			gameObject.transform.SetParent(this.EventParameterGrid);
			gameObject.GetComponent<BehaviourParamCellSetBase>().Init("Direction", null);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.SliderCell);
			gameObject2.transform.SetParent(this.EventParameterGrid);
			gameObject2.GetComponent<BehaviourParamCellSetBase>().Init("Spread", new string[]
			{
				0f.ToString(),
				0f.ToString(),
				10f.ToString(),
				bool.FalseString
			});
			break;
		}
		case 4:
			break;
		case 5:
			break;
		default:
			throw new Exception(TriggerEvent + " IS NOT SETUP!");
		}
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x0004BF78 File Offset: 0x0004A178
	private string[] getParamsFromGrid()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < this.EventParameterGrid.childCount; i++)
		{
			list.Add(this.EventParameterGrid.GetChild(i).GetComponent<BehaviourParamCellSetBase>().getValue());
		}
		return list.ToArray();
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x0004BFCC File Offset: 0x0004A1CC
	private void ClearParameterGrid()
	{
		for (int i = 0; i < this.EventParameterGrid.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.EventParameterGrid.GetChild(i).gameObject);
		}
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0004C00C File Offset: 0x0004A20C
	private void Clear()
	{
		this.EventType_Dropdown.ClearOptions();
		this.EventToTrigger_Dropdown.ClearOptions();
		this.Entity_DropDown.ClearOptions();
		this._eventObjects.Clear();
		this._currentEventInfo = null;
		for (int i = 0; i < this.Grid.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.Grid.GetChild(i).gameObject);
		}
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x0004C080 File Offset: 0x0004A280
	private EventInfo EventWrapperToEventInfo(EventToolLogic.EventWrapper wrapper)
	{
		return wrapper.EventInfo;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x0004C088 File Offset: 0x0004A288
	private List<string> getEventTypesForBlock(levelEditorManager.Block block)
	{
		List<string> list = new List<string>();
		switch (block.type)
		{
		case levelEditorManager.blockType.btruckblock:
			list.Add(EventToolLogic.EventTypes.OnPlayerTouch.ToString());
			list.Add(EventToolLogic.EventTypes.OnTruckTouch.ToString());
			list.Add(EventToolLogic.EventTypes.Timed.ToString());
			list.Add(EventToolLogic.EventTypes.OnAnyTouch.ToString());
			break;
		case levelEditorManager.blockType.bdestructibleblock:
			list.Add(EventToolLogic.EventTypes.OnPlayerTouch.ToString());
			list.Add(EventToolLogic.EventTypes.OnTruckTouch.ToString());
			list.Add(EventToolLogic.EventTypes.Timed.ToString());
			list.Add(EventToolLogic.EventTypes.OnAnyTouch.ToString());
			break;
		case levelEditorManager.blockType.broadsblock:
			list.Add(EventToolLogic.EventTypes.OnPlayerTouch.ToString());
			list.Add(EventToolLogic.EventTypes.OnTruckTouch.ToString());
			list.Add(EventToolLogic.EventTypes.Timed.ToString());
			list.Add(EventToolLogic.EventTypes.OnAnyTouch.ToString());
			break;
		case levelEditorManager.blockType.btrapsblock:
			list.Add(EventToolLogic.EventTypes.OnPlayerTouch.ToString());
			list.Add(EventToolLogic.EventTypes.OnTruckTouch.ToString());
			list.Add(EventToolLogic.EventTypes.Timed.ToString());
			list.Add(EventToolLogic.EventTypes.OnAnyTouch.ToString());
			break;
		case levelEditorManager.blockType.broadpointblock:
			list.Add(EventToolLogic.EventTypes.OnPlayerTouch.ToString());
			list.Add(EventToolLogic.EventTypes.OnTruckTouch.ToString());
			break;
		case levelEditorManager.blockType.bgoal:
			break;
		case levelEditorManager.blockType.bplayerspawn:
			break;
		case levelEditorManager.blockType.bpropsblock:
			list.Add(EventToolLogic.EventTypes.OnPlayerTouch.ToString());
			list.Add(EventToolLogic.EventTypes.OnTruckTouch.ToString());
			list.Add(EventToolLogic.EventTypes.Timed.ToString());
			list.Add(EventToolLogic.EventTypes.OnAnyTouch.ToString());
			break;
		case levelEditorManager.blockType.bshapesblock:
			list.Add(EventToolLogic.EventTypes.OnPlayerTouch.ToString());
			list.Add(EventToolLogic.EventTypes.OnTruckTouch.ToString());
			list.Add(EventToolLogic.EventTypes.OnAnyTouch.ToString());
			list.Add(EventToolLogic.EventTypes.Timed.ToString());
			break;
		case levelEditorManager.blockType.beventsblock:
			list.Add(EventToolLogic.EventTypes.OnPlayerTouch.ToString());
			list.Add(EventToolLogic.EventTypes.OnTruckTouch.ToString());
			list.Add(EventToolLogic.EventTypes.OnAnyTouch.ToString());
			break;
		default:
			Debug.LogError("Inavlid BlockType: " + block.type.ToString());
			break;
		}
		return list;
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x0004C31C File Offset: 0x0004A51C
	private List<string> getEventTriggersForBlock(levelEditorManager.Block _block)
	{
		if (_block == null)
		{
			Debug.Log("Null Block");
		}
		levelEditorManager.Block block = _block ?? this._currentBlock;
		List<string> list = new List<string>();
		Debug.Log("Block: " + block.id);
		if (_block.Index == levelEditorManager.EVENT_THATREFERENCE)
		{
			list.Add(EventToolLogic.TriggerEvents.Destroy.ToString());
			list.Add(EventToolLogic.TriggerEvents.Stop.ToString());
			list.Add(EventToolLogic.TriggerEvents.Go.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddSpin.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddNoGravity.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddRigidBody.ToString());
			return list;
		}
		switch (block.type)
		{
		case levelEditorManager.blockType.btruckblock:
			list.Add(EventToolLogic.TriggerEvents.Destroy.ToString());
			list.Add(EventToolLogic.TriggerEvents.Stop.ToString());
			list.Add(EventToolLogic.TriggerEvents.Go.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddSpin.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddNoGravity.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddRigidBody.ToString());
			break;
		case levelEditorManager.blockType.bdestructibleblock:
			list.Add(EventToolLogic.TriggerEvents.Destroy.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddSpin.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddNoGravity.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddRigidBody.ToString());
			break;
		case levelEditorManager.blockType.broadsblock:
			list.Add(EventToolLogic.TriggerEvents.Destroy.ToString());
			break;
		case levelEditorManager.blockType.btrapsblock:
			list.Add(EventToolLogic.TriggerEvents.Destroy.ToString());
			list.Add(EventToolLogic.TriggerEvents.Stop.ToString());
			list.Add(EventToolLogic.TriggerEvents.Go.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddSpin.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddNoGravity.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddRigidBody.ToString());
			break;
		case levelEditorManager.blockType.broadpointblock:
			break;
		case levelEditorManager.blockType.bgoal:
			break;
		case levelEditorManager.blockType.bplayerspawn:
			break;
		case levelEditorManager.blockType.bpropsblock:
			list.Add(EventToolLogic.TriggerEvents.Destroy.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddSpin.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddNoGravity.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddRigidBody.ToString());
			break;
		case levelEditorManager.blockType.bshapesblock:
			list.Add(EventToolLogic.TriggerEvents.Destroy.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddSpin.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddNoGravity.ToString());
			list.Add(EventToolLogic.TriggerEvents.AddRigidBody.ToString());
			break;
		case levelEditorManager.blockType.beventsblock:
			break;
		default:
			Debug.LogError("Inavlid BlockType: " + block.type.ToString());
			break;
		}
		return list;
	}

	// Token: 0x040008A1 RID: 2209
	private const string EVENT_CELL_KEY = "EventCell";

	// Token: 0x040008A2 RID: 2210
	private levelEditorManager.Block[] _currentBlocks;

	// Token: 0x040008A3 RID: 2211
	private int _currentSelectedObjectIndex;

	// Token: 0x040008A4 RID: 2212
	private List<EventToolLogic.EventWrapper> _eventObjects = new List<EventToolLogic.EventWrapper>();

	// Token: 0x040008A5 RID: 2213
	private EventToolLogic.EventWrapper _currentEventInfo;

	// Token: 0x040008A6 RID: 2214
	private bool settingFlag;

	// Token: 0x040008A7 RID: 2215
	public Scrollbar EventScrollBar;

	// Token: 0x040008A8 RID: 2216
	public Transform Grid;

	// Token: 0x040008A9 RID: 2217
	public Transform EventParameterGrid;

	// Token: 0x040008AA RID: 2218
	public GameObject Event_cell;

	// Token: 0x040008AB RID: 2219
	[Header("EventProperties")]
	public Dropdown EventType_Dropdown;

	// Token: 0x040008AC RID: 2220
	[Header("EventProperties")]
	public Dropdown EventToTrigger_Dropdown;

	// Token: 0x040008AD RID: 2221
	[Header("EventProperties")]
	public Dropdown Entity_DropDown;

	// Token: 0x040008AE RID: 2222
	public InputField delay_Inputfield;

	// Token: 0x040008AF RID: 2223
	public Toggle fireOnlyOnce_Toggle;

	// Token: 0x040008B0 RID: 2224
	[Header("Parameter Types")]
	[SerializeField]
	private GameObject InputCell;

	// Token: 0x040008B1 RID: 2225
	[SerializeField]
	private GameObject SliderCell;

	// Token: 0x040008B2 RID: 2226
	[SerializeField]
	private GameObject VectorCell;

	// Token: 0x040008B3 RID: 2227
	[SerializeField]
	private GameObject DropdownCell;

	// Token: 0x040008B4 RID: 2228
	private static bool _initialized;

	// Token: 0x040008B5 RID: 2229
	private static EventToolLogic _instance;

	// Token: 0x020001FE RID: 510
	public enum EventParameterStructure
	{
		// Token: 0x040008B7 RID: 2231
		eventType,
		// Token: 0x040008B8 RID: 2232
		triggerEvent,
		// Token: 0x040008B9 RID: 2233
		entity,
		// Token: 0x040008BA RID: 2234
		delay,
		// Token: 0x040008BB RID: 2235
		onlyOnce,
		// Token: 0x040008BC RID: 2236
		parameters
	}

	// Token: 0x020001FF RID: 511
	public enum EventTypes
	{
		// Token: 0x040008BE RID: 2238
		OnPlayerTouch,
		// Token: 0x040008BF RID: 2239
		OnTruckTouch,
		// Token: 0x040008C0 RID: 2240
		OnTruckTouchAll,
		// Token: 0x040008C1 RID: 2241
		OnAnyTouch,
		// Token: 0x040008C2 RID: 2242
		OnAnyTouchAll,
		// Token: 0x040008C3 RID: 2243
		Timed
	}

	// Token: 0x02000200 RID: 512
	public enum TriggerEvents
	{
		// Token: 0x040008C5 RID: 2245
		Destroy,
		// Token: 0x040008C6 RID: 2246
		Stop,
		// Token: 0x040008C7 RID: 2247
		Go,
		// Token: 0x040008C8 RID: 2248
		AddSpin,
		// Token: 0x040008C9 RID: 2249
		AddRigidBody,
		// Token: 0x040008CA RID: 2250
		AddNoGravity
	}

	// Token: 0x02000201 RID: 513
	public class EventWrapper
	{
		// Token: 0x06000C2D RID: 3117 RVA: 0x0004C618 File Offset: 0x0004A818
		public EventWrapper(EventInfo info, Event_CellSet cellset)
		{
			this._eventInfo = info;
			this._eventCellSet = cellset;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0004C630 File Offset: 0x0004A830
		public EventInfo EventInfo
		{
			get
			{
				return this._eventInfo;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0004C638 File Offset: 0x0004A838
		public Event_CellSet EventCellSet
		{
			get
			{
				return this._eventCellSet;
			}
		}

		// Token: 0x040008CB RID: 2251
		private EventInfo _eventInfo;

		// Token: 0x040008CC RID: 2252
		private Event_CellSet _eventCellSet;
	}
}
