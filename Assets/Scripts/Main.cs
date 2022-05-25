using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public delegate void StartGame();
    public static event StartGame OnStart;

    void Start()
    {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate() { Play(); });
    }

    void Play() {
        Debug.Log("game started");
        OnStart();
        
    }
}
