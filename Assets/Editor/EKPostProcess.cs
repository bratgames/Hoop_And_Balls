#if UNITY_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public class EKPostProcess : MonoBehaviour
{
    [PostProcessBuild]
    public static void ChangePlist(BuildTarget buildTarget, string projectPath)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            string plistPath = projectPath + "/Info.plist";
            PlistDocument plist = new PlistDocument();

            plist.ReadFromFile(plistPath);
            plist.root["ITSAppUsesNonExemptEncryption"] = new PlistElementBoolean(false);
            // plist.root["GADApplicationIdentifier"] = new PlistElementString("ca-app-pub-3715200615165221~4677568226");
            // plist.root["NSUserTrackingUsageDescription"] = new PlistElementString("Your data will only be used to deliver personalized ads to you.");
            plist.WriteToFile(plistPath);
        }
    }
}
#endif