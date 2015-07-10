using UnityEngine;
using System.Collections;

public class GoalTrigger : MonoBehaviour {

	public MapController mapController;
	public ScoreKeeper scoreKeeper;

	void Start () {
		//wired in unity
		//mapController = GameObject.Find("Terrain").GetComponent<MapController>();
	}

	void OnTriggerEnter(Collider other) {
		scoreKeeper.addScore(ScoreKeeper.LEVEL_COMPLETE_VALUE);
		scoreKeeper.resetMapMoves();
		mapController.GenerateNewMap();
	}
}
