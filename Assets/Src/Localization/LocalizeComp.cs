using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizeComp : MonoBehaviour
{
    private void Awake() {
        LocalizationService.Instance.LocalizeGameObject(gameObject);
    }
}
