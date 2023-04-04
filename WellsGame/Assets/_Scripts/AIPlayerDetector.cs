using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.AIPlayerDetector
{
    public class AIPlayerDetector : MonoBehaviour
    {
        [field: SerializeField]
        public bool PlayerDetected {get; private set;}
        public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;

        [Header("OverlapBox parameters")]
        [SerializeField]
        private Transform detectorOrigin;
        public float detectorSize = 5f;
        public Vector2 detectorOriginOffset = Vector2.zero;

        public float detectionDelay = .3f;

        public LayerMask detectorLayerMask;

        [Header("Gizmo parameters")]
        public Color gizmoIdleColor = Color.green;
        public Color gizmoDetectedColor = Color.red;
        public bool showGizmos = true;

        private GameObject target;

        public GameObject Target
        {
            get => target;
            private set
            {
                target = value;
                PlayerDetected = target != null;
            }
        }
        
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(DetectionCoroutine());
        }

        IEnumerator DetectionCoroutine()
        {
            yield return new WaitForSeconds(detectionDelay);
            PerformDetection();
            StartCoroutine(DetectionCoroutine());
        }

        public void PerformDetection()
        {
            Collider2D collider = Physics2D.OverlapCircle((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, detectorLayerMask);
            if(collider != null)
            {
                Target = collider.gameObject;
            }
            else
            {
                Target = null;
            }
        }
        /*
        private void OnDrawGizmos()
        {
            if(showGizmos && detectorOrigin != null)
            {
                Gizmos.color = gizmoIdleColor;
                if(PlayerDetected)
                {
                    Gizmos.color = gizmoDetectedColor;
                }
                Gizmos.DrawCircle((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
            }
        }
        */
        // Update is called once per frame
        void Update()
        {
            
        }
        
    }
}