using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
    public static ItemCreator i;
    public GameObject globe;

    public GameObject activeAlert;
    Bounds bounds;
    Vector3 xaxis= new Vector3(1,0,0);
    Vector3 yaxis = new Vector3(0,1,0);

    Vector3 standard = Vector3.zero;


    ItemInfo itemInfo;


    public bool isActive;

    public void ToggleActive()
    {
        isActive = isActive ?  false : true;

        if(isActive){
            activeAlert.SetActive(true);
            GlobeController.instance.ChangeMode(GlobeMode.Plant);
        }
    }

    public void SetItemInfo(ItemInfo item){
        this.itemInfo = item;
    }

    void Update()
    {
        if(isActive && Input.touchCount > 0 &&  Input.touches[0].phase == TouchPhase.Ended) 
        {
            if(GlobeController.instance.isDragging) return;

            if(Input.touches[0].phase != TouchPhase.Ended) return;
            Vector3 touchPos = Input.GetTouch(0).position;


            RaycastHit hitObj;
            Ray ray = Camera.main.ScreenPointToRay(touchPos);

            if(Physics.Raycast(ray, out hitObj, Mathf.Infinity))
            {
                Debug.DrawRay(ray.origin, hitObj.point-ray.origin, Color.red, 5f);
                CreateTree(hitObj.point);
            }
 
        }
    }

    private void Awake() {
        i = this;
        
    }
    
    // Update is called once per frame
    private void Start() {
        
        isActive = false;  
        SetGlobeObjects(); 
        // zaxis = globe.transform.position - cam.transform.position;
    }

    public void SetGlobeObjects(){
            if(GameManager.i.getTreeList()!=null){
                foreach(Tree t in GameManager.i.getTreeList().trees){
                    GameObject prefab = ItemManager.i.GetPrefab(t.tree_name);
                
                    if(prefab != null){
                        placeTree(prefab,t);
                    }
                }
            }
        }
    
    public void SwitchPlaceMode(ItemInfo info){
        SetItemInfo(info);
        Invoke("ToggleActive",0.5f);
    }

    public void placeTree(GameObject item,Tree tree){
        
        Vector3 originalScale = item.transform.localScale;
        Vector3 pos = new Vector3((float)tree.position_x, (float)tree.position_y,(float)tree.position_z);
        Quaternion lookRotation = Quaternion.FromToRotation(Vector3.up, pos);

        GameObject clone = Instantiate(item, pos, lookRotation);   
        clone.transform.localScale = originalScale * 0.1f;
        
        clone.transform.parent = globe.transform;
        clone.transform.localPosition = pos;
    }

    public void CreateTree(Vector3 pos){
            if(itemInfo != null){
                Emotion emotion = StringToEnum(itemInfo.emotion);


                GameObject item = ItemManager.i.GetCollection(emotion).getPrefab(itemInfo.prefabName);
                Vector3 direction = globe.transform.position- pos;
                pos += direction * 0.012f;
                Quaternion lookRotation = Quaternion.FromToRotation(Vector3.up, -1f*direction);
                

                Vector3 originalScale = item.transform.localScale;
                GameObject clone = Instantiate(item, pos, lookRotation);  
                Debug.Log("pos " +pos);  

                clone.transform.localScale = originalScale*0.1f;
                clone.transform.parent = globe.transform;

                Debug.Log("clone postion "+clone.transform.position);
                 Debug.Log("clone local postion "+clone.transform.localPosition);

                Tree tree = new Tree(clone.transform.localPosition, itemInfo.prefabName);
                GlobeController.instance.ChangeMode(GlobeMode.View);
                
                Backend.i.CreateTree(tree,(message)=>{
                    GameManager.i.GetUser().UpdatePoints(emotion, -1*itemInfo.cost);
                    Backend.i.UpdateUser((user)=>{
                        GameManager.i.SetUser(user);
                    });

                    itemInfo = null;
                    isActive = false;
                    activeAlert.SetActive(false);
                });
                
            }

    }


    public Emotion StringToEnum(string str){       
        string em = char.ToUpper(str[0]) + str[1..];
        return (Emotion)Enum.Parse(typeof(Emotion), em);
    }
}
