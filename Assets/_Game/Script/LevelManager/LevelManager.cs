using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
public class LevelManager : MonoBehaviour
{
	[SerializeField] private GameObject levelBtnPrefab;
	[SerializeField] private Transform levelContent;

	[SerializeField] private bool[] levelOpen;

	private void Start()
	{
		PlayerPrefs.SetInt("Map" +1+ "Unlocked", 1);
		lvBolen();
		LoadGame();
	}
	private void lvBolen()
	{
		for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
		{
			bool unlocked = PlayerPrefs.GetInt("Map" + i + "Unlocked") == 1;

			if (unlocked)
			{
				levelOpen[i] = true;
			}
			else
			{
				return;
			}

		}
	}
	private void LoadGame()
	{
		for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
		{
			if (!levelOpen[i])
			{
				return;
			}

			string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
			string sceneName = Path.GetFileNameWithoutExtension(scenePath);

			string btnText = "Map " + sceneName;

			GameObject newBtn = Instantiate(levelBtnPrefab, levelContent);
			newBtn.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
			newBtn.GetComponent<LevelBTN>().UpdateTextInfo(sceneName,i);

		}
	}
	public void LoadLevel(string sceneName)
	{
		AudioManager.instance.PlaySFx(4);

		SceneManager.LoadScene(sceneName);
	}
	public void LoadNewGame() 
	{
		AudioManager.instance.PlaySFx(4);

		for (int i = 2; i < SceneManager.sceneCountInBuildSettings; i++)
        {
			bool unlocked = PlayerPrefs.GetInt("Map" +i+ "Unlocked") == 1;

			if (unlocked)
			{
				PlayerPrefs.SetInt("Map" + i + "Unlocked", 0);
			}
			else
			{
				SceneManager.LoadScene("Darkdungeon");
				return;
			}
        }
    }
	public void LoadToContinueGame()
	{
		//AudioManager.instance.PlaySFx(4);
		// Lặp qua danh sách các map đã mở khóa, bắt đầu từ map thứ hai (vì map đầu tiên không cần load lại)
		for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
		{
			bool unlocked = PlayerPrefs.GetInt("Map" + i + "Unlocked") == 1;

			// Nếu map hiện tại đã mở khóa
			if (!unlocked)
			{
				// Load map hiện tại
				SceneManager.LoadScene("Map" + (i - 1));
				return;
			}
		}
	}
}
