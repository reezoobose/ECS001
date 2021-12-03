using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject sheepPrefab;
        private const int NumberOfItems = 9000 ;
        public float xOffset = 56f;
        public float zOffset = 55f;
        public float zOffsetMin = -8f;
        public float xOffsetMin = -5f;

        private void Awake()
        {
            Spawn();
        }

        private void Spawn()
        {
            for (var i = 0; i < NumberOfItems; i++)
            {
                var sheep = Instantiate(sheepPrefab, transform, true);
                var xPosition = Random.Range(xOffsetMin, xOffset);
                const float yPosition = 0f;
                var zPosition = Random.Range(zOffsetMin, zOffset);
                var modifiedPosition = new Vector3(xPosition,yPosition,zPosition);
                sheep.transform.position = modifiedPosition;
                sheep.transform.rotation = Quaternion.identity;
                
            }
        }
    }
}
