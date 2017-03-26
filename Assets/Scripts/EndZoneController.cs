using UnityEngine;
using System.Collections;

public class EndZoneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
		string tag = other.gameObject.tag;
		Debug.Log (tag);
		if (tag.Contains ("Player") || tag.Contains("RecordedPlayer")) {
			PlayerState p = other.gameObject.GetComponent<PlayerState> ();
			//StartCoroutine(p.EndLevel ("success"));
			p.EndLevel("success");
		}
	}
}
