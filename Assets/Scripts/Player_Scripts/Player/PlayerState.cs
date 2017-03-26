using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour {

	private CharacterController controller;
	public Text successText;
	public Text deadText;
	public Text subtitleText;
	public Text tryAgainText;
	public Text mainMenuText;
	public Text nextLevelText;
	public Detonator detonator;
	public AudioSource music;
	public AudioSource levelOver;
	public AudioClip levelSuccess;
	public AudioClip gameOver;
	public AudioClip wilhelmScream;
	public AudioClip timeOver;
	public AudioClip nearExplosionA;
	public AudioClip nearExplosionB;
	public AudioClip electricShock;
	public float detailLevel = 1.0f;
	public float explosionLife = 10;
	public GameState gameState;
		
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		successText.enabled = false;
		deadText.enabled = false;
		subtitleText.enabled = false;
		tryAgainText.enabled = false;
		mainMenuText.enabled = false;
		nextLevelText.enabled = false;
		ResetGameTimeScale ();
	}

	// Update is called once per frame
	void Update () {
		bool levelEnded = gameState.getLevelEndedState ();
		if (!levelEnded && controller.gameObject.transform.position.y < 0){
			//StartCoroutine(EndLevel ("fall"));
			EndLevel("fall");
		}
	}
	public void EndLevel (string state){
		bool levelEnded = gameState.getLevelEndedState ();
		Debug.Log (state);
		Debug.Log (levelEnded);
		if (levelEnded) {
			return;
		}
		Debug.Log ("KEYS:");
		foreach(var i in gameState.endStates.Keys){
			Debug.Log ("State: " + i);
		}
		float sec = 0.0F;
		music.Stop ();
		switch (state){
		case "fall":
			levelOver.clip = wilhelmScream;
			levelOver.Play();
			deadText.enabled = true;
			break;
		case "time":
			SpawnExplosion ();
			Time.timeScale = 0.35F;
			sec = 2.0f;
			AudioSource.PlayClipAtPoint (nearExplosionA, transform.position);
			AudioSource.PlayClipAtPoint (nearExplosionB, transform.position);
			deadText.enabled = true;
			break;
		case "success":
			levelOver.clip = levelSuccess;
			levelOver.Play();
			successText.enabled = true;
			break;
		case "barrier":
			levelOver.clip = electricShock;
			levelOver.Play ();
			deadText.enabled = true;
			break;
		}
		gameState.setLevelEndedState(true);
		subtitleText.text = gameState.endStates [state];
		subtitleText.enabled = true;
		tryAgainText.enabled = true;
		mainMenuText.enabled = true;
		if (SceneManager.GetActiveScene ().name == "level1") {
			nextLevelText.enabled = true;
		}
		StartCoroutine (stopTime (sec));
	}

	IEnumerator stopTime(float seconds){
		yield return new WaitForSeconds(seconds);
		Time.timeScale = 0.0F;
	}

	void ResetGameTimeScale (){
		Time.timeScale = 1.0F;
	}

	private void SpawnExplosion() {
		detonator.detail = detailLevel;
		detonator.Explode ();
		//Destroy(detonator, explosionLife);
	}
}
