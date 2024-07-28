using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEditor;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

//Written by: Tedmund Yap
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

    private Vector3 normalizeDirection;

    public int projectileDamage;

    private thePlayer playerScript;
    
    void Start()
    {
        StartCoroutine(ChangeColor());

        playerScript = GameObject.FindWithTag("Player").GetComponent<thePlayer>();

        normalizeDirection = (destination - transform.position).normalized;
        
        switch (playerScript.xAxis)
        {
            case > 0:
                normalizeDirection.x += Random.Range(0.5f, 1.25f);
                break;

            case < 0:
                normalizeDirection.x -= Random.Range(0.5f, 1.25f);
                break;

            default:
                break;
        }

        switch (playerScript.yAxis)
        {
            case > 0:
                normalizeDirection.y += Random.Range(0.5f, 1.25f);
                break;

            case < 0:
                normalizeDirection.y -= Random.Range(0.5f, 1.25f);
                break;

            default:
                break;
        }

        normalizeDirection = normalizeDirection.normalized;

        Invoke("Destroy", 5f);
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        transform.position += normalizeDirection * projectileSpeed * Time.deltaTime;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<thePlayer>().TakeDamage(projectileDamage);
            Destroy(this.gameObject);
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
