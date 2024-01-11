namespace Meangpu
{
    using SimpleJSON;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Unity.EditorCoroutines.Editor;
    using UnityEditor;
    using UnityEditor.PackageManager;
    using UnityEditor.PackageManager.Requests;
    using UnityEngine;
    using UnityEngine.Networking;

    [InitializeOnLoad]
    public class PackageImporter : EditorWindow
    {
        private static EditorWindow promptWindow;
        private const string WindowPath = "MeangpuTools/EditorUtil/";
        private const string WindowName = "Package Importer";
        private const string DataUri = "https://raw.githubusercontent.com/meangpu/U.MeangpuTools/main/Editor/PackageImport/mePackage.json";
        private const string titleLabel = "MEANGPU Packages To Import";
        private const string addingPackagesMessage = "Adding packages, please wait...";
        private const string loadingText = "Loading";
        private const string filterLabel = "Filter";
        private const int filterLabelWidth = 40;
        private const string addButtonText = "Add";
        private const string viewButtonText = "View";
        private const string viewButtonTooltip = "View on GitHub";
        private const string refreshPackageListButton = "Refresh Package List";
        private static bool windowAlreadyOpen;
        private static List<string> installedPackages = new();
        private static AddRequest addRequest;
        private static ListRequest installedPackagesRequest;

        private string manifestFile;
        private JSONNode rootManifest;
        private EditorCoroutine getWebDataRoutine;
        private Dictionary<string, string> availablePackages = new();
        private Dictionary<string, string> packageDescriptions = new();
        private Dictionary<string, string> packageUrls = new();
        private Vector2 scrollPosition;
        private string searchString = "";

        private const string addSelectedPackagesButton = "Add Selected Packages";
        private static AddAndRemoveRequest addAndRemoveRequest;
        private Dictionary<string, bool> checkboxes = new();
        private List<string> packagesToAdd = new();

        Dictionary<string, string> _dictCanInstall = new();

        public void OnGUI()
        {
            var TextStyle = new GUIStyle();
            TextStyle.normal.textColor = Color.white;
            TextStyle.fontSize = 18;
            TextStyle.fontStyle = FontStyle.Bold;

            if (!windowAlreadyOpen)
            {
                DownloadPackageList();
                windowAlreadyOpen = true;
            }

            DrawHorizontalLine(Color.black);
            GUILayout.Space(1);
            GUILayout.Label(titleLabel, TextStyle);
            DrawHorizontalLine(Color.black);

            GUILayout.Label(loadingText);

            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(filterLabel, GUILayout.Width(filterLabelWidth));
                searchString = EditorGUILayout.TextField(searchString);
            }

            DrawHorizontalLine();

            using (GUILayout.ScrollViewScope scrollViewScope = new(scrollPosition))
            {
                scrollPosition = scrollViewScope.scrollPosition;

                if (addRequest != null) GUILayout.Label(addingPackagesMessage);
                else if (addAndRemoveRequest != null) GUILayout.Label(addingPackagesMessage);
                else SetupAllButton();
            }

            if (HasSelectedPackages() && addRequest == null && addAndRemoveRequest == null)
            {
                DrawHorizontalLine(Color.black);

                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(addSelectedPackagesButton)) AddSelectedPackages();
                }
            }

            DrawHorizontalLine(Color.black);

            using (new EditorGUILayout.HorizontalScope())
            {
                if (addRequest == null && addAndRemoveRequest == null)
                {
                    if (GUILayout.Button(refreshPackageListButton)) DownloadPackageList();
                }
            }

            GUILayout.Space(8);
        }

        private void SetupAllButton()
        {
            SetDictForPackageThatNotInstallYet();

            foreach (var availablePackage in _dictCanInstall)
            {
                if (!string.IsNullOrEmpty(searchString.Trim()) && !availablePackage.Key.Contains(searchString)) continue;

                using (new EditorGUILayout.HorizontalScope())
                {
                    packageDescriptions.TryGetValue(availablePackage.Key, out string packageDescription);
                    packageUrls.TryGetValue(availablePackage.Key, out string packageUrl);

                    if (checkboxes.ContainsKey(availablePackage.Key))
                    {
                        checkboxes[availablePackage.Key] = GUILayout.Toggle(checkboxes[availablePackage.Key], "");
                    }

                    GUILayout.Label(new GUIContent(availablePackage.Key, packageDescription));
                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button(addButtonText))
                    {
                        AddPackage(availablePackage.Value);
                    }

                    if (GUILayout.Button(new GUIContent(viewButtonText, viewButtonTooltip)))
                    {
                        Application.OpenURL(packageUrl);
                    }
                    GUILayout.Label(" ", new GUIStyle { fontSize = 10 });
                }
                DrawHorizontalLine();
            }
        }

        private void SetDictForPackageThatNotInstallYet()
        {
            List<string> notInstallYet = availablePackages.Keys.Except(installedPackages).ToList();
            _dictCanInstall.Clear();

            foreach (string packageName in notInstallYet)
            {
                if (availablePackages.ContainsKey(packageName))
                {
                    _dictCanInstall.Add(packageName, availablePackages[packageName]);
                }
            }
        }

        private void OnDestroy()
        {
            if (getWebDataRoutine != null)
            {
                EditorCoroutineUtility.StopCoroutine(getWebDataRoutine);
            }

            if (addRequest != null)
            {
                EditorApplication.update -= HandlePackageAddRequest;
                addRequest = null;
            }

            if (addAndRemoveRequest != null)
            {
                EditorApplication.update -= HandlePackageAddAndRemoveRequest;
                addAndRemoveRequest = null;
            }
        }

        private void AddPackage(string packageName)
        {
            addRequest = Client.Add(packageName);
            EditorApplication.update += HandlePackageAddRequest;
        }

        private bool HasSelectedPackages()
        {
            const bool result = false;
            foreach (KeyValuePair<string, bool> entry in checkboxes)
            {
                if (entry.Value) return true;
            }

            return result;
        }

        private void AddSelectedPackages()
        {
            packagesToAdd.Clear();

            foreach (KeyValuePair<string, bool> entry in checkboxes)
            {
                if (entry.Value && _dictCanInstall.ContainsKey(entry.Key))
                {
                    packagesToAdd.Add(_dictCanInstall[entry.Key]);
                }
            }

            if (packagesToAdd.Count > 0)
            {
                addAndRemoveRequest = Client.AddAndRemove(packagesToAdd.ToArray());
                EditorApplication.update += HandlePackageAddAndRemoveRequest;
            }
        }

        private static void HandlePackageAddAndRemoveRequest()
        {
            if (addAndRemoveRequest?.IsCompleted == true)
            {
                if (addAndRemoveRequest.Status == StatusCode.Success) GetInstalledPackages();
                else Debug.LogError("Failure to add package: " + addAndRemoveRequest.Error.message);

                EditorApplication.update -= HandlePackageAddAndRemoveRequest;
                addAndRemoveRequest = null;
            }
        }

        private void DownloadPackageList()
        {
            GetInstalledPackages();
            GetRawData();
        }

        private void GetRawData()
        {
            if (getWebDataRoutine != null)
            {
                return;
            }

            getWebDataRoutine = EditorCoroutineUtility.StartCoroutine(GetWebRequest(DataUri), this);
        }

        private IEnumerator GetWebRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                yield return webRequest.SendWebRequest();

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("HTTP Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        ParseRawData(webRequest.downloadHandler.text);
                        break;
                }
            }

            getWebDataRoutine = null;
        }

        private void ParseRawData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }

            JSONNode jsonData = JSONNode.Parse(data);

            availablePackages.Clear();
            packageDescriptions.Clear();
            packageUrls.Clear();
            checkboxes.Clear();

            foreach (JSONNode package in jsonData["packages"])
            {
                availablePackages.Add(package["name"], package["url"]);
                packageDescriptions.Add(package["name"], package["description"] + ".\n\nLatest version: " + package["version"]);
                packageUrls.Add(package["name"], package["url"]);
                checkboxes.Add(package["name"], package["isSelectAtStart"]);
            }
        }

        private static void GetInstalledPackages()
        {
            installedPackagesRequest = Client.List(false, true);
            EditorApplication.update += HandleInstalledPackagesRequest;
        }

        private static void HandleInstalledPackagesRequest()
        {
            if (installedPackagesRequest.IsCompleted)
            {
                if (installedPackagesRequest.Status == StatusCode.Success)
                {
                    installedPackages.Clear();
                    foreach (var packageInfo in installedPackagesRequest.Result)
                    {
                        installedPackages.Add(packageInfo.name);
                    }
                }
                else
                {
                    Debug.LogError("Failure to receive installed packages: " + installedPackagesRequest.Error.message);
                }

                EditorApplication.update -= HandleInstalledPackagesRequest;
            }
        }

        private static void HandlePackageAddRequest()
        {
            if (addRequest?.IsCompleted == true)
            {
                if (addRequest.Status == StatusCode.Success)
                {
                    GetInstalledPackages();
                }
                else
                {
                    Debug.LogError("Failure to add package: " + addRequest.Error.message);
                }

                EditorApplication.update -= HandleInstalledPackagesRequest;
                addRequest = null;
            }
        }

        private static void DrawHorizontalLine(Color color, float height, Vector2 margin)
        {
            GUILayout.Space(margin.x);
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, height), color);
            GUILayout.Space(margin.y);
        }

        private static void DrawHorizontalLine(Color color)
        {
            DrawHorizontalLine(color, 1f, Vector2.one * 5f);
        }

        private static void DrawHorizontalLine()
        {
            DrawHorizontalLine(new Color(0f, 0f, 0f, 0.3f));
        }

        [MenuItem(WindowPath + WindowName)]
        private static void ShowWindow()
        {
            windowAlreadyOpen = false;
            promptWindow = GetWindow(typeof(PackageImporter));
            promptWindow.titleContent = new GUIContent(WindowName);
        }
    }
}