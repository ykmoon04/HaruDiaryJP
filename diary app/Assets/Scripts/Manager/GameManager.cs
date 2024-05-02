using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager i;
    User user;

    TreeList treeList;
    int escCnt = 0;

    private void Awake() {
        if(i==null) i=this;
        escCnt = 0;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escCnt++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 0.5f);
       
        }
        else if (escCnt == 2)
        {
            CancelInvoke("DoubleClick");
            Application.Quit();
        }
    }


    void DoubleClick()
    {
        escCnt = 0;
    }

    public TreeList getTreeList()=>treeList;

    public User GetUser() => user;
    public void SetUser(User user){
        this.user = user;
    }

    public void UpdateUser(DiaryAnalysis analysis) {
        if (user != null) {
            user.UpdatePoints(analysis);
            Backend.i.UpdateUser((newUser)=>{
                this.user = newUser;
            });
        }
    }

    public void SetTreeList(TreeList treeList){
        this.treeList = treeList;
    }

    public void LogOut(){
        DataManager.i.DeleteGameData();
        LoadSceneManager.i.ToLogin();
    }


    private void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus){
            // DataManager.i.SaveGameData<TreeList>(treeList,"/trees.json");
        }
    }

    private void OnApplicationQuit() {
        // DataManager.i.SaveGameData<TreeList>(treeList,"/trees.json");
    }
}
