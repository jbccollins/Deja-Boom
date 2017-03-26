using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class levelEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RestartLevel(){
		Time.timeScale = 1.0F;
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}

	public void ReturnToMainMenu(){
		Time.timeScale = 1.0F;
		SceneManager.LoadScene ("start_screen");
	}

	public void NextLevel(){
		Time.timeScale = 1.0F;
		SceneManager.LoadScene ("level2");
	}
}
