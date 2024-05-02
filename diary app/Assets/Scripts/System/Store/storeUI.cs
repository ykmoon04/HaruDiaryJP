using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using System.Text;

public class storeUI : MonoBehaviour
{

    public static storeUI i;
    [SerializeField]
    ScrollRect scrollView; // 패널의 루트
    [SerializeField]
    GameObject tabs; // 버튼의 루트
    List<Transform> panels;
    List<Image> btns;

    [SerializeField]
    GameObject resources;
    [SerializeField]
    Sprite resourceIcon;

    // 아이템 관련
    [SerializeField]
    GameObject itemBox; 

    ItemInfoList itemList = null; // 모든 아이템 정보 json에서 읽어와 저장
    Vector3 originalScale;

    // 제어
    private GameObject curActivePanel;
    private int curActiveIdx = -1;


    public void Start()
    {
        // Init();
        if(i==null) i=this;
    }

    public void Init(){
        panels = new List<Transform>();
        panels = scrollView.transform.GetChild(0).GetComponentsInChildren<Transform>(true).ToList();
        panels.RemoveAt(0);

        btns = tabs.transform.GetComponentsInChildren<Image>(true).ToList();
       
        originalScale = itemBox.transform.localScale;

        TextAsset jsonData = Resources.Load("Json/items") as TextAsset;
        itemList = JsonUtility.FromJson<ItemInfoList>(jsonData.ToString());
        itemList.Init();

        foreach(Emotion e in Enum.GetValues(typeof(Emotion))){
            SetStore(e);
        }

        SetResourcePanel((int)Emotion.Joy);
        curActivePanel = panels[(int)Emotion.Joy].gameObject;
        curActiveIdx = (int)Emotion.Joy;
        
    }

    public void ReloadStore(){
        foreach(Emotion e in Enum.GetValues(typeof(Emotion))){
            SetStore(e);
            
        }
    }

    public void BuyItem(ItemInfo info){
        this.gameObject.SetActive(false);

        ItemCreator.i.SwitchPlaceMode( info);
    }



    public void ActivatePanel(int target){
        if((int)target < panels.Count){
            
            if(curActivePanel != null && curActiveIdx >= 0){
                // 현재 패널 비활성화
                panels[curActiveIdx].gameObject.SetActive(false); 

                RectTransform view = panels[curActiveIdx].gameObject.GetComponent<RectTransform>();

                float x = view.anchoredPosition.x;
                view.anchoredPosition = new Vector3(x, 0, 0);
            }

            // 패널을 클릭된 타겟으로 업데이트
            curActivePanel = panels[target].gameObject;
            curActivePanel.SetActive(true);
            scrollView.content = curActivePanel.GetComponent<RectTransform>();

            // 리소스 부분
            SetResourcePanel(target);
        }
    }

    public void SetResourcePanel(int target){
            Image icon = resources.transform.GetChild(0).GetComponent<Image>();
            icon.sprite = resourceIcon;
            
            Color color;
            ColorUtility.TryParseHtmlString(MyColor.getColor((Emotion)target), out color); 
            icon.color = color;

            if( GameManager.i.GetUser()!=null){
                resources.GetComponentInChildren<Text>().text = GameManager.i.GetUser().getPoint((Emotion)target).ToString();
            }
    }

    public void ActivateBtn(int target){

        Color inactiveColor = btns[curActiveIdx].color;
        Color activeColor = btns[target].color;

        inactiveColor.a = 0.3f;
        activeColor.a = 1f;

        btns[curActiveIdx].color = inactiveColor;
        btns[curActiveIdx].GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
        btns[target].color = activeColor;
        btns[target].GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
    }

    public void OnHappyBtnClick(){
        ActivatePanel((int)Emotion.Joy);
        ActivateBtn((int)Emotion.Joy);

        curActiveIdx = (int)Emotion.Joy;
    }

    public void OnSadBtnClick(){
        ActivatePanel((int)Emotion.Sadness);
        ActivateBtn((int)Emotion.Sadness);

        curActiveIdx = (int)Emotion.Sadness;
    }

    public void OnAngryBtnClick(){
        ActivatePanel((int)Emotion.Angry);
        ActivateBtn((int)Emotion.Angry);
        
        curActiveIdx = (int)Emotion.Angry;
    }

    public void OnDisgustBtnClick(){
        ActivatePanel((int)Emotion.Disgust);
        ActivateBtn((int)Emotion.Disgust);
        
        curActiveIdx = (int)Emotion.Disgust;
    }

    public void OnFearBtnClick(){
        ActivatePanel((int)Emotion.Fear);
        ActivateBtn((int)Emotion.Fear);
        
        curActiveIdx = (int)Emotion.Fear;
    }

    public void OnSurpriseBtnClick(){
        ActivatePanel((int)Emotion.Surprise);
        ActivateBtn((int)Emotion.Surprise);
        
        curActiveIdx = (int)Emotion.Surprise;
    }



    public void SetStore(Emotion e){
        int pt = GameManager.i.GetUser().getPoint(e);
        SetResourcePanel((int)e);
        if(itemList.data[e] != null){
            foreach(ItemInfo itemInfo in itemList.data[e]){
                // 아이템 추가
                GameObject clone = Instantiate(itemBox);

                clone.transform.GetChild(0).GetComponent<Text>().text = itemInfo.itemName; // 이름
                clone.transform.GetChild(1).GetComponent<Image>().sprite = ItemManager.i.GetCollection(e).getThumbnail(itemInfo.prefabName); // 썸네일

                Color color;
                ColorUtility.TryParseHtmlString(MyColor.getColor(e), out color); 
                clone.transform.GetChild(2).GetComponent<Image>().color = color; // 색 설정 

                Button costBtn = clone.transform.GetChild(3).gameObject.GetComponent<Button>();
                // costBtn.transform.GetChild(0).GetComponent<Image>().color = color;
                ColorBlock cb = costBtn.colors;
                cb.normalColor = color;
                costBtn.colors = cb;

                costBtn.transform.GetChild(1).GetComponent<Text>().text = itemInfo.cost.ToString();

                if(itemInfo.cost > pt){
                    costBtn.interactable =false; 
                }
                costBtn.onClick.AddListener(()=>{
                    BuyItem(itemInfo);
                });

                clone.transform.SetParent(panels[(int)e]); // 패널에 할당
                clone.transform.localPosition = new Vector3(clone.transform.position.x, clone.transform.position.y, 0f);

                clone.transform.localScale = originalScale;
            }
        }
    }
    public void UpdateStore(){
        Debug.Log("상점을 업데이트");
        foreach(Emotion e in Enum.GetValues(typeof(Emotion))){
            UpdateStore(e);
        }
        SetResourcePanel(curActiveIdx);

    }

    public void UpdateStore(Emotion e){
        int pt = GameManager.i.GetUser().getPoint(e);
        Button[] btns = panels[(int)e].GetComponentsInChildren<Button>();
        foreach(Button btn in btns){
            if(Int32.Parse(btn.transform.GetComponentInChildren<Text>().text) > (int)pt){
                btn.interactable =false; 
            }
            else{
                btn.interactable =true; 
            }
        }
    }
}



[System.Serializable]
public class ItemInfo{
    public string emotion;
    public string itemName;
    public string prefabName;
    public int cost;
}

[System.Serializable]
public class ItemInfoList{
    
    [SerializeField]
    private List<ItemInfo> happiness;
    [SerializeField]
    private List<ItemInfo> sadness;
    [SerializeField]
    private List<ItemInfo> angry;
    [SerializeField]
    private List<ItemInfo> fear;
    [SerializeField]
    private List<ItemInfo> surprise;
    [SerializeField]
    private List<ItemInfo> disgust;

    public Dictionary<Emotion, List<ItemInfo>> data;

    public void Init(){
        data = new Dictionary<Emotion, List<ItemInfo>>();
        data.Add(Emotion.Joy, happiness);
        data.Add(Emotion.Sadness, sadness);
        data.Add(Emotion.Angry, angry);
        data.Add(Emotion.Fear, fear);
        data.Add(Emotion.Surprise, surprise);
        data.Add(Emotion.Disgust, disgust);
    }
}
