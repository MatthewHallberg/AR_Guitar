using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LessonBehavior : MonoBehaviour {

	public Text messageText;
	private int currNote = 0;

	private void Start () {
		NewNote ();
	}

	void NewNote () {
		GameObject thisNote = transform.GetChild (currNote).gameObject;
		messageText.text = "Play: " + thisNote.name;
		thisNote.SetActive (true);
	}

	public void GotNote () {
		StartCoroutine (GotNoteRoutine ());
	}

	IEnumerator GotNoteRoutine () {
		messageText.text = "Great job!";
		yield return new WaitForSeconds (2f);
		transform.GetChild (currNote).gameObject.SetActive (false);
		if (currNote == 2) {
			currNote = 0;
		} else {
			currNote++;
		}
		NewNote ();
	}
}
