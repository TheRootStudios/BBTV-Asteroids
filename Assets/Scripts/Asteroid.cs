using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer rend;
    public GameObject asteroidPrefab;
    public GameObject asteroidExplosionPrefab;

    public List<Sprite> sprites;

    public int level = 1; 
    public int maxHealth = 1; 
    public int currentHealth; 
    public float scalePerLevel = 0.5f;
    public int pointsPerLevel = 10;



	private void Awake()
	{
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

	void Start()
    {
        rend.sprite = sprites[Random.Range(0, sprites.Count-1)];

        currentHealth = maxHealth; 

        float scale = level * scalePerLevel;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (level > 1)
            {
                SpawnChildAsteroid();
            }

            GameObject.Instantiate(asteroidExplosionPrefab, transform.position, Quaternion.identity);
            UI.Instance.UpdatePoints(pointsPerLevel * level);


            Destroy(gameObject); 
        }
    }

    void SpawnChildAsteroid()
    {
		for (int i = 0; i < level; i++)
		{
            GameObject childAsteroid = Instantiate(asteroidPrefab, transform.position, Quaternion.identity);
            Asteroid childAsteroidScript = childAsteroid.GetComponent<Asteroid>();
            childAsteroidScript.level = level - 1;
            childAsteroidScript.maxHealth = maxHealth - 1;
            childAsteroidScript.currentHealth = childAsteroidScript.maxHealth;
            childAsteroidScript.rb.AddForce(1 * Random.insideUnitCircle.normalized,ForceMode2D.Impulse);
        }

    }
}