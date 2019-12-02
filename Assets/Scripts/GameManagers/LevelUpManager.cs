using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelUpManager : MonoBehaviour
{
    public void AttackUp()
    {
        PlayerMovement.ATTACK += 10;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (PlayerPrefs.GetString("SceneName") == "Level1") {
            SceneManager.LoadScene("Level2");
        }

    }

    public void DeffendUp()
    {
        PlayerMovement.MAX_HEALTH += 10;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (PlayerPrefs.GetString("SceneName") == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
