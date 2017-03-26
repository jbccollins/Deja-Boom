using UnityEngine;
using System.Collections;

public class StepsController: MonoBehaviour, Resettable {

	private GameObject steps;
	// Use this for initialization
	void Start () {
		steps = transform.Find ("Steps").gameObject;
	}

	// Update is called once per frame
	void Update () {

	}

	public void Reset(){
		steps.SetActive (true);
	}
}
