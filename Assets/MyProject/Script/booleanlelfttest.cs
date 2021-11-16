using System;
using UnityEngine;
using Valve.VR;

public class booleanlelfttest : MonoBehaviour
{
    
        private SteamVR_Action_Boolean Iui =SteamVR_Actions.default_InteractUI;

        private Boolean interacrtui;

        private SteamVR_Action_Boolean GrabG = SteamVR_Actions.default_GrabGrip;

        private Boolean grapgrip;
    

    // Update is called once per frame
    void Update()
    {
        interacrtui = Iui.GetState(SteamVR_Input_Sources.LeftHand);

        if (interacrtui)
        {
            Debug.Log("InteractUI");
        }

        grapgrip = GrabG.GetState(SteamVR_Input_Sources.LeftHand);
        if (grapgrip)
        {
            Debug.Log("GrabGrip");
        }

        // Debug.Log(interacrtui);
    }
}
