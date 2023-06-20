using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002F4 RID: 756
public class levelButton : MonoBehaviour
{
	// Token: 0x060011D5 RID: 4565 RVA: 0x00072C74 File Offset: 0x00070E74
	private void Start()
	{
		this.text = base.GetComponentInChildren<Text>();
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x00072C84 File Offset: 0x00070E84
	private void Update()
	{
		this.text.text = base.gameObject.name;
	}

	// Token: 0x04000EF3 RID: 3827
	public LevelSeletHandler handler;

	// Token: 0x04000EF4 RID: 3828
	private Text text;

	// Token: 0x04000EF5 RID: 3829
	private float counter;
}
