using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {

	public MapController mapController;
	public CharacterController cc;
	public Node currentNodePosition;
	public ScoreKeeper scoreKeeper;

	private float fingerStartTime  = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;
	
	private bool isSwipe = false;
	private float minSwipeDist  = 50.0f;
	private float maxSwipeTime = 0.5f;

	// Use this for initialization
	void Start () {
		
	}

	void OnGUI() {

	}

	// Update is called once per frame
	//Swipe detection credit to http://pfonseca.com/swipe-detection-on-unity/
	void Update () {
		if (Input.touchCount > 0){
			foreach (Touch touch in Input.touches) {
				switch (touch.phase) {
				case TouchPhase.Began :
					/* this is a new touch */
					isSwipe = true;
					fingerStartTime = Time.time;
					fingerStartPos = touch.position;
					break;
				case TouchPhase.Canceled :
					/* The touch is being canceled */
					isSwipe = false;
					break;
				case TouchPhase.Ended :
					float gestureTime = Time.time - fingerStartTime;
					float gestureDist = (touch.position - fingerStartPos).magnitude;
					
					if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist){
						Vector2 direction = touch.position - fingerStartPos;
						Vector2 swipeType = Vector2.zero;
						
						if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
							// the swipe is horizontal:
							swipeType = Vector2.right * Mathf.Sign(direction.x);
						}else{
							// the swipe is vertical:
							swipeType = Vector2.up * Mathf.Sign(direction.y);
						}
						
						if(swipeType.x != 0.0f){
							if(swipeType.x > 0.0f){
								SlideEast();
							}else{
								SlideWest();
							}
						}
						
						if(swipeType.y != 0.0f ){
							if(swipeType.y > 0.0f){
								SlideNorth();
							}else{
								SlideSouth();
							}
						}
						
					}
					break; //break on TouchPhase.Ended
				}
			}
		}
	}

	public void PlayerTriggeredReset() {
		scoreKeeper.resetScore();
		Reset();
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
		scoreKeeper.addMove();
		updatePosition();
	}

	private void updatePosition() {
		cc.transform.position = new Vector3(currentNodePosition.X * mapController.rockSpacing, 0, currentNodePosition.Y * mapController.rockSpacing);
	}

}
