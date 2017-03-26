using UnityEngine;
using System.Collections;

public class FPSInput : MonoBehaviour 
{
	CharacterMotorCS motor;
	Animation anim;

	private bool useVCR;
	private InputVCR vcr;
	private bool performJumpAnimation = false;

	void Awake()
	{
		motor = GetComponent<CharacterMotorCS>();
		anim = GetComponent<Animation> ();
		
		Transform root = transform;
		while ( root.parent != null )
			root = root.parent;
		vcr = root.GetComponent<InputVCR>();
		useVCR = vcr != null;
	}

	void OnJump(){
		performJumpAnimation = true;
		anim.Stop("idle");
		anim.Stop ("run");
		anim.Stop ("walk");
		anim.Play ("jump_pose");
	}

	void OnLand(){
		performJumpAnimation = false;
		anim.Stop("jump_pose");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!motor.GetControllable ()) {
			anim.Stop();
			return;
		}
		// Get the input vector from kayboard or analog stick
		Vector3 directionVector;
		if ( useVCR )
			directionVector = new Vector3( vcr.GetAxis ( "Horizontal" ), 0, vcr.GetAxis ( "Vertical" ) );
		else
			directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical" ) );
	
		if (directionVector != Vector3.zero) {
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			float directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min (1, directionLength);
			
			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;
			
			// Multiply the normalized direction vector by the modified length
			directionVector = directionVector * directionLength;

			if (!performJumpAnimation) {
				anim.Stop ("idle");
				anim.Play ("run");
			}
		} else {
			if (!performJumpAnimation) {
				anim.Stop ("run");
				anim.Play ("idle");
			}
		}
		
		// Apply the direction to the CharacterMotor
		motor.inputMoveDirection = /*transform.rotation **/ directionVector;
		motor.inputJump = useVCR && vcr.GetButton ( "Jump" ) || !useVCR && Input.GetButton("Jump");
	}
}
