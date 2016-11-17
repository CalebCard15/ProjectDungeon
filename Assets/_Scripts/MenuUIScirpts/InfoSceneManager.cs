using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InfoSceneManager : MonoBehaviour {

	public int level = 1;

	public void BackToStartMenu()
	{
		SceneManager.LoadScene(level);
	}
}
