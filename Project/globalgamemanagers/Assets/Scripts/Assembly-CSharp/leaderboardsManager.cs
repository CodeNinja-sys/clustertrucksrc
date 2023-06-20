using UnityEngine;
using UnityEngine.UI;

public class leaderboardsManager : MonoBehaviour
{
	public GameObject LBMapPickerGrid;
	public GameObject LeaderBoardGrid;
	public GameObject LBMapCellPrefab;
	public GameObject LeaderBoardCellPrefab;
	public GameObject[] tabs;
	public Dropdown sortDropdown;
	public Dropdown mapDropDown;
	public Animator anim;
	[SerializeField]
	private Toggle ShowFriendsToggle;
}
