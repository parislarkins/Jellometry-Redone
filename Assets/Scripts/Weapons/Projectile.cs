﻿using System;
using UnityEngine;
using Assets.Scripts;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{

    private float _damage;
    private Transform _owner;

    private PrefabManager _prefabManager;
    private GameObject _splatObj;

    private void Start()
    {
        _prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
        _splatObj = _prefabManager.Get("Splat");
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.transform);
        if (other.transform == _owner) return;
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("ApplyDamage",_damage);
        }else if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            other.gameObject.SendMessage("ApplyDamage",_damage);
        }else if (other.gameObject.tag == "Decal")
        {
            Destroy(other.gameObject);
            CreateSplat(other);
        }else if (other.gameObject.tag == "DefaultShrine")
        {
            other.transform.parent.SendMessage("ApplyDamage",_damage);
        }else
        {
            CreateSplat(other);
        }

        Destroy(gameObject);
    }

    public void SetOwner(Transform owner)
    {
        _owner = owner;
        Debug.Log("set owner to: " + owner);
    }

    void CreateSplat(Collision collision)
    {
        float y = Random.rotation.y * 100;
        float z = Random.rotation.z * 100;

        GameObject splat = (GameObject) Instantiate(_splatObj, transform.position, Quaternion.Euler(135, y, z));

        splat.transform.position = transform.position;
        splat.transform.forward = -collision.contacts[0].normal;
        splat.transform.Rotate(45,0,0);

        GameObject.Find("DecalManager").GetComponent<DecalManager>().AddDecal(splat);
    }

    public Transform GetOwner()
    {
        return _owner;
    }
}
