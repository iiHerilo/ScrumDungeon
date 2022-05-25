using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy: MonoBehaviour
{
    public bool goRight = true;
    public float enemyHorizSpeed = 3.0f;
    public float enemyMaxRight = 7;
    public float enemyMaxLeft = -7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        if (position.x >= enemyMaxRight)
        {
            goRight = false;
        }
        if (position.x <= enemyMaxLeft)
        {
            goRight = true;
        }
        if (goRight == true)
        {
            position.x += enemyHorizSpeed * Time.deltaTime;
        }
        else
        {
            position.x -= enemyHorizSpeed* Time.deltaTime;
        }
        transform.position = position;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
