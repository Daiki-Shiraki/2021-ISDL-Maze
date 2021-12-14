using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;
using System.IO;
using System.Text;

public class pos : MonoBehaviour
{
    //HMD位置座標
    private Vector3 HMDPosition;
    float time,onesecond;
    private Quaternion HMDRotationQ;

    private Vector3 HMDRotation;
    //GameObject SaveHMDCsv;

    //左コントローラ位置座標
    //private Vector3 LeftHandPosition;
    //private Quaternion LeftHandRotationQ;
    //private Vector3 LeftHandRotation;

    //右コントローラ位置座標
    //private Vector3 RightHandPosition;
    //private Quaternion RightHandRotationQ;
    //private Vector3 RightHandRotation;
    private StreamWriter sw1,sw2;
    private string date;
    int i = 0;


    void Start()
    {

        date = DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日" + DateTime.Now.Hour.ToString() + "時" + DateTime.Now.Minute.ToString() + "分";
        sw1 = new StreamWriter(@"SaveHMDData" + date + ".csv", true, Encoding.GetEncoding("Shift_JIS"));
        sw2 = new StreamWriter(@"SaveHMDOnesecondData" + date + ".csv", true, Encoding.GetEncoding("Shift_JIS"));
        //string[] s1 = {"time","LeftPupil X", "LeftPupil Y", "RightPupil X", "RightPupil Y","LeftBlink","RightBlink","CombineFocusPoint X", "CombineFocusPoint Y" , "CombineFocusPoint Z" };

        string[] s1 = { "時間", "HMDx座標", "HMDy座標", "HMDz座標"};
        string s2 = string.Join(",", s1);
        sw1.WriteLine(s2);
        sw2.WriteLine(s2);
    }
    
    // Update is called once per frame
    public void SaveData1(string txt1, string txt2, string txt3, string txt4)
    {
        string[] s1 = { txt1, txt2, txt3, txt4};
        string s2 = string.Join(",", s1);
        sw1.WriteLine(s2);
    }
    public void SaveData2(string txt1, string txt2, string txt3, string txt4)
    {
        string[] s1 = { txt1, txt2, txt3, txt4 };
        string s2 = string.Join(",", s1);
        sw2.WriteLine(s2);
    }
    void Update()
    {
        HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
       // HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
       // HMDRotation = HMDRotationQ.eulerAngles;
        SaveData1(Time.time.ToString(), HMDPosition.x.ToString(), HMDPosition.y.ToString(), HMDPosition.z.ToString());
        if ((int)(Time.time) - i <= 1)
        {
            i++;
            SaveData2(i.ToString(), HMDPosition.x.ToString(), HMDPosition.y.ToString(), HMDPosition.z.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            sw1.Close();
            sw2.Close();
        }

        //        LeftHandPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
        //      LeftHandRotationQ = InputTracking.GetLocalRotation(XRNode.LeftHand);
        //    LeftHandRotation = LeftHandRotationQ.eulerAngles;

        //  RightHandPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
        //RightHandRotationQ = InputTracking.GetLocalRotation(XRNode.RightHand);
        //RightHandRotation = RightHandRotationQ.eulerAngles;


    }
}
