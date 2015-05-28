using UnityEngine;
using System.Collections.Generic;

public class MapController : MonoBehaviour {

	public GameObject rockInstatiate;
	public PlayerMovementController player;
	public Transform goal;

	public float rockSpacing = 2.0f;

	private Queue<GameObject> pool = new Queue<GameObject>();
	private List<GameObject> currentMap = new List<GameObject>();

	private int rockCount = 0;

	// Use this for initialization
	void Start () {
		GenerateNewMap();
	}

	public void GenerateNewMap() {
		ClearOldMap();
		GameGrid gg = SolvableGridFactory.newSolvableGrid(10, 8);
		print(gg.ToString());
		PlaceGrid(gg);
	}

	public void ClearOldMap() {
		foreach (GameObject o in currentMap) {
			pool.Enqueue(o);
			o.transform.position = new Vector3(0, -100, 0);
		}
		currentMap.Clear();
	}

	private void PlaceGrid(GameGrid gg) {
		for (int i = 0; i < gg.rows; i++) {
			for (int j = 0; j < gg.cols; j++) {
				switch(gg.GetNodeAt(i, j).Type) {
				case Node.WALL:
					PlaceRock(new Vector3(i*rockSpacing, 0, j*rockSpacing), Quaternion.identity);
					break;
				case Node.START:
					player.transform.position = new Vector3(i*rockSpacing, 0, j*rockSpacing);
					break;
				case Node.END:
					goal.position = new Vector3(i*rockSpacing, 0, j*rockSpacing);
					break;
				}
			}
		}
	}

	private void PlaceRock(Vector3 position, Quaternion rotation) {
		if (pool.Count > 0) {
			GameObject rock = pool.Dequeue();
			rock.transform.position = position;
			rock.transform.rotation = rotation;
			currentMap.Add(rock);
		}
		else {
			print("created new rock! new rock count is "+ rockCount++);
			GameObject newRock = Instantiate(rockInstatiate, position, rotation) as GameObject;
			currentMap.Add(newRock);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
