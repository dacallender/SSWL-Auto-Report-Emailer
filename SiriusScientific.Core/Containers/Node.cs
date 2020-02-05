using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace SiriusScientific.Core.Containers
{
	[Serializable]
	[DataContract(IsReference = true)]
	public abstract class Node : INode
	{
		[DataMember]
		public INode ParentNode { get; set; }

		[DataMember]
		public IEnumerable<INode> SubNodes { get; set; }

		[DataMember]
		public string NodeLabel { get; set; }

		[DataMember]
		public string Key { get; set; }

		[DataMember]
		public bool IsRoot { get; set; }

		[DataMember]
		public bool HasParent { get; set; }

		[DataMember]
		public bool HasChild { get; set; }

		[DataMember]
		public string UniqueId { get; set; }

		[DataMember]
		public string Value { get; set; }

		[DataMember]
		public INode RootNode { get; set; }

		public abstract void InitializeNode();

		public INodeParams NodeParams { get; set; }

		public void TryGetNode(string key, out INode node)
		{
			node = (from item in SubNodes where item.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase) select item).FirstOrDefault();
		}

		public bool ContainsKey(string key)
		{
			return (from node in SubNodes where node.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase) select node).FirstOrDefault() != null;
		}

		public bool IsSubNodeExist( INode subsNode )
		{
			return IsSubNodeExist(subsNode.NodeLabel);
		}

		public bool IsSubNodeExist( string subsNodeLabel )
		{
			var existingSubNode = SubNodes.Where(p => p.NodeLabel.Equals(subsNodeLabel, StringComparison.InvariantCultureIgnoreCase)).ToList();

			return (existingSubNode.Count > 0);
		}

		public int GetSubNodeIndex(INode subsNode )
		{
			return GetSubNodeIndex(subsNode.NodeLabel);
		}

		public int GetSubNodeIndex(string nodeLabel )
		{
			return SubNodes.ToList().FindIndex(c => c.NodeLabel == nodeLabel);
		}

		public INode GetNode( string nodeLabel )
		{
			return GetNode(GetSubNodeIndex(nodeLabel));
		}

		//These must be abstract since IEnumerable<T> does not support indexing. An indexable collection must first be implemented.
		public abstract INode GetNode(int index);

		public abstract void AddNode(INode subNode);

		public abstract void InsertNode(INode subsNode);

		public abstract void RemoveNode(string key);
		public abstract void ModifyNodeState(INodeParams nodeParams);

		public abstract void RemoveNode(INode node);

		public abstract INode CreateChildNode(INodeParams nodeParams);

		public abstract INode CreateChildNode(string nodeParamString);

		public void InsertNode( int parentNodeIndex, INode subsNode )
		{
			if (!IsSubNodeExist(subsNode))
			{
				HasChild = true;

				subsNode.ParentNode = this;

				((ObservableCollection<INode>) SubNodes)[parentNodeIndex] = subsNode;
			}
		}

		public void InsertNode( string parentNodeLabel, INode childNode )
		{
			InsertNode(GetSubNodeIndex(parentNodeLabel), childNode);
		}

		public virtual void Dispose()
		{
			ParentNode?.Dispose();

			RootNode?.Dispose();
		}
	}
}
