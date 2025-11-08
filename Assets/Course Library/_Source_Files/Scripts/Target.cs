using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Target : MonoBehaviour
{
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;

    private float xRange = 4;
    private float ySpawnPos = -2;

    public GameObject particle;

    public int scoreValue;
    public bool isBad = false;


    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody>();

        rb.AddForce(Vector3.up * Random.Range(minSpeed, maxSpeed), ForceMode.Impulse);
        rb.AddForce(Vector3.right * Random.Range(-maxSpeed, maxSpeed) / 12f, ForceMode.Impulse);
        rb.AddTorque(Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque), Random.Range(-maxTorque, maxTorque));
        transform.position = new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
        
    //Called when Target is Clicked
    private void OnMouseDown(){

        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Si oui, on arrête tout. On ne clique pas sur la cible.
            return;
        }

        if (GameSettings.ShowParticles)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);

        if (isBad) GameManager.instance.UpdateLives(-1);
        else GameManager.instance.UpdateScore(scoreValue);
    }

    //Called when Target reaches the bottom of the screen
    private void OnTriggerEnter(Collider other){
        Destroy(gameObject);

        if(!isBad) GameManager.instance.UpdateLives(-1);
    }
}
