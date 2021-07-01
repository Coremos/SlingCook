using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void CheckLayer(GameObject collider)
    {
        if (collider.layer == LayerMask.NameToLayer("Floor"))
        {
            gameObject.SetActive(false);
            LevelManager.instance.turn = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        CheckLayer(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        CheckLayer(other.gameObject);
    }
}
