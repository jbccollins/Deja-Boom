using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClonePlayer : MonoBehaviour {

	public InputVCR playerVCR;
	public CameraController camera;
	public bool isCloned = false;
	public bool isAClone;
	CharacterMotorCS motor;
	public int playerStartPositionX = -8;
	public int playerStartPositionY = 1;
	public int playerStartPositionZ = -8;
	public int maxClones = 2;
	public int timesCloned = 0;
	public PlayButton playButton;
	public Text remainingClones;
	public GameState gameState;

	void Awake (){
		motor = GetComponent<CharacterMotorCS>();
	}
	// Use this for initialization
	void Start () {
		isAClone = IsCloned (); // Is the current object that this script is 
		isCloned = false;
		playButton.StartRecording ();
		remainingClones.text = "Remaining Clones: " + (maxClones - timesCloned);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Clone") && gameState.getLevelEndedState() == false) {
			GameObject[] recordedPlayers = GameObject.FindGameObjectsWithTag("RecordedPlayer");		
			// If there are other clones still going then don't clone!
			// Avoid a time paradox!!!!
			foreach ( GameObject item in recordedPlayers){
				InputVCR i = item.GetComponent<InputVCR> ();
				if ( i.currentFrame != 0) {
					return;
				}
			}

			timesCloned++;
			if (!isCloned && timesCloned <= maxClones) {
				playButton.StopRecording ();
				CreateClone ();
				motor.SetControllable (false);
				GameObject.Find ("Bip001").SetActive (false);
				CharacterController c = GetComponent<CharacterController> ();
				c.enabled = false;
			}
			if (timesCloned <= maxClones) {
				InputVCR recordedPlayer = playButton.getRecordedPlayer ();
				PlayerState currentPlayerState = playerVCR.GetComponent<PlayerState> ();
				PlayerState recordedPlayerState = recordedPlayer.GetComponent<PlayerState> ();
				recordedPlayerState.successText = currentPlayerState.successText;
				recordedPlayerState.deadText = currentPlayerState.deadText;
				recordedPlayerState.subtitleText = currentPlayerState.subtitleText;
				recordedPlayerState.tryAgainText = currentPlayerState.tryAgainText;
				recordedPlayerState.mainMenuText = currentPlayerState.mainMenuText;
				recordedPlayerState.nextLevelText = currentPlayerState.nextLevelText;
				recordedPlayerState.detonator = currentPlayerState.detonator;
				recordedPlayerState.music = currentPlayerState.music;
				recordedPlayerState.levelOver = currentPlayerState.levelOver;
				recordedPlayerState.gameState = gameState;
				gameState.resetAfterClone ();
				playButton.StartPlay ();
			}
			int rc = (maxClones - timesCloned);
			if (rc < 0) {
				rc = 0;
			}
			remainingClones.text = "Remaining Clones: " + rc;
		}
	}

	void CreateClone(){
		isCloned = true;
		playerStartPositionX += 4;
		//Debug.Log (playerStartPosition.x);
		//playerStartPosition.x = playerStartPosition.x + 1;
		//Debug.Log (playerStartPosition);
		InputVCR clone = GameObject.Instantiate(playerVCR, new Vector3(playerStartPositionX, playerStartPositionY, playerStartPositionZ), Quaternion.identity) as InputVCR;
		camera.player = clone.gameObject;
	}

	public bool IsCloned(){
		return isCloned;
	}

	public bool IsAClone(){
		return isAClone;
	}
}
