﻿using Assets.Scripts.Pickups.Structure;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {

        private float _health = 100;

        public float Speed = 7f;

        public IWeapon Weapon;

        public bool StartWithPistol;

        private GameManager _gameManager;

        // Use this for initialization
        void Start ()
        {
            if (StartWithPistol)
            {
                var pistolObj = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/pistol"));
                pistolObj.transform.SetParent(transform);
                pistolObj.transform.position = transform.FindChild("firePoint").position;
                pistolObj.transform.rotation = new Quaternion(0,0,0,0);

                Weapon = pistolObj.GetComponent<IWeapon>();
            }

            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        void AddModifier(Mod modifier)
        {
            switch (modifier.Target)
            {
                case PlayerStat.Hp:
                    Debug.Log(_health);

                    if (modifier.Type == "direct")
                    {
                        _health += modifier.Modifier;
                    }
                    else
                    {
                        _health = (int)(_health * modifier.Modifier);
                    }

                    Debug.Log(_health);
                    break;
                case PlayerStat.Attackspeed:
                    Debug.Log(Weapon.FireDelay);
                    if (modifier.Type == "direct")
                    {
                        Weapon.FireDelay -= modifier.Modifier;
                    }else{
                        Weapon.FireDelay /= modifier.Modifier;
                    }
                    Debug.Log(Weapon.FireDelay);
                    break;
                case PlayerStat.Speed:
                    Debug.Log(Speed);
                    if (modifier.Type == "direct")
                    {
                        Speed += modifier.Modifier;
                    }else{
                        Speed *= modifier.Modifier;
                    }
                    Debug.Log(Speed);
                    break;
            }
        }

        void RemoveModifier(Mod modifier)
        {
            switch (modifier.Target)
            {
                case PlayerStat.Hp:
                    Debug.Log(_health);

                    if (modifier.Type == "direct")
                    {
                        _health -= modifier.Modifier;
                    }
                    else
                    {
                        _health = (int)(_health / modifier.Modifier);
                    }

                    Debug.Log(_health);
                    break;
                case PlayerStat.Attackspeed:
                    Debug.Log(Weapon.FireDelay);
                    if (modifier.Type == "direct")
                    {
                        Weapon.FireDelay += modifier.Modifier;
                    }else{
                        Weapon.FireDelay *= modifier.Modifier;
                    }
                    Debug.Log(Weapon.FireDelay);
                    break;
                case PlayerStat.Speed:
                    Debug.Log(Speed);
                    if (modifier.Type == "direct")
                    {
                        Speed -= modifier.Modifier;
                    }else{
                        Speed /= modifier.Modifier;
                    }
                    Debug.Log(Speed);
                    break;
            }
        }

        public void ApplyDamage(float damage)
        {
            if (_health - damage <= 0)
            {
                _health = 0;
            }
            else
            {
                _health -= damage;
            }

            Debug.Log("player hit");

            _gameManager.UpdateHealth();
        }

        public float Health()
        {
            return _health;
        }
    }
}