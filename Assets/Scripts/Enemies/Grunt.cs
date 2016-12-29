﻿using Assets.Scripts;
using UnityEngine;

    public class Grunt : MonoBehaviour, IEnemy
    {

        public EnemyType Type { get; set; }
        public int Buffs { get; set; }

        private GameObject _player;

        public float _baseSpeed;

        public float _baseDamage;

//        public float Damage;

        public void Start()
        {
            Type = EnemyType.Grunt;

            Buffs = 0;

            Damage = BaseDamage = _baseDamage;

            Speed = BaseSpeed = _baseSpeed;

            _player = GameObject.Find("player");
        }

        // Update is called once per frame
        public void FixedUpdate () {
            transform.LookAt(_player.transform);
            float speedChanged = Speed * Time.fixedDeltaTime;

            transform.Translate(Vector3.forward * speedChanged);
            transform.rotation = new Quaternion(0f,transform.rotation.y,0f,0f);
        }

        public void ApplySlow(float slow)
        {
            Speed = BaseSpeed / slow;
        }

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Player>().ApplyDamage(Damage);
            }
        }

        public new EnemyType GetType()
        {
            return Type;
        }

        public float Damage { get; set; }
        public float Speed { get; set; }
        public float BaseSpeed { get; set; }
        public float BaseDamage { get; set; }
    }
