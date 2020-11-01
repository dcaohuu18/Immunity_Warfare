using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameObject chosenObject;
    public static Vector3 chosenObjectSize;

    public static bool playGame = true;
    public GameObject continueButton;
    public Text continueButtonText;

    public static int level = 1;
    public static int enemyFreq; 
    public static float bacteria_X_speed;
    
    private int initEnemyNum;
    // the following is used to determined if more enemies should be generated:
    public static int enemyToBeGen;
    // enemies get destroyed along the way, we want to keep track how many we are left with:
    public static int enemyLeftNum; 
    
    public static int enemyEscapeNum;
    public static int enemyEscapeLim = 20;
    public Text enemyEscapeText;

    // Start is called before the first frame update
    void Start()
    {
        chosenObject = GameObject.FindWithTag("Cell");
        chosenObjectSize = chosenObject.GetComponent<Renderer>().bounds.size;

        continueButton.SetActive(false);
        continueButtonText = continueButton.GetComponentInChildren<Text>();

        enemyFreq = 200 - level*50; // the lower, the more densely the enemy will appear
        bacteria_X_speed = 2*level;
        
        initEnemyNum = level*120;
        enemyToBeGen = initEnemyNum;
        enemyLeftNum = initEnemyNum;

        enemyEscapeNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        enemyEscapeText.text = "Enemies Missed: " + enemyEscapeNum;
        if (enemyEscapeNum>enemyEscapeLim || enemyLeftNum==0)
        {
            playGame = false;
            continueButton.SetActive(true); //show continue button

            if (enemyEscapeNum>enemyEscapeLim) //the player fails the level
            {
                continueButtonText.text = "Replay";
                // Reset();
            }
            else
            {
                continueButtonText.text = "Next Level";
                // level++;
                // Reset();
            }
        }
    }   
}
