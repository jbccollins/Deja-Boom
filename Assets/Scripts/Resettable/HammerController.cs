using UnityEngine;
using System.Collections;

public class HammerController : MonoBehaviour, Resettable {
	public Vector3 pointB;
	public Vector3 pointA;
	public float speed = 10f;
	public bool pushHammer = false;
	private Rigidbody rb;
	Vector3 startPosition;
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		rb = GetComponent<Rigidbody>();
		//StartCoroutine(MoveHammer());
	}

	// Update is called once per frame
	void Update () {
		if (pushHammer) {
			push ();
		} else {
			//pull();
		}
	}
	public void Reset(){
		pushHammer = false;
		transform.position = startPosition;
	}

	void push(){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, pointA, step);
	}
	void pull(){
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, pointB, step);
	}
}