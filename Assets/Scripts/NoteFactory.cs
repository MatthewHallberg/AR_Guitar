using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFactory : MonoBehaviour{
	
	public GameObject NotePrefab;

	public void CreateNote(string note){
		GameObject noteObject = Instantiate (NotePrefab);
		noteObject.GetComponent<NoteBehavior> ().SetText (note);
		Destroy (noteObject, 1f);
	}
}
