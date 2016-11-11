using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;


	// Use this for initialization
	void Awake () {

		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	
	}

	public void Battle(ref Enemy enemy)
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
