using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHorizontal : MonoBehaviour
{
    public bool goRight = true;
    public float enemySpeed = 0.01f;
    public float maxRight = 7;
    public float maxLeft = -7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        if (position.x >= maxRight)
        {
            goRight = false;
        }
        if (position.x <= maxLeft)
        {
            goRight = true;
        }
        if (goRight == true)
        {
            position.x += enemySpeed;
        }
        else
        {
            position.x -= enemySpeed;
        }
        transform.position = position;
    }
}
