using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class PitchDetector : MonoBehaviour
{
    [DllImport("AudioPluginDemo")]
    private static extern float PitchDetectorGetFreq(int index);

    [DllImport("AudioPluginDemo")]
    private static extern int PitchDetectorDebug(float[] data);

    float[] history = new float[1000];
    float[] debug = new float[65536];

    string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

	public NoteFactory noteFactory;
    public string frequency = "detected frequency";
    public string note = "detected note";
    public AudioMixer mixer;

	private float lastFreq;
	private List<float> noteVals = new List<float> ();
	private float frameCount = 0;
	private bool freqChanged = false;

	// Update is called once per frame
	void Update () {
		float freq = PitchDetectorGetFreq (0), deviation = 0.0f;
		frequency = freq.ToString () + " Hz";

		//when frequency changes average new frequency skipping frames for transition
		if (!freq.Equals (lastFreq)) {
			freqChanged = true;
		}

		if (freqChanged) {
			frameCount++;
		}
		if (frameCount > 2) {
			float noteval = 57.0f + 12.0f * Mathf.Log10 (freq / 440.0f) / Mathf.Log10 (2.0f);
			noteVals.Add (noteval);
			//every x frames calculate note
			if (noteVals.Count > 5) {
				noteval = noteVals.Average ();
				int octave = (int)Mathf.Floor ((noteval + 0.5f) / 12.0f);
				float f = Mathf.Floor (noteval + 0.5f);
				deviation = Mathf.Floor ((noteval - f) * 100.0f);
				int noteIndex = (int)f % 12;
				note = noteNames [noteIndex] + " " + octave;
				Debug.Log ("Detected frequency: " + frequency + "\nDetected note: " + note + " (deviation: " + deviation + " cents)");
				noteFactory.CreateNote (note);
				//reset states to detect next note
				noteVals.Clear ();
				freqChanged = false;
				frameCount = 0;
			}
		}
		lastFreq = freq;
	}
}
