using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
namespace fov
{

    public class limited_fov : MonoBehaviour
    {
        // Start is called before the first frame update
        PostProcessVolume processVolume;

        Coroutine coroutine;

        [SerializeField]
        [Range(0.01f, 0.05f)]
        float tunnelSpeed = 0.01f;


        void Start()
        {
            processVolume = this.gameObject.GetComponent<PostProcessVolume>();
        }

        void Update()
        {
            Debug.Log("input anykey is " + Input.anyKey);
            //キー押してない間はreturn
            if (Input.anyKey == false)
            {
                return;
            }
            Debug.Log("weight is "+processVolume.weight);
            if (coroutine == null)
            {
                //テスト用　トンネル開始
                Debug.Log(processVolume.weight);

                if (Input.GetKeyDown(KeyCode.Space) && processVolume.weight == 0)
                {
                    coroutine = StartCoroutine(Tunnel_ON_Coroutine());
                    Debug.Log("aaa-----------------------");
                }

                //テスト用　トンネル解除
                if (Input.GetKeyDown(KeyCode.Backspace) && processVolume.weight == 1)
                {
                    coroutine = StartCoroutine(Tunnel_OFF_Coroutine());
                }

            }

        }

        //トンネル開始
        public void Tunnel_ON()
        {
            if (processVolume.weight == 0)
            {
                coroutine = StartCoroutine(Tunnel_ON_Coroutine());
            }

        }

        //トンネル解除
        public void Tunnel_OFF()
        {
            if (processVolume.weight == 1)
            {
                coroutine = StartCoroutine(Tunnel_OFF_Coroutine());
            }

        }


        IEnumerator Tunnel_ON_Coroutine()
        {
            while (processVolume.weight < 1)
            {
                yield return new WaitForSeconds(tunnelSpeed);
                processVolume.weight += 0.01f;
            }

            processVolume.weight = 1;
            StopCoroutine(coroutine);
            coroutine = null;
        }

        IEnumerator Tunnel_OFF_Coroutine()
        {
            while (processVolume.weight > 0)
            {
                yield return new WaitForSeconds(tunnelSpeed);
                processVolume.weight -= 0.01f;
            }

            processVolume.weight = 0;
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}

