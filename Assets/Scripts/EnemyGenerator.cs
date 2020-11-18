using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject bacteria;
    private GameObject bacteriaClone;
    
    public GameObject coronavirus;
    private GameObject coronavirusClone;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Using FixedUpdate() because WebGL seems to have a lower frame rate
    void FixedUpdate()
    {
        if (GameManager.playGame && GameManager.enemyToBeGen > 0 && Random.Range(0f, GameManager.enemyFreq) < 1)
        {
            if (Random.Range(0f, 4*GameManager.enemyFreq) < 1) //the corona virus appears once in a while
            {
                audioManager.Play("Alarm");
                Invoke("createCorona", 2.5f);
            }
            else
            {
                bacteriaClone = Instantiate(bacteria, new Vector3(transform.position.x, Random.Range(-4.5f, 4.4f), 0f), 
                                            transform.rotation) as GameObject;
            }
            GameManager.enemyToBeGen--;
        }
    }

    void createCorona()
    {
        if (GameManager.playGame)
        {
            coronavirusClone = Instantiate(coronavirus, new Vector3(transform.position.x, Random.Range(-4.5f, 4.4f), 0f), 
                                           transform.rotation) as GameObject;
        }
    }
}