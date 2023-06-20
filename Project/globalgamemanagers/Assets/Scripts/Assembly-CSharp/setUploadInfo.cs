using UnityEngine;
using UnityEngine.UI;

public class setUploadInfo : MonoBehaviour
{
	public GameObject fileToUploadGrid;
	public GameObject uploadCellPrefab;
	public InputField _desc;
	public InputField _changeNotes;
	public InputField _title;
	public Dropdown _mapDropdown;
	public Toggle _newMapToggle;
	public Toggle _existingMapToggle;
	public UIblocker _UIBlocker;
}
