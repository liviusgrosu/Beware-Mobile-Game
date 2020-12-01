using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyLocation : MonoBehaviour
{
    public bool copyX, copyY, copyZ; 
    private Transform referenceObj; 
    
    public void CopyLocationOf(Transform referenceObj)
    {
        this.referenceObj = referenceObj;
    }

    public void ToggleAxis(int axis)
    {
        switch(axis)
        {
            case 1: copyX = true; break;
            case 2: copyY = true; break;
            case 3: copyZ = true; break;
        }
    }

    private void Update()
    {
        Debug.Log(referenceObj != null);
        if (referenceObj != null)
        {
            float newY = copyY ? transform.position.x: referenceObj.position.x;

            transform.position = new Vector3(   copyX ? referenceObj.position.x : transform.position.x,
                                                copyY ? referenceObj.position.y : transform.position.y,
                                                copyZ ? referenceObj.position.z : transform.position.z
                                            );
        }
    }
}
