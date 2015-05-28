using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {

	public CharacterController cc;
	public float speed = 1.0f;
	public float gravity = -1.0f;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}

	void OnGUI() {
		GUI.TextArea(new Rect(10, 10, 120, 50), "vel" + velocity.ToString() + "\nccvel" + cc.velocity.ToString());
	}

	// Update is called once per frame
	void Update () {
		if (CCHasSlowed()) {
			velocity = Vector3.zero;
		}
		cc.Move(velocity + new Vector3(0, gravity, 0));
	}

	public void Reset() {
		velocity = Vector3.zero;
	}

	public void SlideNorth() {
		if (CCNotMoving()) {
			velocity = new Vector3(-speed, 0, 0);
		}
	}

	public void SlideEast() {
		if (CCNotMoving()) {
			velocity = new Vector3(0, 0, speed);
		}
	}

	public void SlideSouth() {
		if (CCNotMoving()) {
			velocity = new Vector3(speed, 0, 0);
		}
	}

	public void SlideWest() {
		if (CCNotMoving()) {
			velocity = new Vector3(0, 0, -speed);
		}
	}

	private bool CCNotMoving() {
		return cc.velocity.Equals(Vector3.zero);
	}

	//to prevent minor movement after a collision
	//if we are not equal to speed or zero
	private bool CCHasSlowed() {
		return false;
	}
}
