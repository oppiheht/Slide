using UnityEngine;
using System.Collections;

public class GoalTrigger : MonoBehaviour {

	public MapController mapController;

	void Start () {
		mapController = GameObject.Find("Terrain").GetComponent<MapController>();
	}

	void OnTriggerEnter(Collider other) {
		print ("success!");
		mapController.GenerateNewMap();
	}
}
