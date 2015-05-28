using UnityEngine;
using System.Collections;

public class GameGrid {

	public static readonly float wallDensity = 0.15f;
	private Node[,] grid;
	public int rows;
	public int cols;

	public GameGrid(int playableRows, int playableColumns) {
		this.rows = playableRows+2;
		this.cols = playableColumns+2;
		grid = new Node[rows,cols];
		ReplaceWithNewGameGrid();
	}

	public void ReplaceWithNewGameGrid() {
		for (int i = 0; i < rows; i++) {
 			for (int j = 0; j < cols; j++) {
				if (i == 0 || j == 0 || i == rows-1 || j == cols-1) {
					grid[i, j] = new Node(i, j, Node.WALL);
				} else {
					grid[i, j] = new Node(i, j, Random.value < wallDensity ? Node.WALL : Node.EMPTY);
				}
			}
		}
		grid[0, (int)(Random.value*(cols-2)+1)].Type = Node.END;
		grid[rows-1, (int)(Random.value*(cols-2)+1)].Type = Node.START;
	}

	public Node GetStartNode() {
		for (int i = 0; i < cols; i++) {
			if (grid[rows-1, i].Type == Node.START) {
				return grid[rows-1, i];
			}
		}
		throw new System.Exception("Start node not found!");
	}

	public Node GetEndNode() {
		for (int i = 0; i < rows; i++) {
			if (grid[0, i].Type == Node.START) {
				return grid[0, i];
			}
		}
		throw new System.Exception("End node not found!");
	}

	public override string ToString() {
		string s = "";

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < cols; j++) {
				switch (grid[i, j].Type) {
				case Node.EMPTY: s+=" ";
					break;
				case Node.WALL: s+= "X";
					break;
				case Node.START: s+= "S";
					break;
				case Node.END: s+= "E";
					break;
				}
			}
			s+="\n";
		}
		return s;
	}

	public Node GetNodeAt(int x, int y) {
		if (x >= rows || y >= cols || x < 0 || y < 0) {
			return null;
		}
		return grid[x, y];
	}
}
