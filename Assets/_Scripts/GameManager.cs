using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : PersistentSingleton<GameManager> {

	public GameObject pausePanel;

	private bool isPaused;

	private int startMenuLevel = 1;
	private int gameLevel = 3;

	public int currentLevel;
	public int highScore;



	void Start()
	{
		if(PlayerPrefs.GetInt("HighScore") == 0)
		{
			PlayerPrefs.SetInt("HighScore", 1);
		}

		highScore = PlayerPrefs.GetInt("HighScore");
		currentLevel = 1;
		UIManager.instance.UpdateUI();
		isPaused = false;
	}


	public IEnumerator LoadNewLevel()
	{
		currentLevel++;
		Player.instance.exitAudio.Play();
		yield return new WaitForSeconds(Player.instance.exitAudio.clip.length);
		highScore = currentLevel > highScore ? currentLevel : highScore;
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

	public void PlayerDie()
	{
		if(currentLevel > highScore)
		{
			PlayerPrefs.SetInt("HighScore", currentLevel);
			highScore = PlayerPrefs.GetInt("HighScore");
		}
		currentLevel = 1;
		UIManager.instance.ReactivateEnemyPanel();
		Player.instance.health = Player.instance.maxHealth;
		SceneManager.LoadScene(startMenuLevel);
	}

	void SetPause()
	{
		float timeScale = !isPaused ? 1f : 0f;
		Time.timeScale = timeScale;
		Cursor.visible = isPaused;
		pausePanel.SetActive(isPaused);
	}
}
