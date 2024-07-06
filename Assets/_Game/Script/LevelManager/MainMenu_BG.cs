using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_BG : MonoBehaviour
{
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private Vector2 backGround;

	private void Update()
	{
		meshRenderer.material.mainTextureOffset += backGround * Time.deltaTime;
	}
}
