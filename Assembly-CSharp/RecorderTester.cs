using System;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class RecorderTester : MonoBehaviour
{
	// Token: 0x06000679 RID: 1657 RVA: 0x0002C42C File Offset: 0x0002A62C
	private void Start()
	{
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0002C430 File Offset: 0x0002A630
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			MonoBehaviour.print("Recorder: " + Singleton<DataRecorder>.Instance.P_RecordingState);
			Singleton<DataRecorder>.Instance.RegisterTransmitter(base.GetComponent<RecordingDataTransmitterRigid>());
		}
	}
}
