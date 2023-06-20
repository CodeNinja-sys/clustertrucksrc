using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class cameraEffects : MonoBehaviour
{
	public float shake;
	public float amount;
	public float decrease;
	public SENaturalBloomAndDirtyLens myBloom;
	public SESSAO ssao;
	public ScreenOverlay blueOverlay;
	public ScreenOverlay orangeOverlay;
	public LerpableBlur blur;
	public Transform uiCam;
}
