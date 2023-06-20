using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200023B RID: 571
public class modifierToolLogic : MonoBehaviour
{
	// Token: 0x17000285 RID: 645
	// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00058EB4 File Offset: 0x000570B4
	public Modifier CurrentModifier
	{
		get
		{
			return this._currentModifier;
		}
	}

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00058EBC File Offset: 0x000570BC
	public static bool Initalized
	{
		get
		{
			return modifierToolLogic._initialized;
		}
	}

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00058EC4 File Offset: 0x000570C4
	public static modifierToolLogic Instance
	{
		get
		{
			return modifierToolLogic._instance;
		}
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x00058ECC File Offset: 0x000570CC
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x00058ED4 File Offset: 0x000570D4
	private void Init()
	{
		if (modifierToolLogic._instance)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		modifierToolLogic._instance = this;
		Debug.Log("New Modifer Instance!", modifierToolLogic._instance);
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x00058F04 File Offset: 0x00057104
	private void OnEnable()
	{
		if (this._blocks == null)
		{
			this._objectText.text = string.Empty;
			return;
		}
		this._objectText.text = this._targetRef.name.Split(new char[]
		{
			'(',
			'_'
		})[0];
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x00058F5C File Offset: 0x0005715C
	public void setObjectInfo(List<getModifierOptions.Parameter> _values, GameObject _refObj, Modifier mod)
	{
		this.setObjectInfo(_values, new GameObject[]
		{
			_refObj
		}, mod);
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x00058F70 File Offset: 0x00057170
	public void setObjectInfo(List<getModifierOptions.Parameter> _values, GameObject[] _refObjs, Modifier mod)
	{
		if (modifierToolLogic._initialized)
		{
			return;
		}
		modifierToolLogic._initialized = true;
		this._currentModifier = mod;
		this._blocks = _refObjs.ToBlockList();
		bool flag = false;
		for (int i = 0; i < this._blocks.Count; i++)
		{
			if (this._blocks[i].objsParams != null && !flag)
			{
				Debug.Log("Already Params Here!");
				for (int j = 0; j < this._blocks[i].objsParams.Length; j++)
				{
					_values[j].setValue(this._blocks[i].objsParams[j].getValue());
				}
				break;
			}
		}
		if (this._blocks == null)
		{
			Debug.LogError("Cannot Find Current Block!", _refObjs[0]);
			return;
		}
		this.clearGrid();
		Debug.Log("Setting Object Text for: " + this._blocks[0].id);
		this._objectText.text = this.getObjectTextForBlocks(this._blocks);
		this._targetRef = this._blocks[0].go.transform;
		foreach (getModifierOptions.Parameter parameter in _values)
		{
			switch (parameter.getType())
			{
			case getModifierOptions.Parameter.ParameterTypes.SliderType:
			{
				MonoBehaviour.print(string.Concat(new object[]
				{
					"Slider Type:  ",
					parameter.getName(),
					" Value: ",
					float.Parse(parameter.getValue())
				}));
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.slider_cell);
				gameObject.GetComponent<SliderModifierInfoSet>().SetInfo((getModifierOptions.SliderParameter)parameter);
				gameObject.transform.SetParent(this.grid.transform, false);
				break;
			}
			case getModifierOptions.Parameter.ParameterTypes.BoolType:
			{
				MonoBehaviour.print(string.Concat(new object[]
				{
					"Bool Type: ",
					parameter.getName(),
					" Value: ",
					bool.Parse(parameter.getValue())
				}));
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.bool_cell);
				gameObject2.GetComponent<BoolModifierInfoSet>().SetInfo((getModifierOptions.BoolParameter)parameter);
				gameObject2.transform.SetParent(this.grid.transform, false);
				break;
			}
			case getModifierOptions.Parameter.ParameterTypes.Dropdown:
			{
				MonoBehaviour.print(string.Concat(new object[]
				{
					"Dropdown Type:  ",
					parameter.getName(),
					" Value: ",
					int.Parse(parameter.getValue())
				}));
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.dropDown_cell);
				gameObject3.GetComponent<dropDownModifierSet>().SetInfo((getModifierOptions.DropdownParameter)parameter);
				gameObject3.transform.SetParent(this.grid.transform, false);
				break;
			}
			case getModifierOptions.Parameter.ParameterTypes.Color:
			{
				MonoBehaviour.print("Color Type:  " + parameter.getName() + " Value: " + parameter.getValue());
				GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.color_cell);
				gameObject4.GetComponent<colorModifierSet>().SetInfo((getModifierOptions.ColorParameter)parameter);
				gameObject4.transform.SetParent(this.grid.transform, false);
				break;
			}
			case getModifierOptions.Parameter.ParameterTypes.InputField:
			{
				MonoBehaviour.print("Input Type:  " + parameter.getName() + " Value: " + parameter.getValue());
				GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.input_cell);
				gameObject5.GetComponent<InputfieldModifierSet>().SetInfo((getModifierOptions.InputFieldParameter)parameter);
				gameObject5.transform.SetParent(this.grid.transform, false);
				break;
			}
			default:
				throw new Exception("Invalid ParameterType! " + parameter.getType().ToString());
			}
		}
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x00059374 File Offset: 0x00057574
	private string getObjectTextForBlocks(List<levelEditorManager.Block> _blocks)
	{
		string text = _blocks[0].ObjectName;
		string objectName = _blocks[0].ObjectName;
		for (int i = _blocks.Count - 1; i > 0; i--)
		{
			if (objectName != _blocks[i].ObjectName)
			{
				text = _blocks[i].type.ToString();
				text = text.Substring(1, text.Length - 1 - "block".Length);
				break;
			}
		}
		return text;
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x00059404 File Offset: 0x00057604
	public void setObjectInfoPathRenderer(List<getModifierOptions.Parameter> _values, GameObject _refObj)
	{
		this._blocks = new List<levelEditorManager.Block>(1);
		this._blocks[0].go = _refObj;
		this.clearGrid();
		this._objectText.text = string.Empty;
		foreach (getModifierOptions.Parameter parameter in _values)
		{
			switch (parameter.getType())
			{
			case getModifierOptions.Parameter.ParameterTypes.SliderType:
			{
				MonoBehaviour.print(string.Concat(new object[]
				{
					"Slider Type:  ",
					parameter.getName(),
					" Value: ",
					float.Parse(parameter.getValue())
				}));
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.slider_cell);
				gameObject.GetComponent<SliderModifierInfoSet>().SetInfo((getModifierOptions.SliderParameter)parameter);
				gameObject.transform.SetParent(this.grid.transform, false);
				break;
			}
			case getModifierOptions.Parameter.ParameterTypes.BoolType:
			{
				MonoBehaviour.print(string.Concat(new object[]
				{
					"Bool Type: ",
					parameter.getName(),
					" Value: ",
					bool.Parse(parameter.getValue())
				}));
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.bool_cell);
				gameObject2.GetComponent<BoolModifierInfoSet>().SetInfo((getModifierOptions.BoolParameter)parameter);
				gameObject2.transform.SetParent(this.grid.transform, false);
				break;
			}
			case getModifierOptions.Parameter.ParameterTypes.Dropdown:
			{
				MonoBehaviour.print(string.Concat(new object[]
				{
					"Dropdown Type:  ",
					parameter.getName(),
					" Value: ",
					int.Parse(parameter.getValue())
				}));
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.dropDown_cell);
				gameObject3.GetComponent<dropDownModifierSet>().SetInfo((getModifierOptions.DropdownParameter)parameter);
				gameObject3.transform.SetParent(this.grid.transform, false);
				break;
			}
			case getModifierOptions.Parameter.ParameterTypes.Color:
			{
				MonoBehaviour.print("Color Type:  " + parameter.getName() + " Value: " + parameter.getValue());
				GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.color_cell);
				gameObject4.GetComponent<colorModifierSet>().SetInfo((getModifierOptions.ColorParameter)parameter);
				gameObject4.transform.SetParent(this.grid.transform, false);
				break;
			}
			case getModifierOptions.Parameter.ParameterTypes.InputField:
			{
				MonoBehaviour.print("Input Type:  " + parameter.getName() + " Value: " + parameter.getValue());
				GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.input_cell);
				gameObject5.GetComponent<InputfieldModifierSet>().SetInfo((getModifierOptions.InputFieldParameter)parameter);
				gameObject5.transform.SetParent(this.grid.transform, false);
				break;
			}
			default:
				throw new Exception("Not included param type!!" + parameter.getType().ToString());
			}
		}
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x000596F4 File Offset: 0x000578F4
	private void clearGrid()
	{
		foreach (Transform transform in this.grid.GetComponentsInChildren<Transform>())
		{
			if (transform != this.grid.transform)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x00059748 File Offset: 0x00057948
	public void ParamValuesChanged()
	{
		ObjectParameterContainer[] @params = this.MakeParameterList().ToArray().ToContainerParameters();
		foreach (levelEditorManager.Block block in this._blocks)
		{
			block.setObjParams(@params, true, true);
		}
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x000597C4 File Offset: 0x000579C4
	public void OnButtonDone()
	{
		if (!modifierToolLogic.Initalized)
		{
			return;
		}
		Debug.Log("Done, Send parameters To object plz");
		if (this._currentModifier == null)
		{
			return;
		}
		List<getModifierOptions.Parameter> list = this.MakeParameterList();
		if (this._blocks[0].go == null)
		{
			return;
		}
		if (this._blocks[0].go.transform.parent.name.Contains("Pathrenderer"))
		{
			MonoBehaviour.print("Pathrenderer Block!");
			base.StartCoroutine(this._blocks[0].go.transform.parent.GetComponent<PathrendererModifier>().setTypes(int.Parse(list[0].getValue())));
			return;
		}
		ObjectParameterContainer[] array = list.ToArray().ToContainerParameters();
		if (this.IsParametersEquals(array, DefaultParameters.getDefaultParameters(this._blocks[0].go.name.Split(new char[]
		{
			'('
		})[0]).ToContainerParameters()))
		{
			array = null;
		}
		foreach (levelEditorManager.Block block in this._blocks)
		{
			Debug.Log("Sending parameters to: " + block.go.name, block.go);
			block.setObjParams(array, false, true);
		}
		this._currentModifier = null;
		TutorialHandler.Instance.SpecialEvents[15].Clicked();
		modifierToolLogic._initialized = false;
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x0005997C File Offset: 0x00057B7C
	public void Cancel()
	{
		this.clearGrid();
		if (this._blocks != null)
		{
			foreach (levelEditorManager.Block block in this._blocks)
			{
				block.resetObjParams();
			}
		}
		this._currentModifier = null;
		modifierToolLogic._initialized = false;
		Debug.Log("Cancel Modifier");
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x00059A0C File Offset: 0x00057C0C
	private List<getModifierOptions.Parameter> MakeParameterList()
	{
		List<getModifierOptions.Parameter> list = new List<getModifierOptions.Parameter>();
		foreach (ModifierSetBase modifierSetBase in this.grid.GetComponentsInChildren<ModifierSetBase>())
		{
			list.Add(modifierSetBase.getParameter());
		}
		return list;
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x00059A50 File Offset: 0x00057C50
	private bool IsParametersEquals(ObjectParameterContainer[] arr1, ObjectParameterContainer[] arr2)
	{
		if (arr1 == null)
		{
			return true;
		}
		if (arr1.Length == arr2.Length)
		{
			for (int i = 0; i < arr1.Length; i++)
			{
				if (!arr1[i].Equals(arr2[i]))
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x04000A4F RID: 2639
	private Modifier _currentModifier;

	// Token: 0x04000A50 RID: 2640
	public GameObject grid;

	// Token: 0x04000A51 RID: 2641
	public GameObject bool_cell;

	// Token: 0x04000A52 RID: 2642
	public GameObject slider_cell;

	// Token: 0x04000A53 RID: 2643
	public GameObject dropDown_cell;

	// Token: 0x04000A54 RID: 2644
	public GameObject color_cell;

	// Token: 0x04000A55 RID: 2645
	public GameObject input_cell;

	// Token: 0x04000A56 RID: 2646
	private Transform _targetRef;

	// Token: 0x04000A57 RID: 2647
	public Text _objectText;

	// Token: 0x04000A58 RID: 2648
	private List<levelEditorManager.Block> _blocks;

	// Token: 0x04000A59 RID: 2649
	private static bool _initialized;

	// Token: 0x04000A5A RID: 2650
	private static modifierToolLogic _instance;
}
