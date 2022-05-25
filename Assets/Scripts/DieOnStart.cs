using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnStart : MonoBehaviour
{
    
    void Oops() {
        Destroy(gameObject);
    }

    void OnEnable() {
        Main.OnStart += Oops;
    }
    void OnDisable() {
        Main.OnStart -= Oops;
    }
}
