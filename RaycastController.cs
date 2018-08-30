using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour {

    public GameObject obj1, obj2;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = new Ray(obj1.transform.position, obj2.transform.position);

            bool isHit = Physics.Raycast(ray, out hit, 1000);
            if (isHit)
            {
                Debug.Log(hit.transform.name);
                Debug.Log(hit.distance);
            }

            GameObject lineObj = new GameObject();
            LineRenderer line = lineObj.AddComponent<LineRenderer>();
            line.SetPosition(0, ray.origin);

            if (isHit)
                line.SetPosition(1, hit.point);
            else line.SetPosition(1, ray.direction);

            line.SetWidth(0.05f, 0.05f);
            Destroy(lineObj, 1);
        }
    }
}
