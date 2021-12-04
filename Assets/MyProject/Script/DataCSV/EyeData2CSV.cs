using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {

            public class EyeData2CSV : MonoBehaviour
            {
                private float time,shortTime;
                private float timeOut = 1.0f;
                private float timeshort = 0.1f;
                private float blinkcount = 0;
                private float blinktemp = 0;
                private float blinktempl = 0;
                private string blinked = "null";
                private int n=1;

                GameObject SaveBlinkCsv,SaveRegularCsv,SaveOneSecond;
                SaveBlinkCsvScript SaveBlinkCsvScript;
                SaveRegularCsvScript SaveRegularCsvScript;
                SaveOneSecondCsv SaveOneSecondCsv;
                float BlinkFrag=1;

                EyeData eye;
                //瞳孔位置
                Vector2 LeftPupil;
                Vector2 RightPupil;

                //瞼の開き具合
                float LeftBlink;
                float RightBlink;

                //視線情報
                //origin:起点情報、direction:例の方向(3vector)
                Vector3 CombineGazeRayorigin;
                Vector3 CombineGazeRaydirection;

                Vector3 LeftGazeRayorigin;
                Vector3 LeftGazeRaydirection;

                Vector3 RightGazeRayorigin;
                Vector3 RightGazeRaydirection;

                //焦点情報
                Ray CombineRay;

                public static FocusInfo CombineFocus;

                float CombineFocusradius;

                float CombineFocusmaxDisatance;

                int CombinefocusableLayer = 0;

                // Update is called once per frame
                void Start()
                {
                    SaveBlinkCsv = GameObject.Find("SaveBlinkCsv");
                    SaveBlinkCsvScript = SaveBlinkCsv.GetComponent<SaveBlinkCsvScript>();
                    SaveRegularCsv = GameObject.Find("SaveRegularCsv");
                    SaveRegularCsvScript = SaveRegularCsv.GetComponent<SaveRegularCsvScript>();
                    SaveOneSecond = GameObject.Find("SaveOneSecond");
                    SaveOneSecondCsv = SaveOneSecond.GetComponent<SaveOneSecondCsv>();
                }
                void Update()
                {
                    time += Time.deltaTime;
                    shortTime += Time.deltaTime; 

                    if (SRanipal_Eye_API.GetEyeData(ref eye) == ViveSR.Error.WORK)
                    {

                    }
                    SRanipal_Eye_API.GetEyeData(ref eye);

                    //①瞳孔位置---------------------（HMDを被ると検知される，目をつぶっても位置は返すが，HMDを外すとと止まる．
                    //目をつぶってるときはどこの値返してんのか謎．一応まぶた貫通してるっぽい？？？）
                    //左の瞳孔位置を取得
                    if (SRanipal_Eye.GetPupilPosition(EyeIndex.LEFT, out LeftPupil))
                    {
                        //値が有効なら左の瞳孔位置を表示
                        Debug.Log("Left Pupil" + LeftPupil.x + ", " + LeftPupil.y);
                    }
                    //右の瞳孔位置を取得
                    if (SRanipal_Eye.GetPupilPosition(EyeIndex.RIGHT, out RightPupil))
                    {
                        //値が有効なら右の瞳孔位置を表示
                        Debug.Log("Right Pupil" + RightPupil.x + ", " + RightPupil.y);
                    }
                    //------------------------------
                    //瞳孔径を出力
                    float LeftpupilSize = eye.verbose_data.left.pupil_diameter_mm;
                    float RightpupilSize = eye.verbose_data.right.pupil_diameter_mm;
                    //②まぶたの開き具合------------（HMDを被ってなくても1が返ってくる？？謎）
                    //左のまぶたの開き具合を取得
                    if (SRanipal_Eye.GetEyeOpenness(EyeIndex.LEFT, out LeftBlink, eye))
                    {
                        if (BlinkFrag != LeftBlink)
                        {
                            BlinkFrag = LeftBlink;
                            if (BlinkFrag==0)
                            {
                                blinked = "blinked";
                                blinkcount++;
                                blinktemp++;
                                blinktempl++;
                                
                            }
                        }
                        //値が有効なら左のまぶたの開き具合を表示
                        Debug.Log("Left Blink" + LeftBlink);

                    }
                    //右のまぶたの開き具合を取得
                    if (SRanipal_Eye.GetEyeOpenness(EyeIndex.RIGHT, out RightBlink, eye))
                    {
                        //値が有効なら右のまぶたの開き具合を表示
                        Debug.Log("Right Blink" + RightBlink);
                    }
                    //------------------------------


                    //③視線情報--------------------（目をつぶると検知されない）
                    //両目の視線情報が有効なら視線情報を表示origin：起点，direction：レイの方向
                    if (SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out CombineGazeRayorigin, out CombineGazeRaydirection, eye))
                    {
                        Debug.Log("COMBINE GazeRayorigin" + CombineGazeRayorigin.x + ", " + CombineGazeRayorigin.y + ", " + CombineGazeRayorigin.z);
                        Debug.Log("COMBINE GazeRaydirection" + CombineGazeRaydirection.x + ", " + CombineGazeRaydirection.y + ", " + CombineGazeRaydirection.z);
                    }

                    //左目の視線情報が有効なら視線情報を表示origin：起点，direction：レイの方向
                    if (SRanipal_Eye.GetGazeRay(GazeIndex.LEFT, out LeftGazeRayorigin, out LeftGazeRaydirection, eye))
                    {
                        Debug.Log("Left GazeRayorigin" + LeftGazeRayorigin.x + ", " + LeftGazeRayorigin.y + ", " + LeftGazeRayorigin.z);
                        Debug.Log("Left GazeRaydirection" + LeftGazeRaydirection.x + ", " + LeftGazeRaydirection.y + ", " + LeftGazeRaydirection.z);
                    }


                    //右目の視線情報が有効なら視線情報を表示origin：起点，direction：レイの方向
                    if (SRanipal_Eye.GetGazeRay(GazeIndex.RIGHT, out RightGazeRayorigin, out RightGazeRaydirection, eye))
                    {
                        Debug.Log("Right GazeRayorigin" + RightGazeRayorigin.x + ", " + RightGazeRayorigin.y + ", " + RightGazeRayorigin.z);
                        Debug.Log("Right GazeRaydirection" + RightGazeRaydirection.x + ", " + RightGazeRaydirection.y + ", " + RightGazeRaydirection.z);
                    }
                    //------------------------------

                    //④焦点情報--------------------
                    //radius, maxDistance，CombinefocusableLayerは省略可
                    if (SRanipal_Eye.Focus(GazeIndex.COMBINE, out CombineRay, out CombineFocus/*, CombineFocusradius, CombineFocusmaxDistance, CombinefocusableLayer*/))
                    {
                        Debug.Log("Combine Focus Point" + CombineFocus.point.x + ", " + CombineFocus.point.y + ", " + CombineFocus.point.z);
                    }
                    //------------------------------
                    if (shortTime >=timeshort)
                    { 
                        SaveRegularCsvScript.SaveData(Time.time.ToString(),LeftPupil.x.ToString(),LeftPupil.y.ToString(),RightPupil.x.ToString(),RightPupil.y.ToString(),LeftpupilSize.ToString(), RightpupilSize.ToString(), LeftBlink.ToString(),RightBlink.ToString(),blinked);
                        SaveBlinkCsvScript.SaveData(Time.time.ToString(), blinkcount.ToString(), n.ToString(),blinktempl.ToString());
                        shortTime = 0.0f;
                        blinked = "null";
                        if (Time.time > n)
                        {
                            blinktempl = 0;
                            n++;
                        }
                    }
                    if (time >= timeOut)
                    {
                        int i = (int)(Time.time);
                        SaveOneSecondCsv.SaveData(i.ToString(), LeftPupil.x.ToString(), LeftPupil.y.ToString(), RightPupil.x.ToString(), RightPupil.y.ToString(), LeftpupilSize.ToString(), RightpupilSize.ToString(), LeftBlink.ToString(), RightBlink.ToString(),blinktemp.ToString());
                        time = 0.0f;
                        blinktemp = 0;
                        
                    }
                
                }
            }
        }
    }
}
