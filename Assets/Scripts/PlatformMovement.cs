using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {
	Vector3 pointB;
	public Vector3 pointA;
	public float speed = 0.2f;
	// Use this for initialization
	void Start () {
		pointB = transform.position;
		StartCoroutine(MovePlatform());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator MovePlatform(){
		while (true) {
			float i = Mathf.PingPong(Time.time * speed, 1);
			transform.position=Vector3.Lerp(pointA, pointB, i);
			yield return 0;
		}
	}
}