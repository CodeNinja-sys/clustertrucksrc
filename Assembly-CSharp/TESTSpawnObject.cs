using System;
using UnityEngine;

// Token: 0x0200025A RID: 602
public class TESTSpawnObject : MonoBehaviour
{
	// Token: 0x06000EA7 RID: 3751 RVA: 0x0005E924 File Offset: 0x0005CB24
	private void Start()
	{
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x0005E928 File Offset: 0x0005CB28
	private void Update()
	{
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x0005E92C File Offset: 0x0005CB2C
	public void getInfo(getModifierOptions.Parameter[] _params)
	{
		Debug.Log("Params Recieved");
		Debug.Log(TESTSpawnObject.TestObjectParamTypes.boolean.ToString() + "  : " + bool.Parse(_params[0].getValue()));
	}

	// Token: 0x0200025B RID: 603
	public enum TestObjectParamTypes
	{
		// Token: 0x04000B34 RID: 2868
		boolean,
		// Token: 0x04000B35 RID: 2869
		type,
		// Token: 0x04000B36 RID: 2870
		length
	}
}
