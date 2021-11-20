using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class SaveRegularCsvScript : MonoBehaviour
{
    private StreamWriter sw;
    private string date;
    void Start()
    {
        date = DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日" + DateTime.Now.Hour.ToString() + "時" + DateTime.Now.Minute.ToString() + "分";
        sw = new StreamWriter(@"SaveRegularData" + date + ".csv", true, Encoding.GetEncoding("Shift_JIS"));
        //string[] s1 = {"time","LeftPupil X", "LeftPupil Y", "RightPupil X", "RightPupil Y","LeftBlink","RightBlink","CombineFocusPoint X", "CombineFocusPoint Y" , "CombineFocusPoint Z" };

        string[] s1 = { "time", "LeftPupil X", "LeftPupil Y", "RightPupil X", "RightPupil Y","PupilSize", "LeftBlink", "RightBlink"};
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }

    public void SaveData(string txt1, string txt2, string txt3, string txt4, string txt5, string txt6, string txt7, string txt8)
    {
        string[] s1 = { txt1, txt2,txt3,txt4,txt5,txt6,txt7,txt8 };
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
