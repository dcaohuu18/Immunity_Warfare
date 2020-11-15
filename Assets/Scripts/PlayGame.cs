using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void playGame()
    {
        GameManager.playGame = true;
    }
}
