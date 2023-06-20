using UnityEngine;

public class RecordingDataTransmitterTransform : RecordingDataTransmitterBase
{
	[SerializeField]
	private MonoBehaviour[] mScriptDisabledAtPlay;
	[SerializeField]
	protected float mMinMovementForRecording;
	[SerializeField]
	protected float mMinRotationForRecording;
	[SerializeField]
	private bool mPrintDebugData;
}
