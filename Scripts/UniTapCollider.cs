using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniTapCollider : MonoBehaviour {

    public InfinityManager infinityManager;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        if (infinityManager.curEvent == InfinityManager.Event.Shoot)
        {
            infinityManager.secondUnit = transform.parent.GetComponent<UnitManager>();
            infinityManager.PrepareToDamage();
        }
    }
}
