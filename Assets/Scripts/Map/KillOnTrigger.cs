using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTrigger : MonoBehaviour
{
    public List<string> m_Tags = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (m_Tags.Contains(other.tag))
        {
            Killable killable = other.GetComponent<Killable>();
            if (killable != null)
            {
                killable.Die();
            }
        }
    }
}
