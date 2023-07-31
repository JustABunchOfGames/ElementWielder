using UnityEngine;

namespace Attacks
{
    public class Shockwave : AttackForm
    {

        [SerializeField] private float _offset;
        [SerializeField] private Vector3 _rotation;

        private new void Awake()
        {
            base.Awake();

            transform.localPosition += transform.right * _offset;
            transform.localRotation *= Quaternion.Euler(_rotation.x, _rotation.y, _rotation.z);
        }
    }
}