using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Text;

public class GameHud : MonoBehaviour
{
    [SerializeField] Text timerLabel = null;

    StringBuilder timerSB = new StringBuilder();

    private void Start() {
        GameEvents.OnGameStart += OnStartGame;
    }
    private void OnDestroy() {
        GameEvents.OnGameStart -= OnStartGame;
    }


    void OnStartGame() {
        StartCoroutine(UpdateGameTime());
    }

    IEnumerator UpdateGameTime() {
        do {
            float temTime = GameManager.GetInstance().GetGameTime() - GameManager.GetInstance().GetTimer();
            timerSB.Clear();
            timerSB.Append(temTime.ToString("0"));
            timerLabel.text = timerSB.ToString();
            yield return new WaitForSeconds(1f);
        } while (GameManager.GetInstance().IsPlaying());
        float finalTime = GameManager.GetInstance().GetGameTime() - GameManager.GetInstance().GetTimer();
        timerSB.Clear();
        timerLabel.text = finalTime.ToString("0");
    }

}
