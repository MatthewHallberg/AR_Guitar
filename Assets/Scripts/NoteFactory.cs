using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFactory : MonoBehaviour{
	
	public GameObject NotePrefab;
	private int noteCount = 0;

	public void CreateNote(string note){
		GameObject noteObject = Instantiate (NotePrefab);
		noteObject.GetComponent<NoteBehavior> ().SetText (note);
		//make notes stagger on screen
		Vector3 tempPosition = noteObject.transform.position;
		tempPosition.y = -2f;
		noteCount++;
		if (noteCount % 3 == 0) {
			tempPosition.x = -2f;
		} else if (noteCount % 2 == 0) {
			tempPosition.x = 0f;
		} else {
			tempPosition.x = 2f;
		}
		noteObject.transform.position = tempPosition;
		Destroy (noteObject, 1f);
	}
}
