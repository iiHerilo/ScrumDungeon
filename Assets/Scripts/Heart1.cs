using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart1 : MonoBehaviour
{
    public Sprite sprite1_1;
    public Sprite sprite2_1;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = sprite1_1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FullHealth()
    {
        spriteRenderer.sprite = sprite1_1;
    }

    public void NoHealth()
    {
        spriteRenderer.sprite = sprite2_1;
    }
}
