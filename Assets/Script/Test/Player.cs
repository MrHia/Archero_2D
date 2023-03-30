using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public VariableJoystick joystick;


    Rigidbody2D m_rb;
    Vector2 move;
    float tagetRotation;
    public Transform tagetMonster;

    public Transform pointRotage;

    public static bool PointerDown = false;


    public float moveSpeed;

    private void Start()
    {

        m_rb = GetComponent<Rigidbody2D>();
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
    private void LateUpdate()
    {
        //TagetMonster();
        Rotation();
    }
    void Rotation()
    {
        if (!PointerDown)
        {
            return;
        }
        //if (tagetMonster != null)
        Vector2 direction = (tagetMonster.gameObject.GetComponent<Rigidbody2D>().position - m_rb.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        //Debug.Log(angle);
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
        tagetRotation = m_rb.rotation;




    }


}
