using System;
using UnityEngine;

// Token: 0x0200020B RID: 523
public abstract class ModifierRecieverBase : MonoBehaviour
{
	// Token: 0x06000C55 RID: 3157
	public abstract void sendInfo(ObjectParameterContainer[] _params, bool temporary = false);
}
