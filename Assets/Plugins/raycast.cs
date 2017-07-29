using UnityEngine;
using System.Collections;
public class raycast : MonoBehaviour
{
    
    void Update()
    {
         Vector3 fwd = transform.TransformDirection(Vector3.forward);
         RaycastHit hit;
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

         if (Input.GetMouseButtonDown(0))
         {
             print("mouse click");
     
        }

         // shoots a laser and if it hits a obj it will execute
        if (Physics.SphereCast(transform.position, 1f,fwd, out hit) && Input.GetMouseButtonDown(0))
        {

            if (Physics.SphereCast(transform.position, 1f, fwd, out hit))
                if (hit.collider != null && hit.collider.gameObject.name == "Sphere")
                {
                    Application.LoadLevel("room1");   // to load level
                }

        }



    }
}