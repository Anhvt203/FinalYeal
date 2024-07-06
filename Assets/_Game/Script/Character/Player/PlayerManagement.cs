using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
	public static PlayerManagement instance;
	[HideInInspector] public int Coin;
	public Transform respawnPoint;
	public GameObject playerPrefab;
	public int chosenSkinId;

	[SerializeField] public GameObject currentPlayer;

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
	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.R))
        {
			Debug.Log("R");
			PlayerRes();
		}
    }
	//public void PlayerRes()
	//{
	//	if (currentPlayer != null)
	//	{
	//		Destroy(currentPlayer);
	//	}

	//	currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
	//}
	public void PlayerRes()
	{
		if (currentPlayer == null)
		{
			currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
		}
		else
		{
			Destroy(currentPlayer);
			currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
		}
	}
}
