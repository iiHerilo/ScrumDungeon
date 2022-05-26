using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    private SpriteRenderer spriteRenderer;
    bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = sprite1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            spriteRenderer.sprite = sprite2;
            isOn = true;
        }
        else
        {
            spriteRenderer.sprite = sprite1;
            isOn = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy != null && isOn)
        {
            enemy.ChangeHealth(-1);
        }
    }
}
