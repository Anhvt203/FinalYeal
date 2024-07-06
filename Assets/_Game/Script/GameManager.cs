using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[Header("Timer inf")]
	public float time;
	public bool startTime;

	[Header("Level inf")]
	public int lvNum;

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	private void Start()
	{
		Debug.Log(PlayerPrefs.GetFloat("Map" + lvNum + "Time")); 
	}
	private void Update()
	{
		if (startTime)
		{
			time += Time.deltaTime;
		}
	}
	public void SaveTime()
	{
		startTime = false;

		float lastTime = PlayerPrefs.GetFloat("Map" + lvNum + "BestTime", 999);

		if(time < lastTime)
		{
			PlayerPrefs.SetFloat("Map" + lvNum + "BestTime", time);
		}
		time = 0;

	}
	public void SaveCoin()
	{
		int totalCoin = PlayerPrefs.GetInt("TotalCointCollected");

		int newTotalCoin = totalCoin + PlayerManagement.instance.Coin;

		PlayerPrefs.SetInt("TotalCointCollected", newTotalCoin);
		PlayerPrefs.SetInt("Map" + lvNum + "CointCollected", PlayerManagement.instance.Coin);

		PlayerManagement.instance.Coin = 0;
	}

	public void SaveMap()
	{
		int nextMap = lvNum + 1;
		PlayerPrefs.SetInt("Map" + nextMap + "Unlocked",1);
	}

}
