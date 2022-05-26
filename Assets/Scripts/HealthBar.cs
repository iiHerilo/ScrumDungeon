using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player player;
    public Text myText;
    public bool isOn { get; set; }  = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        myText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            myText.text = player.GetHealth();
        }
    }
}
