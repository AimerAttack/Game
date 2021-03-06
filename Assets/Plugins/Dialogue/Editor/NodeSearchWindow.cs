using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeSearchWindow : ScriptableObject,ISearchWindowProvider
{
    private DialogueGraphView _graphView;
    private EditorWindow _window;
    private Texture2D _indentationIcon;

    public void Init(EditorWindow window,DialogueGraphView graphView)
    {
        _window = window;
        _graphView = graphView;

        _indentationIcon = new Texture2D(1, 1);
        _indentationIcon.SetPixel(0,0,new Color(0,0,0,0));
        _indentationIcon.Apply();
    }
    
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>()
        {
            new SearchTreeGroupEntry(new GUIContent("Create Elements"),0),
            new SearchTreeGroupEntry(new GUIContent("Dialogue"),1),
            new SearchTreeEntry(new GUIContent("Dialogue Node",_indentationIcon))
            {
                userData = new DialogueNode(),level = 2
            },
            new SearchTreeEntry(new GUIContent("Comment Block",_indentationIcon))
            {
                level = 1,
                userData = new Group()
            }
        };
        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var mousePosition = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent,
            context.screenMousePosition - _window.position.position);
        var graphMousePosition = _graphView.contentViewContainer.WorldToLocal(mousePosition);
            
        switch (SearchTreeEntry.userData)
        {
            case DialogueNode dialogueNode:
                _graphView.CreateNode("Dialogue Node",graphMousePosition);
                return true;
                break;
            case Group group:
                var rect = new Rect(graphMousePosition, _graphView.DefaultCommentBlockSize);
                _graphView.CreateCommentBlock(rect);
                return true;
            default:
                return false;
        }
    }
}
