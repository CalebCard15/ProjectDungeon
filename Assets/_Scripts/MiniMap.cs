using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MiniMap : MonoBehaviour {

	public Transform target;
	//public GameObject marker;
	public GameObject mapGUI;
	public float height = 10.0f;
	public float distance = 10.0f;
	public bool rotate = true;
	private Vector3 camAngle;
	private Vector3 camPos;
	private Vector3 targetAngle;
	private Vector3 targetPos;
	private Camera cam;

	// Use this for initialization
	void Start () {
		target = GameObject.Find("Player").GetComponent<Transform>();
		cam = GetComponent<Camera>();
		camAngle = transform.eulerAngles;
		targetAngle = target.transform.eulerAngles;
		camAngle.y = targetAngle.y;
		transform.eulerAngles = camAngle;
	}
	
	// Update is called once per frame
	void Update () {
		targetPos = target.transform.position;
		camPos = targetPos;
		camPos.z -= height;
		transform.position = camPos;
		cam.orthographicSize = distance;
		Vector3 compassAngle = new Vector3();
		compassAngle.z = target.transform.eulerAngles.y;

		if(rotate)
		{
			mapGUI.transform.eulerAngles = compassAngle;
			//marker.transform.eulerAngles = new Vector3();
		}
		else 
		{
			//marker.transform.eulerAngles = -compassAngle;
		}
	}
}
