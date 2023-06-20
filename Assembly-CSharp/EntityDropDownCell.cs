using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FC RID: 508
public class EntityDropDownCell : MonoBehaviour
{
	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0004ACB8 File Offset: 0x00048EB8
	public levelEditorManager.Block Block
	{
		get
		{
			return this._block;
		}
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x0004ACC0 File Offset: 0x00048EC0
	private void Start()
	{
		Debug.Log(this.CellLabel.text);
		int num = int.Parse(this.CellLabel.text.Split(new char[]
		{
			'_'
		})[1]);
		this.CellLabel.text = this.CellLabel.text.Split(new char[]
		{
			'_'
		})[0];
		this._block = ((num != -1) ? levelEditorManager.Instance().getCurrentMap.GetBlockFromIndex(num) : new levelEditorManager.Block
		{
			Index = -1
		});
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x0004AD58 File Offset: 0x00048F58
	private void Update()
	{
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x0004AD5C File Offset: 0x00048F5C
	public void OnClick()
	{
		EventToolLogic.Instance.ChangeSelectedEntity(this._block);
	}

	// Token: 0x0400089F RID: 2207
	public Text CellLabel;

	// Token: 0x040008A0 RID: 2208
	private levelEditorManager.Block _block;
}
