using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // Option 2: Get authority to move directly
        if (!hasAuthority)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(Vector3.up);
        }
    }
}
