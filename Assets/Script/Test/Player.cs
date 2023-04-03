using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public VariableJoystick joystick;

    public GameObject[] monsters;
    public Transform[] transMonster;

    Rigidbody2D m_rb;
    Vector2 move;
    public Transform tagetMonster;

    public static bool PointerDown = false;


    public float moveSpeed;


    private void Start()
    {

        m_rb = GetComponent<Rigidbody2D>();
        Rotation();

    }

    private void Update()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
    }

    private void FixedUpdate()
    {
        Moving();
    }
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        Destroy(rb, 0.6f);

    }


    public float timer = 0.0f;

    private void LateUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (monsters == null)
        {
            return;
        }
        if (PointerDown)
        {
            Rotation();

            if (tagetMonster == null)
            {
                return;
            }
            if (timer >= 0.5f)
            {
                Shoot();
                timer = 0.0f;
            }

        }

    }
    void Rotation()
    {
        if (!PointerDown)
        {
            return;
        }

        tagetMonster = GetClosestEnemy();
        if (tagetMonster == null)
        {
            return;
        }
        Vector2 direction = (tagetMonster.gameObject.GetComponent<Rigidbody2D>().position - m_rb.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;


        m_rb.rotation = angle;
    }


    private void Moving()
    {
        if (PointerDown)
        {
            return;
        }

        float hAxis = move.x;
        float vAxis = move.y;
        float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, -zAxis);
        m_rb.MovePosition(m_rb.position + move * moveSpeed * Time.deltaTime);

    }
    Transform GetClosestEnemy()
    {
        monsters = GameObject.FindGameObjectsWithTag("Monster");

        float closesDistance = Mathf.Infinity;
        Transform trans = null;
        foreach (GameObject iMonster in monsters)
        {
            float currentDistance;

            currentDistance = Vector3.Distance(transform.position, iMonster.transform.position);
            if (currentDistance < closesDistance)
            {
                closesDistance = currentDistance;
                trans = iMonster.transform;
            }


        }
        return trans;


    }

}
