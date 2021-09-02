using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bloodSplat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();   
    }

    private void Fire()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;
           
            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1<<9 | 1<< 0))
            {
                //get reference to object hit health system.
                Health health = hitInfo.collider.GetComponent<Health>();

                if(health != null)
                {
                    Instantiate(bloodSplat, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    health.Damage(50);
                }
            }
        }
    
    }
}
