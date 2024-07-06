using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinSelect_Ul : MonoBehaviour
{
	[SerializeField] private int[] priceSkin;

	[SerializeField] private bool[] skinPur;
	private int skinId;

	[Header("ComSkin")]
	[SerializeField] private TextMeshProUGUI coinText;
	[SerializeField] private GameObject buyBtn;
	[SerializeField] private GameObject selectBtn;
	[SerializeField] private Animator animator;

	private void SettingSkininf()
	{
		skinPur[0] = true;

        for (int i = 1; i < skinPur.Length; i++)
        {
            bool skinUnlock = PlayerPrefs.GetInt("SkinPurchased" + i) == 1;

            if (skinUnlock)
            {
				skinPur[i] = true;
            }
        }

        coinText.text = " " + PlayerPrefs.GetInt("TotalCointCollected").ToString();

		selectBtn.SetActive(skinPur[skinId]);
		buyBtn.SetActive(!skinPur[skinId]);

		if (!skinPur[skinId])
		{
			buyBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + priceSkin[skinId];
		}
		animator.SetInteger("Skinid", skinId);
	}
	public bool EnoughCoin()
	{
		int totalCoin = PlayerPrefs.GetInt("TotalCointCollected");

        if (totalCoin > priceSkin[skinId])
        {
			totalCoin = totalCoin - priceSkin[skinId];

			PlayerPrefs.SetInt("TotalCointCollected", totalCoin);

			AudioManager.instance.PlaySFx(5);
			return true;
        }
		AudioManager.instance.PlaySFx(6);
		return false;
    }
	
	public void NextSkin()
	{
		skinId++;

		if (skinId > 2)
		{
			skinId = 0;
		}

		SettingSkininf();
	}
	public void PrevSkin() 
	{
		skinId--;
		if (skinId < 0)
		{
			skinId = 2;
		}
		SettingSkininf();
	}
	public void Buy()
	{
		if (EnoughCoin())
		{
			PlayerPrefs.SetInt("SkinPurchased" + skinId, 1);
			SettingSkininf();

		}
		else
		{
			Debug.Log("Not enough coin");
		}
	}
	public void Select()
	{
		PlayerManagement.instance.chosenSkinId = skinId;
		Debug.Log("skin");
	}
	public void SwitchSelectBtn(GameObject newBtn)
	{
		selectBtn = newBtn;
	}
	private void OnEnable()
	{
		SettingSkininf();
	}
	private void OnDisable()
	{
		selectBtn.SetActive(false);
	}
}
