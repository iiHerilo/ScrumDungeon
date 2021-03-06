using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    Heart1 heart1;
    Heart2 heart2;
    Heart3 heart3;
    Heart4 heart4;
    // Start is called before the first frame update
    void Start()
    {
        heart1 = GetComponent<Heart1>();
        heart2 = GetComponent<Heart2>();
        heart3 = GetComponent<Heart3>();
        heart4 = GetComponent<Heart4>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetHealth(int current, int low, int high)
    {
        if (current > high)
        {
            current = high;
        }
        if (current < low)
        {
            current = low;
        }
        switch (current)
        {
            case 0: heart1.NoHealth(); heart2.NoHealth(); heart3.NoHealth(); heart4.NoHealth(); break;
            case 1: heart2.NoHealth(); heart1.FullHealth(); break;
            case 2: heart3.NoHealth(); heart2.FullHealth(); break;
            case 3: heart4.NoHealth(); heart3.FullHealth(); break;
            case 4: heart1.FullHealth(); heart2.FullHealth(); heart3.FullHealth(); heart4.FullHealth(); break;
        }
        return current;
    }
}
