using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(TextMesh))]
public class NoteBehavior : MonoBehaviour
{
	public TextMesh tm;

	// Update is called once per frame
	void Update (){
		transform.Translate(Vector3.up * Time.deltaTime * 8f);
	}

	public void SetText (string noteText) {
		tm.text = noteText;
	}
}
