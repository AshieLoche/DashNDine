using System;
using System.Collections.Generic;
using System.IO;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;
using UnityEditor;
using UnityEngine;

namespace DashNDine.EditorSystem
{
    public enum CSVType
    {
        Regions,
        NPCs,
    }

    public class CSVToSOConverter : EditorWindow
    {
        Action<CSVType> _onConfirm;
        CSVType _csvType = CSVType.NPCs;

        static void ShowWindow(Action<CSVType> onConfirm)
        {
            CSVToSOConverter window = GetWindow<CSVToSOConverter>("CSV to SO Converter");
            window.minSize = new(300, 50);
            window._onConfirm = onConfirm;
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("CSV", EditorStyles.boldLabel);
            _csvType = (CSVType)EditorGUILayout.EnumPopup("CSV Type", _csvType);

            if (GUILayout.Button("Confirm")) _onConfirm?.Invoke(_csvType);
            else if (GUILayout.Button("Cancel")) Close();
        }

        [MenuItem("Tools/Dash N Dine/CSV to SO Converter")]
        public static void ShowRebuildWindow()
        {
            ShowWindow(onConfirm: ConvertCSVToSO);
        }

        #region Convert CSV to Scriptable Objects
        private static void ConvertCSVToSO(CSVType csvType)
        {
            string csvFilePath = Application.dataPath + $"/CSVs/{csvType}.csv";
            string scriptableObjectFolderPath = $"Assets/ScriptableObjects/";

            if (!File.Exists(csvFilePath))
            {
                Debug.LogError($"{csvType} doest not exist.");
                return;
            }

            using StreamReader reader = new(csvFilePath);

            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string[] values = reader.ReadLine().Split(',');

                CreateScriptableObject(values: values, csvType: csvType, scriptableObjectFolderPath: scriptableObjectFolderPath);
            }

            AssetDatabase.SaveAssets();
        }

        private static void CreateScriptableObject(string[] values, CSVType csvType, string scriptableObjectFolderPath)
        {
            switch (csvType)
            {
                case CSVType.Regions:
                    // Retrieve RegionSO
                    RegionSO regionSO = TryReplaceBaseSOMainInfo<RegionSO>(csvType, values, scriptableObjectFolderPath);

                    // Replace RegionSO Description
                    regionSO.Description = TryReplaceString(regionSO.Description, values[2]);

                    // Save RegionSO
                    SaveSO(regionSO);

                    // Save RegionListSO
                    TrySaveListSO<RegionSO, RegionListSO>(csvType, regionSO, scriptableObjectFolderPath);
                    break;
                case CSVType.NPCs:
                    // Retrieve NPCSO
                    NPCSO npcSO = TryReplaceBaseSOMainInfo<NPCSO>(csvType, values, scriptableObjectFolderPath);

                    // Replace NPCSO RegionSO
                    npcSO.RegionSO = TryReplaceSO<RegionSO, RegionListSO>(CSVType.Regions, values[2], scriptableObjectFolderPath);

                    // Replace NPCSO Key
                    npcSO.Key = TryReplaceString(npcSO.Key, values[3]);

                    // Replace NPCSO Sprite
                    npcSO.sprite = TryReplaceSprite(npcSO.sprite, csvType, npcSO.name);

                    // Replace NPCSO Prefab Transform;
                    npcSO.PrefabTransform = TryReplacePrefabTransform(npcSO.PrefabTransform, csvType, npcSO.name);

                    // Save NPCSO
                    SaveSO(npcSO);

                    // Save NPCListSo
                    TrySaveListSO<NPCSO, NPCListSO>(csvType, npcSO, scriptableObjectFolderPath);
                    break;
                default:
                    break;
            }

            AssetDatabase.SaveAssets();
        }

        private static TListSO TryGetListSO<TBaseSO, TListSO>(CSVType csvType, string scriptableObjectFilePath) where TBaseSO : BaseSO where TListSO : BaseListSO<TBaseSO>
        {
            string listSOFileName = csvType.ToString()[..^1] + "ListSO";
            return TryGetSO<TListSO>(csvType, listSOFileName, scriptableObjectFilePath);
        }

        private static TBaseSO TryGetBaseSO<TBaseSO>(CSVType csvType, string value, string scriptableObjectFilePath, out string newSOName) where TBaseSO : BaseSO
        {
            newSOName = Utils.RemoveWhiteSpaceAndDash(value) + "SO";
            return TryGetSO<TBaseSO>(csvType, newSOName, scriptableObjectFilePath);
        }

        private static TSO TryGetSO<TSO>(CSVType csvType, string soName, string scriptableObjectFilePath) where TSO : ScriptableObject
        {
            string soFilePath = scriptableObjectFilePath + $"/{csvType}/" + soName + ".asset";

            return LoadOrCreateSO<TSO>(soFilePath);
        }

        private static TBaseSO TryReplaceBaseSOMainInfo<TBaseSO>(CSVType csvType,string[] values, string scriptableObjectFilePath) where TBaseSO : BaseSO
        {
            // Retrieve SO
            TBaseSO so = TryGetBaseSO<TBaseSO>(csvType, values[1], scriptableObjectFilePath, out string newSOFileName);

            // Replace SO File Name
            so.name = TryReplaceString(so.name, newSOFileName);

            // Replace SO ID
            so.ID = TryReplaceInt(so.ID, values[0]);

            // Replace SO Name
            so.Name = TryReplaceString(so.Name, values[1]);

            return so;
        }

        private static string TryReplaceString(string oldString, string newString)
            => (oldString != newString) ? newString : oldString;

        private static int TryReplaceInt(int oldInt, string newString)
        {
            if (Utils.TryConvertStringToInt(newString, out int newValueInt))
                return (oldInt != newValueInt) ? newValueInt : oldInt;

            Debug.Log($"Cannot convert {newString} to int!");
            return 0;
        }

        private static float TryReplaceFloat(float oldFloat, string newString)
        {
            if (Utils.TryConvertStringToFloat(newString, out float newValueFloat))
                return (oldFloat != newValueFloat) ? newValueFloat : oldFloat;

            Debug.Log($"Cannot convert {newString} to float!");
            return 0f;
        }

        private static TSO TryReplaceSO<TSO, TListSO>(CSVType csvType, string newSOID, string scriptableObjectFilePath) where TSO : BaseSO where TListSO : BaseListSO<TSO>
        {
            TListSO listSO = TryGetListSO<TSO, TListSO>(csvType, scriptableObjectFilePath);

            if (Utils.TryConvertStringToInt(newSOID, out int soID))
            {
                TSO so = listSO.SOList.Find(soListElement => soListElement.ID == soID);

                if (so != null)
                    return so;
            }

            Debug.Log($"Cannot convert {newSOID} to SO!");
            return null;
        }

        private static Sprite TryReplaceSprite(Sprite oldSprite, CSVType csvType, string spriteName)
        {
            spriteName = spriteName.Replace("SO", "");
            string spriteFilePath = $"Assets/Gallery/Images/{csvType}/{spriteName}.png";

            if (AssetDatabase.AssetPathExists(spriteFilePath))
            {
                Sprite newSprite = AssetDatabase.LoadAssetAtPath<Sprite>(spriteFilePath);
                return (oldSprite != newSprite) ? newSprite : oldSprite;
            }

            Debug.LogWarning($"Cannot convert {spriteFilePath} to sprite");
            return oldSprite;
        }

        private static Transform TryReplacePrefabTransform(Transform oldPrefabTransform, CSVType csvType, string prefabTransformName)
        {
            prefabTransformName = prefabTransformName.Replace("SO", "");
            string prefabTransformFilePath = $"Assets/Prefabs/{csvType}/{prefabTransformName}.prefab";

            if (AssetDatabase.AssetPathExists(prefabTransformFilePath))
            {
                Transform newPrefabTransform = AssetDatabase.LoadAssetAtPath<Transform>(prefabTransformFilePath);
                return (oldPrefabTransform != newPrefabTransform) ? newPrefabTransform : oldPrefabTransform;
            }

            Debug.LogWarning($"Cannot convert {prefabTransformFilePath} to prefab");
            return oldPrefabTransform;
        }

        private static T LoadOrCreateSO<T>(string path) where T : ScriptableObject
        {
            if (AssetDatabase.AssetPathExists(path))
                return AssetDatabase.LoadAssetAtPath<T>(path);
            else
            {
                T asset = CreateInstance<T>();
                AssetDatabase.CreateAsset(asset, path);
                return asset;
            }
        }

        private static void SaveSO<T>(T so) where T : ScriptableObject
        {
            EditorUtility.SetDirty(so);
            AssetDatabase.SaveAssetIfDirty(so);
        }

        private static void TrySaveListSO<TBaseSO, TListSO>(CSVType csvType, TBaseSO so, string scriptableObjectFilePath) where TBaseSO : BaseSO where TListSO : BaseListSO<TBaseSO>
        {
            // Retrieve ListSO
            TListSO listSO = TryGetListSO<TBaseSO, TListSO>(csvType, scriptableObjectFilePath);

            if (listSO.SOList.Count == 0)
                listSO.SOList.Add(so);
            else
            {
                int soListIndex = listSO.SOList.FindIndex(listSOElement => listSOElement == so);

                if (soListIndex != -1)
                    listSO.SOList[soListIndex] = so;
            }

            SaveSO(listSO);
        }
        #endregion
    }
}