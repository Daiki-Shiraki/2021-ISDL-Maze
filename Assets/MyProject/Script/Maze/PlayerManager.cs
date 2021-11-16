using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{
    int count;
    AudioSource getSE;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        getSE = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            getSE.Play();
        }
    }

}
