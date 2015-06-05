using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {

	public MapController mapController;
	public CharacterController cc;
	public Node currentNodePosition;


	// Use this for initialization
	void Start () {
	
	}

	void OnGUI() {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Reset() {
		currentNodePosition = mapController.currentGameGrid.GetStartNode();
		updatePosition();
	}

	public void SlideNorth() {
		slide(Solver.NORTH);
	}

	public void SlideEast() {
		slide(Solver.EAST);
	}

	public void SlideSouth() {
		slide(Solver.SOUTH);
	}

	public void SlideWest() {
		slide(Solver.WEST);
	}

	private void slide(int direction) {
		currentNodePosition = Solver.SlideDirection(direction, mapController.currentGameGrid, currentNodePosition);
		updatePosition();
	}

	private void updatePosition() {
		cc.transform.position = new Vector3(currentNodePosition.X * mapController.rockSpacing, 0, currentNodePosition.Y * mapController.rockSpacing);
	}

}
