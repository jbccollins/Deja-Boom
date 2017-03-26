/* PlayButton.cs
 * Copyright Eddie Cameron 2012 (See readme for licence)
 * ----------------------------
 */ 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayButton : MonoBehaviour 
{
	public InputVCR playbackCharacterPrefab;
	public InputVCR playerVCR;
	
	public RecordButton recordButton;
	
	public Texture pauseTex;
	
	private bool isRecording;
	private Vector3 recordingStartPos;
	private Quaternion recordingStartRot;
	
	private bool isPlaying;
	/* >>> */
	//Only one recording per InputVCR
	private bool canRecord = true;
	private IEnumerator coroutine;
	/* <<< */
	private InputVCR curPlayer;

	public GUIText instructions;
	
	void Awake()
	{
		Destroy( instructions, 5f );
	}
	
	public void StartRecording()
	{
		if (!canRecord) {
			return;
		}
		if (isRecording) {
			StopRecording ();
		}else{
			recordingStartPos = playerVCR.transform.position;
			recordingStartRot = playerVCR.transform.rotation;
			playerVCR.NewRecording ();
		}
		
		isRecording = !isRecording;
	}
	public InputVCR getRecordedPlayer(){
		return playbackCharacterPrefab;
	}
	/* >>> */
	public void StopRecording(){
		playerVCR.Stop ();
		canRecord = false;
	}
	/* <<< */
	void Update()
	{
		//if ( Input.GetKeyDown ( KeyCode.P ) )
		//	StartPlay ();
	}
	
	void OnMouseDown()
	{
		StartPlay ();
	}

	/*>>>*/
	public void StartPlay() {
		GameObject[] recordedPlayers = GameObject.FindGameObjectsWithTag("RecordedPlayer");		
		if (isPlaying) {
			return;
		}
		// If there are other clones still going then don't play
		foreach ( GameObject item in recordedPlayers){
			InputVCR i = item.GetComponent<InputVCR> ();
			if ( i.currentFrame != 0) {
				return;
			}
		}

		if ( curPlayer != null )
		{
			// unpause
			curPlayer.Play ();
			SwapTex ();
			isPlaying = true;
		}
		else
		{
			// try to start new playback
			if ( isRecording )
				recordButton.Record ();
			StartCoroutine ( Player () );
		}
	}
	/*<<<*/
	/*
	private void StartPlay()	
	{
		if ( isPlaying )
		{
			// pause
			curPlayer.Pause();
			isPlaying = false;
			SwapTex();
		}
		else if ( curPlayer != null )
		{
			// unpause
			curPlayer.Play ();
			SwapTex ();
			isPlaying = true;
		}
		else
		{
			// try to start new playback
			if ( isRecording )
				recordButton.Record ();
			StartCoroutine ( Player () );
		}
	}
	*/
	private IEnumerator Player()
	{
		Recording recording = playerVCR.GetRecording ();
		if ( recording == null )
			yield break;

		curPlayer = (InputVCR)Instantiate ( playbackCharacterPrefab, recordingStartPos, recordingStartRot );
		curPlayer.Play ( Recording.ParseRecording( recording.ToString() ) );
		SwapTex ();
		
		float playTime = recording.recordingLength;
		float curTime = 0f;
		
		isPlaying = true;
		while ( curTime < playTime )
		{
			if ( isPlaying )
				curTime += Time.deltaTime;
			
			yield return 0;
		}
		
		// Play finished
		isPlaying = false;
		Destroy ( curPlayer.gameObject );
		curPlayer = null;
		SwapTex ();
	}

	private void SwapTex()
	{
		Texture playTex = GetComponent<GUITexture>().texture;
		GetComponent<GUITexture>().texture = pauseTex;
		pauseTex = playTex;
	}
}
