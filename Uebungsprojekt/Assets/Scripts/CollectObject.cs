using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectObject : MonoBehaviour
{
    private GameObject player;
    private GameObject interactInfo;
    public GameObject UIObject;

    private AudioSource collectItemAudioSource;
    private CreateCollectibles objectScript;
    private Winning winScript;

    private int count;

    private void Awake()
    {
        player = GameObject.Find("Player");
        interactInfo = GameObject.Find("InGame UI").transform.GetChild(0).gameObject;
        objectScript = GameObject.Find("SpawnHandler").GetComponent<CreateCollectibles>();
        winScript = GameObject.Find("MenueHandler").GetComponent<Winning>();

        count = objectScript.count;

        //interactInfo.SetActive(false);
    
        collectItemAudioSource = GameObject.Find("ItemFound").GetComponent<AudioSource>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            interactInfo.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Destroy(gameObject);
                interactInfo.SetActive(false);
                //UIObject.SetActive(true);

                winScript.AddScore();
                collectItemAudioSource.Play();
                objectScript.uiImage[count].gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}
