using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class InGameUI : MonoBehaviour
{

	private bool gamePause;

	[Header("Menu gameObject")]
	[SerializeField] private GameObject inGameUI;
	[SerializeField] private GameObject pauseUI;
	[SerializeField] private GameObject endLevelUI;

	[Header("TxtComponents")]
    [SerializeField] private TextMeshProUGUI txtTime;
    [SerializeField] private TextMeshProUGUI crrCoinAmount;

	[SerializeField] private TextMeshProUGUI endTimerTxt;
	[SerializeField] private TextMeshProUGUI endBestTimeTxt;
	[SerializeField] private TextMeshProUGUI endGoldtxt;
	private void Start()
	{
		GameManager.instance.lvNum = SceneManager.GetActiveScene().buildIndex;
		Time.timeScale = 1;
		SwitchMenu(inGameUI);
	}
	
	// Update is called once per frame
	void Update()
	{
		UpdateIngame();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
			ChecknotPause();
        }

    }
	private bool ChecknotPause() 
	{
		if (!gamePause)
		{
			gamePause = true;
			Time.timeScale = 0;
			SwitchMenu(pauseUI);
			return true;
		}
		else
		{
			gamePause = false;
			Time.timeScale = 1;
			SwitchMenu(inGameUI);
			return false;
		}
    }
	public void OnLvFinished()
	{
		endGoldtxt.text ="Golds: " + PlayerManagement.instance.Coin.ToString();
		endTimerTxt.text = "Your Time: " + GameManager.instance.time.ToString("00") + " s";
		endBestTimeTxt.text = "Best Time: " + PlayerPrefs.GetFloat("Map" + GameManager.instance.lvNum + "BestTime",999).ToString("00") + " s"; 
		SwitchMenu(endLevelUI);
	}

	private void UpdateIngame()
	{
		txtTime.text = "Time: " + GameManager.instance.time.ToString("00") + " s";
		crrCoinAmount.text = PlayerManagement.instance.Coin.ToString();
	}

	public void SwitchMenu(GameObject uiMenu)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);

		}
		uiMenu.SetActive(true);
	}
	public void LoadMainMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("MainMenu");
	}
	public void ReLoadCrrLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LoadNextLV() 
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
