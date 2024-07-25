using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private float colorChangeSpeed;

    private Color endColor = Color.magenta;

    private bool changingColor;

    public Vector3 destination;
    
    void Start()
    {
        StartCoroutine(ChangeColor());
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        

    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, projectileSpeed * Time.deltaTime);

        if (transform.position == destination)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator ChangeColor()
    {
        if (changingColor)
        {
            yield break;
        }
        changingColor = true;
        for (float t = 0.0f; t < 100; t += Time.deltaTime)
        {
            float colorTime = t / colorChangeSpeed;

            //Cosine is symmetric and periodic, after reaching -1, it starts smoothly increasing back to 1. (1 - cos(t * Pi))/2 reverses at the same time, but once it reaches 1 it then returns smoothly to zero, then reverses again, giving a Ping-Pong effect.
            float mix = 0.5f * (1.0f - (float)Mathf.Cos(Mathf.PI * colorTime));

            //Change color
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.blue, mix);

            //Wait for a frame
            yield return null;
        }

        changingColor = false;
    }

}
