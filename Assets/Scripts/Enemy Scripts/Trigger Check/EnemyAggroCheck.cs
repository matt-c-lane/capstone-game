using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyAggroCheck : MonoBehaviour
{
    public GameObject PlayerTarget {get; set; }
    private Enemy _enemy;
    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)        
        {
            _enemy.SetAggroStatus(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            _enemy.SetAggroStatus(false);
        }
    }
}
