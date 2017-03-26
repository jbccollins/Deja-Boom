using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class menuScript : MonoBehaviour {

	public Canvas quitMenu;
	private Button startButton;
	private Button exitButton;
	private Button instructionsButton;
	private Button backButton;
	public Text backText;
	public Text instructionsText;
	public Text startText;
	public Text exitText;
	public Text instructionsContainer;

	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startButton = startText.GetComponent<Button> ();
		exitButton = exitText.GetComponent<Button> ();
		instructionsButton = instructionsText.GetComponent<Button> ();
		backButton = backText.GetComponent<Button> ();
		backButton.enabled = false;
		backText.enabled = false;
		instructionsContainer.enabled = false;
		quitMenu.enabled = false;
	}

	public void ExitPress(){
		quitMenu.enabled = true;
		startButton.enabled = false;
		exitButton.enabled = false;
		instructionsButton.enabled = false;
	}

	public void BackPress(){
		startButton.enabled = true;
		startText.enabled = true;
		exitButton.enabled = true;
		exitText.enabled = true;
		instructionsButton.enabled = true;
		instructionsText.enabled = true;
		backButton.enabled = false;
		backText.enabled = false;
		instructionsContainer.enabled = false;

	}

	public void InstructionsPress(){
		startButton.enabled = false;
		startText.enabled = false;
		exitButton.enabled = false;
		exitText.enabled = false;
		instructionsText.enabled = false;
		instructionsButton.enabled = false;
		instructionsContainer.enabled = true;
		backButton.enabled = true;
		backText.enabled = true;
	}

	public void NoPress(){
		quitMenu.enabled = false;
		startButton.enabled = true;
		exitButton.enabled = true;
		instructionsButton.enabled = true;
	}

	public void StartLevel(){
		SceneManager.LoadScene ("level1");
	}

	public void ExitGame(){
		Application.Quit ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
