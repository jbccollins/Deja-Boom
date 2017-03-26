using UnityEngine;
using System.Collections;

public class EnableDisableTriggerController : MonoBehaviour, Resettable {

	public GameObject[] objectsToDisable;
	public GameObject[] objectsToEnable;
	public bool reverseOnTriggerExit = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void Reset(){
		gameObject.SetActive (true);
	}
	
	void OnTriggerEnter(Collider other){

		string tag = other.gameObject.tag;
		if (tag.Contains ("Player") || tag.Contains("RecordedPlayer")) {
			foreach(GameObject obj in objectsToDisable){
				obj.SetActive (false);
			}
			foreach(GameObject obj in objectsToEnable){
				obj.SetActive (true);
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (!reverseOnTriggerExit) {
			return;
		}
		string tag = other.gameObject.tag;
		if (tag.Contains ("Player") || tag.Contains ("RecordedPlayer")) {
			foreach (GameObject obj in objectsToDisable) {
				obj.SetActive (true);
			}
			foreach (GameObject obj in objectsToEnable) {
				obj.SetActive (false);
			}
		}
	}
}
