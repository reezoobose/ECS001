using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Jobs;
using Unity.Jobs;

namespace Script
{
    public class SpanParallelAndMove : MonoBehaviour
    {
        public GameObject sheepPrefab;
        private const int NumberOfItems = 9000 ;
        public float xOffset = 56f;
        public float zOffset = 55f;
        public float zOffsetMin = -8f;
        public float xOffsetMin = -5f;
        private Transform[] _items;
        private TransformAccessArray _transformAccessArray;
        private MoveJob _moveJob;
        private JobHandle _jobHandler;
        public bool withJobSystem;
        
        public void Start()
        {
            _items = new Transform[NumberOfItems];
            for (var i = 0; i < NumberOfItems; i++)
            {
                _items[i] = Instantiate(sheepPrefab, transform, true).transform;
                var xPosition = Random.Range(xOffsetMin, xOffset);
                const float yPosition = 0f;
                var zPosition = Random.Range(zOffsetMin, zOffset);
                var modifiedPosition = new Vector3(xPosition,yPosition,zPosition);
                _items[i].transform.position = modifiedPosition;
                _items[i].transform.rotation = Quaternion.identity;
            }
            if(withJobSystem)_transformAccessArray = new TransformAccessArray(_items);
        }


        private void Update()
        {
            
            if(withJobSystem)
            {
                _moveJob = new MoveJob();
                _jobHandler = _moveJob.Schedule(_transformAccessArray);
                
            }
            else
            {
                foreach (var item in _items) Move(item);
            }


        }


        private void LateUpdate()
        {
            // we need to call complete before e move to next update .
           if(withJobSystem) _jobHandler.Complete();
        }

        private void OnDestroy()
        {
            //Cleanup once done. 
            //As it is native array we need to clean up manually .
           if(withJobSystem) _transformAccessArray.Dispose();
        }

        private void Move(Transform item)
        {
            item.Translate(0f, 0f, 0.1f);
            if (item.position.z < zOffset) return;
            ResetPosition(item);
        }

        private void ResetPosition(Transform item)
        {
            var newZ = zOffsetMin;
            var currentPosition = item.position;
            var newY = currentPosition.y;
            var newX = currentPosition.x;
            var newPosition = new Vector3(newX, newY, newZ);
            item.position = newPosition;
        }
        
        
        private struct MoveJob : IJobParallelForTransform
        {
            public void Execute(int index, TransformAccess access)
            {
                access.position += 0.1f *  (access.rotation * new Vector3(0,0,1));
                if (!(access.position.z > 55f)) return;
                const float newZ = -5f;
                var currentPosition = access.position;
                var newY = currentPosition.y;
                var newX = currentPosition.x;
                var newPosition = new Vector3(newX, newY, newZ);
                access.position = newPosition;
            }
        }

    }
}
