using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor;
using UnityEngine;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Lscale_Ray : MonoBehaviour
{
    [Header("光线长度乘数,勿轻易修改")]
    public float lengthMul = 1.0f;
    RaycastHit hit;
    private Transform light0;
    // Start is called before the first frame update
    void Start()
    {
        light0 = transform.parent.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position,transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            if (transform.parent.parent!= null)
            {
                //Debug.Log(transform.parent.parent.name);
                Vector3 pS = transform.parent.lossyScale;
                light0.transform.localScale = new Vector3(6  / pS.x, 6  / pS.y, 6f * lengthMul * hit.distance  / pS.z);
            }
            else {
                Vector3 pS = transform.parent.lossyScale;
                light0.transform.localScale = new Vector3(1 ,1, lengthMul * hit.distance  );
            }
            
            //Debug.Log(light.transform.localScale);
        }
        //Debug.Log(hit.distance);
    }
    public float getDistance() { return hit.distance; }
}
