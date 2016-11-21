using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerUIManager : PersistentSingleton<PlayerUIManager> {

	public Text healthText;
	public Text xpText;
	public Image healthBar;
	public Image xpBar;

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
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
		UpdateUI();
	}

	public void UpdateUI()
	{
		healthText.text = Player.instance.health + " <b>/</b> " + Player.instance.maxHealth;
		healthBar.fillAmount = (float)Player.instance.health/Player.instance.maxHealth;
	}


}
