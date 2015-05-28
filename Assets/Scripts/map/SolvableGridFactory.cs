using UnityEngine;
using System.Collections;

public class SolvableGridFactory : MonoBehaviour {

	private SolvableGridFactory(){}

	public static GameGrid newSolvableGrid(int mapSize, int difficulty) {
		GameGrid grid = new GameGrid(mapSize, mapSize);
		string solution = "";

		while((solution = MakeAndSolveGrid(grid)).Length < difficulty);

		print("Made a new grid with solution "+ solution);
		return grid;
	}

	private static string MakeAndSolveGrid(GameGrid grid) {
		string solution = "";
		grid.ReplaceWithNewGameGrid();
		while ((solution = Solver.solveGrid(grid)) == null) {
			grid.ReplaceWithNewGameGrid();
		}
		return solution;
	}
}
