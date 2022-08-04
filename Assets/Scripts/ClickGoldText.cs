using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ClickGoldText : MonoBehaviour
{
    Camera cam;
    [SerializeField] Text goldText;

    private void Awake()
    {
        cam = Camera.main;
    }
    public void Spawn(double value)
    {
        gameObject.SetActive(true);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, cam,out Vector2 pos);
        (transform as RectTransform).anchoredPosition = pos;

        goldText.text = "+" + value.ToString("0.00");

        Invoke("StoreObject", 2);
    }

    void StoreObject()
    {
        GameManager.Instance.StoreClickObjectText(this);
        gameObject.SetActive(false);
    }
}
