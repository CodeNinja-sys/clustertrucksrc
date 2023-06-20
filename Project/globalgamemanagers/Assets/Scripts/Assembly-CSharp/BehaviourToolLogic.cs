using UnityEngine;
using UnityEngine.UI;

public class BehaviourToolLogic : MonoBehaviour
{
	[SerializeField]
	private Button addButton;
	[SerializeField]
	private Button removeButton;
	public Transform HaveGrid;
	public Transform AvalibleGrid;
	public Transform ParamsGrid;
	public GameObject HaveCell;
	public GameObject AvalibleCell;
	[SerializeField]
	private GameObject InputCell;
	[SerializeField]
	private GameObject SliderCell;
	[SerializeField]
	private GameObject VectorCell;
	[SerializeField]
	private GameObject DropdownCell;
}
