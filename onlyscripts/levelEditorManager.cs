using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using RTEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200022F RID: 559
public class levelEditorManager : MonoBehaviour
{
	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0004F928 File Offset: 0x0004DB28
	public static int getDefaultLayer
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0004F92C File Offset: 0x0004DB2C
	public static string fileEnding
	{
		get
		{
			return ".clustertruck";
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0004F934 File Offset: 0x0004DB34
	public static int EVENT_SELFREFERENCE
	{
		get
		{
			return -2;
		}
	}

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0004F938 File Offset: 0x0004DB38
	public static int EVENT_THATREFERENCE
	{
		get
		{
			return -1;
		}
	}

	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0004F93C File Offset: 0x0004DB3C
	public static string DefaultFilepath
	{
		get
		{
			return levelEditorManager._defaultFilePath;
		}
	}

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0004F944 File Offset: 0x0004DB44
	// (set) Token: 0x06000D2D RID: 3373 RVA: 0x0004F974 File Offset: 0x0004DB74
	public static string CurrentFilePath
	{
		get
		{
			Debug.Log("Current FIlePah: " + PlayerPrefs.GetString("CustomFilePath", levelEditorManager._defaultFilePath));
			return PlayerPrefs.GetString("CustomFilePath", levelEditorManager._defaultFilePath);
		}
		set
		{
			PlayerPrefs.SetString("CustomFilePath", value);
			Debug.Log("New file path: " + levelEditorManager.CurrentFilePath);
		}
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0004F998 File Offset: 0x0004DB98
	public static levelEditorManager Instance()
	{
		if (!levelEditorManager._levelEditorManager)
		{
			levelEditorManager._levelEditorManager = (UnityEngine.Object.FindObjectOfType(typeof(levelEditorManager)) as levelEditorManager);
			if (!levelEditorManager._levelEditorManager)
			{
				Debug.LogError("There needs to be one active levelEditorManager script on a GameObject in your scene.");
			}
		}
		return levelEditorManager._levelEditorManager;
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0004F9EC File Offset: 0x0004DBEC
	private bool HasSelectedGameObject
	{
		get
		{
			if (MonoSingletonBase<EditorObjectSelection>.Instance.SelectedGameObjects.Count == 0)
			{
				this.targetedGobj = null;
				return false;
			}
			return true;
		}
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0004FA0C File Offset: 0x0004DC0C
	public bool TruckColorCoding
	{
		get
		{
			return this._truckColorCodingToggle.isOn || levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step2 || levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step4;
		}
	}

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0004FA38 File Offset: 0x0004DC38
	public Camera MainCamera
	{
		get
		{
			return this.m_editorCamera;
		}
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0004FA40 File Offset: 0x0004DC40
	public bool ListenForHotkeys
	{
		get
		{
			return !this.IsBusy && !levelEditorManager.canLook && !this._playTesting && (TutorialHandler.Instance == null || !TutorialHandler.Instance.IsHelpMenuOpen) && !this.proptertiesInfoPopup.activeInHierarchy;
		}
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0004FAA8 File Offset: 0x0004DCA8
	public bool IsPlayTesting
	{
		get
		{
			return this._playTesting;
		}
	}

	// Token: 0x1700027F RID: 639
	// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0004FAB0 File Offset: 0x0004DCB0
	public bool IsBusy
	{
		get
		{
			return this.loadPopUp.activeSelf || this.uploadInfoPopUp.activeSelf || this._saveMenuOpen || this.dontRecieveInput || this.settingsInfoPopup.activeSelf;
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0004FB04 File Offset: 0x0004DD04
	public bool CanUseGizmos
	{
		get
		{
			bool flag = EventSystem.current.currentSelectedGameObject == null || EventSystem.current.currentSelectedGameObject.layer != 5;
			return this._canUseGizmos && !this.IsBusy && !this._playTesting && flag;
		}
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0004FB68 File Offset: 0x0004DD68
	// (set) Token: 0x06000D37 RID: 3383 RVA: 0x0004FB70 File Offset: 0x0004DD70
	public static levelEditorManager.menuState CurrentMenuState
	{
		get
		{
			return levelEditorManager._CurrentMenuState;
		}
		set
		{
			if (value == levelEditorManager.menuState.step2 || value == levelEditorManager.menuState.step4 || value == levelEditorManager.menuState.step5)
			{
				ToolPanelRef.Instance().ShowIcon(false);
			}
			else
			{
				ToolPanelRef.Instance().ShowIcon(true);
			}
			levelEditorManager._CurrentMenuState = value;
		}
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0004FBB4 File Offset: 0x0004DDB4
	public Transform getTargetedGameObject()
	{
		if (this.targetedGobj != null)
		{
			return this.targetedGobj.transform;
		}
		return null;
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0004FBD4 File Offset: 0x0004DDD4
	public void ApplySettingsToScene()
	{
		string[] array = this.currentMap.getSettings();
		if (array.Length == 0)
		{
			Debug.Log("No Setting, Making Default ones!");
			array = settingsPanelOnSet.defaultSettings;
			foreach (string str in array)
			{
				Debug.Log("Setting: " + str);
			}
		}
		Vector3 position = UnityEngine.Object.FindObjectOfType<KillOnHeight>().transform.position;
		UnityEngine.Object.FindObjectOfType<KillOnHeight>().transform.position = new Vector3(position.x, float.Parse(array[0]), position.z);
		int num = int.Parse(array[2]);
		if (num == 0)
		{
			this.worldPlane.GetComponent<Collider>().enabled = false;
		}
		else
		{
			this.worldPlane.GetComponent<Collider>().enabled = true;
		}
		this.worldPlane.GetComponent<Renderer>().material = this.TerrainMaterials[num];
		Material skybox = null;
		int num2 = int.Parse(array[3]);
		if (num2 >= this.SkyBoxes.Length)
		{
			string[] customSkyBoxSettings = this.currentMap.getCustomSkyBoxSettings();
			if (customSkyBoxSettings.Length == 0)
			{
				throw new Exception("No SkyBox Settings Saved!");
			}
			Debug.Log("CustomSkybox: " + (num2 - this.SkyBoxes.Length));
			if (num2 - this.SkyBoxes.Length == 1)
			{
				Debug.Log("GRADIENT!");
				this.GradientSkyBox.SetFloat("_Exponent", float.Parse(customSkyBoxSettings[3]));
				this.GradientSkyBox.SetFloat("_Intensity", float.Parse(customSkyBoxSettings[2]));
				this.GradientSkyBox.SetFloat("_UpVectorPitch", float.Parse(customSkyBoxSettings[4]));
				this.GradientSkyBox.SetFloat("_UpVectorYaw", float.Parse(customSkyBoxSettings[5]));
				this.GradientSkyBox.SetColor("_Color1", extensionMethods.HexToColor(customSkyBoxSettings[1]));
				this.GradientSkyBox.SetColor("_Color2", extensionMethods.HexToColor(customSkyBoxSettings[0]));
				skybox = this.GradientSkyBox;
			}
			else if (num2 - this.SkyBoxes.Length == 2)
			{
				Debug.Log("HORIOXON!");
				this.HorizonSkyBox.SetFloat("_SkyExponent1", float.Parse(customSkyBoxSettings[1]));
				this.HorizonSkyBox.SetFloat("_SkyExponent2", float.Parse(customSkyBoxSettings[4]));
				this.HorizonSkyBox.SetFloat("_SkyIntensity", float.Parse(customSkyBoxSettings[5]));
				this.HorizonSkyBox.SetFloat("_SunAlpha", float.Parse(customSkyBoxSettings[8]));
				this.HorizonSkyBox.SetFloat("_SunBeta", float.Parse(customSkyBoxSettings[9]));
				this.HorizonSkyBox.SetFloat("_SunIntensity", float.Parse(customSkyBoxSettings[7]));
				this.HorizonSkyBox.SetFloat("_SunAzimuth", float.Parse(customSkyBoxSettings[10]));
				this.HorizonSkyBox.SetFloat("_SunAltitude", float.Parse(customSkyBoxSettings[11]));
				this.HorizonSkyBox.SetColor("_SkyColor1", extensionMethods.HexToColor(customSkyBoxSettings[0]));
				this.HorizonSkyBox.SetColor("_SkyColor2", extensionMethods.HexToColor(customSkyBoxSettings[2]));
				this.HorizonSkyBox.SetColor("_SkyColor3", extensionMethods.HexToColor(customSkyBoxSettings[3]));
				this.HorizonSkyBox.SetColor("_SunColor", extensionMethods.HexToColor(customSkyBoxSettings[6]));
				skybox = this.HorizonSkyBox;
			}
		}
		else
		{
			skybox = this.SkyBoxes[num2];
		}
		RenderSettings.skybox = skybox;
		RenderSettings.fogColor = extensionMethods.HexToColor(array[4]);
		RenderSettings.fogDensity = float.Parse(array[5]);
		RenderSettings.ambientSkyColor = extensionMethods.HexToColor(array[6]);
		RenderSettings.ambientGroundColor = extensionMethods.HexToColor(array[7]);
		RenderSettings.ambientEquatorColor = extensionMethods.HexToColor(array[8]);
		Physics.gravity = new Vector3(Physics.gravity.x, float.Parse(array[10]), Physics.gravity.z);
		this.m_editorCamera.GetComponent<Tonemapping>().middleGrey = float.Parse(array[9]);
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x0004FFC8 File Offset: 0x0004E1C8
	private Color getColorAt(int i)
	{
		return this.colors[i];
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x0004FFDC File Offset: 0x0004E1DC
	private void Awake()
	{
		Debug.LogError("INITLIALIZING LEVELEDITORMANAGER!");
		settingsPanelOnSet.Init();
		List<string> options = new List<string>
		{
			"Destructible",
			"Props",
			"Roads",
			"Shapes",
			"Traps",
			"Vehicles",
			"Events"
		};
		this._blockType_Dropdown.AddOptions(options);
		foreach (Dropdown.OptionData optionData in this._blockType_Dropdown.options)
		{
			this._blockcategoriesArray.Add(optionData.text);
		}
		this._pointerMarker = base.transform.FindChild("pointer").gameObject;
		if (UnityEngine.Object.FindObjectOfType<dataBaseHandler>() == null)
		{
			GameObject gameObject = new GameObject("DB_Handler");
			dataBaseHandler dataBaseHandler = gameObject.AddComponent<dataBaseHandler>();
			this.db_Handler = dataBaseHandler;
		}
		else
		{
			this.db_Handler = UnityEngine.Object.FindObjectOfType<dataBaseHandler>();
		}
		if (UnityEngine.Object.FindObjectOfType<steam_WorkshopHandler>() == null)
		{
			GameObject gameObject2 = new GameObject("WS_Handler");
			steam_WorkshopHandler steam_WorkshopHandler = gameObject2.AddComponent<steam_WorkshopHandler>();
			this.workshop_handler = steam_WorkshopHandler;
		}
		else
		{
			this.workshop_handler = UnityEngine.Object.FindObjectOfType<steam_WorkshopHandler>();
		}
		this.m_editorCamera = base.GetComponent<Camera>();
		Button[] componentsInChildren = this.step2.GetComponentsInChildren<Button>(true);
		this.colors = new Color[componentsInChildren.Length];
		int num = 0;
		foreach (Button button in componentsInChildren)
		{
			if (button.tag == "waypointColor")
			{
				Color normalColor = button.colors.normalColor;
				Debug.Log(button.colors.normalColor);
				this.colors[num] = normalColor;
				num++;
			}
		}
		this.spawnDefaultObjects();
		this.modalPanel = ModalPanel.Instance();
		this.displayManager = DisplayManager.Instance();
		this.myYesAction = new UnityAction(this.SaveYesFunction);
		this.myNoAction = new UnityAction(this.SaveNoFunction);
		this.myCancelAction = new UnityAction(this.SaveCancelFunction);
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0005025C File Offset: 0x0004E45C
	private void spawnDefaultObjects()
	{
		Debug.Log("Spawning Default Objects");
		GameObject gameObject = UnityEngine.Object.FindObjectOfType<editor_first>().gameObject;
		GameObject block = GameObject.FindGameObjectWithTag("Player");
		this.addBlockToMap(gameObject, levelEditorManager.blockType.btruckblock);
		this.addBlockToMap(block, levelEditorManager.blockType.bplayerspawn);
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x0005029C File Offset: 0x0004E49C
	private void Start()
	{
		this.mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.onChangedTool(1);
		this.checkButtons();
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x000502CC File Offset: 0x0004E4CC
	private void checkButtons()
	{
		Debug.Log("Checking Buttons");
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x000502D8 File Offset: 0x0004E4D8
	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			Debug.Log("DOWN");
			this.lockCursor(true);
		}
		if (Input.GetMouseButtonUp(1))
		{
			Debug.Log("UP");
			this.lockCursor(false);
		}
		if (this.ListenForHotkeys)
		{
			this.listenForHotKeyes();
		}
		if (Input.GetKeyDown(KeyCode.V) && this.targetedGobj != null)
		{
			this.zoomToSelectedObject();
		}
		if (Input.GetKeyDown(KeyCode.O))
		{
			int num = 1;
			foreach (levelEditorManager.Block block in this.currentMap.tiles)
			{
				Debug.Log(block.go.name + " INDEX: " + num);
				num++;
			}
		}
		if (!this._playTesting)
		{
			if (this.IsBusy)
			{
				return;
			}
			if (this.CanUseGizmos)
			{
				if (Input.GetKeyDown(KeyCode.Delete))
				{
					this.tryDeleteSelection();
				}
				if (Input.GetKeyDown(KeyCode.F))
				{
					this.groundObjectSelection();
				}
				if (Application.isEditor)
				{
					if (Input.GetKeyDown(KeyCode.D) && InputHelper.IsAnyCtrlOrCommandKeyPressed() && InputHelper.IsAnyShiftKeyPressed())
					{
						this.tryDuplicateSelection();
					}
				}
				else if (Input.GetKeyDown(KeyCode.D) && InputHelper.IsAnyCtrlOrCommandKeyPressed())
				{
					this.tryDuplicateSelection();
				}
			}
			if (levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step1)
			{
				this._canUseGizmos = true;
				switch (this._currentTool)
				{
				case levelEditorManager.Tool.addTool:
					this._canUseGizmos = false;
					if (this.targetedGobj != null)
					{
						roadHandleConnection roadHandleConnection = null;
						if (this.targetedGobj.GetComponentInChildren<RoadHandle>())
						{
							float positiveInfinity = float.PositiveInfinity;
							foreach (RoadHandle roadHandle in this.targetedGobj.GetComponentsInChildren<RoadHandle>())
							{
								if (roadHandle.Search() && roadHandle.currentConnection.distanceBetween < positiveInfinity)
								{
									roadHandleConnection = roadHandle.currentConnection;
									Debug.Log("Closest Transfom:", roadHandleConnection.getT2());
								}
							}
						}
						if (levelEditorManager.canLook && roadHandleConnection == null)
						{
							if (Input.GetMouseButton(1))
							{
								this.targetedGobj.transform.parent = this.mapBase.transform;
								this.setLayerForObject(this.targetedGobj.transform, 0);
								this.revertObject(this.targetedGobj);
								this.onChangedTool((int)this._previousTool);
								this.targetedGobj = null;
							}
							return;
						}
						Vector3 vector = Vector3.zero;
						RaycastHit raycastHit;
						Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit, 500f, this.UIBlockMask);
						if (raycastHit.collider)
						{
							Debug.DrawLine(this.m_editorCamera.transform.position, raycastHit.point, Color.red);
							Vector3 b = (!this.targetedGobj.GetComponentInChildren<Foot>()) ? Vector3.zero : (this.targetedGobj.GetComponentInChildren<Foot>().transform.position - this.targetedGobj.transform.position);
							if (Input.GetKey(KeyCode.LeftControl))
							{
								vector = new Vector3(Mathf.Round(raycastHit.point.x), raycastHit.point.y - b.y, Mathf.Round(raycastHit.point.z));
								levelEditorManager.rotatingBool = false;
							}
							else
							{
								vector = ((!this._freeModeBool) ? (new Vector3(raycastHit.point.x, raycastHit.point.y, raycastHit.point.z) - b) : Vector3.zero);
								levelEditorManager.rotatingBool = false;
							}
						}
						if (Input.GetMouseButtonDown(2))
						{
							this.scrollDelta = 0f;
						}
						this.scrollDelta -= Input.GetAxis("Mouse ScrollWheel") * 0.2f;
						vector -= (vector - this.m_editorCamera.transform.position) * this.scrollDelta;
						if (roadHandleConnection != null && this.roadHandleConnection(vector, roadHandleConnection))
						{
							return;
						}
						this.targetedGobj.transform.position = vector;
					}
					if (Input.GetMouseButtonDown(0))
					{
						RaycastHit raycastHit2;
						Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit2, 500f, this.onlyUIMask);
						if (raycastHit2.collider)
						{
							if (this.targetedGobj != null)
							{
								UnityEngine.Object.Destroy(this.targetedGobj);
								this.targetedGobj = null;
								this.onChangedTool((int)this._previousTool);
							}
							return;
						}
						if (this.targetedGobj != null)
						{
							this.targetedGobj.transform.parent = this.mapBase.transform;
							this.setLayerForObject(this.targetedGobj.transform, 0);
							this.addBlockToMap(this.targetedGobj);
							ObjectSelectionSnapshot objectSelectionSnapshot = new ObjectSelectionSnapshot();
							objectSelectionSnapshot.TakeSnapshot();
							MonoSingletonBase<EditorObjectSelection>.Instance.clearSelection();
							MonoSingletonBase<EditorObjectSelection>.Instance.addObjectToSelection(this.targetedGobj);
							if (objectSelectionSnapshot != null)
							{
								ObjectSelectionSnapshot objectSelectionSnapshot2 = new ObjectSelectionSnapshot();
								objectSelectionSnapshot2.TakeSnapshot();
								PostObjectSelectionChangedAction postObjectSelectionChangedAction = new PostObjectSelectionChangedAction(objectSelectionSnapshot, objectSelectionSnapshot2);
								postObjectSelectionChangedAction.Execute();
							}
							if (this.currentBlockString == "truck")
							{
								TutorialHandler.Instance.SpecialEvents[2].Clicked();
								foreach (GameObject gameObject in this.truckFormations)
								{
									this.addBlockToMap(gameObject);
									foreach (Transform transform in gameObject.GetComponentsInChildren<Transform>())
									{
										this.setLayerForObject(transform, 0);
										this.addBlockToMap(transform.gameObject);
									}
								}
								this.truckFormations.Clear();
								this.currentTruckFormationIndex = 1;
							}
							if (this.targetedGobj.GetComponent<BlockInfo>().Category.ToLower().Contains("trap"))
							{
								TutorialHandler.Instance.SpecialEvents[13].Clicked();
							}
							if (this.currentBlockString != string.Empty && Input.GetKey(KeyCode.LeftShift))
							{
								Vector3 localScale = this.targetedGobj.transform.localScale;
								ObjectParameterContainer[] objsParams = this.currentMap.getTileAt(this.targetedGobj.transform).objsParams;
								string category = this.targetedGobj.GetComponent<BlockInfo>().Category;
								this.targetedGobj = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("mapEditor/" + this.currentCategory + this.currentBlockString), new Vector3(0f, -100f, 0f), this.targetedGobj.transform.rotation);
								this.targetedGobj.AddComponent<BlockInfo>().Category = category;
								this.currentDelayParameter = ((objsParams == null) ? null : new levelEditorManager.delayParameters(this.targetedGobj.transform, objsParams));
								if (this.currentDelayParameter != null)
								{
									Debug.Log("Copy Parameters ", this.currentDelayParameter.getObj());
								}
								this.setLayerForObject(this.targetedGobj.transform, 2);
								this.targetedGobj.transform.localScale = localScale;
								this._assignMaterials(this.targetedGobj);
								if (this.currentBlockString == "truck")
								{
									TutorialHandler.Instance.SpecialEvents[7].Clicked();
								}
							}
							else
							{
								this.targetedGobj = null;
								this.onChangedTool((int)this._previousTool);
							}
						}
						else
						{
							RaycastHit raycastHit3;
							Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit3);
							if (raycastHit3.collider && raycastHit3.collider.GetComponent<World>() == null)
							{
								GameObject gameObject2 = raycastHit3.collider.gameObject;
								Debug.Log("Hit", gameObject2);
								foreach (levelEditorManager.Block block2 in this.currentMap.tiles)
								{
									if (block2.go.Equals(gameObject2))
									{
										Debug.Log("Selecteed: ", gameObject2);
										this.targetedGobj = gameObject2;
										this._assignMaterials(this.targetedGobj);
										this.setLayerForObject(this.targetedGobj.transform, 2);
										this.onChangedTool(1);
									}
								}
							}
						}
					}
					if (Input.GetKeyDown(KeyCode.Delete))
					{
						Debug.Log("Delete");
						this.tryDelete(this.targetedGobj);
					}
					break;
				case levelEditorManager.Tool.moveTool:
					if (this.targetedGobj != null && this.HasSelectedGameObject)
					{
						roadHandleConnection roadHandleConnection2 = null;
						if (this.targetedGobj.GetComponentInChildren<RoadHandle>())
						{
							Debug.Log("Looking for roadhandles!");
							float positiveInfinity2 = float.PositiveInfinity;
							foreach (RoadHandle roadHandle2 in this.targetedGobj.GetComponentsInChildren<RoadHandle>())
							{
								if (roadHandle2.Search() && roadHandle2.currentConnection.distanceBetween < positiveInfinity2)
								{
									roadHandleConnection2 = roadHandle2.currentConnection;
									Debug.Log("Closest Transfom:", roadHandleConnection2.getT2());
								}
							}
						}
						if (roadHandleConnection2 != null)
						{
							Vector3 pos = Vector3.zero;
							pos = MonoSingletonBase<EditorGizmoSystem>.Instance.TranslationGizmo.transform.position;
							if (this.roadHandleConnection(pos, roadHandleConnection2))
							{
								return;
							}
						}
						return;
					}
					if (RoadHandler.Instance().isInitalized || levelEditorManager.canLook)
					{
						return;
					}
					if (Input.GetMouseButtonDown(0))
					{
						RaycastHit raycastHit4;
						Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit4, 0f, this.onlyUIMask);
						if (raycastHit4.collider)
						{
							return;
						}
						Debug.Log(1);
						RaycastHit hit;
						Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out hit);
						if (hit.collider)
						{
							Debug.Log(2);
							if (hit.collider.GetComponent<World>() == null)
							{
								this.checkForRoadhandle(hit);
								return;
							}
						}
					}
					break;
				case levelEditorManager.Tool.rotationTool:
					if (this.targetedGobj != null)
					{
						this.savedRot = this.targetedGobj.transform.rotation.eulerAngles;
						levelEditorManager.rotatingBool = true;
						this.rotationFloat += -(Input.GetAxis("Mouse X") * 10f);
						if (this.targetedGobj.GetComponentInChildren<RoadHandle>())
						{
							if (Input.GetKey(KeyCode.LeftShift))
							{
								this.rotationFloat = Mathf.Round(this.rotationFloat / 22.5f) * 22.5f;
							}
							foreach (RoadHandle roadHandle3 in this.targetedGobj.GetComponentsInChildren<RoadHandle>())
							{
								if (roadHandle3.PermanentConnection)
								{
									this.targetedGobj.transform.RotateAround(roadHandle3.transform.position, Vector3.up, this.rotationFloat);
									this.rotationFloat = 0f;
									if (Input.GetMouseButtonDown(0))
									{
										this.targetedGobj.transform.parent = this.mapBase.transform;
										this.setLayerForObject(this.targetedGobj.transform, 0);
										this.updateBlockToMap(this.targetedGobj.transform);
										this.targetedGobj = null;
									}
									return;
								}
							}
						}
						if (levelEditorManager.canLook)
						{
							if (Input.GetMouseButton(1))
							{
								this.targetedGobj.transform.parent = this.mapBase.transform;
								this.setLayerForObject(this.targetedGobj.transform, 0);
								this.revertObject(this.targetedGobj);
								this.onChangedTool((int)this._previousTool);
								this.targetedGobj = null;
							}
							return;
						}
						Debug.Log("Rotating: " + this.rotationFloat);
						this.savedRot = new Vector3(this.savedRot.x, this.rotationFloat, this.savedRot.z);
						Vector3 euler = this.savedRot;
						if (Input.GetKey(KeyCode.LeftShift))
						{
							euler = new Vector3(this.savedRot.x, Mathf.Round(this.savedRot.y / 22.5f) * 22.5f, this.savedRot.z);
						}
						this.targetedGobj.transform.rotation = Quaternion.Euler(euler);
					}
					if (Input.GetMouseButtonDown(0))
					{
						if (this.targetedGobj != null)
						{
							this.targetedGobj.transform.parent = this.mapBase.transform;
							this.setLayerForObject(this.targetedGobj.transform, 0);
							this.updateBlockToMap(this.targetedGobj.transform);
							this.targetedGobj = null;
						}
						else
						{
							if (levelEditorManager.canLook)
							{
								return;
							}
							RaycastHit raycastHit5;
							Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit5, 0f, this.onlyUIMask);
							if (raycastHit5.collider)
							{
								return;
							}
							RaycastHit hit2;
							Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out hit2);
							if (hit2.collider && hit2.collider.GetComponent<World>() == null)
							{
								this.checkForRoadhandle(hit2);
								return;
							}
						}
					}
					break;
				case levelEditorManager.Tool.deleteTool:
					if (levelEditorManager.canLook)
					{
						return;
					}
					if (Input.GetMouseButtonDown(0))
					{
						RaycastHit raycastHit6;
						Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit6, 500f, this.onlyUIMask);
						if (raycastHit6.collider)
						{
							return;
						}
						RaycastHit raycastHit7;
						Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit7);
						if (raycastHit7.collider)
						{
							Debug.Log(2);
							if (raycastHit7.collider.GetComponent<World>() == null)
							{
								GameObject gameObject3 = raycastHit7.collider.gameObject;
								this.tryDelete(gameObject3);
							}
						}
					}
					break;
				case levelEditorManager.Tool.eventTool:
					if (levelEditorManager.canLook)
					{
						return;
					}
					if (Input.GetMouseButtonDown(0))
					{
						Debug.Log(1);
						RaycastHit raycastHit8;
						Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit8, 500f, this.onlyUIMask);
						if (raycastHit8.collider)
						{
							return;
						}
						RaycastHit hit3;
						Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out hit3);
						GameObject gameObject4 = base.gameObject;
						if (!hit3.collider)
						{
							return;
						}
						if (!(hit3.collider.GetComponent<World>() == null))
						{
							return;
						}
						if (this.checkForRoadhandle(hit3))
						{
							return;
						}
						GameObject gameObject5 = hit3.collider.gameObject;
						Debug.Log("HitParent", gameObject5.transform.parent);
						if (!gameObject5.transform.parent.Equals(this.mapBase.transform))
						{
							while (!gameObject5.transform.parent.Equals(this.mapBase.transform))
							{
								if (gameObject5.GetComponent<editor_first>())
								{
								}
								gameObject5 = gameObject5.transform.parent.gameObject;
							}
							Debug.Log("Root Gameobj: " + gameObject5.name, gameObject5);
						}
						this.targetedGobj = gameObject5;
						if (this.targetedGobj != null)
						{
							GameObject[] array = new GameObject[]
							{
								this.targetedGobj
							};
							if (array.Length > 0)
							{
								EventToolLogic.Instance.Init(array);
							}
						}
					}
					break;
				}
			}
			else
			{
				if (levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step2)
				{
					this._canUseGizmos = false;
					switch (this._currentTool)
					{
					case levelEditorManager.Tool.addTool:
						if (Input.GetMouseButtonDown(0))
						{
							RaycastHit raycastHit9;
							Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit9, 500f, this.onlyUIMask);
							if (raycastHit9.collider)
							{
								return;
							}
							Debug.Log(1);
							RaycastHit raycastHit10;
							Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit10, 500f, this.myMask);
							if (raycastHit10.collider)
							{
								int currentType = this.getCurrentType();
								this.targetedGobj = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("mapEditor/waypointPointer"), Vector3.zero, Quaternion.identity);
								this.currentMaterial = null;
								this.targetedGobj.GetComponent<Renderer>().material.color = this.currentWaypointColor;
								Vector3 position = new Vector3(raycastHit10.point.x, raycastHit10.point.y, raycastHit10.point.z);
								if (!this.mProjector.gameObject.activeSelf)
								{
									this.mProjector.gameObject.SetActive(true);
									this.mProjector.material.color = this.currentWaypointColor;
								}
								this.mProjector.transform.position = position;
								this.prevPos = position;
								this.targetedGobj.transform.position = position;
								GameObject gameObject6 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("roadPoint"), position, Quaternion.identity);
								gameObject6.AddComponent<BlockInfo>().Category = "roadpoint";
								gameObject6.layer = 9;
								this.addBlockToMap(gameObject6);
								gameObject6.GetComponent<wayPointType>().setType(currentType, false);
								Debug.Log("Waypoint with type: " + currentType + " Set!");
							}
						}
						else if (Input.GetMouseButton(0))
						{
							RaycastHit raycastHit9;
							Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit9, 500f, this.onlyUIMask);
							if (raycastHit9.collider)
							{
								return;
							}
							RaycastHit raycastHit11;
							Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit11, 500f, this.myMask);
							if (raycastHit11.collider)
							{
								Debug.Log("Hitting wth roadpoint: " + raycastHit11.collider.name, raycastHit11.collider);
								if (!this.mProjector.gameObject.activeSelf)
								{
									this.mProjector.gameObject.SetActive(true);
								}
								Vector3 vector2 = new Vector3(raycastHit11.point.x, raycastHit11.point.y, raycastHit11.point.z);
								if (Vector3.Distance(this.prevPos, vector2) > this.distance)
								{
									int currentType2 = this.getCurrentType();
									this.prevPos = vector2;
									GameObject gameObject7 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("roadPoint"), vector2, Quaternion.identity);
									gameObject7.AddComponent<BlockInfo>().Category = "roadpoint";
									this.mProjector.transform.position = gameObject7.transform.position;
									gameObject7.layer = 9;
									this.addBlockToMap(gameObject7);
									gameObject7.GetComponent<wayPointType>().setType(currentType2, false);
									Debug.Log("Waypoint with type: " + currentType2 + " Set!");
									this.currentVertexCount++;
								}
								if (this.targetedGobj != null)
								{
									this.targetedGobj.transform.position = vector2;
								}
							}
						}
						if (Input.GetMouseButtonUp(0))
						{
							if (this.justChangedState)
							{
								this.justChangedState = false;
								return;
							}
							RaycastHit raycastHit9;
							Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit9, 500f, this.onlyUIMask);
							if (raycastHit9.collider)
							{
								return;
							}
							TutorialHandler.Instance.SpecialEvents[9].Clicked();
							TutorialHandler.Instance.SpecialEvents[11].Clicked();
							if (this.step2.activeSelf)
							{
								if (this.mProjector.gameObject.activeSelf)
								{
									this.mProjector.gameObject.SetActive(false);
								}
								RaycastHit raycastHit12;
								Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit12, 500f, this.myMask);
								if (raycastHit12.collider)
								{
									Vector3 point = raycastHit12.point;
									int currentType3 = this.getCurrentType();
									GameObject gameObject8 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("roadPoint"), point, Quaternion.identity);
									gameObject8.AddComponent<BlockInfo>().Category = "roadpoint";
									gameObject8.layer = 9;
									this.addBlockToMap(gameObject8);
									gameObject8.GetComponent<wayPointType>().setType(currentType3, false);
								}
								UnityEngine.Object.Destroy(this.targetedGobj);
								this.targetedGobj = null;
							}
						}
						if (Input.GetKeyDown(KeyCode.Escape))
						{
							UnityEngine.Object.Destroy(this.targetedGobj);
							this.targetedGobj = null;
							levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step1;
							this.resetTrucks();
							this.onChangedTool((int)this._previousTool);
						}
						break;
					}
					return;
				}
				if (levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step3)
				{
					this._canUseGizmos = true;
					switch (this._currentTool)
					{
					case levelEditorManager.Tool.addTool:
						this._canUseGizmos = false;
						if (this.targetedGobj != null)
						{
							RaycastHit raycastHit13;
							Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit13, 500f, this.UIBlockMask);
							Vector3 point2 = raycastHit13.point;
							this.targetedGobj.transform.position = point2;
							if (levelEditorManager.canLook && Input.GetMouseButton(1))
							{
								GameObject gameObject9 = this.targetedGobj;
								this.targetedGobj = null;
								gameObject9.transform.parent = this.mapBase.transform;
								this.setLayerForObject(gameObject9.transform, 0);
								if (gameObject9.name.Contains("goal"))
								{
									TutorialHandler.Instance.SpecialEvents[18].Clicked();
								}
								this.addBlockToMap(gameObject9);
								this.onChangedTool((int)this._previousTool);
								levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step1;
							}
							if (Input.GetMouseButtonDown(0))
							{
								RaycastHit raycastHit14;
								Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit14, 500f, this.onlyUIMask);
								if (raycastHit14.collider)
								{
									if (this.targetedGobj != null)
									{
										UnityEngine.Object.Destroy(this.targetedGobj);
										this.targetedGobj = null;
									}
									this.onChangedTool((int)this._previousTool);
									levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step1;
									return;
								}
								if (raycastHit13.collider)
								{
									GameObject gameObject10 = this.targetedGobj;
									this.targetedGobj = null;
									gameObject10.transform.parent = this.mapBase.transform;
									this.setLayerForObject(gameObject10.transform, 0);
									if (gameObject10.name.Contains("goal"))
									{
										TutorialHandler.Instance.SpecialEvents[18].Clicked();
									}
									this.addBlockToMap(gameObject10);
									this.onChangedTool((int)this._previousTool);
									levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step1;
								}
							}
						}
						break;
					case levelEditorManager.Tool.moveTool:
						return;
					case levelEditorManager.Tool.rotationTool:
						return;
					}
					return;
				}
				if (levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step4)
				{
					this._canUseGizmos = false;
					levelEditorManager.Tool currentTool = this._currentTool;
					if (currentTool != levelEditorManager.Tool.addTool)
					{
						throw new Exception("Step 4, Wrong Tool!");
					}
					Debug.Log("Truck Brush!");
					Vector3 position2 = Vector3.zero;
					RaycastHit raycastHit15;
					Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit15, 500f, this.myMask);
					if (raycastHit15.collider)
					{
						position2 = ((!this._freeModeBool) ? new Vector3(raycastHit15.point.x, raycastHit15.point.y, raycastHit15.point.z) : Vector3.zero);
						this.targetedGobj.transform.position = position2;
					}
					if (Input.GetMouseButton(0))
					{
						position2 = Vector3.zero;
						Collider[] array2 = Physics.OverlapSphere(this.targetedGobj.transform.position, 5f);
						foreach (Collider collider in array2)
						{
							if (collider && collider.GetComponent<World>() == null && !collider.gameObject.Equals(this.targetedGobj))
							{
								Debug.Log("Hit: ", collider);
								if (collider.name.ToLower().Contains("truck"))
								{
									Transform transform2 = collider.transform;
									while (transform2.GetComponent<editor_first>() == null)
									{
										transform2 = transform2.transform.parent;
									}
									Debug.Log("Hit Truck!", transform2);
									ObjectParameterContainer[] array4 = DefaultParameters.getDefaultParameters(transform2.name.Split(new char[]
									{
										'('
									})[0]).ToContainerParameters();
									int postColor = this.getCurrentType(this.currentBrushColor) - 1;
									levelEditorManager.Block tileAt = this.currentMap.getTileAt(transform2);
									TruckBrushColorAdded truckBrushColorAdded = new TruckBrushColorAdded(tileAt, int.Parse(Array.Find<ObjectParameterContainer>(array4, (ObjectParameterContainer index) => index.getName() == "WaypointType").getValue()), postColor);
									truckBrushColorAdded.Execute();
									Array.Find<ObjectParameterContainer>(array4, (ObjectParameterContainer index) => index.getName() == "WaypointType").setValue(postColor.ToString());
									tileAt.setObjParams(array4, false, true);
								}
							}
						}
					}
					if (Input.GetKeyDown(KeyCode.Escape))
					{
						UnityEngine.Object.Destroy(this.targetedGobj);
						this.targetedGobj = null;
						levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step1;
						this.onChangedTool((int)this._previousTool);
					}
				}
				else if (levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step5)
				{
					this._canUseGizmos = false;
					levelEditorManager.Tool currentTool = this._currentTool;
					if (currentTool != levelEditorManager.Tool.addTool)
					{
						throw new Exception("Step 4, Wrong Tool!");
					}
					Debug.Log("RoadPoint Delete Brush!");
					Vector3 position3 = Vector3.zero;
					RaycastHit raycastHit16;
					Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit16, 500f, this.myMask);
					if (raycastHit16.collider)
					{
						position3 = ((!this._freeModeBool) ? new Vector3(raycastHit16.point.x, raycastHit16.point.y, raycastHit16.point.z) : Vector3.zero);
						this.targetedGobj.transform.position = position3;
					}
					if (Input.GetMouseButton(0))
					{
						position3 = Vector3.zero;
						Collider[] array5 = Physics.OverlapSphere(this.targetedGobj.transform.position, 5f);
						foreach (Collider collider2 in array5)
						{
							if (collider2 && collider2.GetComponent<World>() == null && !collider2.gameObject.Equals(this.targetedGobj))
							{
								Debug.Log("Hit: ", collider2);
								if (collider2.name.ToLower().Contains("roadpoint"))
								{
									this.removeBlockFromMap(collider2.gameObject);
								}
							}
						}
					}
					if (Input.GetKeyDown(KeyCode.Escape))
					{
						UnityEngine.Object.Destroy(this.targetedGobj);
						this.targetedGobj = null;
						levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step1;
						this.onChangedTool((int)this._previousTool);
					}
				}
			}
		}
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			this.PlaytestEscape();
		}
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x0005202C File Offset: 0x0005022C
	private void LateUpdate()
	{
		this.listenForGeneralHotKeyes();
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x00052034 File Offset: 0x00050234
	private void lockCursor(bool _value)
	{
		this.prevMousePosition = ((!_value) ? this.prevMousePosition : Input.mousePosition);
		if (!_value && Vector3.Distance(this.prevMousePosition, Input.mousePosition) > 50f)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.lockState = CursorLockMode.None;
		}
		levelEditorManager.canLook = _value;
		Cursor.visible = !levelEditorManager.canLook;
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0005209C File Offset: 0x0005029C
	private IEnumerator lockCursorIE(bool _value)
	{
		yield return new WaitForEndOfFrame();
		levelEditorManager.canLook = _value;
		Cursor.visible = !levelEditorManager.canLook;
		Cursor.lockState = ((!_value) ? CursorLockMode.None : CursorLockMode.Locked);
		yield break;
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x000520C0 File Offset: 0x000502C0
	private void paintTruck()
	{
		RaycastHit raycastHit;
		Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit);
		if (raycastHit.collider && this.currentMap.getTileAt(raycastHit.collider.transform) != null)
		{
			GameObject go = this.currentMap.getTileAt(raycastHit.collider.transform).go;
			if (this.currentMap.getTileAt(go.transform).type == levelEditorManager.blockType.btruckblock)
			{
				Debug.Log("Truck!");
				go.GetComponent<Renderer>().material.color = this.currentWaypointColor;
				this.currentMap.getTileAt(go.transform).waypointType = this.getCurrentType();
			}
		}
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x00052188 File Offset: 0x00050388
	private void tryDeleteSelection()
	{
		levelEditorManager.Block[] array = this.tryDelete(MonoSingletonBase<EditorObjectSelection>.Instance.SelectedGameObjects);
		foreach (levelEditorManager.Block block in array)
		{
			Debug.Log("Block Deleted: " + block.id);
		}
		ObjectDeletedAction objectDeletedAction = new ObjectDeletedAction(array);
		objectDeletedAction.Execute();
		GizmosTurnOffAction gizmosTurnOffAction = new GizmosTurnOffAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType);
		gizmosTurnOffAction.Execute();
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x000521FC File Offset: 0x000503FC
	private void groundObjectSelection()
	{
		ObjectSelectionSnapshot objectSelectionSnapshot = new ObjectSelectionSnapshot();
		objectSelectionSnapshot.TakeSnapshot();
		HashSet<GameObject> selectedGameObjects = MonoSingletonBase<EditorObjectSelection>.Instance.SelectedGameObjects;
		foreach (GameObject gameObject in selectedGameObjects)
		{
			RaycastHit raycastHit;
			Physics.Raycast((!gameObject.GetComponentInChildren<Foot>()) ? gameObject.transform.position : gameObject.GetComponentInChildren<Foot>().transform.position, Vector3.down, out raycastHit, 200f);
			if (raycastHit.collider)
			{
				gameObject.transform.position = ((!gameObject.GetComponentInChildren<Foot>()) ? raycastHit.point : (raycastHit.point - (gameObject.GetComponentInChildren<Foot>().transform.position - gameObject.transform.position)));
				this.updateBlockToMap(gameObject.transform);
			}
		}
		if (objectSelectionSnapshot != null)
		{
			ObjectSelectionSnapshot objectSelectionSnapshot2 = new ObjectSelectionSnapshot();
			objectSelectionSnapshot2.TakeSnapshot();
			PostObjectSelectionChangedAction postObjectSelectionChangedAction = new PostObjectSelectionChangedAction(objectSelectionSnapshot, objectSelectionSnapshot2);
			postObjectSelectionChangedAction.Execute();
		}
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x00052344 File Offset: 0x00050544
	private void tryDelete(GameObject _Object)
	{
		this.currentMaterial = null;
		GameObject gameObject = _Object;
		Debug.Log("Hit Object: " + gameObject.name, gameObject);
		if (this._currentTool == levelEditorManager.Tool.addTool)
		{
			if (this.targetedGobj != null)
			{
				UnityEngine.Object.Destroy(this.targetedGobj);
			}
			this.targetedGobj = null;
			this.onChangedTool((int)this._previousTool);
			return;
		}
		while (gameObject.GetComponent<editor_first>() == null)
		{
			gameObject = gameObject.transform.parent.gameObject;
		}
		Debug.Log(gameObject.name);
		Debug.Log("Hit," + gameObject.transform.name);
		for (int i = this.currentMap.tiles.Count - 1; i >= 0; i--)
		{
			if (this.currentMap.tiles[i].go == gameObject)
			{
				Debug.Log("Object Found in map!", gameObject);
				if (this.currentMap.tiles[i].type == levelEditorManager.blockType.broadpointblock)
				{
					int wayPointIndex = this.currentMap.tiles[i].wayPointIndex;
					Debug.Log("RealIndex: " + wayPointIndex);
					for (int j = this.currentMap.tiles.Count - 1; j >= 0; j--)
					{
						if (this.currentMap.tiles[j].type == levelEditorManager.blockType.broadpointblock && this.currentMap.tiles[j].wayPointIndex == wayPointIndex)
						{
							this.removeBlockFromMap(this.currentMap.tiles[j].go);
						}
					}
					UnityEngine.Object.Destroy(this.mapBase.transform.FindChild("Pathrenderer_" + wayPointIndex).gameObject);
					break;
				}
				if (this.currentMap.tiles[i].type == levelEditorManager.blockType.btruckblock)
				{
				}
				if (levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step1)
				{
					this.removeBlockFromMap(gameObject);
					return;
				}
			}
		}
		UnityEngine.Object.Destroy(gameObject);
		this.targetedGobj = null;
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x00052570 File Offset: 0x00050770
	private levelEditorManager.Block[] tryDelete(HashSet<GameObject> _Objects)
	{
		List<levelEditorManager.Block> list = new List<levelEditorManager.Block>();
		this.currentMaterial = null;
		if (this._currentTool == levelEditorManager.Tool.addTool)
		{
			if (this.targetedGobj != null)
			{
				UnityEngine.Object.Destroy(this.targetedGobj);
			}
			this.targetedGobj = null;
			this.onChangedTool((int)this._previousTool);
			return null;
		}
		foreach (GameObject gameObject in _Objects)
		{
			GameObject gameObject2 = gameObject;
			while (gameObject2.GetComponent<editor_first>() == null)
			{
				gameObject2 = gameObject2.transform.parent.gameObject;
			}
			Debug.Log(gameObject2.name);
			Debug.Log("Hit," + gameObject2.transform.name);
			for (int i = this.currentMap.tiles.Count - 1; i >= 0; i--)
			{
				if (this.currentMap.tiles[i].go == gameObject2)
				{
					Debug.Log("Object Found in map!", gameObject2);
					if (this.currentMap.tiles[i].type == levelEditorManager.blockType.broadpointblock)
					{
						int wayPointIndex = this.currentMap.tiles[i].wayPointIndex;
						Debug.Log("RealIndex: " + wayPointIndex);
						for (int j = this.currentMap.tiles.Count - 1; j > 0; j--)
						{
							if (this.currentMap.tiles[j].type == levelEditorManager.blockType.broadpointblock && this.currentMap.tiles[j].wayPointIndex == wayPointIndex)
							{
								this.removeBlockFromMap(this.currentMap.tiles[j].go);
							}
						}
						break;
					}
					if (this.currentMap.tiles[i].type == levelEditorManager.blockType.btruckblock)
					{
					}
					if (levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step1)
					{
						list.Add(this.currentMap.tiles[i]);
						this.removeBlockFromMap(gameObject2);
					}
				}
			}
			UnityEngine.Object.Destroy(gameObject2);
			this.targetedGobj = null;
		}
		return list.ToArray();
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x000527F0 File Offset: 0x000509F0
	private int getCurrentType()
	{
		int num = -1;
		for (int i = 0; i < this.colors.Length; i++)
		{
			if (this.currentWaypointColor == this.colors[i])
			{
				num = i + 1;
			}
		}
		if (num < 0)
		{
			throw new Exception("Wrong Type!");
		}
		return num;
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x00052850 File Offset: 0x00050A50
	private int getCurrentType(Color _colCheck)
	{
		int num = -1;
		for (int i = 0; i < this.colors.Length; i++)
		{
			if (_colCheck == this.colors[i])
			{
				num = i + 1;
			}
		}
		if (num < 0)
		{
			throw new Exception("Wrong Type!");
		}
		return num;
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x000528AC File Offset: 0x00050AAC
	public bool hasGoalOrPlayerBeenAsigned()
	{
		bool flag = false;
		bool flag2 = false;
		foreach (levelEditorManager.Block block in this.currentMap.tiles)
		{
			if (block.type == levelEditorManager.blockType.bplayerspawn)
			{
				flag = true;
			}
			if (block.type == levelEditorManager.blockType.bgoal)
			{
				flag2 = true;
			}
		}
		return flag && flag2;
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x00052940 File Offset: 0x00050B40
	private IEnumerator waitForScreenShot(string _mapName)
	{
		mainCanvas.Instance().disableCanvas();
		levelEditorManager.canLook = true;
		this._pointerMarker.SetActive(false);
		if (!Directory.Exists(levelEditorManager.DefaultFilepath + "/previews/" + _mapName))
		{
			Directory.CreateDirectory(levelEditorManager.DefaultFilepath + "/previews/" + _mapName);
		}
		bool pictureTaken = false;
		while (!pictureTaken)
		{
			Debug.Log("Waiting for picture");
			this.displayManager.DisplayMessage("Press P To take a screenshot!");
			if (Input.GetKeyDown(KeyCode.P))
			{
				this.displayManager.DisplayMessage(string.Empty);
				Application.CaptureScreenshot(levelEditorManager.DefaultFilepath + "/previews/" + _mapName + "/1.jpg");
				yield return new WaitForSeconds(0.5f);
				this.displayManager.DisplayMessage("Picture Taken!");
				pictureTaken = true;
			}
			yield return null;
		}
		if (!pictureTaken)
		{
			this.displayManager.DisplayMessage("Error With Screenshot");
		}
		mainCanvas.Instance().enableCanvas();
		levelEditorManager.canLook = false;
		this._pointerMarker.SetActive(true);
		yield break;
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0005296C File Offset: 0x00050B6C
	public IEnumerator Save(bool overwrite)
	{
		levelEditorManager.SAVING = true;
		this.updateMapForGizmos();
		Debug.Log("Saving: " + levelEditorManager.SAVING);
		if (!this.hasGoalOrPlayerBeenAsigned())
		{
			this.displayManager.DisplayMessage("No Player OR Goal Assigned!");
			yield break;
		}
		this.modalPanel.ClosePanel();
		bool steamUpdating = false;
		string mapName = this.saveInputField.text;
		Debug.Log("LastSaved: " + this.lastLoadedOrSavedMap + "Save Text: " + mapName);
		string path = levelEditorManager.CurrentFilePath;
		if (!steamUpdating)
		{
			if (string.IsNullOrEmpty(mapName) || mapName.Trim().Length == 0)
			{
				Debug.Log("EmptyString: Making one for you!");
				Debug.Log("NEW MAPNAME! " + mapName);
				path = path + "/" + mapName + levelEditorManager.fileEnding;
				Debug.Log(path);
				if (File.Exists(path))
				{
					Debug.LogError("File Already Exists!");
				}
			}
			path = levelEditorManager.CurrentFilePath;
			Debug.Log("PATH: " + path);
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
				Debug.Log("Dir Created! at: " + path);
			}
		}
		if (this.currentMap.getSettings().Length > 0 && int.Parse(this.currentMap.getSettings()[3]) < this.SkyBoxes.Length)
		{
			this.currentMap.setCustomSkyBox(null);
		}
		path = path + "\\" + mapName + levelEditorManager.fileEnding;
		Debug.Log(path);
		if (!overwrite && File.Exists(path))
		{
			Debug.LogError("File Already Exists!");
			this.modalPanel.ClosePanel();
			this.modalPanel.Choice("Do you want to overwrite existing file?", new UnityAction(this.OverwriteYesFunction), new UnityAction(this.OverwriteNoFunction), "Yes", "No");
			yield break;
		}
		JsonSerializerSettings JSONsettings = new JsonSerializerSettings();
		JSONsettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
		string serialized = JsonConvert.SerializeObject(this.currentMap, Formatting.Indented, JSONsettings);
		yield return base.StartCoroutine(this.waitForScreenShot(mapName));
		Debug.Log("PICTURE TAKEN");
		File.WriteAllText(path, serialized);
		this.lastLoadedOrSavedMap = mapName;
		this.displayManager.DisplayMessage("SAVE SUCESSFUL!");
		this._saveMenuOpen = false;
		this.currentMapFile = new FileInfo(path);
		this.hasChangedAnything = false;
		levelEditorManager.SAVING = false;
		Debug.Log("Saving: " + levelEditorManager.SAVING);
		yield break;
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x00052998 File Offset: 0x00050B98
	public IEnumerator Save(string _mapName)
	{
		if (!this.hasGoalOrPlayerBeenAsigned())
		{
			this.displayManager.DisplayMessage("No Player OR Goal Assigned!");
			yield break;
		}
		this.modalPanel.ClosePanel();
		Debug.Log("LastSaved: " + this.lastLoadedOrSavedMap + "Save Text: " + _mapName);
		string path = levelEditorManager.CurrentFilePath;
		if (this.currentMap.getSettings().Length > 0 && int.Parse(this.currentMap.getSettings()[3]) < this.SkyBoxes.Length)
		{
			this.currentMap.setCustomSkyBox(null);
		}
		JsonSerializerSettings settings = new JsonSerializerSettings();
		settings.TypeNameHandling = TypeNameHandling.Objects;
		string serialized = JsonConvert.SerializeObject(this.currentMap, Formatting.Indented, settings);
		path = path + "/" + _mapName + levelEditorManager.fileEnding;
		Debug.Log(path);
		File.WriteAllText(path, serialized);
		this.lastLoadedOrSavedMap = _mapName;
		this.currentMapFile = new FileInfo(path);
		this.displayManager.DisplayMessage("SAVE SUCESSFUL!");
		Debug.Log("Saving: " + levelEditorManager.SAVING);
		yield break;
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x000529C4 File Offset: 0x00050BC4
	public void quickSave()
	{
		if (string.IsNullOrEmpty(this.lastLoadedOrSavedMap))
		{
			this.SAVE_YNC();
			return;
		}
		base.StartCoroutine(this.Save(this.lastLoadedOrSavedMap));
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x000529FC File Offset: 0x00050BFC
	public void load(FileInfo fileName)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (this.lastLoadedOrSavedMap != string.Empty)
		{
			string text = this.lastLoadedOrSavedMap;
		}
		else
		{
			this.mapname = fileName.Name.Split(new char[]
			{
				'.'
			})[0];
		}
		string text2 = File.ReadAllText(fileName.FullName);
		if (text2 == string.Empty)
		{
			this.displayManager.DisplayMessage("Loading Error, missing or corrupt file!");
			return;
		}
		levelEditorManager.Map mapToclear = this.currentMap.Clone();
		JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
		jsonSerializerSettings.TypeNameHandling = TypeNameHandling.Objects;
		jsonSerializerSettings.Converters = new JsonConverter[]
		{
			new ParameterJSONConverter()
		};
		levelEditorManager.Map map = new levelEditorManager.Map();
		try
		{
			map = JsonConvert.DeserializeObject<levelEditorManager.Map>(text2, jsonSerializerSettings);
		}
		catch (Exception ex)
		{
			this.displayManager.DisplayMessage("Error Loading Map, Corrupt File " + ex.ToString());
			return;
		}
		this.currentMap = map;
		this.updateCurrentBlockCount();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = -1;
		try
		{
			foreach (levelEditorManager.Block block in this.currentMap.tiles)
			{
				int num6 = -1;
				string text3 = string.Empty;
				bool flag = false;
				switch (block.type)
				{
				case levelEditorManager.blockType.btruckblock:
					text3 = "Vehicles/";
					break;
				case levelEditorManager.blockType.bdestructibleblock:
					text3 = "Destructible/";
					flag = true;
					break;
				case levelEditorManager.blockType.broadsblock:
					text3 = "Roads/";
					flag = true;
					break;
				case levelEditorManager.blockType.btrapsblock:
					text3 = "Traps/";
					break;
				case levelEditorManager.blockType.broadpointblock:
					text3 = string.Empty;
					num6 = block.wayPointIndex;
					if (block.wayPointIndex > num5)
					{
						num5 = block.wayPointIndex;
					}
					break;
				case levelEditorManager.blockType.bgoal:
					break;
				case levelEditorManager.blockType.bplayerspawn:
					break;
				case levelEditorManager.blockType.bpropsblock:
					text3 = "Props/";
					break;
				case levelEditorManager.blockType.bshapesblock:
					text3 = "Shapes/";
					break;
				case levelEditorManager.blockType.beventsblock:
					text3 = "Events/";
					break;
				default:
					throw new Exception("Invalid Blocktype!" + block.type);
				}
				Vector3 vector = new Vector3(block.x, block.y, block.z);
				Vector3 localScale = new Vector3(block.scalex, block.scaley, block.scalez);
				Vector3 vector2 = new Vector3(block.rotx, block.roty, block.rotz);
				Debug.Log(string.Concat(new object[]
				{
					"Spawning: mapEditor/",
					text3,
					block.id,
					" AT: ",
					vector
				}));
				string text4 = block.id;
				if (block.id.Split(new char[]
				{
					'_'
				}).Length > 1)
				{
					Debug.Log("Has Custom Name! " + block.id.Split(new char[]
					{
						'_'
					})[1]);
					text4 = block.id.Split(new char[]
					{
						'_'
					})[0];
				}
				else
				{
					Debug.Log("Has NO CustomName! " + block.id.Split(new char[]
					{
						'_'
					})[0]);
				}
				if (block.id.Split(new char[]
				{
					'-'
				}).Length > 1)
				{
					text4 = block.id.Split(new char[]
					{
						'-'
					})[1];
					block.id = text4;
				}
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("mapEditor/" + text3 + text4));
				gameObject.AddComponent<BlockInfo>().Category = text3;
				if (gameObject.GetComponentInChildren<useThisObject>())
				{
					float y = (vector - gameObject.GetComponentInChildren<useThisObject>().transform.localPosition).y;
					gameObject.transform.position = vector - gameObject.GetComponentInChildren<useThisObject>().transform.localPosition;
					gameObject.transform.rotation = Quaternion.Euler(vector2 - gameObject.GetComponentInChildren<useThisObject>().transform.localRotation.eulerAngles);
				}
				else
				{
					gameObject.transform.position = vector;
					gameObject.transform.rotation = Quaternion.Euler(vector2);
				}
				if (block.objsParams != null)
				{
					gameObject.SendMessage("getInfo", block.objsParams, SendMessageOptions.DontRequireReceiver);
				}
				if (flag)
				{
				}
				if (gameObject.GetComponent<editor_first>() == null)
				{
					gameObject.AddComponent<editor_first>();
				}
				this.setLayerForObject(gameObject.transform, 0);
				GameObject gameObject2 = (!(gameObject.GetComponentInChildren<useThisObject>() == null)) ? gameObject.GetComponentInChildren<useThisObject>().gameObject : gameObject;
				gameObject2.transform.localScale = localScale;
				if (block.type == levelEditorManager.blockType.bplayerspawn)
				{
					this.playerSpawn = gameObject;
				}
				if (block.type == levelEditorManager.blockType.bgoal)
				{
					this.goal = gameObject;
				}
				block.go = gameObject;
				if (block.type == levelEditorManager.blockType.broadpointblock)
				{
					gameObject.GetComponent<wayPointType>().setType(block.waypointType, false);
					gameObject.layer = 9;
				}
				if (num6 >= 0)
				{
					string name = "Pathrenderer_" + num6;
					if (this.mapBase.transform.FindChild(name) == null)
					{
						GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.pathRendererHolderGameObject);
						gameObject3.name = name;
						gameObject3.transform.SetParent(this.mapBase.transform);
						block.go.transform.SetParent(gameObject3.transform);
					}
					else
					{
						block.go.transform.SetParent(this.mapBase.transform.FindChild(name));
					}
				}
				else
				{
					gameObject.transform.SetParent(this.mapBase.transform);
				}
				this.setLayerForObject(gameObject.transform, 0);
			}
		}
		catch (Exception ex2)
		{
			Debug.Log("Cannot laod map: " + ex2.Message);
			this.displayManager.DisplayMessage("Cannot laod map: " + ex2.Message);
			this.currentMap = mapToclear;
			return;
		}
		Debug.Log("sucessfully Loaded map: " + fileName.Name);
		MonoSingletonBase<EditorUndoRedoSystem>.Instance.ClearActions();
		this.clear(mapToclear);
		this.currentMap.Init();
		Debug.Log("Number of linerenderes: " + num5);
		Debug.Log(string.Concat(new object[]
		{
			"I: ",
			num,
			" II: ",
			num2,
			" III: ",
			num3,
			" IV: ",
			num4
		}));
		Debug.Log("Loading Time: " + (Time.realtimeSinceStartup - realtimeSinceStartup));
		this.hasChangedAnything = false;
		this.lastLoadedOrSavedMap = this.mapname;
		this.loadPopUp.SetActive(false);
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x000531A0 File Offset: 0x000513A0
	private void revertObject(GameObject _Obj)
	{
		foreach (levelEditorManager.Block block in this.currentMap.tiles)
		{
			if (block.go == _Obj)
			{
				GameObject gameObject = (!(_Obj.GetComponentInChildren<useThisObject>() == null)) ? _Obj.GetComponentInChildren<useThisObject>().gameObject : null;
				Vector3 b = (!(gameObject == null)) ? gameObject.transform.localPosition : Vector3.zero;
				Vector3 b2 = (!(gameObject == null)) ? gameObject.transform.localRotation.eulerAngles : Vector3.zero;
				_Obj.transform.position = new Vector3(block.x, block.y, block.z) - b;
				_Obj.transform.rotation = Quaternion.Euler(new Vector3(block.rotx, block.roty, block.rotz) - b2);
				if (this.currentMaterial != null && this.currentMaterial.Length > 0 && gameObject.GetComponentInChildren<Renderer>() != null)
				{
					Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].material = this.currentMaterial[i];
					}
					this.currentMaterial = null;
				}
				return;
			}
		}
		UnityEngine.Object.Destroy(_Obj);
		this.targetedGobj = null;
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x00053354 File Offset: 0x00051554
	private void updateBlockToMap(Transform _new)
	{
		Transform transform = (!(_new.GetComponentInChildren<useThisObject>() == null)) ? _new.GetComponentInChildren<useThisObject>().transform : _new;
		Debug.Log("updateBlockMap: " + transform, transform);
		_new.name = _new.name.Split(new char[]
		{
			'('
		})[0];
		levelEditorManager.Block block = null;
		foreach (levelEditorManager.Block block2 in this.currentMap.tiles)
		{
			if (block2.go.Equals(_new.gameObject))
			{
				block = block2;
			}
		}
		if (block != null)
		{
			block.x = transform.position.x.strip(1);
			block.y = transform.position.y.strip(1);
			block.z = transform.position.z.strip(1);
			block.scalex = transform.lossyScale.x.strip(1);
			block.scaley = transform.lossyScale.y.strip(1);
			block.scalez = transform.lossyScale.z.strip(1);
			block.rotx = transform.rotation.eulerAngles.x.strip(1);
			block.roty = transform.rotation.eulerAngles.y.strip(1);
			block.rotz = transform.rotation.eulerAngles.z.strip(1);
			block.go = _new.gameObject;
			if (this.currentMaterial != null && this.currentMaterial.Length > 0 && transform.GetComponentInChildren<Renderer>() != null)
			{
				Renderer[] componentsInChildren = transform.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].material = this.currentMaterial[i];
				}
				this.currentMaterial = null;
			}
			this.hasChangedAnything = true;
			return;
		}
		if (!_new.parent.Equals(this.mapBase.transform))
		{
			this.updateBlockToMap(_new.parent);
			return;
		}
		this.addBlockToMap(_new.gameObject);
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x000535E8 File Offset: 0x000517E8
	private void updateBlockToMap(levelEditorManager.Block _old, Transform _new)
	{
		Transform transform = (!(_new.GetComponentInChildren<useThisObject>() == null)) ? _new.GetComponentInChildren<useThisObject>().transform : _new;
		_old.x = transform.position.x;
		_old.y = transform.position.y;
		_old.z = transform.position.z;
		_old.scalex = transform.lossyScale.x;
		_old.scaley = transform.lossyScale.y;
		_old.scalez = transform.lossyScale.z;
		_old.rotx = transform.rotation.eulerAngles.x;
		_old.roty = transform.rotation.eulerAngles.y;
		_old.rotz = transform.rotation.eulerAngles.z;
		_old.go = _new.gameObject;
		if (this.currentMaterial != null && this.currentMaterial.Length > 0 && transform.GetComponentInChildren<Renderer>() != null)
		{
			Debug.Log("Assigning Material to: " + _old.go.name, _old.go);
			Renderer[] componentsInChildren = transform.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].material = this.currentMaterial[i];
			}
			this.currentMaterial = null;
		}
		this.hasChangedAnything = true;
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x00053780 File Offset: 0x00051980
	public levelEditorManager.Block addBlockToMap(GameObject block)
	{
		if ((float)levelEditorManager.CurrentObjectCount >= this.OBJECT_MAX_COUNT)
		{
			DisplayManager.Instance().DisplayMessage("Too Many Objects In Scene!");
			if (this.targetedGobj != null)
			{
				UnityEngine.Object.Destroy(this.targetedGobj);
			}
			this.targetedGobj = null;
			return null;
		}
		GameObject gameObject = (!(block.GetComponentInChildren<useThisObject>() == null)) ? block.GetComponentInChildren<useThisObject>().gameObject : block;
		levelEditorManager.Block block2 = new levelEditorManager.Block();
		block2.x = gameObject.transform.position.x.strip(1);
		block2.y = gameObject.transform.position.y.strip(1);
		block2.z = gameObject.transform.position.z.strip(1);
		block2.scalex = gameObject.transform.lossyScale.x.strip(1);
		block2.scaley = gameObject.transform.lossyScale.y.strip(1);
		block2.scalez = gameObject.transform.lossyScale.z.strip(1);
		block2.rotx = gameObject.transform.rotation.eulerAngles.x.strip(1);
		block2.roty = gameObject.transform.rotation.eulerAngles.y.strip(1);
		block2.rotz = gameObject.transform.rotation.eulerAngles.z.strip(1);
		block2.id = block.name.Split(new char[]
		{
			'('
		})[0];
		if (!block.GetComponent<editor_first>())
		{
			block.AddComponent<editor_first>();
		}
		if (this.currentMaterial != null && this.currentMaterial.Length > 0 && gameObject.GetComponentsInChildren<Renderer>() != null)
		{
			Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].material = this.currentMaterial[i];
			}
			this.currentMaterial = null;
		}
		block2.go = block;
		if (block2.go.transform.parent == null)
		{
			block2.go.transform.SetParent(this.mapBase.transform);
		}
		levelEditorManager.blockType blockType;
		if (block.name.ToLower().Contains("playerspawn"))
		{
			Debug.Log("PlayerSpawn!");
			blockType = levelEditorManager.blockType.bplayerspawn;
		}
		else if (block.name.ToLower().Contains("goal"))
		{
			Debug.Log("Goal!");
			blockType = levelEditorManager.blockType.bgoal;
			this.setLayerForObject(block.transform, 17);
		}
		else if (block.name.ToLower().Contains("truck"))
		{
			blockType = levelEditorManager.blockType.btruckblock;
		}
		else
		{
			try
			{
				blockType = (levelEditorManager.blockType)((int)Enum.Parse(typeof(levelEditorManager.blockType), "b" + block2.go.GetComponent<BlockInfo>().Category.ToLower() + "block"));
			}
			catch
			{
				blockType = (levelEditorManager.blockType)((int)Enum.Parse(typeof(levelEditorManager.blockType), "b" + block2.go.GetComponent<BlockInfo>().Category.ToLower().Split(new char[]
				{
					'/'
				})[0] + "block"));
			}
		}
		block2.type = blockType;
		if (!block.name.ToLower().Contains("player") && !block.name.ToLower().Contains("goal") && blockType != levelEditorManager.blockType.broadpointblock && (!block.GetComponent<ModifierRecieverBase>() || !block.GetComponent<Modifier>()))
		{
			this.displayManager.DisplayMessage(block.name + " Has no modifier Reciver or Modifier");
		}
		if (DefaultParameters.getDefaultParameters(block.name.Split(new char[]
		{
			'('
		})[0]) != null)
		{
			block2.objsParams = DefaultParameters.getDefaultParameters(block.name.Split(new char[]
			{
				'('
			})[0]).ToContainerParameters();
		}
		else
		{
			block2.objsParams = null;
		}
		if (this.currentDelayParameter != null)
		{
			Debug.Log("currentDelayParameter.getObj()", this.currentDelayParameter.getObj());
			Debug.Log("block.transform", block.transform);
			if (this.currentDelayParameter.getObj().Equals(block.transform))
			{
				block2.objsParams = this.currentDelayParameter.getParameters();
				Debug.Log("COPYING PARAMETERS!");
			}
			else
			{
				this.currentDelayParameter = null;
			}
		}
		if (block2.objsParams != null)
		{
		}
		ObjectAddedAction objectAddedAction = new ObjectAddedAction(new levelEditorManager.Block[]
		{
			block2
		});
		objectAddedAction.Execute();
		this.currentMap.setTile(block2);
		return block2;
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x00053CAC File Offset: 0x00051EAC
	public void addBlockToMap(GameObject block, levelEditorManager.blockType type)
	{
		GameObject gameObject = (!(block.GetComponentInChildren<useThisObject>() == null)) ? block.GetComponentInChildren<useThisObject>().gameObject : block;
		levelEditorManager.Block block2 = new levelEditorManager.Block();
		block2.x = gameObject.transform.position.x.strip(1);
		block2.y = gameObject.transform.position.y.strip(1);
		block2.z = gameObject.transform.position.z.strip(1);
		block2.scalex = gameObject.transform.lossyScale.x.strip(1);
		block2.scaley = gameObject.transform.lossyScale.y.strip(1);
		block2.scalez = gameObject.transform.lossyScale.z.strip(1);
		block2.rotx = gameObject.transform.rotation.eulerAngles.x.strip(1);
		block2.roty = gameObject.transform.rotation.eulerAngles.y.strip(1);
		block2.rotz = gameObject.transform.rotation.eulerAngles.z.strip(1);
		block2.id = block.name.Split(new char[]
		{
			'('
		})[0];
		Debug.Log(block2.id, gameObject);
		if (!block.GetComponent<editor_first>())
		{
			block.AddComponent<editor_first>();
		}
		if (this.currentMaterial != null && this.currentMaterial.Length > 0 && gameObject.GetComponentsInChildren<Renderer>() != null)
		{
			Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].material = this.currentMaterial[i];
			}
			this.currentMaterial = null;
		}
		block2.go = block;
		block2.go.transform.SetParent(this.mapBase.transform);
		block2.type = type;
		Debug.Log("Block set: " + block.name);
		this.currentMap.setTile(block2);
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x00053F00 File Offset: 0x00052100
	public void addBlockToMap(GameObject block, levelEditorManager.blockType type, bool Override)
	{
		GameObject gameObject = (!(block.GetComponentInChildren<useThisObject>() == null)) ? block.GetComponentInChildren<useThisObject>().gameObject : block;
		levelEditorManager.Block block2 = new levelEditorManager.Block();
		block2.x = gameObject.transform.position.x.strip(1);
		block2.y = gameObject.transform.position.y.strip(1);
		block2.z = gameObject.transform.position.z.strip(1);
		block2.scalex = gameObject.transform.lossyScale.x.strip(1);
		block2.scaley = gameObject.transform.lossyScale.y.strip(1);
		block2.scalez = gameObject.transform.lossyScale.z.strip(1);
		block2.rotx = gameObject.transform.rotation.eulerAngles.x.strip(1);
		block2.roty = gameObject.transform.rotation.eulerAngles.y.strip(1);
		block2.rotz = gameObject.transform.rotation.eulerAngles.z.strip(1);
		block2.id = block.name.Split(new char[]
		{
			'('
		})[0];
		Debug.Log(block2.id, gameObject);
		if (!block.GetComponent<editor_first>())
		{
			block.AddComponent<editor_first>();
		}
		if (this.currentMaterial != null && this.currentMaterial.Length > 0 && gameObject.GetComponentsInChildren<Renderer>() != null)
		{
			Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].material = this.currentMaterial[i];
			}
			this.currentMaterial = null;
		}
		block2.go = block;
		block2.go.transform.SetParent(this.mapBase.transform);
		block2.type = type;
		Debug.Log("Block set: " + block.name);
		ObjectAddedAction objectAddedAction = new ObjectAddedAction(new levelEditorManager.Block[]
		{
			block2
		});
		objectAddedAction.Execute();
		this.currentMap.setTile(block2);
		this.updateCurrentBlockCount();
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x00054170 File Offset: 0x00052370
	private void removeBlockFromMap(GameObject block)
	{
		foreach (levelEditorManager.Block block2 in this.currentMap.tiles)
		{
			if (block2.go.Equals(block))
			{
				UnityEngine.Object.Destroy(this.currentMap.removeTile(block).go);
				this.updateCurrentBlockCount();
				return;
			}
		}
		UnityEngine.Object.Destroy(block);
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x0005420C File Offset: 0x0005240C
	private void initializeBlockMenu(string category)
	{
		for (int i = 0; i < this.grid.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.grid.transform.GetChild(i).gameObject);
			Debug.Log("!!!!");
		}
		this.currentCategory = category + "/";
		Debug.Log("Category: " + category);
		string path = "mapEditor/" + category;
		this.blockMenu.SetActive(true);
		if (category.ToLower() == "vehicles")
		{
			TutorialHandler.Instance.SpecialEvents[0].Clicked();
		}
		foreach (UnityEngine.Object @object in Resources.LoadAll(path))
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this.cellPrefab, Vector3.zero, Quaternion.identity);
			gameObject.SetActive(true);
			gameObject.GetComponentInChildren<Text>().text = @object.name;
			gameObject.transform.SetParent(this.grid.transform, false);
		}
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x00054330 File Offset: 0x00052530
	public void InitializeDownloadGrid()
	{
		if (this.downloadPopUp.activeSelf)
		{
			this.downloadPopUp.SetActive(false);
		}
		else
		{
			this.downloadPopUp.SetActive(true);
			this.workshop_handler.getworkshopMaps();
		}
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x00054378 File Offset: 0x00052578
	public void InitializeLoadGrid()
	{
		if (this.loadPopUp.activeSelf)
		{
			this.loadPopUp.SetActive(false);
		}
		else
		{
			this.openPopUpMenu(this.loadPopUp);
		}
		this._loadMenuOpen = this.loadPopUp.activeSelf;
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x000543C4 File Offset: 0x000525C4
	public void OnSubmit(BaseEventData p)
	{
		PointerEventData pointerEventData = p as PointerEventData;
		if (pointerEventData.pointerId != -1)
		{
			if (pointerEventData.pointerPress.tag == "waypointColor")
			{
				if (p.selectedObject.name.ToLower() == "red")
				{
					TutorialHandler.Instance.SpecialEvents[8].Clicked();
				}
				if (p.selectedObject.name.ToLower() == "yellow")
				{
					TutorialHandler.Instance.SpecialEvents[10].Clicked();
				}
				this.currentWaypointColor = pointerEventData.pointerPress.GetComponent<Button>().colors.normalColor;
				int currentType = this.getCurrentType();
				Debug.Log(currentType);
				for (int i = this.currentMap.tiles.Count - 1; i > 0; i--)
				{
					if (this.currentMap.tiles[i].type == levelEditorManager.blockType.broadpointblock && this.currentMap.tiles[i].waypointType == currentType)
					{
						this.removeBlockFromMap(this.currentMap.tiles[i].go);
					}
				}
			}
			return;
		}
		if (p.selectedObject.tag == "editor")
		{
			SceneManager.LoadScene("game");
		}
		if (p.selectedObject.tag == "mapSelect")
		{
		}
		if (p.selectedObject.tag == "loadMap")
		{
			this.currentMapFile = p.selectedObject.GetComponent<getFile>().getFileInfo();
			this.saveInputField.text = this.currentMapFile.Name.Split(new char[]
			{
				'.'
			})[0];
			this.load(this.currentMapFile);
		}
		if (p.selectedObject.tag == "upload")
		{
		}
		if (p.selectedObject.tag == "Delete")
		{
		}
		if (p.selectedObject.tag == "blockMenu")
		{
			this.initializeBlockMenu(p.selectedObject.name);
		}
		if (p.selectedObject.tag == "brushColor")
		{
			if (this.IsBusy)
			{
				return;
			}
			if (this._currentTool != levelEditorManager.Tool.addTool)
			{
				this._previousTool = this._currentTool;
			}
			this.onChangedTool(0);
			this.currentBlockString = "roadpoint";
			this.justChangedState = true;
			this.currentBrushColor = p.selectedObject.GetComponent<Button>().colors.normalColor;
			this.showBrushColors();
			this.checkButtons();
			Debug.Log("BrushColor Choosen: " + this.currentBrushColor);
		}
		if (p.selectedObject.tag == "roadpointDelete")
		{
			if (this.IsBusy)
			{
				return;
			}
			if (this._currentTool != levelEditorManager.Tool.addTool)
			{
				this._previousTool = this._currentTool;
			}
			this.onChangedTool(0);
			this.currentBlockString = "roadpoint";
			this.justChangedState = true;
			this.ActivateRoadPointDeleteSphere();
			this.checkButtons();
			Debug.Log("ActivateRoadPointDeleteSphere Choosen!");
		}
		if (p.selectedObject.tag == "waypointColor")
		{
			if (this.IsBusy)
			{
				return;
			}
			if (this._currentTool != levelEditorManager.Tool.addTool)
			{
				this._previousTool = this._currentTool;
			}
			this.onChangedTool(0);
			if (p.selectedObject.name.ToLower() == "red")
			{
				TutorialHandler.Instance.SpecialEvents[8].Clicked();
			}
			if (p.selectedObject.name.ToLower() == "yellow")
			{
				TutorialHandler.Instance.SpecialEvents[10].Clicked();
			}
			this.currentBlockString = "roadpoint";
			this.justChangedState = true;
			this.currentWaypointColor = p.selectedObject.GetComponent<Button>().colors.normalColor;
			this.showWaypointColors();
			this.checkButtons();
			Debug.Log("Color Choosen: " + this.currentWaypointColor);
		}
		if (p.selectedObject.tag == "buildBlock")
		{
			if (this.IsBusy)
			{
				return;
			}
			if (this._currentTool != levelEditorManager.Tool.addTool)
			{
				this._previousTool = this._currentTool;
			}
			this.onChangedTool(0);
			if (this.targetedGobj != null)
			{
				UnityEngine.Object.Destroy(this.targetedGobj);
			}
			levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step1;
			string text = p.selectedObject.GetComponentInChildren<Text>().text.ToLower();
			Debug.Log("Spawn: " + text);
			this.currentBlockString = text;
			if (text == "goal" || text.Contains("player"))
			{
				levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step3;
				this.currentCategory = string.Empty;
				if (text.Contains("player"))
				{
					text = "playerSpawn";
				}
			}
			else if (text.ToLower().Contains("truck"))
			{
				this.currentCategory = "Vehicles/";
				text = "Truck";
			}
			else
			{
				this.currentCategory = this._blockcategoriesArray[this._blockType_Dropdown.value] + "/";
			}
			Debug.Log("SPAWNING:  mapEditor/" + this.currentCategory + text);
			if (text.ToLower().Contains("truck"))
			{
				TutorialHandler.Instance.SpecialEvents[1].Clicked();
				TutorialHandler.Instance.SpecialEvents[6].Clicked();
			}
			this.scrollDelta = 0f;
			this.targetedGobj = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("mapEditor/" + this.currentCategory + text));
			this.targetedGobj.AddComponent<BlockInfo>().Category = this.currentCategory;
			this.setLayerForObject(this.targetedGobj.transform, 2);
			this.targetedGobj.transform.position = this.mousePosition;
			this.currentMaterial = null;
			if (this.targetedGobj.GetComponentInChildren<Renderer>() != null)
			{
				this._assignMaterials(this.targetedGobj);
			}
			Debug.Log(this.targetedGobj.transform.rotation.eulerAngles);
			if (text.Contains("player"))
			{
				if (this.playerSpawn != null)
				{
					UnityEngine.Object.Destroy(this.playerSpawn);
					for (int j = this.currentMap.tiles.Count - 1; j >= 0; j--)
					{
						if (this.currentMap.tiles[j].type == levelEditorManager.blockType.bplayerspawn)
						{
							this.currentMap.removeTile(this.currentMap.tiles[j].go);
						}
					}
				}
				this.playerSpawn = this.targetedGobj;
			}
			else if (text == "goal")
			{
				if (this.goal != null)
				{
					UnityEngine.Object.Destroy(this.goal);
					for (int k = this.currentMap.tiles.Count - 1; k >= 0; k--)
					{
						if (this.currentMap.tiles[k].type == levelEditorManager.blockType.bgoal)
						{
							this.currentMap.removeTile(this.currentMap.tiles[k].go);
						}
					}
				}
				this.goal = this.targetedGobj;
			}
			this.checkButtons();
			this.resetTrucks();
		}
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x00054BA4 File Offset: 0x00052DA4
	private void resetTrucks()
	{
		if (this.TruckColorCoding)
		{
			return;
		}
		List<levelEditorManager.Block> list = new List<levelEditorManager.Block>(this.currentMap.tiles.FindAll((levelEditorManager.Block block) => block.type == levelEditorManager.blockType.btruckblock));
		foreach (levelEditorManager.Block block2 in list)
		{
			foreach (Renderer renderer in block2.go.GetComponentsInChildren<Renderer>())
			{
				renderer.material.color = Color.white;
			}
		}
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x00054C7C File Offset: 0x00052E7C
	public void onSwitchedBlockType()
	{
		string category = this._blockcategoriesArray[this._blockType_Dropdown.value];
		this.initializeBlockMenu(category);
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x00054CA8 File Offset: 0x00052EA8
	public void restartPlayTest()
	{
		Debug.Log("Restarting Playtest");
		UnityEngine.Object.Destroy(GameObject.FindGameObjectWithTag("testMap"));
		foreach (RemoveOnMapChange removeOnMapChange in UnityEngine.Object.FindObjectsOfType<RemoveOnMapChange>())
		{
			UnityEngine.Object.Destroy(removeOnMapChange.gameObject);
		}
		this.playTest();
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x00054D00 File Offset: 0x00052F00
	public void PlaytestEscape()
	{
		Debug.Log("Playtesting ESCAPE");
		if (MonoSingletonBase<EditorGizmoSystem>.Instance.AreGizmosTurnedOff)
		{
			switch (this._currentTool)
			{
			case levelEditorManager.Tool.moveTool:
			{
				ActiveGizmoTypeChangeAction activeGizmoTypeChangeAction = new ActiveGizmoTypeChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType, GizmoType.Translation);
				activeGizmoTypeChangeAction.Execute();
				break;
			}
			case levelEditorManager.Tool.rotationTool:
			{
				ActiveGizmoTypeChangeAction activeGizmoTypeChangeAction2 = new ActiveGizmoTypeChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType, GizmoType.Rotation);
				activeGizmoTypeChangeAction2.Execute();
				break;
			}
			case levelEditorManager.Tool.scaleTool:
			{
				ActiveGizmoTypeChangeAction activeGizmoTypeChangeAction3 = new ActiveGizmoTypeChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType, GizmoType.Scale);
				activeGizmoTypeChangeAction3.Execute();
				break;
			}
			}
		}
		UnityEngine.Object.Destroy(GameObject.FindGameObjectWithTag("testMap"));
		foreach (RemoveOnMapChange removeOnMapChange in UnityEngine.Object.FindObjectsOfType<RemoveOnMapChange>())
		{
			UnityEngine.Object.Destroy(removeOnMapChange.gameObject);
		}
		this.worldPlane.GetComponent<Collider>().enabled = true;
		this.mapBase.SetActive(true);
		this.editorUI.SetActive(true);
		this.m_editorCamera.enabled = true;
		this.m_editorCamera.GetComponent<AudioListener>().enabled = true;
		if (this.targetedGobj != null)
		{
			this.targetedGobj.SetActive(true);
		}
		this._playTesting = false;
		base.StartCoroutine(this.lockCursorIE(false));
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x00054E5C File Offset: 0x0005305C
	public void playTest()
	{
		this._playTesting = true;
		this.updateMapForGizmos();
		this._testLevel = new levelEditorManager.Map();
		foreach (levelEditorManager.Block block in this.currentMap.tiles)
		{
			levelEditorManager.Block block2 = new levelEditorManager.Block(block, true);
			block2.EventInfo = block.EventInfo;
			this._testLevel.tiles.Add(block2);
		}
		this._testLevel.settings = this.currentMap.settings;
		this._testLevel.customSkyBoxSettings = this.currentMap.customSkyBoxSettings;
		bool flag = false;
		this.mapBase.SetActive(false);
		if (this.targetedGobj != null)
		{
			this.targetedGobj.SetActive(false);
		}
		Camera.main.enabled = false;
		this.m_editorCamera.GetComponent<AudioListener>().enabled = false;
		this.editorUI.SetActive(false);
		GizmosTurnOffAction gizmosTurnOffAction = new GizmosTurnOffAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType);
		gizmosTurnOffAction.Execute();
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("mapBaseTest"), Vector3.zero, Quaternion.identity);
		GameObject gameObject2 = new GameObject("Kill", new Type[]
		{
			typeof(KillOnHeight)
		});
		gameObject2.transform.position = new Vector3(0f, float.Parse(this._testLevel.getSettings()[0]), 0f);
		gameObject2.transform.SetParent(gameObject.transform);
		gameObject.AddComponent<RemoveOnMapChange>();
		Debug.Log("Spawned MapBAse");
		try
		{
			this.worldPlane.GetComponent<Renderer>().material = this.TerrainMaterials[int.Parse(this._testLevel.getSettings()[2])];
			if (int.Parse(this._testLevel.getSettings()[2]) == 0)
			{
				Debug.Log("Empty WOrld!");
				this.worldPlane.GetComponent<Collider>().enabled = false;
			}
			else
			{
				this.worldPlane.GetComponent<Collider>().enabled = true;
			}
		}
		catch
		{
			this.worldPlane.GetComponent<Renderer>().material = this.TerrainMaterials[0];
		}
		foreach (levelEditorManager.Block block3 in this._testLevel.tiles)
		{
			string str = string.Empty;
			bool flag2 = false;
			switch (block3.type)
			{
			case levelEditorManager.blockType.btruckblock:
				str = "Vehicles/";
				break;
			case levelEditorManager.blockType.bdestructibleblock:
				str = "Destructible/";
				flag2 = true;
				break;
			case levelEditorManager.blockType.broadsblock:
				str = "Roads/";
				break;
			case levelEditorManager.blockType.btrapsblock:
				str = "Traps/";
				break;
			case levelEditorManager.blockType.broadpointblock:
				break;
			case levelEditorManager.blockType.bgoal:
				break;
			case levelEditorManager.blockType.bplayerspawn:
				Debug.Log("Found Player!");
				flag = true;
				break;
			case levelEditorManager.blockType.bpropsblock:
				str = "Props/";
				break;
			case levelEditorManager.blockType.bshapesblock:
				str = "Shapes/";
				break;
			case levelEditorManager.blockType.beventsblock:
				str = "Events/";
				break;
			default:
				throw new Exception("Invalid Blocktype!");
			}
			Vector3 position = new Vector3(block3.x, block3.y, block3.z);
			Vector3 one = new Vector3(block3.scalex, block3.scaley, block3.scalez);
			Vector3 euler = new Vector3(block3.rotx, block3.roty, block3.rotz);
			if (block3.type == levelEditorManager.blockType.bplayerspawn || block3.type == levelEditorManager.blockType.btruckblock)
			{
				one = Vector3.one;
			}
			string text = block3.id;
			if (block3.id.Split(new char[]
			{
				'_'
			}).Length > 1)
			{
				Debug.Log("Has Custom Name! " + block3.id.Split(new char[]
				{
					'_'
				})[1]);
				text = block3.id.Split(new char[]
				{
					'_'
				})[0];
			}
			else
			{
				Debug.Log("Has NO CustomName! " + block3.id.Split(new char[]
				{
					'_'
				})[0]);
			}
			if (block3.id.Split(new char[]
			{
				'-'
			}).Length > 1)
			{
				text = block3.id.Split(new char[]
				{
					'-'
				})[1];
				block3.id = text;
			}
			Debug.Log("Spawning: " + str + text);
			GameObject gameObject3 = UnityEngine.Object.Instantiate(Resources.Load("blocks/" + str + text), position, Quaternion.Euler(euler)) as GameObject;
			if (gameObject3.GetComponent<RemoveOnMapChange>() == null)
			{
				gameObject3.AddComponent<RemoveOnMapChange>();
			}
			if (block3.objsParams != null)
			{
				Debug.Log("Obj Params for: " + gameObject3.name);
				if (gameObject3.GetComponent<ModifierRecieverBase>())
				{
					gameObject3.GetComponent<ModifierRecieverBase>().sendInfo(block3.objsParams, false);
				}
			}
			if (block3.Behaviours != null)
			{
				gameObject3.AddComponent<BehaviourObjectHandler>().Init(block3.Behaviours);
			}
			if (flag2)
			{
			}
			gameObject3.transform.localScale = one;
			if (gameObject3.name.Contains(this.broadpointblock.name))
			{
				gameObject3.layer = 9;
				if (block3.waypointType == 0)
				{
					gameObject3.GetComponent<wayPointType>().setType(1, true);
				}
				else
				{
					gameObject3.GetComponent<wayPointType>().setType(block3.waypointType, true);
				}
			}
			else if (gameObject3.name.Contains(this.realTruck.name))
			{
				if (block3.waypointType == 0)
				{
					gameObject3.GetComponent<car>().setType(1);
				}
				else
				{
					gameObject3.GetComponent<car>().setType(block3.waypointType);
				}
			}
			else if (gameObject3.name.Contains(this.realPlayer.name))
			{
				gameObject3.name = gameObject3.name.Replace("(Clone)", string.Empty);
				gameObject2.GetComponent<KillOnHeight>().setPlayerRef(gameObject3.transform.FindChild("hitbox"));
				gameObject3.GetComponent<GameManager>().playTest();
			}
			block3.go = gameObject3;
		}
		foreach (levelEditorManager.Block block4 in this._testLevel.tiles)
		{
			if (block4.EventInfo != null && block4.EventInfo.Length > 0)
			{
				block4.go.AddComponent<ObjectEventHandler>().Initialize(block4.EventInfo);
			}
		}
		if (!flag)
		{
			GameObject gameObject4 = UnityEngine.Object.Instantiate(Resources.Load("blocks/playerSpawn"), Vector3.zero, Quaternion.identity) as GameObject;
			gameObject2.GetComponent<KillOnHeight>().setPlayerRef(gameObject4.transform.FindChild("hitbox"));
			gameObject4.transform.parent = gameObject.transform;
			gameObject4.layer = 0;
			gameObject4.name = gameObject4.name.Replace("(Clone)", string.Empty);
			gameObject4.GetComponent<GameManager>().playTest();
		}
		Debug.Log("FINISHED");
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x00055640 File Offset: 0x00053840
	public void nextMenu(bool forward)
	{
		if (this.targetedGobj != null)
		{
			UnityEngine.Object.Destroy(this.targetedGobj);
		}
		if (forward)
		{
			if (levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step1)
			{
				levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step2;
				foreach (levelEditorManager.Block block in this.currentMap.tiles)
				{
					if (block.type == levelEditorManager.blockType.btruckblock)
					{
						Debug.Log("FOUND TRUCK WITH: " + block.waypointType + "  WaypointType");
						if (block.waypointType != 0)
						{
							foreach (Renderer renderer in block.go.GetComponentsInChildren<Renderer>())
							{
								renderer.material.color = this.colors[block.waypointType - 1];
							}
						}
						else
						{
							foreach (Renderer renderer2 in block.go.GetComponentsInChildren<Renderer>())
							{
								renderer2.material.color = this.colors[0];
							}
						}
					}
				}
			}
			else if (levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step2)
			{
				this.step2.SetActive(false);
				this.step3.SetActive(true);
				levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step3;
			}
			else if (levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step3)
			{
				levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step4;
				this.targetedGobj = UnityEngine.Object.Instantiate<GameObject>(this.truckBrushSphere);
				this.targetedGobj.GetComponent<Renderer>().material.color = this.currentBrushColor;
				foreach (levelEditorManager.Block block2 in this.currentMap.tiles)
				{
					if (block2.type == levelEditorManager.blockType.btruckblock)
					{
						Debug.Log("FOUND TRUCK WITH: " + block2.waypointType + "  WaypointType");
						if (block2.waypointType != 0)
						{
							foreach (Renderer renderer3 in block2.go.GetComponentsInChildren<Renderer>())
							{
								renderer3.material.color = this.colors[block2.waypointType - 1];
							}
						}
						else
						{
							foreach (Renderer renderer4 in block2.go.GetComponentsInChildren<Renderer>())
							{
								renderer4.material.color = this.colors[0];
							}
						}
					}
				}
			}
			else
			{
				if (levelEditorManager.CurrentMenuState != levelEditorManager.menuState.step4)
				{
					throw new Exception("Finsihed!");
				}
				levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step5;
				this.targetedGobj = UnityEngine.Object.Instantiate<GameObject>(this.roadPointDeleteSphere);
			}
		}
		else if (levelEditorManager.CurrentMenuState == levelEditorManager.menuState.step2)
		{
			this.step2.SetActive(false);
			this.step1.SetActive(true);
			levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step1;
			this.resetTrucks();
		}
		Debug.Log("Current Menu STate: " + levelEditorManager.CurrentMenuState.ToString());
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x000559D0 File Offset: 0x00053BD0
	public void showWaypointColors()
	{
		levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step1;
		bool flag = true;
		if (flag)
		{
			this.nextMenu(true);
		}
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x000559F4 File Offset: 0x00053BF4
	private void showBrushColors()
	{
		levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step3;
		this.nextMenu(true);
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x00055A04 File Offset: 0x00053C04
	private void ActivateRoadPointDeleteSphere()
	{
		levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step4;
		this.nextMenu(true);
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x00055A14 File Offset: 0x00053C14
	public void clear(bool spawnDefault = false)
	{
		MonoSingletonBase<EditorObjectSelection>.Instance.clearSelection();
		GizmosTurnOffAction gizmosTurnOffAction = new GizmosTurnOffAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType);
		gizmosTurnOffAction.Execute();
		foreach (levelEditorManager.Block block in this.currentMap.tiles)
		{
			if (block.go != null)
			{
				UnityEngine.Object.Destroy(block.go);
			}
		}
		if (this.targetedGobj != null)
		{
			UnityEngine.Object.Destroy(this.targetedGobj);
		}
		foreach (PathrendererModifier pathrendererModifier in this.mapBase.GetComponentsInChildren<PathrendererModifier>())
		{
			UnityEngine.Object.Destroy(pathrendererModifier.gameObject);
		}
		this.currentMap.tiles = new List<levelEditorManager.Block>();
		if (spawnDefault)
		{
			this.addBlockToMap((GameObject)UnityEngine.Object.Instantiate(this.defaultTruck, new Vector3(0f, 2.04f, 0f), Quaternion.Euler(0f, 180f, 0f)), levelEditorManager.blockType.btruckblock);
			this.playerSpawn = (GameObject)UnityEngine.Object.Instantiate(this.defaultPlayer, new Vector3(0f, 3.13f, 0f), Quaternion.identity);
			this.addBlockToMap(this.playerSpawn, levelEditorManager.blockType.bplayerspawn);
		}
		this.updateCurrentBlockCount();
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x00055BA0 File Offset: 0x00053DA0
	public void clear(levelEditorManager.Map mapToclear)
	{
		MonoSingletonBase<EditorObjectSelection>.Instance.clearSelection();
		GizmosTurnOffAction gizmosTurnOffAction = new GizmosTurnOffAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType);
		gizmosTurnOffAction.Execute();
		foreach (levelEditorManager.Block block in mapToclear.tiles)
		{
			if (block.go != null)
			{
				Debug.Log("Destroying: " + block.go.name, block.go);
				UnityEngine.Object.Destroy(block.go);
			}
		}
		if (this.targetedGobj != null)
		{
			UnityEngine.Object.Destroy(this.targetedGobj);
		}
		foreach (PathrendererModifier pathrendererModifier in this.mapBase.GetComponentsInChildren<PathrendererModifier>())
		{
			UnityEngine.Object.Destroy(pathrendererModifier.gameObject);
		}
		mapToclear.tiles = new List<levelEditorManager.Block>();
		this.updateCurrentBlockCount();
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x00055CC0 File Offset: 0x00053EC0
	public void quit()
	{
		if (!this.hasChangedAnything)
		{
			SceneManager.LoadScene(0);
		}
		else
		{
			this.openPopUpMenu(null);
			this.modalPanel.Choice("Do you want to quit without saving?", new UnityAction(this.QuitYesFunction), new UnityAction(this.QuitNoFunction), new UnityAction(this.QuitCancelFunction), "Quit", "Save n' Quit", "Cancel");
		}
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x00055D30 File Offset: 0x00053F30
	public void QuitNoFunction()
	{
		levelEditorManager.SAVING = true;
		base.StartCoroutine(this.Save(false));
		base.StartCoroutine(this.WaitForSave());
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x00055D60 File Offset: 0x00053F60
	private IEnumerator WaitForSave()
	{
		while (levelEditorManager.SAVING)
		{
			yield return null;
		}
		SceneManager.LoadScene(0);
		yield break;
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x00055D74 File Offset: 0x00053F74
	public void QuitYesFunction()
	{
		SceneManager.LoadScene(0);
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x00055D7C File Offset: 0x00053F7C
	public void QuitCancelFunction()
	{
		this.modalPanel.ClosePanel();
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x00055D8C File Offset: 0x00053F8C
	public FileInfo getCurrentMapFile()
	{
		if (this.currentMapFile != null)
		{
			Debug.Log("Returning FILE: " + this.currentMapFile.FullName);
		}
		else
		{
			Debug.Log("Returning NULL FILE");
		}
		return this.currentMapFile;
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x00055DD4 File Offset: 0x00053FD4
	public FileInfo saveTempMap(string _mapName)
	{
		this.modalPanel.ClosePanel();
		Debug.Log("LastSaved: " + this.lastLoadedOrSavedMap + "Save Text: " + _mapName);
		string text = Application.dataPath + "/tmp";
		if (this.currentMap.getSettings().Length > 0 && int.Parse(this.currentMap.getSettings()[3]) < this.SkyBoxes.Length)
		{
			this.currentMap.setCustomSkyBox(null);
		}
		JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
		jsonSerializerSettings.TypeNameHandling = TypeNameHandling.Objects;
		string contents = JsonConvert.SerializeObject(this.currentMap, Formatting.None, jsonSerializerSettings);
		if (!Directory.Exists(text + "/"))
		{
			Directory.CreateDirectory(text + "/");
		}
		text = text + "/" + _mapName + levelEditorManager.fileEnding;
		Debug.Log(text);
		File.WriteAllText(text, contents);
		Directory.CreateDirectory(Application.dataPath + "/maps/previews/" + _mapName);
		Application.CaptureScreenshot(Application.dataPath + "/maps/previews/" + _mapName + "/1.jpg");
		this.displayManager.DisplayMessage("SAVE SUCESSFUL!");
		return new FileInfo(text);
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x00055F00 File Offset: 0x00054100
	public void SAVE_YNC()
	{
		if (this._saveMenuOpen)
		{
			this.modalPanel.ClosePanel();
			this._saveMenuOpen = false;
			return;
		}
		this.loadPopUp.SetActive(false);
		this.uploadInfoPopUp.SetActive(false);
		this.settingsInfoPopup.SetActive(false);
		this.openPopUpMenu(null);
		this.modalPanel.Choice("Do you want to Save?", new UnityAction(this.SaveYesFunction), new UnityAction(this.SaveNoFunction), true, "Yes", "No");
		this._saveMenuOpen = true;
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x00055F90 File Offset: 0x00054190
	private void SaveYesFunction()
	{
		int num = this.saveInputField.text.IndexOfAny(Path.GetInvalidFileNameChars());
		Debug.Log(num);
		if (!this.saveInputField.text.IsValid())
		{
			this.displayManager.DisplayMessage("Mapname is Invalid!");
			return;
		}
		base.StartCoroutine(this.Save(false));
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x00055FF4 File Offset: 0x000541F4
	private void SaveNoFunction()
	{
		this.modalPanel.ClosePanel();
		this._saveMenuOpen = false;
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x00056008 File Offset: 0x00054208
	public void UploadYesFunction()
	{
		Debug.Log("Map name: " + this.lastLoadedOrSavedMap);
		if (this.uploadInfoPopUp.activeSelf)
		{
			this.uploadInfoPopUp.SetActive(false);
			this._uploadMenuOpen = false;
			return;
		}
		if (!SteamManager.Initialized)
		{
			this.displayManager.DisplayMessage("Steamworks is not Initialized!");
			return;
		}
		this.openPopUpMenu(this.uploadInfoPopUp);
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x00056078 File Offset: 0x00054278
	public void OpenSettings()
	{
		if (this.settingsInfoPopup.activeSelf)
		{
			this.settingsInfoPopup.SetActive(false);
			this._settingsMenuOpen = false;
			return;
		}
		this.openPopUpMenu(this.settingsInfoPopup);
		TutorialHandler.Instance.SpecialEvents[16].Clicked();
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x000560C8 File Offset: 0x000542C8
	private void UploadNoFunction()
	{
		this.modalPanel.ClosePanel();
		this._uploadMenuOpen = false;
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x000560DC File Offset: 0x000542DC
	private void SaveCancelFunction()
	{
		this.displayManager.DisplayMessage("I give up!");
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x000560F0 File Offset: 0x000542F0
	private void OverwriteYesFunction()
	{
		base.StartCoroutine(this.Save(true));
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x00056100 File Offset: 0x00054300
	private void OverwriteNoFunction()
	{
		this.modalPanel.ClosePanel();
		this.modalPanel.Choice("Do you want to Save?", new UnityAction(this.SaveYesFunction), new UnityAction(this.SaveNoFunction), true, "Yes", "No");
		this._saveMenuOpen = false;
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x00056154 File Offset: 0x00054354
	public void onChangedTool(int _tool)
	{
		if (levelEditorManager.CurrentMenuState.Equals(levelEditorManager.menuState.step4) || levelEditorManager.CurrentMenuState.Equals(levelEditorManager.menuState.step5))
		{
			UnityEngine.Object.Destroy(this.targetedGobj);
			this.targetedGobj = null;
			levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step1;
		}
		if (this._currentTool.Equals((levelEditorManager.Tool)_tool))
		{
			return;
		}
		if (levelEditorManager.CurrentMenuState.Equals(levelEditorManager.menuState.step2))
		{
			levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step1;
			if (this.targetedGobj != null && this.targetedGobj.name.ToLower().Contains("waypoint"))
			{
				UnityEngine.Object.Destroy(this.targetedGobj);
				this.targetedGobj = null;
			}
		}
		if (levelEditorManager.CurrentMenuState.Equals(levelEditorManager.menuState.step3))
		{
			levelEditorManager.CurrentMenuState = levelEditorManager.menuState.step1;
			if (this.targetedGobj != null)
			{
				this.targetedGobj.transform.parent = this.mapBase.transform;
				if (this.targetedGobj.name.Contains("goal"))
				{
					this.setLayerForObject(this.targetedGobj.transform, 17);
				}
				else
				{
					this.setLayerForObject(this.targetedGobj.transform, 0);
				}
				this.addBlockToMap(this.targetedGobj);
				this.targetedGobj = null;
			}
		}
		this.resetTrucks();
		if (_tool == 0 && this._currentTool.Equals(levelEditorManager.Tool.modifierTool) && this.targetedGobj != null)
		{
			this.targetedGobj = null;
		}
		if (this._currentTool == levelEditorManager.Tool.addTool && this.targetedGobj != null)
		{
			this.targetedGobj.transform.parent = this.mapBase.transform;
			this.setLayerForObject(this.targetedGobj.transform, 0);
			this.revertObject(this.targetedGobj);
			this.targetedGobj = null;
		}
		if (this._currentTool == levelEditorManager.Tool.propertyTool)
		{
			if (this.targetedGobj != null)
			{
				this.setLayerForObject(this.targetedGobj.transform, 0);
				this.targetedGobj = null;
			}
			PropertyToolLogic.Instance.Close();
		}
		if (_tool.Equals(4))
		{
			if (this.targetedGobj != null)
			{
				this.setLayerForObject(this.targetedGobj.transform, 0);
			}
			this.currentModifierComponent = null;
		}
		if (this._currentTool != levelEditorManager.Tool.moveTool || _tool != 2)
		{
			if (this.targetedGobj != null)
			{
				this.setLayerForObject(this.targetedGobj.transform, 0);
				GameObject gameObject = (!(this.targetedGobj.GetComponentInChildren<useThisObject>() == null)) ? this.targetedGobj.GetComponentInChildren<useThisObject>().gameObject : this.targetedGobj;
				if (this.currentMaterial != null && this.currentMaterial.Length > 0 && gameObject.GetComponentInChildren<Renderer>() != null)
				{
					Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].material = this.currentMaterial[i];
					}
					this.currentMaterial = null;
				}
				this.targetedGobj = null;
			}
		}
		this._currentTool = (levelEditorManager.Tool)_tool;
		Debug.Log("New Tool: " + this._currentTool.ToString());
		ToolPanelRef.Instance().changedTool(_tool);
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x000564E4 File Offset: 0x000546E4
	public void setLayerForObject(Transform obj, int _layer)
	{
		Transform transform = obj;
		if (transform.parent != null && !transform.parent.Equals(this.mapBase.transform))
		{
			while (!transform.parent.Equals(this.mapBase.transform))
			{
				transform = transform.parent;
			}
		}
		transform.gameObject.layer = _layer;
		foreach (Transform transform2 in transform.GetComponentsInChildren<Transform>())
		{
			transform2.gameObject.layer = _layer;
		}
		if (_layer != 0)
		{
			return;
		}
		transform.SendMessage("fixLayer", SendMessageOptions.DontRequireReceiver);
		foreach (Transform transform3 in transform.GetComponentsInChildren<Transform>())
		{
			transform3.SendMessage("fixLayer", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x000565C8 File Offset: 0x000547C8
	private void listenForGeneralHotKeyes()
	{
		if (Application.isEditor)
		{
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S))
			{
				this.quickSave();
			}
			return;
		}
		if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
		{
			this.quickSave();
		}
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x00056634 File Offset: 0x00054834
	private void listenForHotKeyes()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			this.onChangedTool(2);
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			this.onChangedTool(1);
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			this.onChangedTool(5);
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			TransformSpaceChangeAction transformSpaceChangeAction = new TransformSpaceChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.TransformSpace, TransformSpace.Global);
			transformSpaceChangeAction.Execute();
		}
		else if (Input.GetKeyDown(KeyCode.L))
		{
			TransformSpaceChangeAction transformSpaceChangeAction2 = new TransformSpaceChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.TransformSpace, TransformSpace.Local);
			transformSpaceChangeAction2.Execute();
		}
	}

	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06000D7A RID: 3450 RVA: 0x000566E0 File Offset: 0x000548E0
	public levelEditorManager.Map getCurrentMap
	{
		get
		{
			return this.currentMap;
		}
	}

	// Token: 0x17000283 RID: 643
	// (get) Token: 0x06000D7B RID: 3451 RVA: 0x000566E8 File Offset: 0x000548E8
	public levelEditorManager.Map getActiveMap
	{
		get
		{
			return (!this.IsPlayTesting) ? this.currentMap : this._testLevel;
		}
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x00056708 File Offset: 0x00054908
	private void zoomToSelectedObject()
	{
		this.dontRecieveInput = true;
		this.m_editorCamera.transform.parent.position = this.targetedGobj.transform.position;
		this.dontRecieveInput = false;
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x00056748 File Offset: 0x00054948
	private void updateCurrentBlockCount()
	{
		levelEditorManager.CurrentObjectCount = this.currentMap.tiles.Count;
		this._currentObjectCountText.text = "Objects: " + levelEditorManager.CurrentObjectCount.ToString() + " / " + this.OBJECT_MAX_COUNT.ToString();
		this.hasChangedAnything = true;
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x000567A0 File Offset: 0x000549A0
	public void openPopUpMenu(GameObject _popUpToOpen)
	{
		this.modalPanel.ClosePanel();
		this.loadPopUp.SetActive(false);
		this.uploadInfoPopUp.SetActive(false);
		this.settingsInfoPopup.SetActive(false);
		TutorialHandler.Instance.HelpMenu.SetActive(false);
		this.proptertiesInfoPopup.SetActive(false);
		if (_popUpToOpen != null)
		{
			this._saveMenuOpen = false;
			_popUpToOpen.SetActive(true);
		}
		this._loadMenuOpen = this.loadPopUp.activeSelf;
		this._uploadMenuOpen = this.uploadInfoPopUp.activeSelf;
		this._settingsMenuOpen = this.settingsInfoPopup.activeSelf;
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x00056848 File Offset: 0x00054A48
	public void _assignMaterials(GameObject _obj)
	{
		GameObject gameObject = (!(_obj.GetComponentInChildren<useThisObject>() == null)) ? _obj.GetComponentInChildren<useThisObject>().gameObject : _obj;
		Debug.Log("Assigning Material for, " + gameObject.name, gameObject);
		if (gameObject.GetComponentsInChildren<Renderer>().Length == 0)
		{
			this.currentMaterial = null;
			return;
		}
		Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
		this.currentMaterial = new Material[componentsInChildren.Length];
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			this.currentMaterial[i] = componentsInChildren[i].material;
			componentsInChildren[i].material = this.ghostMaterial;
		}
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x000568EC File Offset: 0x00054AEC
	private bool roadHandleConnection(Vector3 pos, roadHandleConnection closestConnection)
	{
		Vector3 b = (!this.targetedGobj.GetComponentInChildren<useThisObject>()) ? Vector3.zero : this.targetedGobj.GetComponentInChildren<useThisObject>().transform.localPosition;
		Vector3 vector = pos + b + closestConnection.getT1().localPosition;
		this.HELPME.transform.position = vector;
		Debug.Log("Theory PosBEFORE:  " + vector);
		Vector3 vector2 = vector - new Vector3(this.targetedGobj.GetComponent<Collider>().bounds.center.x, closestConnection.getT1().position.y, this.targetedGobj.GetComponent<Collider>().bounds.center.z);
		Debug.Log("Direction B.C: " + vector2);
		vector2 = Quaternion.Euler(0f, this.targetedGobj.transform.rotation.eulerAngles.y, 0f) * vector2;
		Debug.Log("Direction: " + vector2);
		Vector3 a = vector2 + new Vector3(this.targetedGobj.GetComponent<Collider>().bounds.center.x, closestConnection.getT1().position.y, this.targetedGobj.GetComponent<Collider>().bounds.center.z);
		Debug.Log(string.Concat(new object[]
		{
			"Theory Pos: ",
			vector,
			" STAY: ",
			closestConnection.getT2().position
		}), closestConnection.getT1());
		if (Vector3.Distance(a, closestConnection.getT2().position) < 20f)
		{
			Debug.Log("ClosestConnection T1: ", closestConnection.getT1());
			Debug.Log("ClosestConnection T2: ", closestConnection.getT2());
			Vector3 vector3 = this.snappingPosition(closestConnection);
			this.HELPMEDIR.transform.position = vector3;
			this.HELPMEDIR.GetComponentInChildren<Text>().text = this.targetedGobj.transform.rotation.eulerAngles.y.ToString();
			if (vector3 != this.targetedGobj.transform.position)
			{
				this.targetedGobj.transform.position = vector3;
			}
			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit raycastHit;
				Physics.Raycast(this.m_editorCamera.ScreenPointToRay(Input.mousePosition), out raycastHit, 500f, this.onlyUIMask);
				if (raycastHit.collider)
				{
					if (this.targetedGobj != null)
					{
						this.removeBlockFromMap(this.targetedGobj);
						this.targetedGobj = null;
					}
					return true;
				}
				foreach (levelEditorManager.Block block in this.currentMap.tiles)
				{
					if (block.go.Equals(this.targetedGobj))
					{
						this.setLayerForObject(this.targetedGobj.transform, 0);
						Debug.Log("currentBlockString: " + this.currentBlockString + " Updating Object ", this.targetedGobj);
						this.targetedGobj.transform.SetParent(this.mapBase.transform);
						this.updateBlockToMap(block, this.targetedGobj.transform);
						closestConnection.getT1().GetComponent<RoadHandle>().makeConnection(closestConnection.getT2());
						closestConnection.getT2().GetComponent<RoadHandle>().makeConnection(closestConnection.getT1());
						this.targetedGobj = null;
						return true;
					}
				}
				this.setLayerForObject(this.targetedGobj.transform, 0);
				Debug.Log("currentBlockString: " + this.currentBlockString + " Updating Object ", this.targetedGobj);
				this.targetedGobj.transform.SetParent(this.mapBase.transform);
				this.addBlockToMap(this.targetedGobj);
				closestConnection.getT1().GetComponent<RoadHandle>().makeConnection(closestConnection.getT2());
				closestConnection.getT2().GetComponent<RoadHandle>().makeConnection(closestConnection.getT1());
				this.onChangedTool((int)this._previousTool);
				this.targetedGobj = null;
				return true;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x00056DA8 File Offset: 0x00054FA8
	private bool checkForRoadhandle(RaycastHit hit)
	{
		if (hit.collider.GetComponent<RoadHandle>() != null)
		{
			RoadHandle.blockFamily blockFamily = hit.collider.GetComponent<RoadHandle>().getBlockFamily();
			if (blockFamily != RoadHandle.blockFamily.castle)
			{
				Debug.Log("NOT");
			}
			else
			{
				Debug.Log("Its a Castle Road!");
				List<UnityEngine.Object> list = new List<UnityEngine.Object>();
				foreach (UnityEngine.Object @object in Resources.LoadAll("mapEditor/"))
				{
					if (@object.name.ToLower().Contains("castleroad"))
					{
						list.Add(@object);
						Debug.Log(@object.name);
					}
				}
				RoadHandler.Instance().Initialize(list.ToArray(), hit.collider.gameObject);
			}
			Debug.Log("RoadHandle!!");
			return true;
		}
		return false;
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x00056E88 File Offset: 0x00055088
	private Vector3 snappingPosition(roadHandleConnection connection)
	{
		Transform t = connection.getT2();
		Vector3 localPosition = connection.getT1().localPosition;
		Vector3 b = (!(this.targetedGobj.GetComponentInChildren<useThisObject>() == null)) ? this.targetedGobj.GetComponentInChildren<useThisObject>().transform.localPosition : Vector3.zero;
		Vector3 vector = t.position - (localPosition + b);
		float y = t.GetComponentInParent<useThisObject>().transform.localPosition.y - this.targetedGobj.GetComponentInChildren<useThisObject>().transform.localPosition.y + t.GetComponentInParent<editor_first>().transform.position.y;
		Vector3 a = new Vector3(vector.x, y, vector.z);
		Vector3 vector2 = a - t.position;
		vector2 = Quaternion.Euler(0f, this.targetedGobj.transform.rotation.eulerAngles.y, 0f) * vector2;
		return vector2 + t.position;
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x00056FB8 File Offset: 0x000551B8
	public void saveCustomSkyBoxSettingsFor(int skybox)
	{
		Debug.Log("Custom SKy box saving: " + (skybox - this.SkyBoxes.Length).ToString());
		string[] array = new string[(skybox - this.SkyBoxes.Length != 1) ? Enum.GetNames(typeof(levelEditorManager.customSkyBoxHorizonSettings)).Length : Enum.GetNames(typeof(levelEditorManager.customSkyBoxGradientSettings)).Length];
		if (array.Length == Enum.GetNames(typeof(levelEditorManager.customSkyBoxGradientSettings)).Length)
		{
			array[1] = extensionMethods.ColorToHex(this.GradientSkyBox.GetColor("_Color1"));
			array[0] = extensionMethods.ColorToHex(this.GradientSkyBox.GetColor("_Color2"));
			array[3] = this.GradientSkyBox.GetFloat("_Exponent").ToString("F2");
			array[2] = this.GradientSkyBox.GetFloat("_Intensity").ToString("F2");
			array[4] = this.GradientSkyBox.GetFloat("_UpVectorPitch").ToString("F2");
			array[5] = this.GradientSkyBox.GetFloat("_UpVectorYaw").ToString("F2");
		}
		else
		{
			array[0] = extensionMethods.ColorToHex(this.HorizonSkyBox.GetColor("_SkyColor1"));
			array[2] = extensionMethods.ColorToHex(this.HorizonSkyBox.GetColor("_SkyColor2"));
			array[3] = extensionMethods.ColorToHex(this.HorizonSkyBox.GetColor("_SkyColor3"));
			array[6] = extensionMethods.ColorToHex(this.HorizonSkyBox.GetColor("_SunColor"));
			array[8] = this.HorizonSkyBox.GetFloat("_SunAlpha").ToString("F2");
			array[9] = this.HorizonSkyBox.GetFloat("_SunBeta").ToString("F2");
			array[11] = this.HorizonSkyBox.GetFloat("_SunAltitude").ToString("F2");
			array[10] = this.HorizonSkyBox.GetFloat("_SunAzimuth").ToString("F2");
			array[7] = this.HorizonSkyBox.GetFloat("_SunIntensity").ToString("F2");
			array[5] = this.HorizonSkyBox.GetFloat("_SkyIntensity").ToString("F2");
			array[1] = this.HorizonSkyBox.GetFloat("_SkyExponent1").ToString("F2");
			array[4] = this.HorizonSkyBox.GetFloat("_SkyExponent2").ToString("F2");
		}
		int num = 0;
		foreach (string str in array)
		{
			Debug.Log(((levelEditorManager.customSkyBoxHorizonSettings)num).ToString() + " : " + str);
			num++;
		}
		this.currentMap.setCustomSkyBox(array);
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x000572CC File Offset: 0x000554CC
	public void changeWorldMaterial(Material mat)
	{
		this.worldPlane.GetComponent<Renderer>().material = mat;
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x000572E0 File Offset: 0x000554E0
	public void changeWorldMaterial(int index)
	{
		this.worldPlane.GetComponent<Renderer>().material = this.TerrainMaterials[index];
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x000572FC File Offset: 0x000554FC
	public void updateMapForGizmos()
	{
		if (this.currentMaterial != null && this.currentMaterial.Length != 0)
		{
			return;
		}
		Debug.Log("OnUpdateMapForGizmos");
		for (int i = this.currentMap.tiles.Count - 1; i >= 0; i--)
		{
			if (this.currentMap.tiles[i].go)
			{
				this.updateBlockToMap(this.currentMap.tiles[i], this.currentMap.tiles[i].go.transform);
			}
			else
			{
				this.currentMap.tiles.Remove(this.currentMap.tiles[i]);
			}
		}
		this.updateCurrentBlockCount();
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x000573D0 File Offset: 0x000555D0
	public GameObject isGameObjectValidForGizmoSelection(GameObject found)
	{
		if (found.GetComponent<RoadHandle>() != null)
		{
			return null;
		}
		GameObject gameObject = base.gameObject;
		GameObject gameObject2 = found;
		if (found.GetComponent<World>())
		{
			return null;
		}
		if (found.GetComponent<editor_first>())
		{
			return found;
		}
		if (!gameObject2.transform.parent)
		{
			return null;
		}
		while (!gameObject2.transform.parent.Equals(this.mapBase.transform))
		{
			if (gameObject2.transform.parent.GetComponent<editor_first>())
			{
				gameObject = gameObject2.transform.parent.gameObject;
			}
			gameObject2 = gameObject2.transform.parent.gameObject;
		}
		return (!(gameObject == base.gameObject)) ? gameObject : null;
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x000574B4 File Offset: 0x000556B4
	public void OpenProperties()
	{
		if (!this.propertyToolButton.interactable)
		{
			return;
		}
		Debug.Log("Opening Properties!");
		if (this.settingsInfoPopup.activeSelf)
		{
			this.settingsInfoPopup.SetActive(false);
			this._settingsMenuOpen = false;
			return;
		}
		this.openPopUpMenu(this.proptertiesInfoPopup);
		this.objectSelectionChanged(true);
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x00057514 File Offset: 0x00055714
	public void objectSelectionChanged(bool openProperties = false)
	{
		Debug.Log("Object selection changed!");
		HashSet<GameObject> selectedGameObjects = MonoSingletonBase<EditorObjectSelection>.Instance.SelectedGameObjects;
		if (this.goal && selectedGameObjects.Contains(this.goal))
		{
			selectedGameObjects.Remove(this.goal);
		}
		if (this.playerSpawn && selectedGameObjects.Contains(this.playerSpawn))
		{
			selectedGameObjects.Remove(this.playerSpawn);
		}
		if (openProperties)
		{
			List<GameObject> list = new List<GameObject>();
			Dictionary<Modifier, int> dictionary = new Dictionary<Modifier, int>();
			foreach (GameObject gameObject in selectedGameObjects)
			{
				if (gameObject.GetComponent<Modifier>())
				{
					Modifier component = gameObject.GetComponent<Modifier>();
					Modifier modifier = component;
					if (dictionary.ContainsKey(modifier))
					{
						Dictionary<Modifier, int> dictionary3;
						Dictionary<Modifier, int> dictionary2 = dictionary3 = dictionary;
						Modifier key2;
						Modifier key = key2 = modifier;
						int num = dictionary3[key2];
						dictionary2[key] = num + 1;
					}
					else
					{
						dictionary.Add(modifier, 1);
					}
					list.Add(gameObject);
				}
			}
			Debug.Log("Selecting Modifiers!");
			foreach (KeyValuePair<Modifier, int> keyValuePair in dictionary)
			{
				Debug.Log(keyValuePair.Key + " Modifier : " + keyValuePair.Value);
			}
			if (dictionary.Count > 0)
			{
				Modifier modifier2 = null;
				int num2 = 0;
				foreach (KeyValuePair<Modifier, int> keyValuePair2 in dictionary)
				{
					if (keyValuePair2.Value > num2)
					{
						modifier2 = keyValuePair2.Key;
						num2 = keyValuePair2.Value;
					}
				}
				Debug.Log("LEADING MODIFIER: " + modifier2);
				List<GameObject> list2 = new List<GameObject>();
				Modifier modifier3 = null;
				foreach (GameObject gameObject2 in list)
				{
					if (gameObject2.GetComponent<Modifier>().Equals(modifier2))
					{
						Debug.Log("Found Leaders: ", gameObject2);
						if (!modifier3)
						{
							modifier3 = gameObject2.GetComponent<Modifier>();
						}
						list2.Add(gameObject2);
					}
				}
				PropertyToolLogic.Instance.Initialize(modifier3, list2.ToArray());
				TutorialHandler.Instance.SpecialEvents[14].Clicked();
			}
			else if (selectedGameObjects.Count > 0)
			{
				PropertyToolLogic.Instance.Initialize(null, new List<GameObject>(selectedGameObjects).ToArray());
			}
		}
		else if (MonoSingletonBase<EditorGizmoSystem>.Instance.AreGizmosTurnedOff)
		{
			switch (this._currentTool)
			{
			case levelEditorManager.Tool.moveTool:
			{
				ActiveGizmoTypeChangeAction activeGizmoTypeChangeAction = new ActiveGizmoTypeChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType, GizmoType.Translation);
				activeGizmoTypeChangeAction.Execute();
				break;
			}
			case levelEditorManager.Tool.rotationTool:
			{
				ActiveGizmoTypeChangeAction activeGizmoTypeChangeAction2 = new ActiveGizmoTypeChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType, GizmoType.Rotation);
				activeGizmoTypeChangeAction2.Execute();
				break;
			}
			case levelEditorManager.Tool.scaleTool:
			{
				ActiveGizmoTypeChangeAction activeGizmoTypeChangeAction3 = new ActiveGizmoTypeChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.ActiveGizmoType, GizmoType.Scale);
				activeGizmoTypeChangeAction3.Execute();
				break;
			}
			}
		}
		this.propertyToolButton.interactable = (selectedGameObjects.Count > 0);
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x000578E8 File Offset: 0x00055AE8
	private bool TryCopySelection()
	{
		foreach (levelEditorManager.CopyBlock copyBlock in this._currentCopyBlocks)
		{
			UnityEngine.Object.Destroy(copyBlock.go);
		}
		HashSet<GameObject> selectedGameObjects = MonoSingletonBase<EditorObjectSelection>.Instance.SelectedGameObjects;
		if (selectedGameObjects.Count == 0)
		{
			return false;
		}
		this.updateMapForGizmos();
		this._currentCopyBlocks = new List<levelEditorManager.CopyBlock>();
		foreach (GameObject gameObject in selectedGameObjects)
		{
			if (this.playerSpawn != null && gameObject == this.playerSpawn)
			{
				Debug.Log("Trying To copy Player! Skipping");
			}
			else if (this.goal != null && gameObject == this.goal)
			{
				Debug.Log("Trying To copy Goal! Skipping");
			}
			else
			{
				levelEditorManager.CopyBlock copyBlock2 = new levelEditorManager.CopyBlock();
				ObjectParameterContainer[] array = (this.currentMap.getTileAt(gameObject.transform).objsParams != null) ? this.currentMap.getTileAt(gameObject.transform).objsParams : DefaultParameters.getDefaultParameters(gameObject.name.Split(new char[]
				{
					'('
				})[0]).ToContainerParameters();
				if (array != null)
				{
					copyBlock2.objsParams = array;
				}
				EventInfo[] eventInfo = this.currentMap.getTileAt(gameObject.transform).EventInfo;
				if (eventInfo != null)
				{
					EventInfo[] array2 = eventInfo;
					foreach (EventInfo eventInfo2 in array2)
					{
						if (eventInfo2.EntityIndex == levelEditorManager.EVENT_SELFREFERENCE)
						{
							Debug.LogError("Self Reference!");
						}
					}
					copyBlock2.eventInfo = array2;
				}
				copyBlock2.objBehaviours = this.currentMap.getTileAt(gameObject.transform).Behaviours;
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject2.GetComponent<BlockInfo>().Category = gameObject.GetComponent<BlockInfo>().Category;
				copyBlock2.go = gameObject2;
				gameObject2.SetActive(false);
				this._currentCopyBlocks.Add(copyBlock2);
				gameObject2.name = gameObject2.name.Split(new char[]
				{
					'('
				})[0];
			}
		}
		return true;
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x00057B80 File Offset: 0x00055D80
	private void tryPasteSelection()
	{
		List<levelEditorManager.Block> list = new List<levelEditorManager.Block>();
		Vector3 zero = Vector3.zero;
		foreach (levelEditorManager.CopyBlock copyBlock in this._currentCopyBlocks)
		{
			copyBlock.go.SetActive(true);
			levelEditorManager.Block block = this.addBlockToMap(copyBlock.go);
			if (block == null)
			{
				TransformPivotPoint newPivotPoint = (MonoSingletonBase<EditorGizmoSystem>.Instance.TransformPivotPoint != TransformPivotPoint.Center) ? TransformPivotPoint.Center : TransformPivotPoint.MeshPivot;
				TransformPivotPointChangeAction transformPivotPointChangeAction = new TransformPivotPointChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.TransformPivotPoint, newPivotPoint);
				transformPivotPointChangeAction.Execute();
			}
			else
			{
				list.Add(block);
				block.setObjParams(copyBlock.objsParams, false, false);
				block.setObjectEvents(copyBlock.eventInfo);
				block.setBehaviours(copyBlock.objBehaviours);
				copyBlock.go.transform.position += zero;
			}
		}
		ObjectAddedAction objectAddedAction = new ObjectAddedAction(list.ToArray());
		objectAddedAction.Execute();
		this._currentCopyBlocks = new List<levelEditorManager.CopyBlock>();
		foreach (GameObject gameObject in MonoSingletonBase<EditorObjectSelection>.Instance.SelectedGameObjects)
		{
			if (this.playerSpawn != null && gameObject == this.playerSpawn)
			{
				Debug.Log("Trying To copy Player! Skipping");
				MonoSingletonBase<EditorObjectSelection>.Instance.removeFromSelection(gameObject);
			}
			else if (this.goal != null && gameObject == this.goal)
			{
				Debug.Log("Trying To copy Player! Skipping");
				MonoSingletonBase<EditorObjectSelection>.Instance.removeFromSelection(gameObject);
			}
		}
		TransformPivotPoint newPivotPoint2 = (MonoSingletonBase<EditorGizmoSystem>.Instance.TransformPivotPoint != TransformPivotPoint.Center) ? TransformPivotPoint.Center : TransformPivotPoint.MeshPivot;
		TransformPivotPointChangeAction transformPivotPointChangeAction2 = new TransformPivotPointChangeAction(MonoSingletonBase<EditorGizmoSystem>.Instance.TransformPivotPoint, newPivotPoint2);
		transformPivotPointChangeAction2.Execute();
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x00057DB4 File Offset: 0x00055FB4
	private GameObject BlockToGameObject(levelEditorManager.Block b)
	{
		return b.go;
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x00057DBC File Offset: 0x00055FBC
	private void tryDuplicateSelection()
	{
		if (this.TryCopySelection())
		{
			this.tryPasteSelection();
		}
		this.onChangedTool(1);
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x00057DD8 File Offset: 0x00055FD8
	public void setTargetedGameobject(GameObject newTarget, string message = "")
	{
		Debug.Log(message);
		this.targetedGobj = newTarget;
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x00057DE8 File Offset: 0x00055FE8
	public void UpdateColorCodedTrucks()
	{
		base.StartCoroutine(this.colorCodingCoroutine());
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x00057DF8 File Offset: 0x00055FF8
	private IEnumerator colorCodingCoroutine()
	{
		List<levelEditorManager.Block> _list = new List<levelEditorManager.Block>(this.currentMap.tiles.FindAll((levelEditorManager.Block block) => block.type == levelEditorManager.blockType.btruckblock));
		foreach (levelEditorManager.Block item in _list)
		{
			item.SetColorCode(this.TruckColorCoding);
		}
		yield return null;
		yield break;
	}

	// Token: 0x04000982 RID: 2434
	private const string FILE_PATH_KEY = "CustomFilePath";

	// Token: 0x04000983 RID: 2435
	private const int GOAL_COLLIDER_LAYER = 17;

	// Token: 0x04000984 RID: 2436
	private const int UI_LAYER = 5;

	// Token: 0x04000985 RID: 2437
	private const int IGNORE_RAYCAST_LAYER = 2;

	// Token: 0x04000986 RID: 2438
	private const int DEFAULT_LAYER = 0;

	// Token: 0x04000987 RID: 2439
	private const int CUSTOMSKYBOX_GRADIENT = 1;

	// Token: 0x04000988 RID: 2440
	private const int CUSTOMSKYBOX_HORIZON = 2;

	// Token: 0x04000989 RID: 2441
	public static int CurrentObjectCount = 0;

	// Token: 0x0400098A RID: 2442
	public static bool SAVING = false;

	// Token: 0x0400098B RID: 2443
	private static string _defaultFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\ClusterTruck\\Maps";

	// Token: 0x0400098C RID: 2444
	public static bool canLook = false;

	// Token: 0x0400098D RID: 2445
	public static bool rotatingBool = false;

	// Token: 0x0400098E RID: 2446
	private static levelEditorManager _levelEditorManager;

	// Token: 0x0400098F RID: 2447
	public GameObject HELPME;

	// Token: 0x04000990 RID: 2448
	public GameObject HELPMEDIR;

	// Token: 0x04000991 RID: 2449
	private float OBJECT_MAX_COUNT = float.PositiveInfinity;

	// Token: 0x04000992 RID: 2450
	public Material[] SkyBoxes;

	// Token: 0x04000993 RID: 2451
	public Material[] TerrainMaterials;

	// Token: 0x04000994 RID: 2452
	public Material ghostMaterial;

	// Token: 0x04000995 RID: 2453
	public Material GradientSkyBox;

	// Token: 0x04000996 RID: 2454
	public Material HorizonSkyBox;

	// Token: 0x04000997 RID: 2455
	public Material[] currentMaterial;

	// Token: 0x04000998 RID: 2456
	private bool hasChangedAnything;

	// Token: 0x04000999 RID: 2457
	public Text _currentObjectCountText;

	// Token: 0x0400099A RID: 2458
	private Component currentModifierComponent;

	// Token: 0x0400099B RID: 2459
	private GameObject _pointerMarker;

	// Token: 0x0400099C RID: 2460
	public Toggle _truckColorCodingToggle;

	// Token: 0x0400099D RID: 2461
	public Button upload_confirmButton;

	// Token: 0x0400099E RID: 2462
	public Button propertyToolButton;

	// Token: 0x0400099F RID: 2463
	public InputField saveInputField;

	// Token: 0x040009A0 RID: 2464
	private Camera m_editorCamera;

	// Token: 0x040009A1 RID: 2465
	public LayerMask myMask;

	// Token: 0x040009A2 RID: 2466
	public LayerMask UIBlockMask;

	// Token: 0x040009A3 RID: 2467
	public LayerMask onlyUIMask;

	// Token: 0x040009A4 RID: 2468
	public LayerMask onlyTruckMask;

	// Token: 0x040009A5 RID: 2469
	private bool justChangedState;

	// Token: 0x040009A6 RID: 2470
	public GameObject mapBase;

	// Token: 0x040009A7 RID: 2471
	public GameObject wayPointColors;

	// Token: 0x040009A8 RID: 2472
	public List<GameObject> truckFormations = new List<GameObject>();

	// Token: 0x040009A9 RID: 2473
	[SerializeField]
	private List<levelEditorManager.CopyBlock> _currentCopyBlocks = new List<levelEditorManager.CopyBlock>();

	// Token: 0x040009AA RID: 2474
	public GameObject broadpointblock;

	// Token: 0x040009AB RID: 2475
	public GameObject worldPlane;

	// Token: 0x040009AC RID: 2476
	public GameObject realPlayer;

	// Token: 0x040009AD RID: 2477
	public GameObject realTruck;

	// Token: 0x040009AE RID: 2478
	public GameObject editorUI;

	// Token: 0x040009AF RID: 2479
	public GameObject pathRendererHolderGameObject;

	// Token: 0x040009B0 RID: 2480
	public GameObject truckBrushSphere;

	// Token: 0x040009B1 RID: 2481
	[SerializeField]
	private GameObject roadPointDeleteSphere;

	// Token: 0x040009B2 RID: 2482
	[SerializeField]
	private GameObject defaultTruck;

	// Token: 0x040009B3 RID: 2483
	[SerializeField]
	private GameObject defaultPlayer;

	// Token: 0x040009B4 RID: 2484
	public GameObject blockMenu;

	// Token: 0x040009B5 RID: 2485
	public GameObject grid;

	// Token: 0x040009B6 RID: 2486
	public GameObject cellPrefab;

	// Token: 0x040009B7 RID: 2487
	public GameObject downloadCellPrefab;

	// Token: 0x040009B8 RID: 2488
	public GameObject loadPopUp;

	// Token: 0x040009B9 RID: 2489
	public GameObject step1;

	// Token: 0x040009BA RID: 2490
	public GameObject step2;

	// Token: 0x040009BB RID: 2491
	public GameObject step3;

	// Token: 0x040009BC RID: 2492
	public GameObject downloadGrid;

	// Token: 0x040009BD RID: 2493
	public GameObject downloadPopUp;

	// Token: 0x040009BE RID: 2494
	public GameObject uploadInfoPopUp;

	// Token: 0x040009BF RID: 2495
	public GameObject settingsInfoPopup;

	// Token: 0x040009C0 RID: 2496
	public GameObject proptertiesInfoPopup;

	// Token: 0x040009C1 RID: 2497
	private Color currentWaypointColor = Color.clear;

	// Token: 0x040009C2 RID: 2498
	private Color currentBrushColor = Color.clear;

	// Token: 0x040009C3 RID: 2499
	private bool truckPainter;

	// Token: 0x040009C4 RID: 2500
	private bool _playTesting;

	// Token: 0x040009C5 RID: 2501
	private bool _keepBlockMenuOpen;

	// Token: 0x040009C6 RID: 2502
	private bool _freeModeBool;

	// Token: 0x040009C7 RID: 2503
	private bool _saveMenuOpen;

	// Token: 0x040009C8 RID: 2504
	private bool _loadMenuOpen;

	// Token: 0x040009C9 RID: 2505
	private bool _uploadMenuOpen;

	// Token: 0x040009CA RID: 2506
	private bool dontRecieveInput;

	// Token: 0x040009CB RID: 2507
	private bool _settingsMenuOpen;

	// Token: 0x040009CC RID: 2508
	private bool _canUseGizmos;

	// Token: 0x040009CD RID: 2509
	public Projector mProjector;

	// Token: 0x040009CE RID: 2510
	private int currentVertexCount;

	// Token: 0x040009CF RID: 2511
	public GameObject targetedGobj;

	// Token: 0x040009D0 RID: 2512
	public GameObject goal;

	// Token: 0x040009D1 RID: 2513
	public GameObject playerSpawn;

	// Token: 0x040009D2 RID: 2514
	private string currentBlockString = string.Empty;

	// Token: 0x040009D3 RID: 2515
	private Vector3 mousePosition;

	// Token: 0x040009D4 RID: 2516
	private levelEditorManager.Map currentMap = new levelEditorManager.Map();

	// Token: 0x040009D5 RID: 2517
	private levelEditorManager.Map _testLevel;

	// Token: 0x040009D6 RID: 2518
	private string mapname = string.Empty;

	// Token: 0x040009D7 RID: 2519
	private string lastLoadedOrSavedMap = string.Empty;

	// Token: 0x040009D8 RID: 2520
	private string currentCategory = string.Empty;

	// Token: 0x040009D9 RID: 2521
	private float distance = 10f;

	// Token: 0x040009DA RID: 2522
	private float rotationFloat;

	// Token: 0x040009DB RID: 2523
	private Vector3 prevPos;

	// Token: 0x040009DC RID: 2524
	private Vector3 prevMousePosition;

	// Token: 0x040009DD RID: 2525
	private static levelEditorManager.menuState _CurrentMenuState = levelEditorManager.menuState.step1;

	// Token: 0x040009DE RID: 2526
	private float scrollDelta;

	// Token: 0x040009DF RID: 2527
	private Vector3 savedRot;

	// Token: 0x040009E0 RID: 2528
	private int currentTruckFormationIndex = 1;

	// Token: 0x040009E1 RID: 2529
	public Color[] colors;

	// Token: 0x040009E2 RID: 2530
	public Button addTool_Button;

	// Token: 0x040009E3 RID: 2531
	public Button moveTool_Button;

	// Token: 0x040009E4 RID: 2532
	public Button rotationTool_Button;

	// Token: 0x040009E5 RID: 2533
	private levelEditorManager.Tool _currentTool;

	// Token: 0x040009E6 RID: 2534
	private levelEditorManager.Tool _previousTool = levelEditorManager.Tool.moveTool;

	// Token: 0x040009E7 RID: 2535
	private List<string> _blockcategoriesArray = new List<string>();

	// Token: 0x040009E8 RID: 2536
	public Dropdown _blockType_Dropdown;

	// Token: 0x040009E9 RID: 2537
	private levelEditorManager.delayParameters currentDelayParameter;

	// Token: 0x040009EA RID: 2538
	private FileInfo currentMapFile;

	// Token: 0x040009EB RID: 2539
	private dataBaseHandler db_Handler;

	// Token: 0x040009EC RID: 2540
	private steam_WorkshopHandler workshop_handler;

	// Token: 0x040009ED RID: 2541
	private ModalPanel modalPanel;

	// Token: 0x040009EE RID: 2542
	private DisplayManager displayManager;

	// Token: 0x040009EF RID: 2543
	private UnityAction myYesAction;

	// Token: 0x040009F0 RID: 2544
	private UnityAction myNoAction;

	// Token: 0x040009F1 RID: 2545
	private UnityAction myCancelAction;

	// Token: 0x02000230 RID: 560
	public enum blockType
	{
		// Token: 0x040009F6 RID: 2550
		btruckblock,
		// Token: 0x040009F7 RID: 2551
		bdestructibleblock,
		// Token: 0x040009F8 RID: 2552
		broadsblock,
		// Token: 0x040009F9 RID: 2553
		btrapsblock,
		// Token: 0x040009FA RID: 2554
		broadpointblock,
		// Token: 0x040009FB RID: 2555
		bgoal,
		// Token: 0x040009FC RID: 2556
		bplayerspawn,
		// Token: 0x040009FD RID: 2557
		bpropsblock,
		// Token: 0x040009FE RID: 2558
		bshapesblock,
		// Token: 0x040009FF RID: 2559
		beventsblock
	}

	// Token: 0x02000231 RID: 561
	public enum settings
	{
		// Token: 0x04000A01 RID: 2561
		killheight,
		// Token: 0x04000A02 RID: 2562
		wind,
		// Token: 0x04000A03 RID: 2563
		terrain,
		// Token: 0x04000A04 RID: 2564
		skybox,
		// Token: 0x04000A05 RID: 2565
		fogcolor,
		// Token: 0x04000A06 RID: 2566
		fogDensity,
		// Token: 0x04000A07 RID: 2567
		skyboxcolor,
		// Token: 0x04000A08 RID: 2568
		groundcolor,
		// Token: 0x04000A09 RID: 2569
		horizoncolor,
		// Token: 0x04000A0A RID: 2570
		brightness,
		// Token: 0x04000A0B RID: 2571
		gravity,
		// Token: 0x04000A0C RID: 2572
		weather,
		// Token: 0x04000A0D RID: 2573
		objective
	}

	// Token: 0x02000232 RID: 562
	public enum customSkyBoxGradientSettings
	{
		// Token: 0x04000A0F RID: 2575
		topColor,
		// Token: 0x04000A10 RID: 2576
		bottomColor,
		// Token: 0x04000A11 RID: 2577
		intensity,
		// Token: 0x04000A12 RID: 2578
		exponent,
		// Token: 0x04000A13 RID: 2579
		pitch,
		// Token: 0x04000A14 RID: 2580
		yaw
	}

	// Token: 0x02000233 RID: 563
	public enum customSkyBoxHorizonSettings
	{
		// Token: 0x04000A16 RID: 2582
		topColor,
		// Token: 0x04000A17 RID: 2583
		topFactor,
		// Token: 0x04000A18 RID: 2584
		horizonColor,
		// Token: 0x04000A19 RID: 2585
		bottomColor,
		// Token: 0x04000A1A RID: 2586
		bottomFactor,
		// Token: 0x04000A1B RID: 2587
		intensity,
		// Token: 0x04000A1C RID: 2588
		sunColor,
		// Token: 0x04000A1D RID: 2589
		sunIntensity,
		// Token: 0x04000A1E RID: 2590
		Alpha,
		// Token: 0x04000A1F RID: 2591
		Beta,
		// Token: 0x04000A20 RID: 2592
		Azimuth,
		// Token: 0x04000A21 RID: 2593
		Altitude
	}

	// Token: 0x02000234 RID: 564
	public enum menuState
	{
		// Token: 0x04000A23 RID: 2595
		step1,
		// Token: 0x04000A24 RID: 2596
		step2,
		// Token: 0x04000A25 RID: 2597
		step3,
		// Token: 0x04000A26 RID: 2598
		step4,
		// Token: 0x04000A27 RID: 2599
		step5
	}

	// Token: 0x02000235 RID: 565
	public enum Tool
	{
		// Token: 0x04000A29 RID: 2601
		addTool,
		// Token: 0x04000A2A RID: 2602
		moveTool,
		// Token: 0x04000A2B RID: 2603
		rotationTool,
		// Token: 0x04000A2C RID: 2604
		deleteTool,
		// Token: 0x04000A2D RID: 2605
		modifierTool,
		// Token: 0x04000A2E RID: 2606
		scaleTool,
		// Token: 0x04000A2F RID: 2607
		eventTool,
		// Token: 0x04000A30 RID: 2608
		propertyTool
	}

	// Token: 0x02000236 RID: 566
	public class Map
	{
		// Token: 0x06000D95 RID: 3477 RVA: 0x00057E5C File Offset: 0x0005605C
		public levelEditorManager.Block getTileAt(Transform _pos)
		{
			Vector3 position = _pos.position;
			foreach (levelEditorManager.Block block in this.tiles)
			{
				if (block.go.Equals(_pos.gameObject))
				{
					return block;
				}
			}
			Debug.LogError("Cannot Find Block! " + _pos.name, _pos);
			return null;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00057EF8 File Offset: 0x000560F8
		public levelEditorManager.Block GetBlockFromIndex(int index)
		{
			foreach (levelEditorManager.Block block in this.tiles)
			{
				if (block.Index == index)
				{
					return block;
				}
			}
			Debug.LogError("Cannot frinf block with index: " + index);
			return null;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00057F84 File Offset: 0x00056184
		public int GetIndexOfBlockByName(string name)
		{
			foreach (levelEditorManager.Block block in this.tiles)
			{
				if (block.id.Split(new char[]
				{
					'_'
				}).Length > 1 && block.id.Split(new char[]
				{
					'_'
				})[1] == name)
				{
					return block.Index;
				}
			}
			throw new Exception("Cannot find block with name: " + name);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00058044 File Offset: 0x00056244
		public void setTile(levelEditorManager.Block tile)
		{
			foreach (levelEditorManager.Block block in this.tiles)
			{
				if (block.go == tile.go)
				{
					UnityEngine.Object.Destroy(this.removeTile(block.go).go);
					break;
				}
			}
			if (this.tiles.Count == 0)
			{
				tile.Index = 0;
			}
			else
			{
				int num = 0;
				for (int i = 0; i < this.tiles.Count; i++)
				{
					if (this.tiles[i].Index == num)
					{
						num++;
						i = 0;
					}
				}
				tile.Index = num;
			}
			this.tiles.Add(tile);
			if (tile.go.transform.parent == null)
			{
				tile.go.transform.SetParent(levelEditorManager.Instance().mapBase.transform);
			}
			levelEditorManager.Instance().updateCurrentBlockCount();
			if (tile.type == levelEditorManager.blockType.btruckblock)
			{
				tile.SetColorCode(levelEditorManager.Instance().TruckColorCoding);
			}
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x000581A0 File Offset: 0x000563A0
		public levelEditorManager.Block removeTile(GameObject _pos)
		{
			foreach (levelEditorManager.Block block in this.tiles)
			{
				if (block.go == _pos)
				{
					this.tiles.Remove(block);
					levelEditorManager.Instance().updateCurrentBlockCount();
					return block;
				}
			}
			return null;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00058234 File Offset: 0x00056434
		public void removeTile(levelEditorManager.Block _block)
		{
			if (!this.tiles.Remove(_block))
			{
				Debug.LogError("Block was not found, " + _block.id, _block.go);
				if (_block.go)
				{
					UnityEngine.Object.Destroy(_block.go);
				}
			}
			levelEditorManager.Instance().updateCurrentBlockCount();
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00058294 File Offset: 0x00056494
		public void setSettings(string[] _settings)
		{
			if (this.SameAsDefaultSettings(_settings))
			{
				this.settings = null;
				return;
			}
			if (this.settings == null)
			{
				Debug.Log("Brand NEw Settings!");
				this.settings = new string[Enum.GetNames(typeof(levelEditorManager.settings)).Length];
			}
			for (int i = 0; i < _settings.Length; i++)
			{
				this.changeSetting(i, _settings[i]);
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00058304 File Offset: 0x00056504
		private bool SameAsDefaultSettings(string[] newSettings)
		{
			string[] defaultSettings = settingsPanelOnSet.defaultSettings;
			return float.Parse(newSettings[0]) == float.Parse(defaultSettings[0]) && int.Parse(newSettings[1]) == int.Parse(defaultSettings[1]) && int.Parse(newSettings[2]) == int.Parse(defaultSettings[2]) && int.Parse(newSettings[3]) == int.Parse(defaultSettings[3]) && float.Parse(newSettings[10]) == float.Parse(defaultSettings[10]) && float.Parse(newSettings[9]) == float.Parse(defaultSettings[9]) && int.Parse(newSettings[11]) == int.Parse(defaultSettings[11]) && int.Parse(newSettings[12]) == int.Parse(defaultSettings[12]) && float.Parse(newSettings[5]) == float.Parse(defaultSettings[5]) && !(newSettings[4] != defaultSettings[4]);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00058404 File Offset: 0x00056604
		public void setCustomSkyBox(string[] _settings)
		{
			this.customSkyBoxSettings = _settings;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00058410 File Offset: 0x00056610
		public void changeSetting(int index, string value)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Changin: ",
				(levelEditorManager.settings)index,
				" From: ",
				this.settings[index],
				" To: ",
				value
			}));
			this.settings[index] = value;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00058468 File Offset: 0x00056668
		public string[] getSettings()
		{
			return (this.settings != null) ? this.settings : settingsPanelOnSet.defaultSettings;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00058488 File Offset: 0x00056688
		public string[] getCustomSkyBoxSettings()
		{
			return (this.customSkyBoxSettings != null) ? this.customSkyBoxSettings : new string[0];
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000584A8 File Offset: 0x000566A8
		public void Init()
		{
			Debug.Log("Initializing Map");
			levelEditorManager.Instance().ApplySettingsToScene();
			foreach (levelEditorManager.Block block in this.tiles)
			{
				if (block.type == levelEditorManager.blockType.broadsblock)
				{
					foreach (RoadHandle roadHandle in block.go.GetComponentsInChildren<RoadHandle>())
					{
						roadHandle.Initialize();
					}
				}
			}
		}

		// Token: 0x04000A31 RID: 2609
		public List<levelEditorManager.Block> tiles = new List<levelEditorManager.Block>();

		// Token: 0x04000A32 RID: 2610
		public string[] settings;

		// Token: 0x04000A33 RID: 2611
		public string[] customSkyBoxSettings;
	}

	// Token: 0x02000237 RID: 567
	[Serializable]
	public class CopyBlock
	{
		// Token: 0x04000A34 RID: 2612
		public ObjectParameterContainer[] objsParams;

		// Token: 0x04000A35 RID: 2613
		public GameObject go;

		// Token: 0x04000A36 RID: 2614
		public EventInfo[] eventInfo;

		// Token: 0x04000A37 RID: 2615
		public ObjectBehaviourContainer[] objBehaviours;
	}

	// Token: 0x02000238 RID: 568
	public class Block
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x00058560 File Offset: 0x00056760
		public Block()
		{
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00058568 File Offset: 0x00056768
		public Block(levelEditorManager.Block b)
		{
			this.x = b.x;
			this.y = b.y;
			this.z = b.z;
			this.scalex = b.scalex;
			this.scaley = b.scaley;
			this.scalez = b.scalez;
			this.rotx = b.rotx;
			this.roty = b.roty;
			this.rotz = b.rotz;
			this.type = b.type;
			this.waypointType = b.waypointType;
			this.wayPointIndex = b.wayPointIndex;
			this.id = b.id;
			this.Index = b.Index;
			this.objsParams = b.objsParams;
			this.Behaviours = b.Behaviours;
			this.go = UnityEngine.Object.Instantiate<GameObject>(b.go);
			this.go.name = this.go.name.Split(new char[]
			{
				'('
			})[0];
			this.go.SetActive(false);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00058680 File Offset: 0x00056880
		public Block(levelEditorManager.Block b, int check)
		{
			this.x = b.x;
			this.y = b.y;
			this.z = b.z;
			this.scalex = b.scalex;
			this.scaley = b.scaley;
			this.scalez = b.scalez;
			this.rotx = b.rotx;
			this.roty = b.roty;
			this.rotz = b.rotz;
			this.type = b.type;
			this.waypointType = b.waypointType;
			this.wayPointIndex = b.wayPointIndex;
			this.id = b.id;
			this.Index = b.Index;
			this.objsParams = b.objsParams;
			this.Behaviours = b.Behaviours;
			this.go = b.go;
			this.go.name = this.go.name.Split(new char[]
			{
				'('
			})[0];
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00058788 File Offset: 0x00056988
		public Block(levelEditorManager.Block b, bool cutGameObject)
		{
			this.x = b.x;
			this.y = b.y;
			this.z = b.z;
			this.scalex = b.scalex;
			this.scaley = b.scaley;
			this.scalez = b.scalez;
			this.rotx = b.rotx;
			this.roty = b.roty;
			this.rotz = b.rotz;
			this.type = b.type;
			this.waypointType = b.waypointType;
			this.wayPointIndex = b.wayPointIndex;
			this.id = b.id;
			this.Index = b.Index;
			this.Behaviours = b.Behaviours;
			this.objsParams = b.objsParams;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0005885C File Offset: 0x00056A5C
		[JsonIgnore]
		public string ObjectName
		{
			get
			{
				return this.id.Split(new char[]
				{
					'_'
				})[0];
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00058878 File Offset: 0x00056A78
		public void setBehaviours(ObjectBehaviourContainer[] behaviours)
		{
			Debug.Log("Setting Behaviours for: " + this.id);
			if (behaviours != null)
			{
				for (int i = 0; i < behaviours.Length; i++)
				{
					Debug.Log(string.Concat(new object[]
					{
						"Behaviour: ",
						i,
						"  ",
						behaviours[i].BehaviourType
					}));
				}
			}
			this.Behaviours = behaviours;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x000588F4 File Offset: 0x00056AF4
		public void setObjectEvents(EventInfo[] events)
		{
			Debug.Log("Setting Events for: " + this.id);
			if (events != null)
			{
				foreach (EventInfo eventInfo in events)
				{
					Debug.Log(eventInfo.EventType.ToString());
				}
			}
			this.EventInfo = events;
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00058954 File Offset: 0x00056B54
		public void resetObjParams()
		{
			this.setObjParams(this.objsParams, false, true);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00058964 File Offset: 0x00056B64
		public void SetColorCode(bool ColorCode)
		{
			if (this.type != levelEditorManager.blockType.btruckblock)
			{
				Debug.LogError("Setting Color On NoTruck: " + this.id);
			}
			int num;
			if (this.objsParams == null)
			{
				num = 0;
			}
			else
			{
				num = int.Parse(Array.Find<ObjectParameterContainer>(this.objsParams, (ObjectParameterContainer index) => index.getName() == "WaypointType").getValue());
			}
			int i = num;
			foreach (Renderer renderer in this.go.GetComponentsInChildren<Renderer>())
			{
				renderer.material.color = ((!ColorCode) ? Color.white : levelEditorManager.Instance().getColorAt(i));
			}
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00058A20 File Offset: 0x00056C20
		public void setObjParams(ObjectParameterContainer[] _params, bool temporary = false, bool changeObject = true)
		{
			if (!temporary)
			{
				this.objsParams = _params;
			}
			if (this.type == levelEditorManager.blockType.btruckblock)
			{
				int num;
				if (this.objsParams == null)
				{
					num = 0;
				}
				else
				{
					num = int.Parse(Array.Find<ObjectParameterContainer>(this.objsParams, (ObjectParameterContainer index) => index.getName() == "WaypointType").getValue());
				}
				int num2 = num;
				if (this.waypointType != num2 + 1)
				{
					TutorialHandler.Instance.SpecialEvents[12].Clicked();
				}
				if (!temporary)
				{
					this.waypointType = num2 + 1;
				}
				if (changeObject && levelEditorManager.Instance().TruckColorCoding)
				{
					foreach (Renderer renderer in this.go.GetComponentsInChildren<Renderer>())
					{
						renderer.material.color = levelEditorManager.Instance().getColorAt(num2);
					}
				}
			}
			if (_params != null)
			{
				foreach (ObjectParameterContainer objectParameterContainer in _params)
				{
					if (objectParameterContainer.getName() == "Name" && !string.IsNullOrEmpty(objectParameterContainer.getValue()))
					{
						this.id = this.id.Split(new char[]
						{
							'_'
						})[0] + "_" + objectParameterContainer.getValue();
						Debug.Log("New custom name: " + this.id);
					}
				}
				if (changeObject)
				{
					if (this.go.GetComponent<ModifierRecieverBase>())
					{
						this.go.GetComponent<ModifierRecieverBase>().sendInfo(_params, temporary);
					}
					else
					{
						Debug.Log("Cannot find Modifier Reciever for: " + this.id);
					}
				}
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00058BE4 File Offset: 0x00056DE4
		public void Update()
		{
			Transform transform = (!(this.go.GetComponentInChildren<useThisObject>() == null)) ? this.go.GetComponentInChildren<useThisObject>().transform : this.go.transform;
			if (this.x != transform.position.x || this.y != transform.position.y || this.z != transform.position.z || this.scalex != transform.lossyScale.x || this.scaley != transform.lossyScale.y || this.scalez != transform.lossyScale.z || this.rotx != transform.rotation.eulerAngles.x || this.roty != transform.rotation.eulerAngles.y || this.rotz != transform.rotation.eulerAngles.z)
			{
				levelEditorManager.Instance().hasChangedAnything = true;
			}
			this.x = transform.position.x;
			this.y = transform.position.y;
			this.z = transform.position.z;
			this.scalex = transform.lossyScale.x;
			this.scaley = transform.lossyScale.y;
			this.scalez = transform.lossyScale.z;
			this.rotx = transform.rotation.eulerAngles.x;
			this.roty = transform.rotation.eulerAngles.y;
			this.rotz = transform.rotation.eulerAngles.z;
		}

		// Token: 0x04000A38 RID: 2616
		public float x;

		// Token: 0x04000A39 RID: 2617
		public float y;

		// Token: 0x04000A3A RID: 2618
		public float z;

		// Token: 0x04000A3B RID: 2619
		public float scalex;

		// Token: 0x04000A3C RID: 2620
		public float scaley;

		// Token: 0x04000A3D RID: 2621
		public float scalez;

		// Token: 0x04000A3E RID: 2622
		public float rotx;

		// Token: 0x04000A3F RID: 2623
		public float roty;

		// Token: 0x04000A40 RID: 2624
		public float rotz;

		// Token: 0x04000A41 RID: 2625
		public levelEditorManager.blockType type;

		// Token: 0x04000A42 RID: 2626
		public int waypointType;

		// Token: 0x04000A43 RID: 2627
		public int wayPointIndex;

		// Token: 0x04000A44 RID: 2628
		public string id;

		// Token: 0x04000A45 RID: 2629
		public int Index;

		// Token: 0x04000A46 RID: 2630
		public ObjectParameterContainer[] objsParams;

		// Token: 0x04000A47 RID: 2631
		public ObjectBehaviourContainer[] Behaviours;

		// Token: 0x04000A48 RID: 2632
		public EventInfo[] EventInfo;

		// Token: 0x04000A49 RID: 2633
		[JsonIgnore]
		public GameObject go;
	}

	// Token: 0x02000239 RID: 569
	public class delayParameters
	{
		// Token: 0x06000DB0 RID: 3504 RVA: 0x00058E38 File Offset: 0x00057038
		public delayParameters(Transform obj, ObjectParameterContainer[] parameters)
		{
			this._obj = obj;
			this._parameters = parameters;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00058E50 File Offset: 0x00057050
		public Transform getObj()
		{
			return this._obj;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00058E58 File Offset: 0x00057058
		public ObjectParameterContainer[] getParameters()
		{
			return this._parameters;
		}

		// Token: 0x04000A4C RID: 2636
		private Transform _obj;

		// Token: 0x04000A4D RID: 2637
		private ObjectParameterContainer[] _parameters;
	}
}
