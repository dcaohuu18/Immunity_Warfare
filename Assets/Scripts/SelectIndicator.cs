using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(GameManager.chosenObject.transform.position.x, 
                                         GameManager.chosenObject.transform.position.y+GameManager.chosenObjectSize.y/1.5f, 0);
    }
}
