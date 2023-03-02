using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerShip : Ship
{
    public GameObject bulletPrefab; 
    public float fireRate = 0.5f; 
    private float timeSinceLastShot; 
    public Transform firePoint; 
    public ParticleSystem fire;
    Vector2 inputAxis;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        UI.Instance.UpdateLife(currentLife);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (currentLife <= 0) { return; }

        if (SimpleInput.GetButtonDown("Jump") && Time.time - timeSinceLastShot > fireRate)
        {
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            timeSinceLastShot = Time.time;
        }

        inputAxis = new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));

        fire.enableEmission = inputAxis.y > 0;



        rb.AddForce(transform.up * speed * inputAxis.y * Time.deltaTime, ForceMode2D.Impulse);
        transform.Rotate(0, 0, -inputAxis.x * rotationSpeed * Time.deltaTime);
    }

    public override void GetDamage(int damage, Vector3 direction, float collisionForce = 100)
    {
        base.GetDamage(damage, direction, collisionForce);

        if (!DOTween.IsTweening(transform))
        {
            transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f);
        }
        
        

        if (!DOTween.IsTweening(rend))
        {
            rend.material.DOColor(Color.red, 0.1f).SetEase(Ease.InFlash, 4, 0);
        }



        UI.Instance.UpdateLife(currentLife);
    }



}
