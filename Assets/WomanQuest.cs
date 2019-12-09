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
        SceneManager.LoadScene("LevelUp", LoadSceneMode.Additive);
    }
    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            womanTalk.text = "Please, find a power crystal and save our world! Take this gem, it will give you more power!";
            GameManager.score += 200;
            Invoke("DisableText", 5f);
            Invoke("nextLevel", 2f);
        }
    }
}
