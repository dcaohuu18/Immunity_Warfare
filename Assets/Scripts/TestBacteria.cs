using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBacteria : MonoBehaviour
{
	public float bacteriaSpeed = 2;
	private float new_y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        new_y = Random.Range(-2f, 2f);
        transform.Translate(new Vector3(bacteriaSpeed*Time.deltaTime, new_y*Time.deltaTime, 0));
    }
}
