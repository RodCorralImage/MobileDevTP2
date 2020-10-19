using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instnce = null;
    public static GameManager GetInstance() {
        if (_instnce == null) {
            _instnce = FindObjectOfType<GameManager>();
        }
        return _instnce;
    }


    public float GameTime = 30f;
    bool playing = false;
    float gameTimer = 0f;

    private void OnDestroy() {
        if (_instnce == this) {
            _instnce = null;
        }
    }

    private void Update() {
        if (playing) {
            gameTimer += Time.deltaTime;

            if (gameTimer >= GameTime) {
                TimeOut();
            }
        }
    }

    public void StartGame() {
        GameEvents.TriggerOnGameStart();
        playing = true;
    }

    public void GoalReached() {
        if (playing)
            FinishGame();
    }
    void TimeOut() {
        if (playing)
            FinishGame();
    }

    void FinishGame() {
        playing = false;
        GameEvents.TriggerOnGameFinish();
    }

    public float GetGameTime() {
        return GameTime;
    }
    public float GetTimer() {
        return gameTimer;
    }
    public bool IsPlaying() {
        return playing;
    }
}
