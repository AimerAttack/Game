using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView _graphView;
    private string _fileName = "New Narrative";
    
    // [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }
    

    private void OnEnable()
    {
       ConstructGraphView();
       GenerateToolbar();
       GenerateMiniMap();
       GenerateBlackBoard();
    }

    private void GenerateBlackBoard()
    {
        var blackboard = new Blackboard(_graphView);
        blackboard.Add(new BlackboardSection()
        {
            title = "Exposed Properties"
        });
        blackboard.addItemRequested = _blackboard =>
        {
            _graphView.AddPropertyToBlackBoard(new ExposedProperty());
        };
        blackboard.editTextRequested = (blackboard1, element, newValue) =>
        {
            var oldPropertyName = ((BlackboardField) element).text;
            if (_graphView.ExposedProperties.Any(x => x.PropertyName == newValue))
            {
                EditorUtility.DisplayDialog("Error", "This property name already exists","OK");
                return;
            }

            var index = _graphView.ExposedProperties.FindIndex(x => x.PropertyName == oldPropertyName);
            _graphView.ExposedProperties[index].PropertyName = newValue;
            ((BlackboardField) element).text = newValue;
        };
        blackboard.SetPosition(new Rect(10,30,200,300));

        _graphView.Add(blackboard);
        _graphView.Blackboard = blackboard;
    }

    private void GenerateMiniMap()
    {
        var miniMap = new MiniMap{anchored = true};
        miniMap.SetPosition(new Rect(10,30,200,140));
        _graphView.Add(miniMap);
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);   
    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField("File Name:");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt =>
        {
            _fileName = evt.newValue;
        });
        toolbar.Add(fileNameTextField);
        toolbar.Add(new Button(()=>RequestDataOperation(true)){text = "Save Data"});
        toolbar.Add(new Button(()=>RequestDataOperation(false)){text = "Load Data"});
        
        rootVisualElement.Add(toolbar);
    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog("Invalid file name","Please enter a valid file name.","OK");
            return;
        }

        var saveUtility = GraphSaveUtility.GetInstance(_graphView);
        if(save)
            saveUtility.SaveGraph(_fileName);
        else
        {
            saveUtility.LoadGraph(_fileName);
        }
    }


    private void ConstructGraphView()
    {
        _graphView = new DialogueGraphView(this)
        {
            name = "Dialogue Graph",
        };
        
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }
}
