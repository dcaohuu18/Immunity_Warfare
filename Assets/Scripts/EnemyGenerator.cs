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
            bacteriaClone = Instantiate(bacteria, new Vector3(transform.position.x, Random.Range(-4.6f, 4.5f), 0f), 
                                        transform.rotation) as GameObject;
            GameManager.enemyToBeGen--;
        }
    }
}