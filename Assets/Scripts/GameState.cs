using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


[System.Serializable]
public class GameState : MonoBehaviour {
	private bool levelEnded = false;
	public GameObject[] toBeReset;
	public bool test = false;
	public Dictionary<string, string> endStates = new Dictionary<string, string>();

	// Use this for initialization
	void Start () {
		endStates.Add("fall", "You fell to your death!");
		endStates.Add("time", "You failed to stop the bomb!");
		endStates.Add("success", "Phew! You stopped the bomb");
		endStates.Add ("barrier", "You hit a death wall! RIP the dream...");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("r")) {
			Time.timeScale = 1.0F;
			int scene = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(scene, LoadSceneMode.Single);
		}
	}

	public bool getLevelEndedState(){
		return levelEnded;
	}

	public void setLevelEndedState(bool state){
		levelEnded = state;
	}

	public void resetAfterClone(){
		foreach ( GameObject r in toBeReset){
			Resettable i = (Resettable) r.GetComponent<Resettable>();
			i.Reset ();
		}
	}
}
