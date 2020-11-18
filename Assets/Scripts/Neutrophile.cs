using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrophile : MonoBehaviour
{
    public float speed = 3;

    public Vector3 targetPosition;
    public Vector3 dir;

    private bool justSelected = false;

    private AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        dir = new Vector3(-1, 0, 0); // move left at first
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.playGame)
        {
            if (Input.GetMouseButton(0) && gameObject == GameManager.chosenObject && justSelected == false)
                SetTargetPosition();

            Move();
        }
    }

    IEnumerator OnMouseDown() // highlight when chosen !!
    {
        GameManager.chosenObject = gameObject;
        GameManager.chosenObjectSize = GetComponent<Renderer>().bounds.size;
        // to avoid calling SetTargetPosition() the first time the cell is selected:
        justSelected = true;
        yield return new WaitForSeconds(0.1f);
        justSelected = false; 
    }

    void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z  = transform.position.z;
        dir = (targetPosition - transform.position).normalized; // dir facing the targetPosition

        // to prevent the neutrophil from standing still:
        if (dir == new Vector3(0, 0, 0)) 
            dir = new Vector3(-1, 0, 0); 

        //Rotate:
        transform.right = transform.position - targetPosition;
    }

    void Move()
    {
        transform.position +=  dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
            audioManager.Play("Swallow");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag.Contains("Edge") || collision.gameObject.tag == "Cell")
        {
            // Bounce back:
            targetPosition.x = collision.GetContact(0).point.x + Random.Range(-1f, 1f);
            targetPosition.y = collision.GetContact(0).point.y + Random.Range(-1f, 1f);;
            dir = -(targetPosition - transform.position).normalized;
        }
    }
}
