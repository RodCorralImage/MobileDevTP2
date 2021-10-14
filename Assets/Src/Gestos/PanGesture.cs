using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanGesture : Gesture
{
    public override int Priority() => 5;

    public override bool IsRunning() {
        //...
        return false;
    }

    public override void ApplyEffect(GameObject player) {
        player.transform.position += Vector3.up * Time.deltaTime;
    }
}
