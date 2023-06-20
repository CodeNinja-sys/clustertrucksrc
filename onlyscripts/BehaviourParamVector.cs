using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020A RID: 522
public class BehaviourParamVector : BehaviourParamCellSetBase
{
	// Token: 0x06000C52 RID: 3154 RVA: 0x0004CB64 File Offset: 0x0004AD64
	public override void Init(string description, string[] Parameters)
	{
		this._descriptionText.text = description;
		if (Parameters == null)
		{
			this._inputFieldX.text = "0";
			this._inputFieldY.text = "0";
			this._inputFieldZ.text = "0";
			return;
		}
		if (Parameters.Length != 0)
		{
			Vector3 vector = Parameters[0].ToVector3();
			this._inputFieldX.text = vector.x.ToString();
			this._inputFieldY.text = vector.y.ToString();
			this._inputFieldZ.text = vector.z.ToString();
		}
		else
		{
			this._inputFieldX.text = "0";
			this._inputFieldY.text = "0";
			this._inputFieldZ.text = "0";
		}
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x0004CC40 File Offset: 0x0004AE40
	public override string getValue()
	{
		Vector3 vector = new Vector3((float)int.Parse(this._inputFieldX.text), (float)int.Parse(this._inputFieldY.text), (float)int.Parse(this._inputFieldZ.text));
		string text = vector.ToString();
		Debug.Log("S: " + text);
		Debug.Log("Vec: " + text.ToVector3());
		return text;
	}

	// Token: 0x040008E1 RID: 2273
	[SerializeField]
	private Text _descriptionText;

	// Token: 0x040008E2 RID: 2274
	[SerializeField]
	private InputField _inputFieldX;

	// Token: 0x040008E3 RID: 2275
	[SerializeField]
	private InputField _inputFieldY;

	// Token: 0x040008E4 RID: 2276
	[SerializeField]
	private InputField _inputFieldZ;
}
