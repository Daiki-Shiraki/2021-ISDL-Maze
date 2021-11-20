using System.IO;
using System.Text;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SaveBlinkCsvScript : MonoBehaviour
{
    private StreamWriter sw;
    private string date;

    void Start()
    {
        date = DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日" + DateTime.Now.Hour.ToString() + "時" + DateTime.Now.Minute.ToString() + "分";
        //ClockText = GetComponentInChildren<Text>();
        //ClockText.text = DateTime.Now.ToShortTimeString();
        sw = new StreamWriter(@"SaveBlinkData"+ date + ".csv", true, Encoding.GetEncoding("Shift_JIS"));
        string[] s1 = { "Blink", "time","count" };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }

    public void SaveData(string txt1, string txt2,string txt3)
    {
        string[] s1 = { txt1, txt2,txt3 };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            sw.Close();
        }

    }
}