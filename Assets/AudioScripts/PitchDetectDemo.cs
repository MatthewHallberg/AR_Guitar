using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class PitchDetectDemo : MonoBehaviour
{
    [DllImport("AudioPluginDemo")]
    private static extern float PitchDetectorGetFreq(int index);

    [DllImport("AudioPluginDemo")]
    private static extern int PitchDetectorDebug(float[] data);

    float[] history = new float[1000];
    float[] debug = new float[65536];

    string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

    public Material mat;
    public string frequency = "detected frequency";
    public string note = "detected note";
    public AudioMixer mixer;

    public Text text;

    // Update is called once per frame
    void Update()
    {
        float freq = PitchDetectorGetFreq(0), deviation = 0.0f;
        frequency = freq.ToString() + " Hz";

        if (freq > 0.0f)
        {
            float noteval = 57.0f + 12.0f * Mathf.Log10(freq / 440.0f) / Mathf.Log10(2.0f);
            float f = Mathf.Floor(noteval + 0.5f);
            deviation = Mathf.Floor((noteval - f) * 100.0f);
            int noteIndex = (int)f % 12;
            int octave = (int)Mathf.Floor((noteval + 0.5f) / 12.0f);
            note = noteNames[noteIndex] + " " + octave;
        }
        else
        {
            note = "unknown";
        }

        if (text != null)
            text.text = "Detected frequency: " + frequency + "\nDetected note: " + note + " (deviation: " + deviation + " cents)";
    }
}
