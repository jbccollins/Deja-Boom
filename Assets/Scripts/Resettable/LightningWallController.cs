using UnityEngine;
using System.Collections;

public class LightningWallController : MonoBehaviour, Resettable {

	private GameObject barrier;
	private GameObject lightning;
	private bool barrierIsActive;
	private bool lightningIsActive;
	// Use this for initialization
	void Start () {
		barrier = transform.Find ("Barrier").gameObject;
		lightning = transform.Find ("Lightning").gameObject;
		barrierIsActive = barrier.activeSelf;
		lightningIsActive = lightning.activeSelf;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reset(){
		barrier.SetActive (barrierIsActive);
		lightning.SetActive (lightningIsActive);
	}
}
