using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonsScaler : MonoBehaviour
{
    [SerializeField] Vector3 scaleSize;

    public void PointerEnter() {
        transform.localScale = scaleSize;
    }

    public void PointerExit() {
        transform .localScale = new Vector3(1,1,1);
    }
}
