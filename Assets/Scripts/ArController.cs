// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR.ARFoundation;
//
// public class ArController : MonoBehaviour
// {
//     [SerializeField] ARSession m_Session;
//     [SerializeField] ARRaycastManager m_RaycastManager;
//
//     [SerializeField] private GameObject swimmingPoolPrefab;
//     [SerializeField] private UnityEngine.UI.Text _text;
//     private bool _instantiated;
//     IEnumerator Start() {
//         if ((ARSession.state == ARSessionState.None) ||
//             (ARSession.state == ARSessionState.CheckingAvailability))
//         {
//             yield return ARSession.CheckAvailability();
//         }
//
//         if (ARSession.state == ARSessionState.Unsupported)
//         {
//             // Start some fallback experience for unsupported devices
//         }
//         else
//         {
//             // Start the AR session
//             m_Session.enabled = true;
//         }
//     }
//     
//
//     List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
//
//     void Update()
//     {
//         if (Input.touchCount == 0)
//             return;
//
//         if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
//         {
//             // Only returns true if there is at least one hit
//             HandleRaycast(m_Hits[0]);
//         }
//     }
//     
//     void HandleRaycast(ARRaycastHit hit)
//     {
//         if (!_instantiated && hit.trackable is ARPlane plane)
//         {
//             // SetText($"Raycast hit a {hit.hitType}; \n position: {hit.pose}");
//             // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//             GameObject go = Instantiate(swimmingPoolPrefab);
//             go.transform.position = hit.pose.position;
//             go.transform.rotation = hit.pose.rotation;
//             _instantiated = true;
//         }
//         else
//         {
//             // What type of thing did we hit?
//             SetText($"Raycast hit a {hit.hitType}");
//         }
//     }
//
//     void SetText(string text)
//     {
//         // _text.text = text;
//     }
// }
//
