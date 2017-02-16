using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : PersistentSingleton<UIManager> {

	public GameObject uiCanvas;
	public GameObject levelUpCanvas;
	public GameObject pauseScreen;
	public Text healthText;
	public Text xpText;
	public Image healthBar;
	public Image xpBar;
	public Text highScore;
	public Text currentScore;
	public Text enemyName;
	public GameObject enemyStatsPanel;
	public Text enemyDefenseText;
	public Text enemyPowerText;
	public Image enemyHealth;
	public Text enemyHealthText;


	// Use this for initialization
	void Start () {
		//PauseScreen
		pauseScreen = GameObject.Find("OptionsCanvas");
		pauseScreen.SetActive(false);

		//UI Canvas
		uiCanvas = GameObject.Find("Canvas");

		//Player UI
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
		highScore = GameObject.Find("HighScore").GetComponent<Text>();
		currentScore = GameObject.Find("CurrentScore").GetComponent<Text>();
		xpText = GameObject.Find("XPText").GetComponent<Text>();
		xpBar = GameObject.Find("XPBar").GetComponent<Image>();

		//Enemy UI 
		enemyName = GameObject.Find("EnemyNameText").GetComponent<Text>();
		enemyStatsPanel = GameObject.Find("EnemyUIPanel");
		enemyDefenseText = GameObject.Find("EnemyDefenseText").GetComponent<Text>();
		enemyPowerText = GameObject.Find("EnemyPowerText").GetComponent<Text>();
		enemyHealth = GameObject.Find("EnemyHealthBar").GetComponent<Image>();
		enemyHealthText = GameObject.Find("EnemyHealthText").GetComponent<Text>();
		enemyStatsPanel.SetActive(false);

		//UpdateUI();
	}



	public void ReactivateEnemyPanel()
	{
		enemyStatsPanel.SetActive(true);
	}

	public void UpdateUI()
	{
		if(Player.instance != null)
		{
			healthText.text = Player.instance.health + " <b>/</b> " + Player.instance.maxHealth;
			healthBar.fillAmount = (float)Player.instance.health/Player.instance.maxHealth;
			xpText.text = Player.instance.currentXp + " <b>/</b> " + Player.instance.xpToNextLevel;
			xpBar.fillAmount = (float)Player.instance.currentXp/Player.instance.xpToNextLevel;
		}
		currentScore.text = "Current Level: " + GameManager.instance.currentLevel;
		highScore.text = "HighScore: " + GameManager.instance.highScore; 
	}

	public void UpdateEnemyUI(Enemy enemy)
	{
		if(enemy.health <= 0)
		{
			enemyStatsPanel.SetActive(false);
		}
		else
		{
			enemyStatsPanel.SetActive(true);
			enemyName.text = enemy.name;
			enemyDefenseText.text = "Defense: " + enemy.defense;
			enemyPowerText.text = "Power: " + enemy.power;
			enemyHealth.fillAmount = (float)enemy.health/enemy.maxHealth;
			enemyHealthText.text = enemy.health + " <b>/</b> " + enemy.maxHealth;
		}
	}

	public void SetPause()
	{
		pauseScreen.SetActive(!pauseScreen.activeSelf);
		Time.timeScale = pauseScreen.activeSelf ? 0f : 1f;
		Cursor.visible = !pauseScreen.activeSelf;

	}

	public void UIOn()
	{
		uiCanvas.SetActive(true);
	}

	public void UIOff()
	{
		uiCanvas.SetActive(false);
	}



	/*
	 * 
	 * Scrap code pieces that broke my shit
	 * 
	 * 
	void Init()
	{
		//PauseScreen
		pauseScreen = GameObject.Find("OptionsCanvas");
		pauseScreen.SetActive(false);

		//UI Canvas
		uiCanvas = GameObject.Find("Canvas");

		//Player UI
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
		highScore = GameObject.Find("HighScore").GetComponent<Text>();
		currentScore = GameObject.Find("CurrentScore").GetComponent<Text>();

		//Enemy UI 
		enemyName = GameObject.Find("EnemyNameText").GetComponent<Text>();
		enemyStatsPanel = GameObject.Find("EnemyUIPanel");
		enemyDefenseText = GameObject.Find("EnemyDefenseText").GetComponent<Text>();
		enemyPowerText = GameObject.Find("EnemyPowerText").GetComponent<Text>();
		enemyHealth = GameObject.Find("EnemyHealthBar").GetComponent<Image>();
		enemyHealthText = GameObject.Find("EnemyHealthText").GetComponent<Text>();
		enemyStatsPanel.SetActive(false);
		UpdateUI();
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
	{
		Player.instance.transform.position = Vector3.zero;
		Init();
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	*/
}
