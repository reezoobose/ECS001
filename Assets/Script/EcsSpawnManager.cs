using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = UnityEngine.Random;

namespace Script
{
    public class EcsSpawnManager : MonoBehaviour
    {
        private static World WorldEntity => World.DefaultGameObjectInjectionWorld;
        private static EntityManager Manager => WorldEntity.EntityManager;
        public GameObject sheepPrefab;
        private const int NumberOfSheep = 9990;
        public float xOffset = 56f;
        public float zOffset = 55f;
        public float zOffsetMin = -8f;
        public float xOffsetMin = -5f;
        private const float YPosition = 0f;

        private void Start()
        {
            // create a game object conversion setting . This setting will be used in  converting a game object in entity .
            var setting = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
            //Convert game object in entity .
            var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(sheepPrefab, setting);
            //Create objects
            for (var i = 0; i < NumberOfSheep; i++)
            {
                //Create the entity .
                var instance = Manager.Instantiate(prefab);
                var xPosition = Random.Range(xOffsetMin, xOffset);
                var zPosition = Random.Range(zOffsetMin, zOffset);
                //Crate a float 3 . 
                var modifiedPosition = new float3(xPosition,YPosition,zPosition);
                //Convert it in vector 3 .
                var position = transform.TransformPoint(modifiedPosition);
                //create translation .
                var entityTransform = new Translation { Value = position };
                //Create a quaternion .
                var angle = new quaternion(0,0,0,0);
                //Create rotation .
                var entityRotation = new Rotation{Value = angle};
                //Add position component .
                Manager.SetComponentData(instance,entityTransform);
                //Add rotation component .
                Manager.SetComponentData(instance,entityRotation);
            }
            
        }
    }
}
