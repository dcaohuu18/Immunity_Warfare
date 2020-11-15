using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject bacteria;
    private GameObject bacteriaClone;
    
    public GameObject coronavirus;
    private GameObject coronavirusClone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.playGame && GameManager.enemyToBeGen > 0 && Random.Range(0f, GameManager.enemyFreq) < 1)
        {
            if (Random.Range(0f, 2*GameManager.enemyFreq) < 1) //the corona virus appears once in a while
            // For WebGL: 3*GameManager.enemyFreq
            {
                coronavirusClone = Instantiate(coronavirus, new Vector3(transform.position.x, Random.Range(-4.5f, 4.4f), 0f), 
                                               transform.rotation) as GameObject;
            }
            else
            {
                bacteriaClone = Instantiate(bacteria, new Vector3(transform.position.x, Random.Range(-4.5f, 4.4f), 0f), 
                                            transform.rotation) as GameObject;
            }
            GameManager.enemyToBeGen--;
        }
    }
}