using UnityEngine;
using UnityEngine.UI;

public class EventToolLogic : MonoBehaviour
{
	public Scrollbar EventScrollBar;
	public Transform Grid;
	public Transform EventParameterGrid;
	public GameObject Event_cell;
	public Dropdown EventType_Dropdown;
	public Dropdown EventToTrigger_Dropdown;
	public Dropdown Entity_DropDown;
	public InputField delay_Inputfield;
	public Toggle fireOnlyOnce_Toggle;
	[SerializeField]
	private GameObject InputCell;
	[SerializeField]
	private GameObject SliderCell;
	[SerializeField]
	private GameObject VectorCell;
	[SerializeField]
	private GameObject DropdownCell;
}
