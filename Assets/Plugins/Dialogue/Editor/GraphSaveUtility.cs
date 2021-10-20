using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphSaveUtility
{
    private DialogueGraphView _targetGraphView;
    private DialogueContainer _containerCache;
    
    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<DialogueNode> Nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();
    private List<Group> CommentBlocks =>
        _targetGraphView.graphElements.ToList().Where(x => x is Group).Cast<Group>().ToList();

    public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    public void SaveGraph(string fileName)
    {
        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
       
        if(!SaveNodes(dialogueContainer))
            return;

        SaveExposedProperties(dialogueContainer);
        SaveCommentBlocks(dialogueContainer);
        
        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");

        UnityEngine.Object loadedAsset = AssetDatabase.LoadAssetAtPath($"Assets/Resources/{fileName}.asset", typeof(DialogueContainer));

        if (loadedAsset == null || !AssetDatabase.Contains(loadedAsset)) 
        {
            AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/{fileName}.asset");
        }
        else 
        {
            DialogueContainer container = loadedAsset as DialogueContainer;
            container.NodeLinks = dialogueContainer.NodeLinks;
            container.DialogueNodeData = dialogueContainer.DialogueNodeData;
            container.ExposedProperties = dialogueContainer.ExposedProperties;
            container.CommentBlockData = dialogueContainer.CommentBlockData;
            EditorUtility.SetDirty(container);
        }

        AssetDatabase.SaveAssets();
    }

    private void SaveCommentBlocks(DialogueContainer dialogueContainer)
    {
        foreach (var block in CommentBlocks)
        {
            var nodes = block.containedElements.Where(x => x is DialogueNode).Cast<DialogueNode>().Select(x => x.GUID)
                .ToList();

            dialogueContainer.CommentBlockData.Add(new CommentBlockData
            {
                ChildNodes = nodes,
                Title = block.title,
                Position = block.GetPosition().position
            });
        }
    }
    
    private void SaveExposedProperties(DialogueContainer dialogueContainer)
    {
        dialogueContainer.ExposedProperties.AddRange(_targetGraphView.ExposedProperties);
    }

    bool SaveNodes(DialogueContainer dialogueContainer)
    {
        if(!Edges.Any())
            return false;
        
        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
        for (int i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogueNode;
            var inputNode = connectedPorts[i].input.node as DialogueNode;
            
            dialogueContainer.NodeLinks.Add(new NodeLinkData()
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID
            });
        }

        foreach (var dialogueNode in Nodes.Where(node=>!node.EntryPoint))
        {
            dialogueContainer.DialogueNodeData.Add(new DialogueNodeData()
            {
                Guid = dialogueNode.GUID,
                DialogueText = dialogueNode.DialogueText,
                Position = dialogueNode.GetPosition().position
            });   
        }

        return true;
    }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<DialogueContainer>(fileName);

        if (_containerCache == null)
        {
            EditorUtility.DisplayDialog("File not found", "Target dialogue graph file does not exists!", "OK");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
        CreateExposedProperties();
        GenerateCommentBlocks();
    }

    private void CreateExposedProperties()
    {
        _targetGraphView.ClearBlackBoardAndExposedProperties();
        
        foreach (var exposedProperty in _containerCache.ExposedProperties)
        {
            _targetGraphView.AddPropertyToBlackBoard(exposedProperty);
        }
    }

    private void ConnectNodes()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            var connections = _containerCache.NodeLinks.Where(x => x.BaseNodeGuid == Nodes[i].GUID).ToList();
            for (int j = 0; j < connections.Count; j++)
            {
                var targetNodeGuid = connections[j].TargetNodeGuid;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGuid);
                LinkNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);
                
                targetNode.SetPosition(new Rect(_containerCache.DialogueNodeData.First(x=>x.Guid == targetNodeGuid).Position,
                    _targetGraphView.defaultNodeSize));
            }
        }   
    }

    private void LinkNodes(Port output, Port input)
    {
        var tempEdge = new Edge()
        {
            output = output,
            input = input
        };
        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);
        _targetGraphView.Add(tempEdge);
    }


    private void CreateNodes()
    {
        foreach (var nodeData in _containerCache.DialogueNodeData)
        {
            //We pass position later on , so we can just use vec2 zero for now as position while loading
            var tempNode = _targetGraphView.CreateDialogueNode(nodeData.DialogueText,Vector2.zero);
            tempNode.GUID = nodeData.Guid;
            _targetGraphView.AddElement(tempNode);

            var nodePorts = _containerCache.NodeLinks.Where(x => x.BaseNodeGuid == nodeData.Guid).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.PortName));
        }
    }

    private void ClearGraph()
    {
        //Set entry points guid back from the save.Discard existing guid
        Nodes.Find(x => x.EntryPoint).GUID = _containerCache.NodeLinks[0].BaseNodeGuid;

        foreach (var node in Nodes)
        {
            if(node.EntryPoint)
                continue;
            //Remove edges that connected to this node
            Edges.Where(x => x.input.node == node).ToList().ForEach(edge => _targetGraphView.RemoveElement(edge));
            //Then remove the node
            _targetGraphView.RemoveElement(node);
        }
    }
    
    private void GenerateCommentBlocks()
    {
        foreach (var commentBlock in CommentBlocks)
        {
            _targetGraphView.RemoveElement(commentBlock);
        }

        foreach (var commentBlockData in _containerCache.CommentBlockData)
        {
            var block = _targetGraphView.CreateCommentBlock(new Rect(commentBlockData.Position, _targetGraphView.DefaultCommentBlockSize),
                commentBlockData);
            block.AddElements(Nodes.Where(x=>commentBlockData.ChildNodes.Contains(x.GUID)));
        }
    }
}
