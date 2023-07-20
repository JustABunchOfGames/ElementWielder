using UnityEngine;

namespace Attacks
{
    public class BasicAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _castPoint;
        [SerializeField] private GameObject _basicAttackPrefab;

        [SerializeField] private float _speed;

        [SerializeField] private float _cooldown;
        private float _cooldownTimer = 0;
        private bool _canCast = false;

        private void Update()
        {
            if (Input.GetButton("BasicAttack") && _canCast)
            {
                _canCast = false;

                GameObject attack = Instantiate(_basicAttackPrefab, _castPoint.transform.position, _castPoint.transform.rotation);

                attack.GetComponent<Rigidbody2D>().velocity = _castPoint.transform.right * _speed;
            }

            if (!_canCast)
            {
                _cooldownTimer += Time.deltaTime;
                
                if (_cooldownTimer > _cooldown)
                {
                    _canCast = true;
                    _cooldownTimer = 0;
                }
            }
        }
    }
}