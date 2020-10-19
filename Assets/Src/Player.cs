using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPointerClickHandler
{
    public float FlyingSpeed = 2;
    bool flying = false;
    Rigidbody2D rigid = null;

    Quaternion starterRot = Quaternion.identity;
    Vector3 starterPos = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start() {
        starterPos = transform.position;
        starterRot = transform.rotation;
    }

    // Update is called once per frame
    void Update() {
        if (flying) {
            transform.position += transform.up * FlyingSpeed * Time.deltaTime;
        }
    }
    

    private void OnCollisionEnter2D(Collision2D collision) {
        if (flying && collision.collider.GetComponent<Asteroid>() != null) {

            int count = collision.contactCount;
            Vector2 contact = Vector2.zero;

            for (int i = 0; i < count; i++) {
                contact += collision.GetContact(i).point;
            }
            contact /= count;

            flying = false;
            transform.position = contact;
            transform.up = transform.position - collision.transform.position;
            transform.SetParent(collision.transform);
        }
    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.3f);
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 2);
    }
#endif


    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("OnTriggerEnter2D " + collision.name);
    }

    public void RestartGame() {
        if (!flying)
            return;
        transform.SetParent(null);
        transform.position = starterPos;
        transform.rotation = starterRot;
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;
        flying = false;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
        if (!flying) {
            Jump();
        }
    }

    void Jump() {
        flying = true;
        transform.SetParent(null);
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;
    }

    public void OnGoal() {
        flying = false;
    }
}
