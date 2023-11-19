using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;
public class BuyButton : MonoBehaviour
{
    [NonSerialized] public UnityEngine.UI.Button button;
    [NonSerialized] public TextMeshProUGUI displayPanelText;
    [NonSerialized] public TextMeshProUGUI displayButtonText;
    public string[] displayText;
    public int baseCost;
    public float costPlusRatio;
    public int cost;
    public bool canBuy;
    public Level level;
    [Serializable]
    public enum ProductType
    {
        Parameter,
        Equipment,
    }
    public void C_BuyButtonInitialize()
    {
        button = transform.GetChild(1).GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(BuyButtonOnClick);
        displayPanelText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        displayButtonText = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void C_BuyButtonUpdate()
    {
        cost = (level.now == 0) ? baseCost : (int)(baseCost * (costPlusRatio * level.now));
        displayButtonText.SetText((level.now >= level.max) ? "MAX" : cost.ToString());
        if (level.now >= level.max)
        {
            displayPanelText.SetText("SoldOut!");
        }
        C_CanBuyJudge();
    }
    public void C_CanBuyJudge()
    {
        canBuy = (level.now < level.max && cost < World.score) ? true : false;
    }
    public void C_Buy()
    {
        World.score -= cost;

        // ボタンのSelectをリセットする
        EventSystem.current.SetSelectedGameObject(null);
    }
    protected virtual void BuyButtonOnClick() { }

    public void C_EquipmentChenge()
    {

    }
}
[Serializable] public class UIGenerator
{
    [SerializeField] protected private GameObject obj;
    public GameObject Obj
    {
        get { return obj; }
    }
}

[Serializable] public class UI_Static : UIGenerator
{
    [NonSerialized] public GameObject clone;
    [SerializeField] private bool displaying;

    public UI_Static() { }

    public void UIUpdate()
    {
        displaying = clone;
    }

    public bool Displaying
    {
        get { return displaying; }
    }
    public void InstantiateUI()
    {
        displaying = (clone == null) ? false : true;
        if (displaying == false)
        {
            clone = GameObject.Instantiate(obj);

            displaying = true;
        }
    }
    public void InstantiateUI(string tagChangeName)
    {
        displaying = (clone == null) ? false : true;
        if (displaying == false)
        {
            clone = GameObject.Instantiate(obj);
            clone.tag = tagChangeName;
            displaying = true;
        }
    }

    public void CloseUI()
    {
        displaying = (clone == null) ? false : true;
        if (clone != null)
        {
            if (displaying == true)
            {
                GameObject.Destroy(clone);
                displaying = false;
            };
        }
    }
    public void CloseUI(Action a)
    {
        displaying = (clone == null) ? false : true;
        if (clone != null)
        {
            if (displaying == true)
            {
                GameObject.Destroy(clone);
                displaying = false;
                a();
            }
        }
    }
}


[Serializable] public class UI_Instant : UIGenerator
{
    [NonSerialized] public GameObject clone;
    [SerializeField] private int displayed = 0;

    public UI_Instant() { }

    public UI_Instant(GameObject referenceObj, bool value)
    {
        referenceObj.SetActive(value);
        obj = referenceObj;
    }
    public void InstantiateUI()
    {
        clone = GameObject.Instantiate(obj);
        displayed++;
    }
    public int Displayed
    {
        get { return displayed; }
    }
}



/// <summary>
/// Unity StartにInitializeを書く<br/>
/// OnValueChangedに処理を書く<br/>
/// ApplyにSaveSettingメソッドを書く
/// </summary>
[Serializable]
public class SliderClass : MonoBehaviour
{
    [NonSerialized] public Slider slider;

    protected virtual void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    protected virtual void OnValueChanged(float sliderValue)
    {

    }
    protected virtual void Apply()
    {

    }
}