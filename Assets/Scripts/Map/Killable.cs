using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnDie;

    public void Die()
    {
            Debug.Log("just Die");
        if (OnDie != null)
        {
            OnDie.Invoke();
        }
    }
}
