using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
	}
}
