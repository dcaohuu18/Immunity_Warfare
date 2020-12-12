using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCell : MonoBehaviour
{
    public float speed = 0.2f;
    private float enterSceneTimer = 0f;
    private Rigidbody2D rbComp;

    public GameObject antibody;
    private GameObject antibodyClone;

    public float timeToFire = 1f;
    private float fireTimer = 0f;

    private Vector3 targetPosition;
    private Vector3 dir;
    private bool justSelected = false;

    private bool mouseOnObject = false; //

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        dir = new Vector3(-1, 0, 0); // move left/forward at first
        rbComp = GetComponent<Rigidbody2D>();
        rbComp.isKinematic = true; // "disable" the rigidbody at first so it can cross the right edge
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.playGame)
        {
            enterSceneTimer += Time.deltaTime;

            if (enterSceneTimer >= 20f)
            {
            	rbComp.isKinematic = false; // "activate" the rigidbody
            	if (Input.GetMouseButton(0) && gameObject==GameManager.chosenObject && justSelected==false)
                	SetTargetPosition();                
            }

            Move();

            if (Input.GetMouseButton(0) && mouseOnObject)
                fireAntibody(); // Hold mouse button to fire
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

    void OnMouseEnter()
    {
        mouseOnObject = true;
    }

    void OnMouseExit()
    {
        mouseOnObject = false;
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
        if ((collision.gameObject.tag.Contains("Edge") || collision.gameObject.tag == "Cell") && enterSceneTimer >= 20f)
        {
            // Bounce back:
            targetPosition.x = collision.GetContact(0).point.x + Random.Range(-1f, 1f);
            targetPosition.y = collision.GetContact(0).point.y + Random.Range(-1f, 1f);;
            dir = -(targetPosition - transform.position).normalized;
        }
    }

    private void fireAntibody()
    {
    	if (fireTimer >= timeToFire)
        {
            audioManager.Play("Laser");
            //North:
            antibodyClone = Instantiate(antibody, new Vector3(transform.position.x, transform.position.y+1f, 0),
                                    	Quaternion.Euler(0, 0, 0)) as GameObject;
            //North-East: 
            antibodyClone = Instantiate(antibody, new Vector3(transform.position.x+0.7f, transform.position.y+0.7f, 0),
                                        Quaternion.Euler(0, 0, -45)) as GameObject;
            //East:
            antibodyClone = Instantiate(antibody, new Vector3(transform.position.x+1f, transform.position.y, 0),
                                        Quaternion.Euler(0, 0, -90)) as GameObject;
            //South-East:
            antibodyClone = Instantiate(antibody, new Vector3(transform.position.x+0.7f, transform.position.y-0.7f, 0),
                                        Quaternion.Euler(0, 0, -135)) as GameObject;
            //South:
            antibodyClone = Instantiate(antibody, new Vector3(transform.position.x, transform.position.y-1f, 0),
                                    	Quaternion.Euler(0, 0, 180)) as GameObject;
            //South-West:
            antibodyClone = Instantiate(antibody, new Vector3(transform.position.x-0.7f, transform.position.y-0.7f, 0),
                                        Quaternion.Euler(0, 0, 135)) as GameObject;
            //West:
            antibodyClone = Instantiate(antibody, new Vector3(transform.position.x-1f, transform.position.y, 0),
                                    	Quaternion.Euler(0, 0, 90)) as GameObject;
            //North-West: 
            antibodyClone = Instantiate(antibody, new Vector3(transform.position.x-0.7f, transform.position.y+0.7f, 0),
                                        Quaternion.Euler(0, 0, 45)) as GameObject;

            fireTimer = 0f;
        }
        fireTimer += Time.deltaTime;
    }
}
