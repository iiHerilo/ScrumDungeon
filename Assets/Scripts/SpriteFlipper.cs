using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    Vector2 last;
    bool flipped = false;
    public bool isPlayer = false;
    public bool isSword = false;
    void Start() {
        last = transform.position;
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        float difference = last.x - transform.position.x;
        //Debug.Log(string.Format("Player: {0}\nInput: {1}\n Last X:"))
        if((((isPlayer || isSword) && input < 0) ^ (!isPlayer && !isSword && difference > 0)) && !flipped) {
            if (!isSword)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            flipped = true;
            if (isSword)
            {
                GetComponent<SpriteRenderer>().flipY = true;
                Vector2 position = transform.position;
                position.x = -0.5f;
            }
        }
        else if((((isPlayer || isSword) && input > 0) ^ (!isPlayer && !isSword && difference < 0)) && flipped) {
            if (!isSword)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
                flipped = false;
            if (isSword)
            {
                GetComponent<SpriteRenderer>().flipY = false;
                Vector2 position = transform.position;
                position.x = 0.5f;
            }
        }
        last = transform.position;
    }
}
