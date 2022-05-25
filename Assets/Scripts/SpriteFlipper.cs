using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    Vector2 last;
    bool flipped = false;
    public bool isPlayer = false;
    void Start() {
        last = transform.position;
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        float difference = last.x - transform.position.x;
        //Debug.Log(string.Format("Player: {0}\nInput: {1}\n Last X:"))
        if(((isPlayer && input < 0) ^ (!isPlayer && difference > 0)) && !flipped) {
            GetComponent<SpriteRenderer>().flipX = true;
            flipped = true;
        }
   else if(((isPlayer && input > 0) ^ (!isPlayer && difference < 0)) && flipped) {
            GetComponent<SpriteRenderer>().flipX = false;
            flipped = false;
        }
        last = transform.position;
    }
}
