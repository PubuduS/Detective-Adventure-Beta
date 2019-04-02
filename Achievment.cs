using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievment {

    private string name;
    private string description;
    private bool unlocked;

    private int points;
    private int spriteIndex;
    private GameObject achievmentRef;

    private List<Achievment> dependencies = new List<Achievment>();

    private string child;
    private int currentProgression;
    private int maxProgression;
    

    public Achievment(string name, string description, int points, int spriteIndex, GameObject achievmentRef, int maxProgression) {
        this.Name = name;
        this.Description = description;
        this.Unlocked = false;
        this.Points = points;
        this.SpriteIndex = spriteIndex;
        this.achievmentRef = achievmentRef;
        this.maxProgression = maxProgression;
        LoadAchievment();
    }

    public string Name
    {
        get { return name;}
        set { name = value;}
    }

    public string Description
    {
        get { return description; }
        set {description = value; }
    }

    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value;  }
    }

    public int Points
    {
        get{return points; }
        set{points = value;}
    }

    public int SpriteIndex
    {
        get{return spriteIndex;}
        set {spriteIndex = value;}
    }

    public string Child
    {
        get {return child;}
        set {child = value; }
    }

    public bool EarnAchievment() {
        bool flag = false;

        if (!Unlocked && !dependencies.Exists( x => x.unlocked == false) && CheckProgress())
        {
           
            achievmentRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;  //use the singleton
            SaveAchievment(true);

            if (child != null) {
                AchievementManager.Instance.EarnAchievment(child);

            }

            flag = true;
        }
        else {
            flag = false;
        }

        return flag;
    }

    public void SaveAchievment(bool value) {
      
            Unlocked = value;
       
            //get the amount of points
            PlayerPrefs.DeleteAll();
            int tmpPoints = PlayerPrefs.GetInt("Points"); //PlayerPrefs is used to save and retrive values in unity Engine (non volatile)

            PlayerPrefs.SetInt("Points", tmpPoints += Points); //store the points
            Debug.Log("Points " + Points);
            PlayerPrefs.SetInt("Progression" + Name, currentProgression);
            PlayerPrefs.SetInt(Name, value ? 1 : 0); //Saving Boolean
            PlayerPrefs.Save();
           
        
    }

    public void LoadAchievment() {

        Unlocked = PlayerPrefs.GetInt(Name) == 1 ? true : false;

        if (Unlocked) {

            AchievementManager.Instance.textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
            currentProgression = PlayerPrefs.GetInt("Progression" + Name);
            achievmentRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;  //use the singleton
        }
        
    }

    public void AddDependency(Achievment dependency) {
        dependencies.Add(dependency);
    }

    public bool CheckProgress() {

       
        currentProgression++;

        if (maxProgression != 0)
        {
            achievmentRef.transform.GetChild(0).GetComponent<Text>().text = Name + " " + currentProgression + "/" + maxProgression;
        }

        SaveAchievment(false);

        if (maxProgression == 0)
        {
            return true;
        }

        if(currentProgression >= maxProgression){
            return true;
        }


        return false;
    }
}
