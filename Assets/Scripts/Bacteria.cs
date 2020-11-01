using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    public float Y_SpeedRange = 4;
    private float Y_Speed;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Y_Speed = Random.Range(-Y_SpeedRange/2, Y_SpeedRange/2);
        transform.Translate(new Vector3(GameManager.bacteria_X_speed*Time.deltaTime, Y_Speed*Time.deltaTime, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RightEdge")
        {
            Destroy(gameObject); // Destroy self
            GameManager.enemyEscapeNum++;
            GameManager.enemyLeftNum--;
        }
    }
}
