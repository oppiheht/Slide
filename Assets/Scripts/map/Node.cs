using UnityEngine;
using System.Collections;

public class Node {

	public const int EMPTY = 0;
	public const int WALL = 1;
	public const int START = 2;
	public const int END = 3;

	private int _type;
	private int _x;
	private int _y;

	public Node(int x, int y, int type) {
		_type = type;
		_x = x;
		_y = y;
	}

	public int Type {
		get{return _type;} 
		set{_type = value;}
	}

	public int X {
		get{return _x;}
		set{_x = value;}
	}

	public int Y {
		get{return _y;}
		set{_y = value;}
	}
}
