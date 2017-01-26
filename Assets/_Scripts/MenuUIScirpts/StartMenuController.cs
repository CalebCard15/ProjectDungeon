using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour {

	int infoLevel = 2;
	int gameLevel = 3;

	public void StartGameButtonClick()
	{
		UIManager.instance.UIOn();
		UIManager.instance.UpdateUI();
		SceneManager.LoadScene(gameLevel);
	}

	public void InfoScreenButtonClick()
	{
		SceneManager.LoadScene(infoLevel);
	}
}
