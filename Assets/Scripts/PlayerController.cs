using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text gameOverText;
	public GameObject pickups;

	public Rigidbody rb;
	private int countLeft;
	public int pickupQuota;
	private bool gameInProgress;
	private bool canJump = false;
	private bool canPowerUp = false;
	public Material PlayerPowerUpMaterial;
	public Material PlayerDefaultMaterial;
	private float timePowerUpStarted;
	public float PowerUpDuration;
	private bool isPoweredUp = false;
	private bool isAlive = true;

	private bool useVCR;
	private InputVCR vcr;

	void Awake()
	{
		Transform root = transform;
		while ( root.parent != null )
			root = root.parent;
		vcr = root.GetComponent<InputVCR>();
		useVCR = vcr != null;
		Debug.Log (useVCR);
	}

	void Start () {
		rb = GetComponent<Rigidbody>();
		countLeft = pickupQuota;
		SetCountText ();
		gameOverText.text = "";
		gameInProgress = true;
		if (SceneManager.GetActiveScene ().name == "level2") {
			canJump = true;	
			canPowerUp = true;
		}
		ResetGameTimeScale ();
	}

	void Update ()
	{
		if (Input.GetButtonDown("Restart")) { // Restart the game
			Time.timeScale = 1.0F;
			UnityEngine.SceneManagement.SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		if (Input.GetButtonDown ("Pause")) { // Pause the game
			if (gameInProgress) {
				if (Time.timeScale == 1.0F)
					Time.timeScale = 0.0F;
				else
					Time.timeScale = 1.0F;
			}
		}
		if (Input.GetButtonDown("Quit")) { // Quit the game
			Application.Quit();
		}
		if (Input.GetButtonDown("Level 2")) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("level2");
		}
		if (Input.GetButtonDown("Level 1")) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("level1");
		}
		if (Input.GetButtonDown("Power Up")) {
			powerUpPlayer ();
		}
		if (transform.position.y < 0) {
			isAlive = false;
			EndGame ();
		}
		if (isPoweredUp && Time.time - PowerUpDuration > timePowerUpStarted) {
			GetComponent<Renderer> ().material = PlayerDefaultMaterial;
			isPoweredUp = false;
		}
		if (countLeft == 0) {
			EndGame ();
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		transform.rotation = Quaternion.LookRotation(movement);

		rb.AddForce (movement * speed);

		if (Input.GetButtonDown ("Jump") && canJump) {
			Vector3 jump = new Vector3 (0.0f, 500.0f, 0.0f);
			rb.AddForce (jump);
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "BouncyBall")) {
			if (isPoweredUp) {
				other.gameObject.SetActive (false);
				decrementCount ();
				SetCountText ();
				if (countLeft == 0) {
					EndGame ();
				}
			} else {
				incrementCount ();
				SetCountText ();
			}
		}
		if (other.gameObject.CompareTag ("Powerup") && !isPoweredUp) {
			powerUpPlayer();
		}
		if (other.gameObject.CompareTag ( "Pick Up")) {
			other.gameObject.SetActive (false);
			decrementCount ();
			SetCountText ();
			if (countLeft == 0) {
				EndGame ();
			}
		}
	}

	public void SetCountText ()
	{
		countText.text = "Remaining Pickups: " + countLeft.ToString ();
	}

	void EndGame (){
		gameInProgress = false;
		Time.timeScale = 0.0F; // Freeze the game

		if (countLeft == 0 && isAlive) {
			gameOverText.text = "You Win!\n Next Level? Hit '1' or '2'";
		} else if (!isAlive) {
			gameOverText.text = "You Lose!\n Next Level? Hit '1' or '2'";
		}
	}

	void ResetGameTimeScale (){
		Time.timeScale = 1.0F;
	}

	void powerUpPlayer (){
		if (canPowerUp) {
			GetComponent<Renderer> ().material = PlayerPowerUpMaterial;
			isPoweredUp = true;
			timePowerUpStarted = Time.time;
		}
	}

	public bool getPoweredUpState (){
		return isPoweredUp;
	}

	public void incrementCount(){
		countLeft = countLeft + 1;
	}

	public void decrementCount(){
		countLeft = countLeft - 1;
	}
}