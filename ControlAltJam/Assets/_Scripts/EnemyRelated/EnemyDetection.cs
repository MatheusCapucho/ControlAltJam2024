using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameEvents.OnPlayerDetected?.Invoke();
        }
        
        else
        if (collision.CompareTag("Pebble"))
        {
            GameEvents.OnEnemyDistracted?.Invoke(collision.transform.position.x);
            collision.gameObject.tag = "Untagged";
        }
    }
}
