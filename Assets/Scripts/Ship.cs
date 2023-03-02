using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    

    [Header("Components")]
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer rend;


    [Header("Parameters")]
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float rotationSpeed = 100f;
    public int maxLife = 5;
    public int currentLife;

    public event Action<Ship> OnDeath;



    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rend = GetComponent<SpriteRenderer>();
        currentLife = maxLife;
    }

    public virtual void Update()
    {
        if(currentLife <= 0) { return; }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
	{
        ContactPoint2D contact = collision.GetContact(0);
		Vector3 pos = contact.point;
        Vector3 dir = (transform.position - pos).normalized;
        
        GetDamage(1,dir);
    }

    public virtual void GetDamage(int damage, Vector3 direction, float collisionForce = 100)
	{
        currentLife -= damage;

        if(currentLife > 0)
		{
            rb.AddForce(collisionForce * direction);
		} else
		{
            Death();
		}

    }


    void Death()
	{
        rb.velocity = Vector2.zero;
        col.enabled = false;

        StartCoroutine(DeathAnimation());

        OnDeath?.Invoke(this);
    }




    IEnumerator DeathAnimation()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        GetComponent<SpriteRenderer>().DOColor(Color.clear, 0.5f);

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(0);        
    }    
}
