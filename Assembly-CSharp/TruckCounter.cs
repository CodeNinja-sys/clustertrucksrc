using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002DF RID: 735
public class TruckCounter : MonoBehaviour
{
	// Token: 0x0600114A RID: 4426 RVA: 0x000705EC File Offset: 0x0006E7EC
	private void Start()
	{
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x000705FC File Offset: 0x0006E7FC
	private void Update()
	{
		this.text.text = "TRUCKS: " + car.numberOfTrucks;
	}

	// Token: 0x04000E71 RID: 3697
	private Text text;
}
