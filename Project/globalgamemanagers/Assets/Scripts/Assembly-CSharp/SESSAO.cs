using UnityEngine;

public class SESSAO : MonoBehaviour
{
	public bool visualizeSSAO;
	public float radius;
	public float bias;
	public float bilateralDepthTolerance;
	public float zThickness;
	public float occlusionIntensity;
	public float sampleDistributionCurve;
	public float colorBleedAmount;
	public float brightnessThreshold;
	public float drawDistance;
	public float drawDistanceFadeSize;
	public bool reduceSelfBleeding;
	public bool useDownsampling;
	public bool halfSampling;
	public bool preserveDetails;
	public Camera attachedCamera;
}
