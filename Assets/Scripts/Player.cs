using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    /*public float maxRight = 7;
    public float maxLeft = -7;
    public float maxUp = 4;
    public float maxDown = -4.5f;*/

    public int maxHealth = 5;
    public int currentHealth;

    Rigidbody2D physics;
    float horizontal;
    float vertical;

    string titleScene;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        titleScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        // I get what this is supposed to do but it doesnt work since the axis aren't acceleration
        // Commented this code out in favor of movement that interacts with the actual physics engine
        // --Aaron
        /*
        Vector2 position = transform.position;
        if (horizontal == 1 && position.x >= maxRight || horizontal == -1 && position.x <= maxLeft)
        {
            horizontal = 0;
        }
        if (vertical == 1 && position.y >= maxUp || vertical == -1 && position.y <= maxDown)
        {
            vertical = 0;
        }
        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;
        transform.position = position;*/
    }
    void FixedUpdate() {
        Vector3 position = transform.position;
        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;

        physics.MovePosition(position);
    }   

    public void ChangeHealth(int amount)
    {
        currentHealth = GetHealth(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    int GetHealth(int current, int low, int high)
    {
        if (current > high)
        {
            current = high;
        }
        if (current <= low)
        {
            SceneManager.LoadScene(titleScene);
            current = maxHealth;
        }
        return current;
    }

    public string GetHealth()
    {
        return currentHealth + "/" + maxHealth;
    }

    void OnEnable() {
        TeleporterZone.OnTeleport += Teleport;
        RoomLayerOuter.OnSpawn += Goto;
    }
    void OnDisable() {
        TeleporterZone.OnTeleport -= Teleport;
        RoomLayerOuter.OnSpawn -= Goto;
    }


    void Teleport(Vector2 pos) {
        Vector3 position = transform.position;
        position.x += pos.x;
        position.y += pos.y;
        transform.position = position;
    }

    void Goto(Vector2 pos) {
        Vector3 position = transform.position;
        position.x = pos.x;
        position.y = pos.y;
        transform.position = position;
    }
}
