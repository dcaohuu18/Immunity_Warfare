using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutroTrap : MonoBehaviour
{
    public float speed = 1f;
    public float switchDirTimer = 0f;
    public float timeToSwitch = 1f;

    private AudioManager audioManager; 

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.playGame)
        {
            Move();
        }
    }

    void Move()
    {
        if (switchDirTimer >= timeToSwitch)
        {
            speed = -speed;
            switchDirTimer = 0f;
        }
        
        transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
        switchDirTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
            audioManager.Play("Swallow");
            Destroy(collision.gameObject, 0.14f);
        }
    }   
}
