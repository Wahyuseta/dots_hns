using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class BeatDetectorManager : MonoBehaviour
{
	public SimpleBeatDetection beatProcessor;

	List<float> times = new List<float>();
	public float smoothnessChange;
	float timer = 0f;
	public float timeku = 2f;

	void Start()
	{
		beatProcessor.OnBeat += OnBeat;
	}

	void Update()
	{
        if (beatProcessor.audioSource.isPlaying)
        {
			timeku += Time.deltaTime;
        }

		timer += Time.deltaTime;
	}

	void OnBeat()
    {
        if (timer < .2f)
            return;
		timer = 0;

		Debug.Log(timeku);
	}

	public void LogTime()
    {
		Debug.Log(timeku);
    }

	public void ReadString()
	{
		string path = "Assets/FurEliseNote.txt";
		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader(path);

		string elise = "start";

		while (!string.IsNullOrEmpty(elise))
		{
			elise = reader.ReadLine();

			if (!string.IsNullOrEmpty(elise))
			{
				times.Add(float.Parse(elise, System.Globalization.NumberStyles.Float));
			}
		}

		reader.Close();
	}
}