using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    float minForce = 12f;
    float maxForce = 16f;
    float posXBound = 5f;
    float torqueMagnitude = 10f;
    Rigidbody Rb;
    SpawnManager spawnManager;

    public ParticleSystem explosionParticle;
    public int objectScore;
    public bool isGood;


    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        sendUp();
    }

    // Update is called once per frame
    void Update()
    {
    }

    
    void sendUp()
    {
        //random force
        float upForce = Random.Range(minForce, maxForce);
        Rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);

        //random postion
        transform.position = new Vector3(Random.Range(-posXBound, posXBound), -4, 0);

        //random torque
        float randTorque = Random.Range(-torqueMagnitude, torqueMagnitude);
        Rb.AddTorque(randTorque, randTorque, randTorque, ForceMode.Impulse);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sensor"))
        {
            //if object missed is good then game over
            if (isGood)
                spawnManager.lives--;

            Destroy(gameObject);
            
        }
    }


    public void DestroyTarget()
    {
        if (!spawnManager.isGameOver && !spawnManager.isPaused)
        {
            //adding score
            spawnManager.score += objectScore;

            //particle effects
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

            Destroy(gameObject);

            //if object destroyed is not good then game over
            if (!isGood)
                spawnManager.lives--;
        }
    }
}
