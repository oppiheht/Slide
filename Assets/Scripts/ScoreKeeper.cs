using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int LEVEL_COMPLETE_VALUE = 1;

	private int score = 0;
	private int mapMoves = 0;
	private int totalMoves = 0;
	public Text scoreText;
	public Text mapMovesText;
	public Text totalMovesText;

	// Use this for initialization
	void Start () {
		updateScoreText();
	}

	public void addScore(int amountToAdd) {
		score += amountToAdd;
		updateScoreText();
	}

	public void resetScore() {
		score = 0;
		updateScoreText();
	}

	public void addMove() {
		mapMoves++;
		totalMoves++;
		updateScoreText();
	}

	public void resetMapMoves() {
		mapMoves = 0;
		updateScoreText();
	}

	private void updateScoreText() {
		scoreText.text = "Score: " + score;
		mapMovesText.text = "Moves: " + mapMoves;
		totalMovesText.text = "Total Moves: " + totalMoves;
	}

}
