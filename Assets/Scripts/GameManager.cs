using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameObject chosenObject;
    public static Vector3 chosenObjectSize;

    public static bool playGame;
    public Button continueButton;
    public TextMeshProUGUI continueButtonText;

    public static int level = 1;
    public static bool levelUp = false;

    public static int enemyFreq; 
    public static float bacteria_X_speed;
    
    private int initEnemyNum;
    // the following is used to determined if more enemies should be generated:
    public static int enemyToBeGen;
    // enemies get destroyed along the way, we want to keep track how many we are left with:
    public static int enemyLeftNum; 
    
    public static int enemyEscapeNum;
    public int enemyEscapeLim = 20;
    public static bool coronaEscape;
    public Text enemyEscapeText;

    // Start is called before the first frame update
    void Start()
    {
        chosenObject = GameObject.FindWithTag("Cell");
        chosenObjectSize = chosenObject.GetComponent<Renderer>().bounds.size;

        playGame = false;

        if (levelUp==true) 
            level++;

        continueButton = GameObject.Find("continueButton").GetComponent<Button>();
        continueButton.gameObject.SetActive(false);
        continueButtonText = continueButton.GetComponentInChildren<TextMeshProUGUI>();

        enemyFreq = 37 - level*10;
        
        bacteria_X_speed = 2*level;
        
        initEnemyNum = level*120;
        enemyToBeGen = initEnemyNum;
        enemyLeftNum = initEnemyNum;

        enemyEscapeNum = 0;
        coronaEscape = false;
        enemyEscapeText = GameObject.Find("EnemyEscape").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyEscapeText.text = "Enemies Missed: " + enemyEscapeNum;
        if (enemyEscapeNum >= 20)
            // change the text color to red to indicate the player is very close to losing:
            enemyEscapeText.color= new Color32(247,59,63,255);
        else if (enemyEscapeNum >= 15)
            // change the text color to orange to indicate the player is closer to losing:
            enemyEscapeText.color= new Color32(246,140,31,255);

        if (enemyEscapeNum>enemyEscapeLim || coronaEscape==true || enemyLeftNum==0)
        {
            playGame = false;

            if (enemyEscapeNum>enemyEscapeLim || coronaEscape==true) //the player fails the level
            {
                levelUp = false; 
                continueButtonText.SetText("Replay");
            }
            else if (level==3) // the player wins the final level
            {
                levelUp = true; 
                level = 0; // this will be set to level 1
                continueButtonText.SetText("Restart");
            }
            else if (level!=0) 
            /* the condition level!=0 is to avoid it falling into this case 
            if it has already gone through the else if right above*/
            {
                levelUp = true;
                continueButtonText.SetText("Next Level");
            }

            continueButton.gameObject.SetActive(true); //show continue button
        }
    }
}
