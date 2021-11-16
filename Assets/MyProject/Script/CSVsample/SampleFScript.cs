using UnityEngine;
using System.IO;

public class SampleFScript : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            SampleSaveCsvScript.SaveData("F", " ", time.ToString());
        }
    }
}