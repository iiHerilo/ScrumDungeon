using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health = 2;
    public float speed = 4.0f;
    public float sightRange = 15f;
    public int WaitToStartTimer = 30;

    Rigidbody2D physics;
    GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 diffs = new Vector2((transform.position.x - Player.transform.position.x), (transform.transform.position.y - Player.transform.position.y));
        // Enemy only moves if player is within a certain range
        if(Mathf.Abs(Mathf.Sqrt((diffs.x * diffs.x) + (diffs.y * diffs.y))) <= sightRange && WaitToStartTimer <= 0) {
            float theta = Mathf.Atan(diffs.y / diffs.x);
            // For some reason omitting this causes the enemy to run in the wrong direction when this condition is met
            if(diffs.x <= 0) 
                theta += Mathf.PI;
            Vector2 velocity = new Vector2(Mathf.Cos(theta) * speed, Mathf.Sin(theta) * speed);
            physics.velocity = -velocity;
        }
    }

    void FixedUpdate() {
        if(WaitToStartTimer > -1) {
            WaitToStartTimer--;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void ChangeHealth(int amount)
    {
        health += amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
