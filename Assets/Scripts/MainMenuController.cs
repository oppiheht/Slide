using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public void StartGameButtonHandler() {
		Application.LoadLevel("Main");
	}

	public void ExitGameButtonHandler() {
		Application.Quit();
	}

}
