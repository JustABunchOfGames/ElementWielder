using UnityEngine;

namespace Player
{
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private void Update()
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);

            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;

            // Rotate Player
            transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        }
    }
}