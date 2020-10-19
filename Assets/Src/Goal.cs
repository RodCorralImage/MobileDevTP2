using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] SpriteRenderer goalRenderer = null;

    private void OnTriggerEnter2D(Collider2D collision) {
        var player = collision.GetComponent<Player>();
        if (player != null) {
            GameManager.GetInstance().GoalReached();
            goalRenderer.color = Color.green;
            player.OnGoal();
        }
    }
}
