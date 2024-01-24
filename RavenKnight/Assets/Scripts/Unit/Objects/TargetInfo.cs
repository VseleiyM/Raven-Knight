using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class TargetInfo : MonoBehaviour
    {
        public Collider2D PhysicsCollider => _physicsCollider;
        private Collider2D _physicsCollider;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public Animator Animator => _animator;
        private Animator _animator;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        private Rigidbody2D _rigidbody2D;
        public AudioSource AudioSource => _audioSource;
        private AudioSource _audioSource;
        public Target Target => _target;
        private Target _target;
        public MobInfo MobInfo => _mobInfo;
        private MobInfo _mobInfo;
        public PlayerInfo PlayerInfo => _playerInfo;
        private PlayerInfo _playerInfo;
        public Vector2 Normal => _normal;
        private Vector2 _normal;
        public Transform PointForProjectile => _pointForProjectile;
        [SerializeField] private Transform _pointForProjectile;
        public List<Collider2D> Colliders2D => _colliders2D;
        [SerializeField] private List<Collider2D> _colliders2D;

        private void Awake()
        {
            _physicsCollider = GetComponent<Collider2D>();
            _animator = GetComponentInChildren<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
            _target = GetComponent<Target>();
            _mobInfo = GetComponent<MobInfo>();
            _playerInfo = GetComponent<PlayerInfo>();
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 29)
                _normal = collision.contacts[collision.contacts.Length - 1].normal;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            _normal = Vector2.zero;
        }

        public Vector2 Project(Vector2 direction)
        {
            if (Vector2.Dot(direction, _normal) > 0)
                return direction;
            else
                return (direction - Vector2.Dot(direction, _normal) * _normal).normalized;
        }

        public void FlipModel(bool value)
        {
            Vector3 newScale = SpriteRenderer.gameObject.transform.localScale;
            if (value)
            {
                newScale = new Vector3(Mathf.Abs(newScale.x) * -1, newScale.y, newScale.z);
                SpriteRenderer.gameObject.transform.localScale = newScale;
            }
            else
            {
                newScale = new Vector3(Mathf.Abs(newScale.x), newScale.y, newScale.z);
                SpriteRenderer.gameObject.transform.localScale = newScale;
            }
        }
    }
}