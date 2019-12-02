using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed = 0.5f;
    Renderer rend;

    public Transform Background1;
    public Transform Background2;

    private bool Whichone = true;

    public Transform cam;

    private float currentLength = 87f;


    // Update is called once per frame
    void Update()
    {
        if (currentLength < cam.position.x) 
        {
            if (Whichone)
            {
                Background1.localPosition = new Vector3(Background1.localPosition.x + 174f, 0, 39);
            }
            else 
            {
                Background2.localPosition = new Vector3(Background2.localPosition.x + 174f, 0, 39);
            }
            currentLength += 87f;
            Whichone = !Whichone;
        }
        if (currentLength > cam.position.x + 87f)
        {
            if (Whichone)
            {
                Background2.localPosition = new Vector3(Background2.localPosition.x - 174f, 0, 39);
            }
            else
            {
                Background1.localPosition = new Vector3(Background1.localPosition.x - 174f, 0, 39);
            }
            currentLength -= 87f;
            Whichone = !Whichone;
        }
    }
}
