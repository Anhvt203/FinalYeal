using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	[SerializeField] private AudioSource[] soundf;
	[SerializeField] private AudioSource[] bgm;

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
		PlayBGM(0);
	}

	public void PlaySFx(int sfxToPlay)
	{
		if (sfxToPlay < soundf.Length)
		{
			soundf[sfxToPlay].Play();
		}
	}
	public void Stop(int sfxToStop)
	{
		soundf[sfxToStop].Stop();
	}
	public void PlayBGM(int bgmToPlay)
	{
		foreach (var audioSource in bgm)
		{
			audioSource.Stop();
		}
		bgm[bgmToPlay].Play();
	}
}
