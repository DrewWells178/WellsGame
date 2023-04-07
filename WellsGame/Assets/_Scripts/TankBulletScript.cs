using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBulletScript : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;

    [SerializeField] private int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        Vector3 rotation = transform.position - player.transform.position;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
    }

    private void OnTriggerEnter2D (Collider2D hitInfo)
    {
        //Debug.Log(hitInfo.name);
        
        if(hitInfo.name != "Tank")
        {
            Player player = hitInfo.GetComponent<Player>();
            if(player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
