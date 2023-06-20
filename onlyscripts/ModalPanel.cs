using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000247 RID: 583
public class ModalPanel : MonoBehaviour
{
	// Token: 0x06000E46 RID: 3654 RVA: 0x0005D0F4 File Offset: 0x0005B2F4
	public static ModalPanel Instance()
	{
		if (!ModalPanel.modalPanel)
		{
			ModalPanel.modalPanel = (UnityEngine.Object.FindObjectOfType(typeof(ModalPanel)) as ModalPanel);
			if (!ModalPanel.modalPanel)
			{
				Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
			}
		}
		return ModalPanel.modalPanel;
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x0005D148 File Offset: 0x0005B348
	public void Choice(string question, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent, string yesButtonText = "Yes", string noButtonText = "No", string cancelButtonText = "Cancel")
	{
		this.modalPanelObject.SetActive(true);
		this.yesButton.onClick.RemoveAllListeners();
		this.yesButton.onClick.AddListener(yesEvent);
		this.yesButton.GetComponentInChildren<Text>().text = yesButtonText;
		this.noButton.onClick.RemoveAllListeners();
		this.noButton.onClick.AddListener(noEvent);
		this.noButton.GetComponentInChildren<Text>().text = noButtonText;
		this.cancelButton.onClick.RemoveAllListeners();
		this.cancelButton.onClick.AddListener(cancelEvent);
		this.cancelButton.GetComponentInChildren<Text>().text = cancelButtonText;
		this.question.text = question;
		this.yesButton.gameObject.SetActive(true);
		this.noButton.gameObject.SetActive(true);
		this.cancelButton.gameObject.SetActive(true);
		this.inputField.SetActive(false);
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x0005D248 File Offset: 0x0005B448
	public void Choice(string question, UnityAction cancelEvent, string cancelButtonText = "Cancel")
	{
		this.modalPanelObject.SetActive(true);
		this.cancelButton.onClick.RemoveAllListeners();
		this.cancelButton.onClick.AddListener(cancelEvent);
		this.cancelButton.GetComponentInChildren<Text>().text = cancelButtonText;
		this.question.text = question;
		this.yesButton.gameObject.SetActive(false);
		this.noButton.gameObject.SetActive(false);
		this.cancelButton.gameObject.SetActive(true);
		this.inputField.SetActive(false);
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x0005D2E0 File Offset: 0x0005B4E0
	public void Choice(string question, UnityAction yesEvent, UnityAction noEvent, string yesButtonText = "Yes", string noButtonText = "No")
	{
		this.modalPanelObject.SetActive(true);
		this.yesButton.onClick.RemoveAllListeners();
		this.yesButton.onClick.AddListener(yesEvent);
		this.yesButton.GetComponentInChildren<Text>().text = yesButtonText;
		this.noButton.onClick.RemoveAllListeners();
		this.noButton.onClick.AddListener(noEvent);
		this.noButton.GetComponentInChildren<Text>().text = noButtonText;
		this.question.text = question;
		this.yesButton.gameObject.SetActive(true);
		this.noButton.gameObject.SetActive(true);
		this.cancelButton.gameObject.SetActive(false);
		this.inputField.SetActive(false);
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x0005D3AC File Offset: 0x0005B5AC
	public void Choice(string question, UnityAction yesEvent, UnityAction noEvent, bool field, string yesButtonText = "Yes", string noButtonText = "No")
	{
		this.modalPanelObject.SetActive(true);
		this.inputField.SetActive(field);
		this.yesButton.onClick.RemoveAllListeners();
		this.yesButton.onClick.AddListener(yesEvent);
		this.yesButton.GetComponentInChildren<Text>().text = yesButtonText;
		this.noButton.onClick.RemoveAllListeners();
		this.noButton.onClick.AddListener(noEvent);
		this.noButton.GetComponentInChildren<Text>().text = noButtonText;
		this.question.text = question;
		this.yesButton.gameObject.SetActive(true);
		this.noButton.gameObject.SetActive(true);
		this.cancelButton.gameObject.SetActive(false);
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x0005D478 File Offset: 0x0005B678
	public void ClosePanel()
	{
		this.modalPanelObject.SetActive(false);
	}

	// Token: 0x04000AE2 RID: 2786
	public Text question;

	// Token: 0x04000AE3 RID: 2787
	public Button yesButton;

	// Token: 0x04000AE4 RID: 2788
	public Button noButton;

	// Token: 0x04000AE5 RID: 2789
	public Button cancelButton;

	// Token: 0x04000AE6 RID: 2790
	public GameObject inputField;

	// Token: 0x04000AE7 RID: 2791
	public GameObject modalPanelObject;

	// Token: 0x04000AE8 RID: 2792
	private static ModalPanel modalPanel;
}
