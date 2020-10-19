using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialGameScreen : MonoBehaviour
{
    [SerializeField] Text timeToPlayLabel = null;


    private void OnEnable() {
        GameEvents.OnGameStart += OnStartGame;
    }
    private void OnDisable() {
        GameEvents.OnGameStart -= OnStartGame;
    }

    private void Start() {
        timeToPlayLabel.text = GameManager.GetInstance().GetGameTime().ToString("0");
    }

    void OnStartGame() {
        gameObject.SetActive(false);
    }

    public void PlayGmaeBtn() {
        GameManager.GetInstance().StartGame();
    }
}
