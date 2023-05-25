using UnityEngine;
using UnityEditor;
using System.IO;
using SuperScale.Services;

namespace SuperScale.Editor
{
    public class ComponentCreator : EditorWindow
    {
        private const string StyleSuffix = "Style";
        private const string ScriptExtention = ".cs";
        private const string UxmlExtention = ".uxml";
        private const string UssExtention = ".uss";
        private const string ScriptsBasePath = "Assets/Scripts";
        private const string VisualBasePath = "Assets/00SuperScale/UI/Elements/Components";
        private const string Namespace = "SuperScale.UI.Components";
        private const string AddressablesGroup = "Components";

        private string _nameInput;
        private string _namespaceInput;

        private string _scriptFolderPath;
        private string _visualFolderPath;
        private string _scriptPath;
        private string _uxmlPath;
        private string _ussPath;

        private string _componentName;
        private string _namespace;
        private string _uxmlName;
        private string _ussName;

        private string VisualFolderPath => Path.Combine(VisualBasePath, GetComponentName());


        [MenuItem("Assets/Create/SuperScale/UI/Component", priority = 1)]
        public static void OpenWindow()
        {
            ComponentCreator window = (ComponentCreator)GetWindow(typeof(ComponentCreator), true, "Component Creator", true);
            window.Show();
        }

        private string GetScriptExtention(bool includeExtention = false) => $"{(includeExtention ? ScriptExtention : string.Empty)}";
        private string GetComponentName(bool includeExtention = false) => $"{_nameInput}{GetScriptExtention(includeExtention)}";
        private string GetUxmlName(bool includeExtention = false) => $"{GetComponentName()}{(includeExtention ? UxmlExtention : string.Empty)}";
        private string GetUssName(bool includeExtention = false) => $"{GetComponentName()}{StyleSuffix}{(includeExtention ? UssExtention : string.Empty)}";
        private string GetNamespace() => $"{Namespace}{(!string.IsNullOrEmpty(_namespaceInput) ? $".{_namespaceInput}" : string.Empty)}";

        private string TextField(string name, ref string property) => EditorGUILayout.TextField(name, property, EditorStyles.textField);

        private void OnEnable()
        {
            if(string.IsNullOrEmpty(_scriptFolderPath) && Selection.activeObject != null)
            {
                _scriptFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
                _visualFolderPath = Path.Combine(VisualBasePath, GetComponentName()).Replace("\\", "/");
            }
        }

        private void OnInspectorUpdate()
        {
            _uxmlName = GetUxmlName(true);
            _ussName = GetUssName(true);
            _componentName = GetComponentName(true);
            _namespace = GetNamespace();

            _visualFolderPath = Path.Combine(VisualBasePath, GetComponentName()).Replace("\\", "/");

            Repaint();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(10f);

            if (GUILayout.Button("Set Scripts Path"))
            {
                _scriptFolderPath = EditorUtility.OpenFolderPanel("Select Path", ScriptsBasePath, "");
            }
            GUILayout.Label($"Sripts Path: {_scriptFolderPath?.Replace(Application.dataPath, "Assets")}");

            EditorGUILayout.Space(5f);
            if (GUILayout.Button("Set Visual Path"))
            {
                _visualFolderPath = EditorUtility.OpenFolderPanel("Select Path", VisualBasePath, "");
            }
            GUILayout.Label($"Visual Path: {_visualFolderPath?.Replace("\\", "/")}");

            if (string.IsNullOrEmpty(_scriptFolderPath) || string.IsNullOrEmpty(_visualFolderPath))
            {
                return;
            }

            EditorGUILayout.Space(10f);

            _nameInput = TextField("Name", ref _nameInput);
            _namespaceInput = TextField("Namespace", ref _namespaceInput);

            EditorGUILayout.Space(10f);
            EditorGUI.BeginDisabledGroup(true);

            _componentName = TextField("Component", ref _componentName);
            _namespace = TextField("Namespace", ref _namespace);
            _uxmlName = TextField("Uxml", ref _uxmlName);
            _ussName = TextField("Uss", ref _ussName);

            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space(10f);

            if (!string.IsNullOrEmpty(_nameInput))
            {
                if (GUILayout.Button("Create"))
                {
                    CreateFiles();
                }
            }
        }

        private void CreateFiles()
        {
            AssetsService assetsService = new AssetsService();

            if (!Directory.Exists(VisualFolderPath))
            {
                Directory.CreateDirectory(VisualFolderPath);
            }

            _uxmlPath = Path.Combine(VisualFolderPath, GetUxmlName(true));
            _ussPath = Path.Combine(VisualFolderPath, GetUssName(true));
            _scriptPath = Path.Combine(_scriptFolderPath, $"{GetComponentName(true)}");

            assetsService.CreateFile(_scriptPath, GetScriptTemplate());
            assetsService.CreateFile(_uxmlPath, GetViewUXMLTemplate());
            assetsService.CreateFile(_ussPath, string.Empty);

            Debug.Log(_uxmlPath);
            assetsService.AddAssetToGroup(_uxmlPath, _nameInput.ToLower(), AddressablesGroup);

            AssetDatabase.Refresh();
        }

        #region Templates

        private string GetViewUXMLTemplate()
        {
            return $@"<ui:UXML xmlns:ui=""UnityEngine.UIElements"" xmlns:uie=""UnityEditor.UIElements"" xsi=""http://www.w3.org/2001/XMLSchema-instance"" engine=""UnityEngine.UIElements"" editor=""UnityEditor.UIElements"" noNamespaceSchemaLocation=""../../../../../UIElementsSchema/UIElements.xsd"" editor-extension-mode=""False"">
    <Style src=""{GetUssName(true)}""/>
 </ui:UXML>";
        }

        private string GetScriptTemplate()
        {
            return $@"using UnityEngine.UIElements;

namespace {GetNamespace()}
{{
    public class {GetComponentName()} : AbstractComponent
    {{
        public new class UxmlFactory : UxmlFactory<{GetComponentName()}, UxmlTraits> {{ }}
        public override string ID => ""{_nameInput.ToLower()}"";

        public {GetComponentName()}()
        {{
            AddToClassList(ID);
            RegisterAssetsLoaderListener();
        }}

        protected override void GetElements()
        {{

        }}
    }}
}}";
        }

        #endregion
    } 
}
