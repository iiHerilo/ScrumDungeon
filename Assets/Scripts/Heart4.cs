using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart4 : MonoBehaviour
{
    public Sprite sprite1_4;
    public Sprite sprite2_4;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = sprite1_4;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FullHealth()
    {
        spriteRenderer.sprite = sprite1_4;
    }

    public void NoHealth()
    {
        spriteRenderer.sprite = sprite2_4;
    }
}
