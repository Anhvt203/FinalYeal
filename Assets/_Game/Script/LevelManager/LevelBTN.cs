using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelBTN : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI levelName;
	[SerializeField] private TextMeshProUGUI bestTime;
	[SerializeField] private TextMeshProUGUI collectedCoin;
	[SerializeField] private TextMeshProUGUI totalCoin;

	public void UpdateTextInfo(string sceneName,int lvNumber)
	{
		levelName.text = "Map " + sceneName;
		bestTime.text = "Best Time: " + PlayerPrefs.GetFloat("Map" + lvNumber + "BestTime", 999).ToString("00") + " s";
		collectedCoin.text = PlayerPrefs.GetInt("Map" + lvNumber + "CointCollected", PlayerManagement.instance.Coin).ToString();
		totalCoin.text = "/ " + PlayerPrefs.GetInt("Map" + lvNumber + "TotalCoin").ToString();
	}

}
