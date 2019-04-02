using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public class AchievementManager : MonoBehaviour {

    public GameObject achievmentPrefab;

    public Sprite[] sprites;

    private AchievementButton activeButton;

    public ScrollRect scrollRect;

    public GameObject achievmentMenu;

    public GameObject visualAchievment;

    public Dictionary<string, Achievment> achievments = new Dictionary<string, Achievment>();

    public Sprite unlockedSprite;

    public Text textPoints;

    private int fadeTime = 2;

    private static AchievementManager instance; //singleton pattern 



    public static AchievementManager Instance
    {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<AchievementManager>();
            }
            return AchievementManager.instance;
        }
       
    }



    // Use this for initialization
    void Start () {

        //Comment out when in need of saving data
        PlayerPrefs.DeleteAll(); //delete all the saved data 

        activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievementButton>();

        createAchievment("General", "Keep the doctor away","Collect 25 Apples",10,1,0); //Call this function to generate an achievment
        createAchievment("General", "Press S", "Press S to Unlock", 5, 2,0);
        createAchievment("General", "Press W", "Press W to Unlock", 5, 3,0);
        createAchievment("General", "Key Master", "Press all keys to Unlock", 20, 0,0, new string[] { "Press S", "Press W" });
        createAchievment("General", "Press X", "Press X 3 times to Unlock", 5, 7, 3);
        


        foreach (GameObject achievmentList in GameObject.FindGameObjectsWithTag("AchievmentList")) {
            achievmentList.SetActive(false);
            achievmentMenu.SetActive(false);

        }
        activeButton.Click();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I)) {
            achievmentMenu.SetActive(! achievmentMenu.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            
            EarnAchievment("Press W");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            EarnAchievment("Keep the doctor away");
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            EarnAchievment("Press S");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            EarnAchievment("Press X");
        }


    }

    public void EarnAchievment(string title) { //This function only allowed to earn achievments which is not earned previously

        if (achievments[title].EarnAchievment() ) {

            GameObject achievment = (GameObject)Instantiate(visualAchievment); //Display POP Up message upon earning an achievment
            setAchievmentInfor("EarnCanvas", achievment, title);   //Earn can can be set upper center or lower center
            textPoints.text = "Points: " + PlayerPrefs.GetInt("Points")/2;
            StartCoroutine(FadeAchievment(achievment)); //This routine hide the archievement pop up after several seconds
        }

    }

    public IEnumerator HideAchievment(GameObject achievment) {

        yield return new WaitForSeconds(3);
        Destroy(achievment);
    }

    public void createAchievment(string parent, string title, string description, int points, int spriteIndex, int progress, string[] dependencies = null) {

        GameObject achievment = (GameObject)Instantiate(achievmentPrefab); //instantiate achievment prefab

        Achievment newAchivment = new Achievment(title, description, points, spriteIndex, achievment, progress);
        achievments.Add(title, newAchivment); //add the achievment to dictionary

        setAchievmentInfor(parent, achievment, title, progress);

        if (dependencies != null) {

            foreach (string achievmentTitle in dependencies) {

                Achievment dependency = achievments[achievmentTitle];
                dependency.Child = title;
                newAchivment.AddDependency(dependency);

                //Dependency = Press Space <-- Press W 
                //New Achievment = Press W --> Press Space 
            }

        }

    }

    public void setAchievmentInfor(string parent, GameObject achievment, string title, int progression = 0) {

        //Set the Parent of the achievment
        achievment.transform.SetParent(GameObject.Find(parent).transform);
        achievment.transform.localScale = new Vector3(1,1,1);

        //if progression is larger than 0 get the data 
        string progress = progression > 0 ? " " + PlayerPrefs.GetInt("Progression" + title) + "/" + progression.ToString() : string.Empty;

        //Set the information of the achievment
        achievment.transform.GetChild(0).GetComponent<Text>().text = title + progress;
        achievment.transform.GetChild(1).GetComponent<Text>().text = achievments[title].Description;
        achievment.transform.GetChild(2).GetComponent<Text>().text = achievments[title].Points.ToString();

        achievment.transform.GetChild(3).GetComponent<Image>().sprite = sprites[achievments[title].SpriteIndex];
    }

    public void changeCategory(GameObject button) {

        AchievementButton achievmentbtn = button.GetComponent<AchievementButton>();

        scrollRect.content = achievmentbtn.achivementList.GetComponent<RectTransform>();
        achievmentbtn.Click();
        activeButton.Click();
        activeButton = achievmentbtn;


    }

    private IEnumerator FadeAchievment(GameObject achievment) {

        CanvasGroup canvasGroup = achievment.GetComponent<CanvasGroup>();

        float rate = 1.0f / fadeTime;
        int startAlpha = 0;
        int endAlpha = 1;

        float progress = 0.0f;

        for (int i=0; i<2; i++) {

            progress = 0.0f;

            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }//end of thw while


            yield return new WaitForSeconds(2 );
            startAlpha = 1;
            endAlpha = 0;


        } //endAlpha of the for loop


        Destroy(achievment);

    }

}
