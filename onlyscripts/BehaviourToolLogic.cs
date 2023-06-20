using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020001F7 RID: 503
public class BehaviourToolLogic : MonoBehaviour
{
	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0004951C File Offset: 0x0004771C
	public static bool Initalized
	{
		get
		{
			return BehaviourToolLogic._initialized;
		}
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00049524 File Offset: 0x00047724
	public static BehaviourToolLogic Instance
	{
		get
		{
			return BehaviourToolLogic._instance;
		}
	}

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0004952C File Offset: 0x0004772C
	// (set) Token: 0x06000BDC RID: 3036 RVA: 0x00049534 File Offset: 0x00047734
	public BehaviourCellSet CurrentSelectedBehaviour
	{
		get
		{
			return this._currentSelectedBehaviour;
		}
		set
		{
			this._currentSelectedBehaviour = value;
		}
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x00049540 File Offset: 0x00047740
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x00049548 File Offset: 0x00047748
	private void Init()
	{
		if (BehaviourToolLogic._instance)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		BehaviourToolLogic._instance = this;
		Debug.Log("New Behaviour Instance!", BehaviourToolLogic._instance);
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x00049578 File Offset: 0x00047778
	public void Init(GameObject[] Blocks)
	{
		if (BehaviourToolLogic._initialized)
		{
			return;
		}
		BehaviourToolLogic._initialized = true;
		this._blocks = Blocks.ToList().ConvertAll<levelEditorManager.Block>(new Converter<GameObject, levelEditorManager.Block>(this.GameObjectToBlock)).ToArray();
		string[] behavioursFor = this.getBehavioursFor(this._blocks[0]);
		for (int i = 0; i < behavioursFor.Length; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AvalibleCell);
			gameObject.GetComponent<BehaviourCellSet>().Init(behavioursFor[i]);
			gameObject.transform.SetParent(this.AvalibleGrid);
		}
		ObjectBehaviourContainer[] behaviours = this._blocks[0].Behaviours;
		if (behaviours == null)
		{
			return;
		}
		for (int j = 0; j < behaviours.Length; j++)
		{
			this.AddBehaviour(behaviours[j], false);
		}
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x00049640 File Offset: 0x00047840
	private void AddBehaviour(GameObject cell, bool select = true)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(cell);
		if (select)
		{
			this.populateParamsGrid(this._currentSelectedBehaviour);
			this._currentSelectedBehaviour = gameObject.GetComponent<BehaviourCellSet>();
		}
		UnityEngine.Object.Destroy(cell);
		gameObject.transform.SetParent(this.HaveGrid);
		this.addButton.interactable = false;
		if (this._currentSelectedBehaviour != null)
		{
			this.removeButton.interactable = true;
		}
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x000496B4 File Offset: 0x000478B4
	private void AddBehaviour(ObjectBehaviourContainer behaviour, bool select = true)
	{
		for (int i = 0; i < this.AvalibleGrid.childCount; i++)
		{
			if (this.AvalibleGrid.GetChild(i).GetComponent<BehaviourCellSet>().Behaviour == behaviour.Behaviour)
			{
				this.AvalibleGrid.GetChild(i).GetComponent<BehaviourCellSet>().setParams(behaviour.Params);
				this.AddBehaviour(this.AvalibleGrid.GetChild(i).gameObject, select);
				return;
			}
		}
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00049734 File Offset: 0x00047934
	private void RemoveBehaviour(GameObject cell)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(cell);
		this._currentSelectedBehaviour = gameObject.GetComponent<BehaviourCellSet>();
		UnityEngine.Object.Destroy(cell);
		gameObject.transform.SetParent(this.AvalibleGrid);
		this.removeButton.interactable = false;
		this.addButton.interactable = true;
		this.clearParameterGrid();
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x0004978C File Offset: 0x0004798C
	private levelEditorManager.Block GameObjectToBlock(GameObject target)
	{
		return levelEditorManager.Instance().getCurrentMap.getTileAt(target.transform);
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x000497A4 File Offset: 0x000479A4
	private string[] getBehavioursFor(levelEditorManager.Block block)
	{
		switch (block.type)
		{
		case levelEditorManager.blockType.btruckblock:
			return new string[]
			{
				BehaviourToolLogic.BehaviourTypes.RigidBody.ToString(),
				BehaviourToolLogic.BehaviourTypes.Spin.ToString(),
				BehaviourToolLogic.BehaviourTypes.NoGravity.ToString()
			};
		case levelEditorManager.blockType.bdestructibleblock:
			return new string[]
			{
				BehaviourToolLogic.BehaviourTypes.RigidBody.ToString(),
				BehaviourToolLogic.BehaviourTypes.Spin.ToString(),
				BehaviourToolLogic.BehaviourTypes.NoGravity.ToString()
			};
		case levelEditorManager.blockType.btrapsblock:
			return new string[]
			{
				BehaviourToolLogic.BehaviourTypes.RigidBody.ToString(),
				BehaviourToolLogic.BehaviourTypes.Spin.ToString(),
				BehaviourToolLogic.BehaviourTypes.NoGravity.ToString()
			};
		case levelEditorManager.blockType.bpropsblock:
			return new string[]
			{
				BehaviourToolLogic.BehaviourTypes.RigidBody.ToString(),
				BehaviourToolLogic.BehaviourTypes.Spin.ToString(),
				BehaviourToolLogic.BehaviourTypes.NoGravity.ToString()
			};
		case levelEditorManager.blockType.bshapesblock:
			return new string[]
			{
				BehaviourToolLogic.BehaviourTypes.RigidBody.ToString(),
				BehaviourToolLogic.BehaviourTypes.Spin.ToString(),
				BehaviourToolLogic.BehaviourTypes.NoGravity.ToString()
			};
		}
		throw new Exception("Invalid Type: " + block.type);
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x00049918 File Offset: 0x00047B18
	public void OnClickDone()
	{
		if (!BehaviourToolLogic.Initalized)
		{
			return;
		}
		if (this._currentSelectedBehaviour != null)
		{
			this._currentSelectedBehaviour.setParams(this.getParamsFromGrid());
		}
		Debug.Log("Done");
		this.clearAllgrids();
		if (this.HaveGrid.childCount > 0)
		{
			List<ObjectBehaviourContainer> list = new List<ObjectBehaviourContainer>();
			for (int i = 0; i < this.HaveGrid.childCount; i++)
			{
				BehaviourCellSet component = this.HaveGrid.GetChild(i).GetComponent<BehaviourCellSet>();
				list.Add(new ObjectBehaviourContainer(component.Behaviour, component.Params, 0));
			}
			for (int j = 0; j < this._blocks.Length; j++)
			{
				this._blocks[j].setBehaviours(list.ToArray());
			}
		}
		else if (this._blocks != null)
		{
			for (int k = 0; k < this._blocks.Length; k++)
			{
				this._blocks[k].setBehaviours(null);
			}
		}
		this._currentSelectedBehaviour = null;
		this.removeButton.interactable = false;
		this.addButton.interactable = false;
		BehaviourToolLogic._initialized = false;
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00049A4C File Offset: 0x00047C4C
	public void OnClickCancel()
	{
		Debug.Log("Cancel!");
		this.clearAllgrids();
		this._blocks = null;
		this._currentSelectedBehaviour = null;
		this.removeButton.interactable = false;
		this.addButton.interactable = false;
		BehaviourToolLogic._initialized = false;
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00049A98 File Offset: 0x00047C98
	public void OnSubmit(BaseEventData b)
	{
		if (b.selectedObject.tag == "EventCell")
		{
			Debug.Log("Pressed a behaviour button!");
			this.clearParameterGrid();
			if (this._currentSelectedBehaviour != null)
			{
				this._currentSelectedBehaviour.setParams(this.getParamsFromGrid());
				this._currentSelectedBehaviour.GetComponentInChildren<Image>().color = Color.white;
			}
			this._currentSelectedBehaviour = b.selectedObject.GetComponentInParent<BehaviourCellSet>();
			this._currentSelectedBehaviour.GetComponentInChildren<Image>().color = Color.red;
			if (this._currentSelectedBehaviour.transform.parent == this.AvalibleGrid)
			{
				this.removeButton.interactable = false;
				this.addButton.interactable = true;
			}
			else
			{
				this.populateParamsGrid(this._currentSelectedBehaviour);
				this.addButton.interactable = false;
				this.removeButton.interactable = true;
			}
		}
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x00049B90 File Offset: 0x00047D90
	private string[] getParamsFromGrid()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < this.ParamsGrid.childCount; i++)
		{
			list.Add(this.ParamsGrid.GetChild(i).GetComponent<BehaviourParamCellSetBase>().getValue());
		}
		return list.ToArray();
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x00049BE4 File Offset: 0x00047DE4
	private void populateParamsGrid(BehaviourCellSet behaviour)
	{
		switch (behaviour.BehaviourType)
		{
		case BehaviourToolLogic.BehaviourTypes.Spin:
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.VectorCell);
			gameObject.transform.SetParent(this.ParamsGrid);
			gameObject.GetComponent<BehaviourParamCellSetBase>().Init("Direction", behaviour.Params);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.SliderCell);
			gameObject2.transform.SetParent(this.ParamsGrid);
			gameObject2.GetComponent<BehaviourParamCellSetBase>().Init("Spread", new string[]
			{
				(behaviour.Params.Length <= 1) ? 0f.ToString() : behaviour.Params[1],
				0f.ToString(),
				10f.ToString(),
				bool.FalseString
			});
			break;
		}
		case BehaviourToolLogic.BehaviourTypes.RigidBody:
			break;
		case BehaviourToolLogic.BehaviourTypes.NoGravity:
			break;
		default:
			throw new Exception(behaviour.BehaviourType.ToString() + " Is not Set!");
		}
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x00049CFC File Offset: 0x00047EFC
	public void OnClickAdd()
	{
		if (this._currentSelectedBehaviour == null)
		{
			return;
		}
		if (this._currentSelectedBehaviour.transform.parent == this.AvalibleGrid)
		{
			Debug.Log("Pressed a AVAILABLE button!");
			this.AddBehaviour(this._currentSelectedBehaviour.gameObject, true);
		}
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x00049D58 File Offset: 0x00047F58
	public void OnClickRemove()
	{
		if (this._currentSelectedBehaviour == null)
		{
			return;
		}
		if (this._currentSelectedBehaviour.transform.parent == this.HaveGrid)
		{
			Debug.Log("Pressed a HAVE button!");
			this.RemoveBehaviour(this._currentSelectedBehaviour.gameObject);
		}
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x00049DB4 File Offset: 0x00047FB4
	private void clearAllgrids()
	{
		for (int i = 0; i < this.AvalibleGrid.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.AvalibleGrid.GetChild(i).gameObject);
		}
		for (int j = 0; j < this.HaveGrid.childCount; j++)
		{
			UnityEngine.Object.Destroy(this.HaveGrid.GetChild(j).gameObject);
		}
		for (int k = 0; k < this.ParamsGrid.childCount; k++)
		{
			UnityEngine.Object.Destroy(this.ParamsGrid.GetChild(k).gameObject);
		}
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x00049E58 File Offset: 0x00048058
	private void clearParameterGrid()
	{
		for (int i = 0; i < this.ParamsGrid.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.ParamsGrid.GetChild(i).gameObject);
		}
	}

	// Token: 0x0400086D RID: 2157
	private const string EVENT_CELL_KEY = "EventCell";

	// Token: 0x0400086E RID: 2158
	[SerializeField]
	private Button addButton;

	// Token: 0x0400086F RID: 2159
	[SerializeField]
	private Button removeButton;

	// Token: 0x04000870 RID: 2160
	public Transform HaveGrid;

	// Token: 0x04000871 RID: 2161
	public Transform AvalibleGrid;

	// Token: 0x04000872 RID: 2162
	public Transform ParamsGrid;

	// Token: 0x04000873 RID: 2163
	public GameObject HaveCell;

	// Token: 0x04000874 RID: 2164
	public GameObject AvalibleCell;

	// Token: 0x04000875 RID: 2165
	[SerializeField]
	private GameObject InputCell;

	// Token: 0x04000876 RID: 2166
	[SerializeField]
	private GameObject SliderCell;

	// Token: 0x04000877 RID: 2167
	[SerializeField]
	private GameObject VectorCell;

	// Token: 0x04000878 RID: 2168
	[SerializeField]
	private GameObject DropdownCell;

	// Token: 0x04000879 RID: 2169
	private levelEditorManager.Block[] _blocks;

	// Token: 0x0400087A RID: 2170
	private static bool _initialized;

	// Token: 0x0400087B RID: 2171
	private static BehaviourToolLogic _instance;

	// Token: 0x0400087C RID: 2172
	private BehaviourCellSet _currentSelectedBehaviour;

	// Token: 0x020001F8 RID: 504
	public enum BehaviourTypes
	{
		// Token: 0x0400087E RID: 2174
		Spin,
		// Token: 0x0400087F RID: 2175
		RigidBody,
		// Token: 0x04000880 RID: 2176
		NoGravity
	}
}
