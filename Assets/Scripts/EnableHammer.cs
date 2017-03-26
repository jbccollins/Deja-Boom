using UnityEngine;
using System.Collections;

public class EnableHammer : MonoBehaviour {

	public HammerController hammer;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		hammer.pushHammer = true;
	}
}
