using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Splash : MonoBehaviour {

	public int level = 1;
	public float timeBeforeLoadStartScreen = 5f;
	public float timeBeforeAddLoadingDot = .25f;

	public Text loadingText;

	private int loadingDots;
	private Camera camera;
	private float timeLoadCounter;
	private float timeDotCounter;

	// Use this for initialization
	void Start () {
		timeDotCounter = 0;
		timeLoadCounter = 0;
		loadingDots = 0;
		camera = GetComponent<Camera>();
	
	}
	
	// Update is called once per frame
	void Update () {

		timeDotCounter += Time.deltaTime;
		timeLoadCounter += Time.deltaTime;

		if(timeDotCounter >= timeBeforeAddLoadingDot)
		{
			if(loadingDots < 3)
			{
				loadingText.text += " .";
				loadingDots++;
			}
			else
			{
				loadingText.text = "Loading";
				loadingDots = 0;
			}
			timeDotCounter = 0;
		}

		if(timeLoadCounter >= timeBeforeLoadStartScreen)
		{
			SceneManager.LoadScene(level);
		}
	
	}
}
