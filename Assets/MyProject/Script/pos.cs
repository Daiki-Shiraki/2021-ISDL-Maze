using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class pos : MonoBehaviour
{
    //HMD位置座標
    private Vector3 HMDPosition;

    private Quaternion HMDRotationQ;

    private Vector3 HMDRotation;

    //左コントローラ位置座標
    private Vector3 LeftHandPosition;
    private Quaternion LeftHandRotationQ;
    private Vector3 LeftHandRotation;

    //右コントローラ位置座標
    private Vector3 RightHandPosition;
    private Quaternion RightHandRotationQ;
    private Vector3 RightHandRotation;

    // Update is called once per frame
    void Update()
    {
        HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
        HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
        HMDRotation = HMDRotationQ.eulerAngles;

        LeftHandPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
        LeftHandRotationQ = InputTracking.GetLocalRotation(XRNode.LeftHand);
        LeftHandRotation = LeftHandRotationQ.eulerAngles;

        RightHandPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
        RightHandRotationQ = InputTracking.GetLocalRotation(XRNode.RightHand);
        RightHandRotation = RightHandRotationQ.eulerAngles;


        Debug.Log("HMDP:" + HMDPosition.x + ", " + HMDPosition.y + ", " + HMDPosition.z + "\n" +
                    "HMDR:" + HMDRotation.x + ", " + HMDRotation.y + ", " + HMDRotation.z);
        Debug.Log("LFHP:" + LeftHandPosition.x + ", " + LeftHandPosition.y + ", " + LeftHandPosition.z + "\n" +
                    "LFHR:" + LeftHandRotation.x + ", " + LeftHandRotation.y + ", " + LeftHandRotation.z);
        Debug.Log("RGHP:" + RightHandPosition.x + ", " + RightHandPosition.y + ", " + RightHandPosition.z + "\n" +
                    "RGHR:" + RightHandRotation.x + ", " + RightHandRotation.y + ", " + RightHandRotation.z);
    }
}
