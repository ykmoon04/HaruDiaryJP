using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InitializeScript : MonoBehaviour
{
    public GameObject popup;
    public Button btn;
    public TMP_InputField inputField;
    void Start()
    {
        LoadingWindow.i.StartLoading();
        

        string token = LoadToken();        
        if (!string.IsNullOrEmpty(token))
        {
            AutoLogin(token);
        }
        else
        {
            btn.onClick.AddListener(()=>{
                popup.SetActive(false);

                Backend.i.CreateUser(inputField.text,(user)=>{
                    SaveToken(user.GetId());
                    GameManager.i.SetUser(user);
                    LoadSceneManager.i.ToMain();    
                    });
                });

            LoadingWindow.i.EndLoading(2f,()=>{
                popup.SetActive(true);
            });
        }
    }

    public void AutoLogin(string id){
       Backend.i?.ReadUser(id, (user)=>{
            GameManager.i.SetUser(user);
            LoadSceneManager.i.ToMain();
            /*
            Backend.i?.ReadAllObjects(GameManager.i.GetUser().GetId(),(treeList)=>{

                // if(treeList != null) GameManager.i.SetTreeList(treeList);
                LoadSceneManager.i.ToMain();
            });
            */
            
        }); 
    }


    public void SaveToken(string token)
    {
        PlayerPrefs.SetString("userToken", token);
        PlayerPrefs.Save();
    }

    public string LoadToken()
    {
        return PlayerPrefs.GetString("userToken", "");
    }   
            
}
