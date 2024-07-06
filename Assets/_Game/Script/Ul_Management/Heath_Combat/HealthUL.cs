using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUL : MonoBehaviour
{
	float hpP;
	float maxHp;

	private Transform target;

	[SerializeField] Image imageFills;
	[SerializeField] Vector3 off;
	// Update is called once per frame
	void Update()
	{
		imageFills.fillAmount = Mathf.Lerp(imageFills.fillAmount, hpP / maxHp, Time.deltaTime * 5f);
		transform.position = target.position + off;
	}
	public void OnInit(float maxHp, Transform target)
	{
		this.target = target;
		this.maxHp = maxHp;
		hpP = maxHp;
		imageFills.fillAmount = 1;

	}
	public void SetNewHp(float hpP)
	{
		this.hpP = hpP;
	}

}
