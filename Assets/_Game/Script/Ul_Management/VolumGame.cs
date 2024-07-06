using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumGame : MonoBehaviour
{
	[SerializeField] private string mixerPara;
	[SerializeField] private AudioMixer audioMixer;
	[SerializeField] private Slider slider;
	[SerializeField] private float sliderMulti;


	public void Soundsetting()
	{
		slider.onValueChanged.AddListener(SliderValue);
		slider.minValue = .001f;
		slider.value = PlayerPrefs.GetFloat(mixerPara, slider.value);
	}

	private void OnDisable()
	{
		PlayerPrefs.SetFloat(mixerPara, slider.value);
	}

	private void SliderValue(float value)
	{
		audioMixer.SetFloat(mixerPara,Mathf.Log10(value) * sliderMulti);	
	}
}
