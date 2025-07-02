using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SensorEvents.PlayerEntered(gameObject.name, other.transform);   
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            SensorEvents.PlayerExit(gameObject.name);
        }
    }
}
