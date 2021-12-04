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

            public class EyeCheck : MonoBehaviour
            {
                private float time, shortTime;
                private float timeOut = 1.0f;
                private float timeshort = 0.1f;
                private float blinkcount = 0;
                private float blinktemp = 0;
                private float blinktempl = 0;
                private string blinked = "null";
                private int n = 1;

                GameObject SaveBlinkCsv, SaveRegularCsv, SaveOneSecond;
                SaveBlinkCsvScript SaveBlinkCsvScript;
                SaveRegularCsvScript SaveRegularCsvScript;
                SaveOneSecondCsv SaveOneSecondCsv;
                float BlinkFrag = 1;

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
                    SaveRegularCsv = GameObject.Find("SaveRegularCsv");
                    SaveRegularCsvScript = SaveRegularCsv.GetComponent<SaveRegularCsvScript>();
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
                            if (BlinkFrag == 0)
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
                    if (shortTime >= timeshort)
                    {
                        SaveRegularCsvScript.SaveData(Time.time.ToString(), LeftPupil.x.ToString(), LeftPupil.y.ToString(), RightPupil.x.ToString(), RightPupil.y.ToString(), LeftpupilSize.ToString(), RightpupilSize.ToString(), LeftBlink.ToString(), RightBlink.ToString(), blinked);
                        //SaveBlinkCsvScript.SaveData(Time.time.ToString(), blinkcount.ToString(), n.ToString(), blinktempl.ToString());
                        shortTime = 0.0f;
                        blinked = "null";
                        if (Time.time > n)
                        {
                            blinktempl = 0;
                            n++;
                        }
                    }
                    //右のまぶたの開き具合を取得
                    //------------------------------




                }
            }
        }
    }
}
