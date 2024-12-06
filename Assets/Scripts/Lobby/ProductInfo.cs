using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductInfo : MonoBehaviour
{
    [Header("초상화"),SerializeField] Image _productImg;
    [Header("이름"), SerializeField] TextMeshProUGUI _productName;
    [Header("가격"), SerializeField] TextMeshProUGUI _productPrice;
    [Header("선택 버튼"), SerializeField] Button _productButton;

    [SerializeField] int _productIndex = 0;

    CChar _productChar = new CChar();

    public bool _isPurchased = false;

    private void Awake()
    {
        _productButton.onClick.AddListener(() => Click_Select());
    }
    void Click_Select()
    {
        SoundManager.Instance.PlaySound(eUISOUNDTYPE.Button);
        LobbyUIManager.Instance.Click_Select(_productChar);
    }
    public void SetInfo(Sprite img, CChar charInfo)
    {
        _productChar = charInfo;

        _productImg.sprite = img;
        _productName.text = _productChar._charName;
        _productPrice.text = _productChar._price.ToString();
        _isPurchased = _productChar._isPurchased;
    }
    public void SetShopIndex(int index)
    {
        _productIndex = index;
    }

}
