using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIndex : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int index = transform.GetSiblingIndex();
        Debug.Log("hi, my index is " + index + "!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
