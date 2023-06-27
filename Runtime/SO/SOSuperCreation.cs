using UnityEngine;
using UnityEditor;
// using EasyButtons;

[CreateAssetMenu(fileName = "New SOSuperXrayCreator", menuName = "Meangpu/SOSuperXrayCreator")]
public class SoSuperCreation : ScriptableObject
{
    // is SO that loop create SO object it self
    public Sprite[] _xrayTutorial;
    public Sprite[] _xrayImg;
    public string[] _operationName;

    // public XrayLegLocation[] _legLocation;
    // public LegPose[] _legPose;

    // [Button]
    // void CreateSOobject()
    // {
    //     if (_xrayTutorial.Length != _xrayImg.Length ||
    //     _xrayTutorial.Length != _operationName.Length ||
    //     _xrayTutorial.Length != _legLocation.Length ||
    //     _xrayTutorial.Length != _legPose.Length)
    //     {
    //         Debug.Log($"<color=red>Image is not equal</color>");
    //         return;
    //     }

    //     for (var i = 0; i < _xrayTutorial.Length; i++)
    //     {
    //         SOXrayOperation nowXrayOperation = ScriptableObject.CreateInstance<SOXrayOperation>();
    //         string path = $"Assets/_Project/__Scripts/Xray/SO/OperationData/{i + 1}.asset";

    //         nowXrayOperation.TutorialImg = _xrayTutorial[i];
    //         nowXrayOperation.XrayOutImage = _xrayImg[i];
    //         nowXrayOperation.XrayName = _operationName[i];
    //         nowXrayOperation.LegLocation = _legLocation[i];
    //         nowXrayOperation.LegPose = _legPose[i];

    //         nowXrayOperation.name = (i + 1).ToString();

    //         AssetDatabase.CreateAsset(nowXrayOperation, path);
    //         AssetDatabase.SaveAssets();
    //         Debug.Log($"create {i + 1} obj at {path}!");
    //         AssetDatabase.Refresh();
    //         EditorUtility.FocusProjectWindow();
    //         Selection.activeObject = nowXrayOperation;
    //     }


    // }
}