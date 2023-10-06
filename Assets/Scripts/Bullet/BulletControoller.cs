using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class BulletControoller : MonoBehaviour
{
    Rigidbody2D m_bulletRigidbody;


    // Start is called before the first frame update
    void Awake()
    {
        m_bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    public void BulletStart(float speed, Vector2 vec)
    {
        m_bulletRigidbody.AddForce(vec * speed);
        Destroy(this.gameObject, 1);
    }
}
