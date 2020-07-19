using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text title;
    [SerializeField] Text description;
    [SerializeField] Text price;

    public int priceCount;

	private void Start()
	{
        price.text = priceCount.ToString();
	}
}
