using UnityEngine;
using UnityEngine.UI;

public class abilityMenu : MonoBehaviour
{
	public Text nameText;
	public Text infoText;
	public UI_Slide myUI;
	public abilityInfo selectedButton;
	public abilityInfo pressedButtonController;
	public Transform usedM;
	public Transform usedU;
	public Image selector;
	public Image selector2;
	public AudioSource au;
	public AudioClip unlock;
	public AudioClip nope;
	public AudioClip equip;
	public Transform movementParent;
	public Transform utilityParent;
	public Button UnlockButton;
	[SerializeField]
	private Transform MovementGrid;
	[SerializeField]
	private Transform UtilityGrid;
}
