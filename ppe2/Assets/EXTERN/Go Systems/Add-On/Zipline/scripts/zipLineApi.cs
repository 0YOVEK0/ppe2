using UnityEngine;
namespace GoSystem
{
    [GBehaviourAttributeAttribute("ZipLine Hub", false)]
    public class zipLineApi : GoSystemsBehaviour
    {
        public Transform PointA;
        public Transform PointB;
        public Transform PointC;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(PointA.transform.position, 0.1f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(PointB.transform.position, 0.1f);
        }
    }
}