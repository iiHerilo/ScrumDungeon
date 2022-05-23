using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        GameObject[] enemies = FindGameObjectsWithTag("enemy");
        for (int x = 0; x < enemies.Length; x++)
        {
            if (position == enemies[x].transform.position)
            {

            }
        }
    }
}
