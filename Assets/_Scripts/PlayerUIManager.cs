using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIManager : PersistentSingleton<PlayerUIManager> {

	public Text healthText;
	public Text xpText;
	public Image healthBar;
	public Image xpBar;

	// Use this for initialization
	void Start () {
		Init();
		UpdateUI();
	}

	void Init()
	{
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
	}

	public void UpdateUI()
	{
		healthText.text = Player.instance.health + " <b>/</b> " + Player.instance.maxHealth;
		healthBar.fillAmount = (float)Player.instance.health/Player.instance.maxHealth;
	}


}
