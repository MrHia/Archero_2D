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
    public Transform bulletPost;
    public GameObject bullet;

    /*    float tagetRotation;*/
    public Transform tagetMonster;

    public Transform pointRotage;

    public static bool PointerDown = false;


    public float moveSpeed;


    private void Start()
    {

        m_rb = GetComponent<Rigidbody2D>();
        monsters = GameObject.FindGameObjectsWithTag("Monster");
        /*for (int i = 0; i < monsters.Length; i++)
        {
            transMonster[i] = monsters[i].transform;
        }*/
        



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
    IEnumerator Shoot()
    {
        if (PointerDown)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(bullet, bulletPost.position, transform.rotation);

            StartCoroutine(Shoot());
        }

        
    }
    private void LateUpdate()
    {
        if(monsters == null)
        {
            return;
        }
        if (PointerDown)
        {
            Rotation();

            StartCoroutine(Shoot());
        }

    }
    void Rotation()
    {
        if (!PointerDown)
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
    

}
