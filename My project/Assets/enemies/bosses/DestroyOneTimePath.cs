using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOneTimePath : MonoBehaviour
{
    [SerializeField] GameObject path;
    private void OnDisable()
    {
        path.SetActive(false);
    }
}
