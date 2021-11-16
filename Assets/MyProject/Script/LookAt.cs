using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {

            public class LookAt : MonoBehaviour
            {
                private Ray ray;
                public int LengthOfRay = 25;
                // Start is called before the first frame update
                void Start()
                {

                }

                // Update is called once per frame
                void Update()
                {
                    Vector3 GazeOriginCombinedLocal, GazeDirectionCobinedLocal;

                    SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCobinedLocal);

                    Vector3 GazeDirectionCombined = Camera.main.transform.TransformDirection(GazeDirectionCobinedLocal);
                    RaycastHit hit;

                    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.position + GazeDirectionCombined * LengthOfRay, out hit))
                    {
                        string objectName = hit.collider.gameObject.name;
                        Debug.Log(objectName + ":" + hit.point.ToString("F2"));
                    }
                }
            }
        }
    }
}
