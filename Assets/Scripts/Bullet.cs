using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly System.Random _random = new System.Random();
    private const int MinDmg = 7;
    private const int MaxDmg = 11;
    private int _damage;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        _damage = _random.Next(MinDmg, MaxDmg);

        if (health != null)
        {
            health.TakeDamage(_damage);
        }

        Debug.Log("Bullet collided with " + hit.name + ", and produced " + _damage + " DMG");
        if (_damage >= MaxDmg - 1)
        {
            Debug.Log("CRITICAL!");
        }

        Destroy(gameObject);
    }
}
