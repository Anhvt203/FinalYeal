using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Transform[] coinPosition;
    [SerializeField] private GameObject coinPrefab;


    void Start()
    {
        coinPosition = GetComponentsInChildren<Transform>();
		int lvNum = GameManager.instance.lvNum;

		for (int i = 1; i < coinPosition.Length; i++)
        {
			GameObject newCoin = Instantiate(coinPrefab, coinPosition[i]);

			coinPosition[i].GetComponent<SpriteRenderer>().sprite = null;
		}
		int totalCoin = PlayerPrefs.GetInt("Map" + lvNum + "TotalCoin");

		if (totalCoin != coinPosition.Length - 1)
		{
			PlayerPrefs.SetInt("Map" + lvNum + "TotalCoin", coinPosition.Length - 1);
		}
	}
}
