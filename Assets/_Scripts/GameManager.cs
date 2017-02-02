using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : PersistentSingleton<GameManager> {

	private int startMenuLevel = 1;
	private int gameLevel = 3;

	public int currentLevel;
	public int highScore;

	public Canvas canvas;



	void Start()
	{
		canvas = GetComponentInChildren<Canvas>();

		highScore = PlayerPrefs.GetInt("HighScore");
		print(highScore);

		if(highScore == 0)
		{
			highScore = 1;
		}
			

		currentLevel = 1;
	}


	public IEnumerator LoadNewLevel()
	{
		currentLevel++;
		Player.instance.exitAudio.Play();
		yield return new WaitForSeconds(Player.instance.exitAudio.clip.length);
		highScore = currentLevel > highScore ? currentLevel : highScore;
		UIManager.instance.UpdateUI();
		SceneManager.LoadScene(gameLevel);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			canvas.enabled = !canvas.enabled;
		}
	}

	public void CloseOptions()
	{
		canvas.enabled = !canvas.enabled;
	}

	public void PlayerDie()
	{
		print(currentLevel + "\t\t" + highScore);
		if(highScore > PlayerPrefs.GetInt("HighScore"))
		{
			PlayerPrefs.SetInt("HighScore", currentLevel);
			PlayerPrefs.Save();
		}
		currentLevel = 1;
		UIManager.instance.ReactivateEnemyPanel();
		Player.instance.health = Player.instance.maxHealth;
		UIManager.instance.UIOff();
		SceneManager.LoadScene(startMenuLevel);
	}


	/*
	void SetPause()
	{
		float timeScale = !isPaused ? 1f : 0f;
		Time.timeScale = timeScale;
		Cursor.visible = isPaused;
		pausePanel.SetActive(isPaused);
	}
	*/
}
