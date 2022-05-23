using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementVertical : MonoBehaviour
{
    public bool goUp = true;
    public float enemyVertiSpeed = 0.01f;
    public float enemyMaxUp = 4;
    public float enemyMaxDown = -4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        if (position.y >= enemyMaxUp)
        {
            goUp = false;
        }
        if (position.y <= enemyMaxDown)
        {
            goUp = true;
        }
        if (goUp == true)
        {
            position.y += enemyVertiSpeed;
        }
        else
        {
            position.y -= enemyVertiSpeed;
        }
        transform.position = position;
    }
}
