using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
	public void ChangeScene()
	{
		if (GameManager.levelUp == true)
		{
			SceneManager.LoadScene("Level_"+(GameManager.level+1));
		}
		else
		{
			SceneManager.LoadScene("Level_"+GameManager.level);
		}
	}
}
