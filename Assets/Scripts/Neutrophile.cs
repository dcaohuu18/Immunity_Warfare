using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrophile : MonoBehaviour
{
	public GameObject trap;
	public GameObject trapClone;
	
    // Start is called before the first frame update
    void Start()
    {
    	trapClone = Instantiate(trap, new Vector3(transform.position.x - 1f, transform.position.y, 0), 
    							transform.rotation) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
