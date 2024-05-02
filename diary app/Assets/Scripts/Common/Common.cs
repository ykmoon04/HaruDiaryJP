using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : MonoBehaviour
{

}

public class MyColor{
    public static string joy = "#FFACB3";
    public static string disgust = "#4BCF9B";
    public static string sadness = "#7ECFE0";
    public static string surprise = "#FFD328";
    public static string angry = "#DE3745";
    public static string fear = "#CCA2DE";

    public static string getColor(Emotion e){
        switch(e){
            case Emotion.Joy:
                return joy;
            case Emotion.Sadness:
                return sadness;
            case Emotion.Angry:
                return angry;
            case Emotion.Fear:
                return fear;
            case Emotion.Surprise:
                return surprise;
            case Emotion.Disgust:
                return disgust;
        }

        return null;
    }
}

public enum Emotion{
    Joy = 0,
    Sadness = 1,
    Angry = 2,
    Fear = 3,
    Surprise = 4,
    Disgust = 5
}