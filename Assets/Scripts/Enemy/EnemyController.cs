using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] BulletControoller m_bulletPrefab;

    [SerializeField] float m_bulletSpeed=350;

    public bool isLead;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") && isLead)
            EnemyManager.instance.ChangeDirection();

        if (collision.CompareTag("Bullet"))
        {
            EnemyManager.instance.m_EnemyObjects.Remove(this);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

  
    public void Shoot()
    {
        var bullet = Instantiate(m_bulletPrefab, transform.position, Quaternion.identity);
        bullet.tag = "EnemyBullet";
        bullet.BulletStart(m_bulletSpeed, Vector2.down);
    }
}
