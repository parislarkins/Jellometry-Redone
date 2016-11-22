﻿using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.Experimental.Director;

public class Projectile : MonoBehaviour
{

    private float _damage;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().ApplyDamage(_damage);
        }else if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().ApplyDamage(_damage);
        }else if (other.gameObject.tag == "Decal")
        {
            Destroy(other.gameObject);

            CreateSplat(other);
        }else
        {
            CreateSplat(other);
        }

    }

    void CreateSplat(Collision collision)
    {
        float y = Random.rotation.y * 100;
        float z = Random.rotation.z * 100;

        GameObject splat = (GameObject) Instantiate(GameObject.Find("Splat"), transform.position, Quaternion.Euler(90, y, z));

        Destroy(gameObject);

        GameObject.Find("DecalManager").GetComponent<DecalManager>().addDecal(splat);
    }
}
