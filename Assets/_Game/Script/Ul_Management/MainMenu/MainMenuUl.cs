using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUl : MonoBehaviour
{
	[SerializeField] private GameObject continueBtn;
	[SerializeField] private VolumGame[] volumGame;
	private void Start()
	{
		bool showBtn = PlayerPrefs.GetInt("Map" +2+ "Unlocked") == 1;
		continueBtn.SetActive(showBtn);

        for (int i = 0; i < volumGame.Length; i++)
        {
			volumGame[i].GetComponent<VolumGame>().Soundsetting();

		}
    }
	public void SwitchMenu(GameObject uiMenu) 
	{
        for (int i = 0; i < transform.childCount; i++)
        {
        transform.GetChild(i).gameObject.SetActive(false);

		}
		AudioManager.instance.PlaySFx(4);
		uiMenu.SetActive(true);

	}
}
