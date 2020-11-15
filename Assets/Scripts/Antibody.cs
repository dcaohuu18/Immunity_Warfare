using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibody : MonoBehaviour
{
    public float timeToDie = 4;
    private float dieTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 1*Time.deltaTime, 0));
        if (dieTimer >= timeToDie)
            Destroy(gameObject);
        dieTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject); // Destroy self
        }
        if (collision.gameObject.tag.Contains("Edge"))
        {
            Destroy(gameObject); // Destroy self
        }
    }
}
