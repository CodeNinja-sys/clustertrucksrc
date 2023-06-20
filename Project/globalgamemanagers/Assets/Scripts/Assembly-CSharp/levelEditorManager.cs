using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class levelEditorManager : MonoBehaviour
{
	[Serializable]
	public class CopyBlock
	{
		public GameObject go;
	}

	public GameObject HELPME;
	public GameObject HELPMEDIR;
	public Material[] SkyBoxes;
	public Material[] TerrainMaterials;
	public Material ghostMaterial;
	public Material GradientSkyBox;
	public Material HorizonSkyBox;
	public Material[] currentMaterial;
	public Text _currentObjectCountText;
	public Toggle _truckColorCodingToggle;
	public Button upload_confirmButton;
	public Button propertyToolButton;
	public InputField saveInputField;
	public LayerMask myMask;
	public LayerMask UIBlockMask;
	public LayerMask onlyUIMask;
	public LayerMask onlyTruckMask;
	public GameObject mapBase;
	public GameObject wayPointColors;
	public List<GameObject> truckFormations;
	[SerializeField]
	private List<levelEditorManager.CopyBlock> _currentCopyBlocks;
	public GameObject broadpointblock;
	public GameObject worldPlane;
	public GameObject realPlayer;
	public GameObject realTruck;
	public GameObject editorUI;
	public GameObject pathRendererHolderGameObject;
	public GameObject truckBrushSphere;
	[SerializeField]
	private GameObject roadPointDeleteSphere;
	[SerializeField]
	private GameObject defaultTruck;
	[SerializeField]
	private GameObject defaultPlayer;
	public GameObject blockMenu;
	public GameObject grid;
	public GameObject cellPrefab;
	public GameObject downloadCellPrefab;
	public GameObject loadPopUp;
	public GameObject step1;
	public GameObject step2;
	public GameObject step3;
	public GameObject downloadGrid;
	public GameObject downloadPopUp;
	public GameObject uploadInfoPopUp;
	public GameObject settingsInfoPopup;
	public GameObject proptertiesInfoPopup;
	public Projector mProjector;
	public GameObject targetedGobj;
	public GameObject goal;
	public GameObject playerSpawn;
	public Color[] colors;
	public Button addTool_Button;
	public Button moveTool_Button;
	public Button rotationTool_Button;
	public Dropdown _blockType_Dropdown;
}
