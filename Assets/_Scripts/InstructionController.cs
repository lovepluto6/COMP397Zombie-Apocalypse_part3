using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionController : MonoBehaviour
{
    public float t1, t2;
    // Start is called before the first frame update
    void Start()
    {
        t1 = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        t2 = Time.fixedTime;
        if (t2 - t1 >= 5)
        {
            Destroy(this.gameObject);
        }

    }
}
