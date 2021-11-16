using UnityEngine;
using System.IO;

public class SampleJScript : MonoBehaviour
{
    private float time;
    private StreamWriter sw;

    GameObject SaveCsv;
    SampleSaveCsvScript SampleSaveCsvScript;

    void Start()
    {
        SaveCsv = GameObject.Find("SaveCsv");
        SampleSaveCsvScript = SaveCsv.GetComponent<SampleSaveCsvScript>();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.J))
        {
            SampleSaveCsvScript.SaveData(" ", "J", time.ToString());
        }
    }
}

