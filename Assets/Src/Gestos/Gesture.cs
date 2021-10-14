using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gesture : MonoBehaviour
{
    public abstract int Priority();
    public abstract bool IsRunning();
    public abstract void ApplyEffect(GameObject player);
}
