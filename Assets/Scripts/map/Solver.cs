using UnityEngine;
using System.Collections.Generic;

public class Solver {

	public const int NORTH = 0;
	public const int EAST = 1;
	public const int SOUTH = 2;
	public const int WEST = 3;

	/**
	 * Attempts to solve the given game grid. A grid is solveable if some
	 * combination of moves going North, East, South, or West move the character
	 * in a sliding motion from the start to the end. Will return -1 if the
	 * GameGrid is unable to be solved.
	 * 
	 * The solver relies on the fact that the border of the GameGrid consists
	 * only of walls, start, and end nodes.
	 * 
	 * @param grid The game grid to attempt to solve.
	 * @return The number of moves to solve the given grid, or -1 if the grid is
	 *         unable to be solved.
	 */
	public static string solveGrid(GameGrid grid) {
		HashSet<Node> visited = new HashSet<Node>();
		NodePath currentNodePath;
		Node currentNode;

		Queue<NodePath> fringe = new Queue<NodePath>();
		fringe.Enqueue (new NodePath(grid.GetStartNode(), ""));

		while (fringe.Count > 0) {
			currentNodePath = fringe.Dequeue();
			currentNode = currentNodePath.node;
			if (currentNode.Type == Node.END) {
				return currentNodePath.path;
			}
			if (!visited.Contains(currentNode)) {
				visited.Add(currentNode);
				fringe.Enqueue(new NodePath(SlideDirection(NORTH, grid, currentNode), currentNodePath.path + "N"));
				fringe.Enqueue(new NodePath(SlideDirection(EAST, grid, currentNode), currentNodePath.path + "E"));
				fringe.Enqueue(new NodePath(SlideDirection(SOUTH, grid, currentNode), currentNodePath.path + "S"));
				fringe.Enqueue(new NodePath(SlideDirection(WEST, grid, currentNode), currentNodePath.path + "W"));

			}
		}

		return null; //not solvable
	}

	/**
	 * Returns the node that is in the given direction in a sliding motion from
	 * the given start node on the given grid.
	 * 
	 * @param direction The direction to slide.
	 * @param grid The game grid to move on.
	 * @param start The node to start the move from.
	 * 
	 * @return The node the character will end at.
	 */
	public static Node SlideDirection(int direction, GameGrid gameGrid, Node start) {
		Node currentNode = start;
		Node nextNodeInDirection = getNodeInDirection(direction, gameGrid, currentNode);

		//if we hit a wall, return the node we are at
		if (nextNodeInDirection == currentNode) {
			return currentNode;
		}

		//simulate sliding on the floor until a wall or the end is hit
		while (nextNodeInDirection != null && nextNodeInDirection.Type != Node.WALL) {
			if (nextNodeInDirection.Type == Node.END) {
				return nextNodeInDirection;
			}
			currentNode = nextNodeInDirection;
			nextNodeInDirection = getNodeInDirection(direction, gameGrid, currentNode);
		}

		return currentNode;
	}

	private static Node getNodeInDirection(int direction, GameGrid gameGrid, Node start) {
		Node nextNode = null;
		switch (direction) {
		case NORTH:
			nextNode = gameGrid.GetNodeAt(start.X - 1, start.Y);
			break;
		case EAST:
			nextNode = gameGrid.GetNodeAt(start.X, start.Y + 1);
			break;
		case SOUTH:
			nextNode = gameGrid.GetNodeAt(start.X + 1, start.Y);
			break;
		case WEST:
			nextNode = gameGrid.GetNodeAt(start.X, start.Y - 1);
			break;
		}
		return nextNode;
	}

	private class NodePath {
		public Node node;
		public string path;

		public NodePath(Node node, string path) {
			this.node = node;
			this.path = path;
		}
	}
}

