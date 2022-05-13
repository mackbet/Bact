    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panel_script : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject container;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0 , 1 + container.transform.childCount*3);
    }
}
