using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotTrail : MonoBehaviour {

	public PitchDetector pitchDetector;
	bool shouldMove = false;
	Vector3 startPosition = Vector3.zero;

	private void Start () {
		if (startPosition == Vector3.zero) {
			startPosition = transform.localPosition;
		} else {
			transform.localPosition = startPosition;
		}
	}

	private void OnDisable () {
		shouldMove = false;
		GetComponent<TrailRenderer> ().emitting = false;
		transform.localPosition = startPosition;
		print ("reset");
	}

	// Update is called once per frame
	void Update () {

		if (pitchDetector.note.Contains (this.name) && !shouldMove){
			GotNote ();
		}

		if (shouldMove) {
			transform.Translate (Vector3.right * 1);
		}
	}

	public void GotNote () {
		GetComponent<TrailRenderer> ().emitting = true;
		shouldMove = true;
		transform.parent.GetComponent<LessonBehavior> ().GotNote ();
	}
}
