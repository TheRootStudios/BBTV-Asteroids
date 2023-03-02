using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraWrap : MonoBehaviour
{
    Camera camera;
    public float startPreventDelay = 1f;
    public float offset = 2f;
    bool canWrap;

	private void Start()
	{
        camera = Camera.main;
        StartCoroutine(StartPreventDelay());
	}

	void Update()
    {
        if (!GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), GetComponent<Renderer>().bounds) && canWrap)
        {
            Vector3 position = transform.position;

            if (position.x > camera.transform.position.x + camera.orthographicSize * camera.aspect + offset)
            {
                position.x -= camera.orthographicSize * camera.aspect * 2f  + offset;
            }
            else if (position.x < camera.transform.position.x - camera.orthographicSize * camera.aspect - offset)
            {
                position.x += camera.orthographicSize * camera.aspect * 2f  + offset;
            }

            if (position.y > camera.transform.position.y + camera.orthographicSize + offset)
            {
                position.y -= camera.orthographicSize * 2f  + offset;
            }
            else if (position.y < camera.transform.position.y - camera.orthographicSize  - offset)
            {
                position.y += camera.orthographicSize * 2f  + offset;
            }

            transform.position = position;
        }
    }

    IEnumerator StartPreventDelay()
	{
        yield return new WaitForSeconds(startPreventDelay);
        canWrap = true;

    }
}