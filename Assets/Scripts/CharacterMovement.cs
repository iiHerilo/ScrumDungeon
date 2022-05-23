using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical *  Time.deltaTime;
        transform.position = position;
    }



    void OnEnable() {
        TeleporterZone.OnTeleport += Teleport;
    }
    void OnDisable() {
        TeleporterZone.OnTeleport -= Teleport;
    }


    void Teleport(Vector2 pos) {
        Vector2 position = transform.position;
        position.x += pos.x;
        position.y += pos.y;
        transform.position = position;
    }
}
