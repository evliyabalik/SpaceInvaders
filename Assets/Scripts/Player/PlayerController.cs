using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_moveSpeed;
    private bool IsPressKeyShot => Input.GetKeyDown(KeyCode.Space);
    [SerializeField] BulletControoller m_bulletPrefab;
    [SerializeField] float m_bulletSpeed = 50f;


    // Update is called once per frame
    void Update()
    {
        Move();

        if (IsPressKeyShot)
            Shoot();
    }

    private void Shoot()
    {
        var bullet = Instantiate(m_bulletPrefab, transform.position, Quaternion.identity);

        bullet.BulletStart(m_bulletSpeed, Vector2.up);
    }

    void Move()
    {
        var position = transform.position;

        float border = (transform.localScale.x / 2 + Camera.main.orthographicSize / 2.3f);//-Camera.main.orthographicSize * Camera.main.aspect;

        //Movement
        var input = Input.GetAxis("Horizontal");
        position.x += input * m_moveSpeed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, -border, border);

        transform.position = position;
    }

    void Death()
    {
        print("Game Over");
        transform.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            Death();
        }
    }


}
