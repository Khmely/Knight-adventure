using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WomanQuest : MonoBehaviour
{
    private bool isInRange;
    public Text womanTalk;
    GameManager gm;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Main Collider")
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Main Collider")
        {
            isInRange = false;
        }
    }
    void DisableText()
    {
        womanTalk.enabled = false;
    }

    void nextLevel() {
        SceneManager.LoadScene("LevelUp");
    }
    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            womanTalk.text = "Greetings warrior, please listen to me. Dark times will come if you do not help. The demon has to break free from his crypt, stop him! Take this crystal, it will give you the strength you need.";
            GameManager.score += 200;
            isInRange = false;
            Invoke("DisableText", 5f);
            Invoke("nextLevel", 3f);
        }
    }
}
