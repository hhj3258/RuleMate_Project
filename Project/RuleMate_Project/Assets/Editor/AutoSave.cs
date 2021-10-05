using UnityEngine;

using UnityEditor;

using System;

using System.IO;

#if UNITY_5_3_OR_NEWER

using UnityEditor.SceneManagement;

using UnityEngine.SceneManagement;

#endif



#region "자동저장 옵션"

[Serializable]

struct AutoSaveOption

{

    public bool StartAutoSave;

    public bool PlayModeSceneSave;

    public bool CompileToSave;

    public bool TimedAutoSave;

    public int SaveTimeTickMinute;

}

#endregion



public class AutoSave : EditorWindow

{

    [SerializeField] static AutoSaveOption Option;

    static private DateTime lastSaveTime = DateTime.Now;

    static private Vector2 ScrollPosition = Vector2.zero;



    #region "에디터 윈도우"

    [MenuItem("Tools/AutoSave")]

    static void AutoSaveWindow()

    {

#if UNITY_5_3_OR_NEWER

   // Get existing open window or if none, make a new one:

   AutoSave window = (AutoSave)GetWindow(typeof(AutoSave));



   window.minSize = new Vector2(500f, 400f);

   window.maxSize = new Vector2(500f, 400f);



   window.autoRepaintOnSceneChange = true;

   window.Show();

#else

        Debug.LogError("Unity 5.3버전 미만은 사용할수 없습니다");

#endif

    }

    #endregion



    #region "에디터 이벤트 함수"

    [InitializeOnLoadMethod]

    static private void OnInitalrize()

    {

        EditorApplication.update += update;

    }



    private void OnDestroy()

    {

        SaveAutoSaveOption();

        EditorSceneManager.sceneSaving -= (scene, path) => Debug.Log(DateTime.Now + " " + path + " 저장완료");

    }



    private void OnEnable()

    {

        EditorSceneManager.sceneSaving += (scene, path) => Debug.Log(DateTime.Now + " " + path + " 저장완료");

        try

        {

            LoadAutoSaveOption();

        }

        catch

        {

            Option = new AutoSaveOption();

            SaveAutoSaveOption();

        }

    }

    #endregion



    #region "옵션 저장 불러오기"

    void SaveAutoSaveOption()

    {

        StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/AutoSaveOption.json");

        writer.Write(JsonUtility.ToJson(Option));

        writer.Close();

    }



    void LoadAutoSaveOption()

    {

        StreamReader reader = new StreamReader(Application.persistentDataPath + "/AutoSaveOption.json");

        Option = JsonUtility.FromJson<AutoSaveOption>(reader.ReadToEnd());

        reader.Close();

    }

    #endregion



    #region "자동 저장"

    static private void update()

    {

        if (Option.StartAutoSave)

        {

            if (Option.TimedAutoSave && !EditorApplication.isPlaying)

            {

                TimeSpan TickTime = DateTime.Now - lastSaveTime;

                if (TickTime.TotalMinutes >= Option.SaveTimeTickMinute)

                {

                    SaveScene();

                    lastSaveTime = DateTime.Now;

                }

            }



            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)

            {

                if (Option.PlayModeSceneSave)

                {

                    SaveScene();

                    lastSaveTime = DateTime.Now;

                }

            }

            if (EditorApplication.isCompiling)

            {

                if (Option.CompileToSave)

                {

                    SaveScene();

                    lastSaveTime = DateTime.Now;

                }

            }

        }

    }



    static void SaveScene()

    {

        SceneSetup[] Setup = EditorSceneManager.GetSceneManagerSetup();

        for (int i = 0; i < Setup.Length; i++)

        {

            if (Setup[i].isLoaded)

            {

                Scene scene = EditorSceneManager.GetSceneByPath(Setup[i].path);

                EditorSceneManager.SaveScene(scene);

            }

        }

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        Debug.Log("자동 저장 완료!");

    }

    #endregion



    #region "에디터 윈도우를 그립니다"

    void OnGUI()

    {

        if (!EditorApplication.isPlaying)

        {

            GUILayout.Label("환경설정", EditorStyles.boldLabel);

            Option.PlayModeSceneSave = GUILayout.Toggle(Option.PlayModeSceneSave, "플레이모드시 저장");

            Option.CompileToSave = GUILayout.Toggle(Option.CompileToSave, "스크립트 컴파일시 저장");

            Option.TimedAutoSave = EditorGUILayout.BeginToggleGroup("시간 간격에 따라 저장", Option.TimedAutoSave);

            Option.SaveTimeTickMinute = EditorGUILayout.IntSlider("간격(분)", Option.SaveTimeTickMinute, 1, 60);

            EditorGUILayout.EndToggleGroup();



            EditorGUILayout.HelpBox("옵션정보는 " + Application.persistentDataPath + "/AutoSaveOption.json에 저장 됩니다", MessageType.Info, true);



            GUILayout.Label("현재 열린씬 " + EditorSceneManager.loadedSceneCount, EditorStyles.boldLabel);

            EditorGUILayout.BeginVertical();

            ScrollPosition = EditorGUILayout.BeginScrollView(ScrollPosition);

            SceneSetup[] Setup = EditorSceneManager.GetSceneManagerSetup();

            if (Setup != null)

            {

                for (int i = 0; i < Setup.Length; i++)

                {

                    if (Setup[i].isLoaded)

                    {

                        EditorGUILayout.LabelField(Setup[i].path, EditorStyles.textField);

                    }

                }

            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();



            if (Option.StartAutoSave)

            {

                EditorGUILayout.HelpBox("자동저장 실행중 입니다", MessageType.Info, true);

            }

            else

            {

                EditorGUILayout.HelpBox("자동저장 중단중 입니다", MessageType.Warning, true);

            }



            if (GUILayout.Button("즉시저장"))

            {

                SaveScene();

            }



            if (GUILayout.Button("자동저장 시작"))

            {

                Option.StartAutoSave = true;

                DateTime lastSaveTime = DateTime.Now;

            }



            if (GUILayout.Button("자동저장 중지"))

            {

                Option.StartAutoSave = false; ;

            }

        }

    }

    #endregion

}