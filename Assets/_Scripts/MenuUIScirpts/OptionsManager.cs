using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsManager : MonoBehaviour {

	public AudioSource audio;
	public Text muteText;
	public Scrollbar muteBar;
	public Slider volumeSlider;

	// Use this for initialization
	void Start () {

		audio = GetComponentInParent<AudioSource>();

		volumeSlider.value = audio.volume;

	
	}

	public void HandleCloseMenu()
	{
		GameManager.instance.CloseOptions();
	}
	
	public void HandleMute()
	{
		if(muteBar.value == 1)
		{
			muteText.text = "OFF";
			audio.mute = true;
		}
		else
		{
			muteText.text = "ON";
			audio.mute = false;
		}
	}

	public void HandleVolumeSlider()
	{
		audio.volume = volumeSlider.value;
	}
		
}
