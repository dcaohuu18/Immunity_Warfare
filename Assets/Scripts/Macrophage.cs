using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macrophage : MonoBehaviour
{
    public float speed = 1;

    private Vector3 targetPosition;
    private Vector3 dir;
    private bool justSelected = false;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    void Move()
    {
    	transform.position +=  dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            GameManager.enemyLeftNum--;
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
