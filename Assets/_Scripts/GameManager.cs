using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : PersistentSingleton<GameManager> {

	public GameObject pausePanel;

	private bool isPaused;

	private int gameLevel = 3;



	void Start()
	{
		isPaused = false;
	}


	public void LoadNewLevel()
	{
		SceneManager.LoadScene(gameLevel);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			isPaused = !isPaused;
			SetPause();
		}
	}

	void SetPause()
	{
		float timeScale = !isPaused ? 1f : 0f;
		Time.timeScale = timeScale;
		Cursor.visible = isPaused;
		pausePanel.SetActive(isPaused);
	}
}
