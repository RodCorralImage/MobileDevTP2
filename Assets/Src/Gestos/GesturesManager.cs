using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GesturesManager : MonoBehaviour
{
    public Gesture[] gestures;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        foreach (var g in gestures) {
            if (g.IsRunning()) {
                g.ApplyEffect(player);
            }
        }
    }
}
