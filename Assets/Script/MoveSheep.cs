using UnityEngine;

namespace Script
{
    public class MoveSheep : MonoBehaviour
    {
        public SpawnManager managerReference;

        private void Start()
        {
            managerReference = FindObjectOfType<SpawnManager>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            gameObject.transform.Translate(0f, 0f, 0.1f);
            if (gameObject.transform.position.z > managerReference.zOffset)
            {
                Reset();
            }
        }

        private void Reset()
        {
            var newZ = managerReference.zOffsetMin;
            var currentPosition = transform.position;
            var newY = currentPosition.y;
            var newX = currentPosition.x;
            var newPosition = new Vector3(newX, newY, newZ);
            gameObject.transform.position = newPosition;
        }
    }
}