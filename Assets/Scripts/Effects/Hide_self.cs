using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Hide_self : MonoBehaviour
{
    public IEnumerator Hide(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }
}
