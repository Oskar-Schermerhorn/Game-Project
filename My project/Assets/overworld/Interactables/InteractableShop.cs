using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableShop : Interactable
{
    public static event Action<List<ShopInfo>> OpenShop;
    [SerializeField] List<ShopInfo> shopItems = new List<ShopInfo>();
    [SerializeField] GameObject ShopPanel;
    public override void Use()
    {
        StartCoroutine(OpenShopCoroutine());
    }
    IEnumerator OpenShopCoroutine()
    {
        GameObject ShopMenu = Instantiate<GameObject>(ShopPanel, GameObject.Find("Canvas").transform);
        ShopMenu.name = "ShopPanel";
        yield return new WaitForEndOfFrame();
        OpenShop(shopItems);
    }
}
