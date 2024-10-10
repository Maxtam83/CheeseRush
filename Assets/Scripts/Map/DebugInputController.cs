using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugInputController : MonoBehaviour
{

    Coroutine m_coroutine;

    public void Move(InputAction.CallbackContext context)
    {
        if (m_coroutine != null)
        {
            m_coroutine = StartCoroutine(Moving(context));
        }
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("jump");
    }

    IEnumerator Moving(InputAction.CallbackContext context)
    {
        Vector2 value;
        Debug.Log("test");
        while (context.started)
        {
            value = transform.position;
            Debug.Log(value);
            yield return null;
        }
        m_coroutine = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
