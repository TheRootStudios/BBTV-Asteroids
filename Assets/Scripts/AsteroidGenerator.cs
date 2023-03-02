using UnityEngine;
using System.Collections;

public class AsteroidGenerator : MonoBehaviour
{
    Camera camera;
    public PlayerShip player;
    public GameObject asteroidPrefab; 
    public float spawnTime = 2f; 
    public float spawnRadius = 10f; 
    public float asteroidMinSpeed = 1f; 
    public float asteroidMaxSpeed = 5f; 

    void Start()
    {
        camera = Camera.main;
        InvokeRepeating("SpawnAsteroid", 0.5f, spawnTime);
    }

    void SpawnAsteroid()
    {
        if (player == null) return;

        Vector2 spawnPosition = GetRandomPositionOutsideScreen();
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        asteroid.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));


        Vector3 targPosition = Random.insideUnitCircle * 3;

        Vector2 direction = ((player.transform.position + targPosition) - asteroid.transform.position).normalized;

        float speed = Random.Range(asteroidMinSpeed, asteroidMaxSpeed);

        asteroid.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
    }

    public Vector3 GetRandomPositionOutsideScreen()
    {
        float halfHeight = camera.orthographicSize;
        float halfWidth = halfHeight * camera.aspect;

        Vector3 position = Vector3.zero;
        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: // Esquerda
                position = new Vector3(camera.transform.position.x - halfWidth - spawnRadius, Random.Range(camera.transform.position.y - halfHeight - spawnRadius, camera.transform.position.y + halfHeight + spawnRadius), 0.0f);
                break;
            case 1: // Direita
                position = new Vector3(camera.transform.position.x + halfWidth + spawnRadius, Random.Range(camera.transform.position.y - halfHeight - spawnRadius, camera.transform.position.y + halfHeight + spawnRadius), 0.0f);
                break;
            case 2: // Baixo
                position = new Vector3(Random.Range(camera.transform.position.x - halfWidth - spawnRadius, camera.transform.position.x + halfWidth + spawnRadius), camera.transform.position.y - halfHeight - spawnRadius, 0.0f);
                break;
            case 3: // Cima
                position = new Vector3(Random.Range(camera.transform.position.x - halfWidth - spawnRadius, camera.transform.position.x + halfWidth + spawnRadius), camera.transform.position.y + halfHeight + spawnRadius, 0.0f);
                break;
        }

        return position;
    }
}