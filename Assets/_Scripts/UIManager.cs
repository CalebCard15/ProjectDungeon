﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : PersistentSingleton<UIManager> {

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

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
	{
		Player.instance.transform.position = Vector3.zero;
		Init();
	}

	// Use this for initialization
	void Start () {
		
	}

	void Init()
	{
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

	public void ReactivateEnemyPanel()
	{
		enemyStatsPanel.SetActive(true);
	}

	public void UpdateUI()
	{
		healthText.text = Player.instance.health + " <b>/</b> " + Player.instance.maxHealth;
		healthBar.fillAmount = (float)Player.instance.health/Player.instance.maxHealth;
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


}
