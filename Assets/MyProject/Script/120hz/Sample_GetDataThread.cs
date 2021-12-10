// Sample_GetDataThread.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;
using System.Threading;
using System.IO;
using ViveSR.anipal.Eye;
using Valve.VR;
namespace Test120FPS{    
    public class Sample_GetDataThread : MonoBehaviour    {        
        public EyeData data = new EyeData();        
        private Thread thread;        
        private const int FrequencyControl = 1;        
        private const int MaxFrameCount = 3600;
        GameObject SaveBlinkCsv, SaveRegularCsv, SaveOneSecond;
        SaveBlinkCsvScript SaveBlinkCsvScript;
        SaveRegularCsvScript SaveRegularCsvScript;
        SaveOneSecondCsv SaveOneSecondCsv;

        //HMDの位置座標
        private Vector3 HMDPosition;
        

        //瞼の開き具合
        float LeftBlink;
        float RightBlink;
        float BlinkFrag = 1;
        int BlinkUnder1=0;
        private string blinked = "non";
        //累計瞬き回数
        private float blinkcount = 0;
        //一秒あたりの瞬き回数
        private float blinktemp = 0;

        //瞳孔径
        float LeftpupilSize;
        float RightpupilSize;

        void Start()        
        {
            SaveBlinkCsv = GameObject.Find("SaveBlinkCsv");
            SaveBlinkCsvScript = SaveBlinkCsv.GetComponent<SaveBlinkCsvScript>();
            SaveRegularCsv = GameObject.Find("SaveRegularCsv");
            SaveRegularCsvScript = SaveRegularCsv.GetComponent<SaveRegularCsvScript>();
            SaveOneSecond = GameObject.Find("SaveOneSecond");
            SaveOneSecondCsv = SaveOneSecond.GetComponent<SaveOneSecondCsv>();
            thread = new Thread(QueryEyeData);            
            thread.Start();        
        }        
        private void OnApplicationQuit()        
        {            
            thread.Abort();        
        }        
        private void OnDisable()        
        {            
            thread.Abort();        
        }        
        // You can only use C# native function in Unity's thread.        
        // Use EyeData's frame_sequence to calculate frame numbers and record data in file.
        void QueryEyeData()        
        {            
            int FrameCount = 0;            
            int PrevFrameSequence = 0, CurrFrameSequence = 0;            
            bool StartRecord = false;            
            while (FrameCount < MaxFrameCount)            
            {                
                ViveSR.Error error = SRanipal_Eye_API.GetEyeData(ref data);                
                if (error == ViveSR.Error.WORK)                
                {                    
                    CurrFrameSequence = data.frame_sequence;                    
                    if (CurrFrameSequence != PrevFrameSequence)                    
                    {                        
                        FrameCount++;                        
                        PrevFrameSequence = CurrFrameSequence;                        
                        StartRecord = true;
                        //瞳孔径を出力
                        LeftpupilSize = data.verbose_data.left.pupil_diameter_mm;
                        RightpupilSize = data.verbose_data.right.pupil_diameter_mm;

                        //瞼の開き具合を取得
                        if (SRanipal_Eye.GetEyeOpenness(EyeIndex.LEFT, out LeftBlink, data))
                        {
                            if (BlinkFrag != LeftBlink)
                            {

                                BlinkFrag = LeftBlink;
                                if (BlinkFrag < 0.1 && BlinkUnder1!=1)
                                {
                                    BlinkUnder1 = 1;
                                    blinked = "blinked";
                                    blinkcount++;
                                    blinktemp++;
                                    SaveBlinkCsvScript.SaveData(CurrFrameSequence.ToString(), blinkcount.ToString());
                                    Debug.Log("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                                }
                                if(BlinkFrag > 0.1)
                                {
                                    BlinkUnder1 = 0;
                                }
                            }
                        }
                        SRanipal_Eye.GetEyeOpenness(EyeIndex.RIGHT, out RightBlink, data);
                        SaveRegularCsvScript.SaveData(CurrFrameSequence.ToString(), LeftpupilSize.ToString(), RightpupilSize.ToString(), LeftBlink.ToString(), RightBlink.ToString(), blinked);
                        blinked = "non";
                    }                
                }                
                // Record time stamp every 120 frame.



                if (FrameCount % 120 == 0 && StartRecord)                
                {                    
                    long ms = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;                    
                    string text = "CurrentFrameSequence: " + CurrFrameSequence +                        
                        " CurrentSystemTime(ms): " + ms.ToString() + Environment.NewLine;                    
                    File.AppendAllText("DataRecord.txt", text);                    
                    FrameCount = 0;
                    SaveOneSecondCsv.SaveData((CurrFrameSequence/120).ToString(), LeftpupilSize.ToString(), RightpupilSize.ToString(), LeftBlink.ToString(), RightBlink.ToString(), blinktemp.ToString());
                    blinktemp = 0;
                }                
                Thread.Sleep(FrequencyControl);            
            }        
        }    
    }
}


