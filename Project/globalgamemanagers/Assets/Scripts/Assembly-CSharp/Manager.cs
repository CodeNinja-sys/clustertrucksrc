using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
	public bool ControllerSet;
	public bool ShowbuildSet;
	public string SkipMap_textController;
	public string SkipMap_text;
	public int[] MapcycleArrayPublic;
	public bool usingMapcyclePublic;
	public UI_Slide_General UI_Slide_Small;
	public UI_Slide_General UI_Slide_Big;
	public GameObject MainCanvas;
	public GameObject MenuCam;
	public GameObject uiCam;
	public GameObject levelSelect;
	public menuBlack menuBlack;
	public AudioSource windAU;
	public SENaturalBloomAndDirtyLens menuBloom;
	public menuBar myMenuBar;
	public GameObject emptyObject;
	public GameObject thanksScreen;
	public Text playThroughText;
	public Text endGameText;
	public musicManager musicMan;
}
