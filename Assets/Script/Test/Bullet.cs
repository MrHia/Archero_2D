using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,1f);
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu va chạm với Collider có tag là "Monster"
        if (collision.collider.tag == "Monster")
        {
            // Dừng chuyển động của đạn
            rb.velocity = Vector2.zero;
            // Dính đạn vào Collider
            transform.parent = collision.transform;
            // Vô hiệu hóa Collider của đạn để không va chạm với các đối tượng khác
            GetComponent<Collider2D>().enabled = false;
            // Hủy Rigidbody2D component của đạn để ngăn không cho đạn di chuyển
            Destroy(rb);
        }
        if (collision.collider.tag == "Monster")
        {
            //Destroy(gameObject, 0f);
            Destroy(collision.gameObject, 0f);
        }
            
    }
}
