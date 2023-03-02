using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; 
    public int damage = 1; 
    public GameObject attackParticle;

    void Start()
    {
        Destroy(gameObject, 3f); 
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Asteroid asteroid = other.GetComponent<Asteroid>(); 
            if (asteroid != null)
            {
                GameObject.Instantiate(attackParticle, transform.position, Quaternion.identity);
                asteroid.TakeDamage(damage); 

                Vector3 dir = (asteroid.transform.position - transform.position).normalized;

                asteroid.rb.AddForce(50 * dir);
                Destroy(gameObject);
            }
        }
    }
}