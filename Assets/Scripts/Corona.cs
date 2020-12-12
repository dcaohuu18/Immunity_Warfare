using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corona : MonoBehaviour
{
    public float X_Speed = 10;
    
    public float Y_SpeedRange = 4;
    private float Y_Speed;

    public float jumpAmount = 70;

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
        if (Random.Range(0f, 300f) < 1) // jump once in a while
        {
            if (Random.Range(0, 2) == 1)
            {
                Y_Speed = jumpAmount; // jump UP
            }
            else
            {
                Y_Speed = -jumpAmount; // jump DOWN
            }

            transform.Translate(new Vector3(jumpAmount*Time.deltaTime, Y_Speed*Time.deltaTime, 0));    
        }
        else
        {
            Y_Speed = Random.Range(-Y_SpeedRange/2, Y_SpeedRange/2);
            transform.Translate(new Vector3(X_Speed*Time.deltaTime, Y_Speed*Time.deltaTime, 0));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RightEdge")
        {
            Destroy(gameObject); // Destroy self
            GameManager.coronaEscape = true;
            GameManager.enemyEscapeNum++;
        }
        // To avoid it jumping off the top and bottom edges:
        else if (collision.gameObject.tag == "TopEdge")
        {
            transform.Translate(new Vector3(X_Speed*Time.deltaTime, -jumpAmount*Time.deltaTime, 0));
            // jump DOWN
        }
        else if (collision.gameObject.tag == "BotEdge")
        {
            transform.Translate(new Vector3(X_Speed*Time.deltaTime, jumpAmount*Time.deltaTime, 0));
            // jump UP
        }
    }

    private void OnDestroy()
    {
        GameManager.enemyLeftNum--;
    }
}
