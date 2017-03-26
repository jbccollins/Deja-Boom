using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerCountdown : MonoBehaviour {

	public float seconds = 5.0f;
	public PlayerState p;
	public Text timer;
	private bool detonated = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (detonated) {
			return;
		}
		seconds -= Time.deltaTime;
		timer.text = seconds.ToString ("F1");
		if (seconds <= 0 ){
			detonated = true;
			timer.text = "0";
			p.EndLevel ("time");
		}
	}
}
