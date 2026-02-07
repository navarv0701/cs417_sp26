using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float rotationSpeed = 30f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
