using System;
using UnityEngine;

// Token: 0x020002F8 RID: 760
public class objectHolder : MonoBehaviour
{
	// Token: 0x060011E8 RID: 4584 RVA: 0x00072FFC File Offset: 0x000711FC
	private void Start()
	{
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x00073000 File Offset: 0x00071200
	private void Update()
	{
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x00073004 File Offset: 0x00071204
	public void setObject(UnityEngine.Object ob)
	{
		this._obj = ob;
	}

	// Token: 0x060011EB RID: 4587 RVA: 0x00073010 File Offset: 0x00071210
	public UnityEngine.Object getObject()
	{
		if (this._obj != null)
		{
			return this._obj;
		}
		throw new Exception("No Object Assigned!");
	}

	// Token: 0x04000F08 RID: 3848
	private UnityEngine.Object _obj;
}
