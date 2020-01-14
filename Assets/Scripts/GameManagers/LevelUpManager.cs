using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelUpManager : MonoBehaviour
{
    public void AttackUp()
    {
        PlayerMovement.ATTACK += 20;
        if (PlayerPrefs.GetString("SceneName") == "Level1") {
            SceneManager.LoadScene("Level2");
        }
        else if (PlayerPrefs.GetString("SceneName") == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
        else if (PlayerPrefs.GetString("SceneName") == "Level3")
        {
            SceneManager.LoadScene("Level4");
        }
    }

    public void DeffendUp()
    {
        PlayerMovement.MAX_HEALTH += 15;
        if (PlayerPrefs.GetString("SceneName") == "Level1")
        {
            SceneManager.LoadScene("Level2");
        } 
        else if (PlayerPrefs.GetString("SceneName") == "Level2") 
        {
            SceneManager.LoadScene("Level3");
        }
        else if (PlayerPrefs.GetString("SceneName") == "Level3")
        {
            SceneManager.LoadScene("Level4");
        }
    }
}
