using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Enemy_bot : MonoBehaviour
{
    private Transform playerPos;
    private Rigidbody2D m_rb;
    public Transform bulletPost;
    public GameObject bullet;
    public float speed=.3f;

    /*public delegate void mosterDestroy(GameObject Enemy_bot);
    public static event mosterDestroy OnMonsterDefeated;*/

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        //StartCoroutine(Shoot());
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > 3.5f)
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        Rotation();
    }
    void Rotation()
    {
        Vector2 direction = (playerPos.gameObject.GetComponent<Rigidbody2D>().position - m_rb.position).normalized;
        float angle =  Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg-90f;
        //Debug.Log(angle);
        m_rb.rotation = angle;
    }
   
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(bullet,bulletPost.position,transform.rotation);
        
        StartCoroutine(Shoot());
    }

    private void OnDestroy()
    {
        /*// Gọi event OnMonsterDefeated khi quái vật bị tiêu diệt
        if (OnMonsterDefeated != null)
        {
            OnMonsterDefeated(this.gameObject);
        }*/
    }
}
    