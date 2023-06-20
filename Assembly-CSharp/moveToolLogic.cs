using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200023C RID: 572
public class moveToolLogic : MonoBehaviour
{
	// Token: 0x06000DCA RID: 3530 RVA: 0x00059AA4 File Offset: 0x00057CA4
	public void Init()
	{
		Debug.Log("HEEEJ!");
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x00059AB0 File Offset: 0x00057CB0
	private void OnEnable()
	{
		this._targetRef = levelEditorManager.Instance().getTargetedGameObject();
		if (this._targetRef == null)
		{
			Debug.Log(1);
			this.X_Input.text = "Nan";
			this.Y_Input.text = "Nan";
			this.Z_Input.text = "Nan";
			return;
		}
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x00059B1C File Offset: 0x00057D1C
	private void Update()
	{
		if (!this.checkForinput())
		{
			if (levelEditorManager.Instance().getTargetedGameObject() == null)
			{
				this.X_Input.text = "Nan";
				this.Y_Input.text = "Nan";
				this.Z_Input.text = "Nan";
				return;
			}
			this._targetRef = levelEditorManager.Instance().getTargetedGameObject();
			this.X_Input.text = this._targetRef.position.x.ToString();
			this.Y_Input.text = this._targetRef.position.y.ToString();
			this.Z_Input.text = this._targetRef.position.z.ToString();
		}
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x00059BF8 File Offset: 0x00057DF8
	private bool checkForinput()
	{
		return !(EventSystem.current.currentSelectedGameObject == null) && (EventSystem.current.currentSelectedGameObject.Equals(this.X_Input.gameObject) || EventSystem.current.currentSelectedGameObject.Equals(this.Y_Input.gameObject) || EventSystem.current.currentSelectedGameObject.Equals(this.Z_Input.gameObject));
	}

	// Token: 0x04000A5B RID: 2651
	public InputField X_Input;

	// Token: 0x04000A5C RID: 2652
	public InputField Y_Input;

	// Token: 0x04000A5D RID: 2653
	public InputField Z_Input;

	// Token: 0x04000A5E RID: 2654
	public Text X_Text;

	// Token: 0x04000A5F RID: 2655
	public Text Y_Text;

	// Token: 0x04000A60 RID: 2656
	public Text Z_Text;

	// Token: 0x04000A61 RID: 2657
	private bool _typing;

	// Token: 0x04000A62 RID: 2658
	private Transform _targetRef;
}
