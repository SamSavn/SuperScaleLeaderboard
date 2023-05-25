using UnityEngine;
using UnityEditor;
using System.IO;
using SuperScale.Services;

namespace SuperScale.Editor
{
    public class ViewCreator : EditorWindow
    {
        private const string ViewSuffix = "View";
        private const string ModelSuffix = "ViewModel";
        private const string PresenterSuffix = "ViewPresenter";
        private const string StyleSuffix = "Style";
        private const string ScriptExtention = ".cs";
        private const string UxmlExtention = ".uxml";
        private const string UssExtention = ".uss";
        private const string ScriptsBasePath = "Assets/00SuperScale/Scripts";
        private const string VisualBasePath = "Assets/00SuperScale/UI/Elements/Views";
        private const string AddressablesGroup = "Views";

        private string _nameInput;
        private string _modelDataType;
        private bool _createModel = true;
        private bool _setDataType = false;

        private string _folderPath;
        private string _visualFolderPath;
        private string _viewPath;
        private string _viewInterfacePath;
        private string _modelPath;
        private string _modelInterfacePath;
        private string _presenterPath;

        private string _uxmlPath;
        private string _ussPath;

        private string _viewName;
        private string _viewInterfaceName;
        private string _modelName;
        private string _modelInterfaceName;
        private string _presenterName;

        private string VisualFolderPath => Path.Combine(VisualBasePath, GetViewName());


        [MenuItem("Assets/Create/SuperScale/UI/View", priority = 0)]
        public static void OpenWindow()
        {
            ViewCreator window = (ViewCreator)GetWindow(typeof(ViewCreator), true, "View Creator", true);
            window.Show();
        }

        private string GetScriptExtention(bool includeExtention = false) => $"{(includeExtention ? ScriptExtention : string.Empty)}";
        private string GetViewName(bool includeExtention = false) => $"{_nameInput}{ViewSuffix}{GetScriptExtention(includeExtention)}";
        private string GetViewInterfaceName(bool includeExtention = false) => $"I{GetViewName(includeExtention)}";
        private string GetModelName(bool includeExtention = false) => $"{_nameInput}{ModelSuffix}{GetScriptExtention(includeExtention)}";
        private string GetModelInterfaceName(bool includeExtention = false) => $"I{GetModelName(includeExtention)}";
        private string GetPresenterName(bool includeExtention = false) => $"{_nameInput}{PresenterSuffix}{GetScriptExtention(includeExtention)}";

        private string GetUxmlName(bool includeExtention = false) => $"{GetViewName()}{(includeExtention ? UxmlExtention : string.Empty)}";
        private string GetUssName(bool includeExtention = false) => $"{GetViewName()}{StyleSuffix}{(includeExtention ? UssExtention : string.Empty)}";

        private string TextField(string name, ref string property) => EditorGUILayout.TextField(name, property, EditorStyles.textField);
        private bool ToggleField(string name, ref bool property) => EditorGUILayout.Toggle(name, property, EditorStyles.toggle);

        private void OnEnable()
        {
            if(string.IsNullOrEmpty(_folderPath) && Selection.activeObject != null)
            {
                _folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
                _visualFolderPath = Path.Combine(VisualBasePath, GetViewName()).Replace("\\", "/");
            }
        }

        private void OnInspectorUpdate()
        {
            _viewName = GetViewName(true);
            _viewInterfaceName = GetViewInterfaceName(true);
            _visualFolderPath = Path.Combine(VisualBasePath, GetViewName()).Replace("\\", "/");

            if (_createModel)
            {
                _modelName = GetModelName(true);
                _modelInterfaceName = GetModelInterfaceName();
            }

            _presenterName = GetPresenterName(true);

            Repaint();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(10f);

            if (GUILayout.Button("Set Scripts Path"))
            {
                _folderPath = EditorUtility.OpenFolderPanel("Select Path", ScriptsBasePath, "");
            }
            GUILayout.Label($"Sripts Path: {_folderPath?.Replace(Application.dataPath, "Assets")}");

            EditorGUILayout.Space(5f);
            if (GUILayout.Button("Set Visual Path"))
            {
                _visualFolderPath = EditorUtility.OpenFolderPanel("Select Path", VisualBasePath, "");
            }
            GUILayout.Label($"Visual Path: {_visualFolderPath.Replace("\\", "/")}");

            if (string.IsNullOrEmpty(_folderPath) || string.IsNullOrEmpty(_visualFolderPath))
            {
                return;
            }

            EditorGUILayout.Space(10f);

            _nameInput = TextField("Name", ref _nameInput);
            _createModel = ToggleField("Create Model", ref _createModel);

            if (_createModel)
            {
                _setDataType = ToggleField("Set Model Data Type", ref _setDataType);

                if (_setDataType)
                {
                    _modelDataType = TextField("Model Data Type", ref _modelDataType);
                }
            }

            EditorGUILayout.Space(10f);
            EditorGUI.BeginDisabledGroup(true);

            _viewName = TextField("View", ref _viewName);
            _viewInterfaceName = TextField("View Interface", ref _viewInterfaceName);

            if (_createModel)
            {
                _modelName = TextField("Model", ref _modelName);
                _modelInterfaceName = TextField("Model Interface", ref _modelInterfaceName);
            }

            _presenterName = TextField("Presenter", ref _presenterName);

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
            Debug.Log(_uxmlPath);

            assetsService.CreateFile(_uxmlPath, GetViewUXMLTemplate());
            assetsService.CreateFile(_ussPath, string.Empty);

            assetsService.AddAssetToGroup(_uxmlPath, _nameInput.ToLower(), AddressablesGroup);

            _viewPath = Path.Combine(_folderPath, $"{GetViewName(true)}");
            _viewInterfacePath = Path.Combine(_folderPath, $"{GetViewInterfaceName(true)}");
            _presenterPath = Path.Combine(_folderPath, $"{GetPresenterName(true)}");

            if (_createModel)
            {
                _modelPath = Path.Combine(_folderPath, $"{GetModelName(true)}");
                _modelInterfacePath = Path.Combine(_folderPath, $"{GetModelInterfaceName(true)}");

                assetsService.CreateFile(_viewPath, GetViewTemplate(_createModel));
                assetsService.CreateFile(_presenterPath, GetPresenterWithModelTemplate());
                assetsService.CreateFile(_modelInterfacePath, GetModelInterfaceTemplate());

                if (_setDataType)
                {
                    assetsService.CreateFile(_modelPath, GetModelWithDataTypeTemplate());
                }
                else
                {
                    assetsService.CreateFile(_modelPath, GetModelTemplate());
                }
            }
            else
            {
                assetsService.CreateFile(_viewPath, GetViewTemplate(_createModel));
                assetsService.CreateFile(_presenterPath, GetPresenterTemplate());
            }

            assetsService.CreateFile(_viewInterfacePath, GetViewInterfaceTemplate());

            AssetDatabase.Refresh();
        }

        #region Templates

        private string GetViewUXMLTemplate()
        {
            return
                $@"<ui:UXML xmlns:ui=""UnityEngine.UIElements"" xmlns:uie=""UnityEditor.UIElements"" xsi=""http://www.w3.org/2001/XMLSchema-instance"" engine=""UnityEngine.UIElements"" editor=""UnityEditor.UIElements"" noNamespaceSchemaLocation=""../../UIElementsSchema/UIElements.xsd"" editor-extension-mode=""False"">
    <Style src=""{GetUssName(true)}""/>
    <ui:VisualElement name=""container"" style=""flex-grow: 1;"">
    </ui:VisualElement>
</ui:UXML>";
        }

        private string GetViewModelParameter(bool hasModel)
        {
            return hasModel ? $@"{GetModelName()} model" : string.Empty;
        }

        private string GetViewTemplate(bool hasModel)
        {
            return $@"using SuperScale.UI.Transitions;

namespace SuperScale.UI.Views
{{
    public class {GetViewName()} : View<{GetPresenterName()}, {GetViewInterfaceName()}>, {GetViewInterfaceName()}
    {{
        public override string ID => ""{_nameInput.ToLower()}"";
        public override ITransition GetEnterTransition() => null;
        public override ITransition GetExitTransition() => null;

        public {GetViewName()}({GetViewModelParameter(hasModel)})
        {{
            Presenter = new {GetPresenterName()}(this);
            RegisterAssetLoadedListener();
        }}

        public override void GetElements()
        {{
            TriggerReadyState();
        }}
    }}
}}";
        }

        private string GetViewInterfaceTemplate()
        {
            return $@"namespace SuperScale.UI.Views
{{
    public interface {GetViewInterfaceName()} : IView
    {{
        
    }}
}}";
        }

        private string GetModelInterfaceTemplate()
        {
            return $@"namespace SuperScale.UI.Views
{{
    public interface {GetModelInterfaceName()} : IModel
    {{
        
    }}
}}";
        }

        private string GetModelTemplate()
        {
            return $@"namespace SuperScale.UI.Views
{{
    public class {GetModelName()} : {GetModelInterfaceName()}
    {{
        
    }}
}}";
        }

        private string GetModelWithDataTypeTemplate()
        {
            return $@"namespace SuperScale.UI.Views
{{
    public class {GetModelName()} : Model<{_modelDataType}>, {GetModelInterfaceName()}
    {{
        public {GetModelName()}({_modelDataType} data) : base(data)
        {{

        }}
    }}
}}";
        }

        private string GetPresenterTemplate()
        {
            return $@"namespace SuperScale.UI.Views
{{
    public class {GetPresenterName()} : Presenter<{GetViewInterfaceName()}>
    {{
        public {GetPresenterName()}({GetViewInterfaceName()} view) : base(view)
        {{
        
        }}
    }}
}}";
        }

        private string GetPresenterWithModelTemplate()
        {
            return $@"namespace SuperScale.UI.Views
{{
    public class {GetPresenterName()} : Presenter<{GetViewInterfaceName()}, {GetModelInterfaceName()}>
    {{
        public {GetPresenterName()}({GetViewInterfaceName()} view, {GetModelInterfaceName()} model) : base(view, model)
        {{
        
        }}
    }}
}}";
        }

        #endregion
    } 
}
