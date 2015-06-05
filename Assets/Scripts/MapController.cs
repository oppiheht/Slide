using UnityEngine;
using System.Collections.Generic;

public class MapController : MonoBehaviour {

	public GameObject rockInstatiate;
	public PlayerMovementController player;
	public Transform goal;

	public float rockSpacing = 2.0f;

	public GameGrid currentGameGrid;
	private Queue<GameObject> pool = new Queue<GameObject>();
	private List<GameObject> currentRocksInMap = new List<GameObject>();

	// Use this for initialization
	void Start () {
		GenerateNewMap();
	}

	public void GenerateNewMap() {
		ClearOldMap();
		currentGameGrid = SolvableGridFactory.newSolvableGrid(10, 8);
		player.Reset();
		print(currentGameGrid.ToString());
		PlaceGrid(currentGameGrid);
	}

	public void ClearOldMap() {
		foreach (GameObject o in currentRocksInMap) {
			pool.Enqueue(o);
			o.transform.position = new Vector3(0, -100, 0);
		}
		currentRocksInMap.Clear();
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
			currentRocksInMap.Add(rock);
		}
		else {
			GameObject newRock = Instantiate(rockInstatiate, position, rotation) as GameObject;
			currentRocksInMap.Add(newRock);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
