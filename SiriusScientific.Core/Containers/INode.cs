using System;
using System.Collections.Generic;

namespace SiriusScientific.Core.Containers
{
	public interface INode : IDisposable
	{
		string NodeLabel { get; set; }
		string Key { get; set; }
		bool IsRoot { get; set; }
		bool HasParent { get; set; }
		bool HasChild { get; set; }
		string UniqueId { get; set; }
		string Value { get; set; }
		INode RootNode { get; set; }
		INode ParentNode { get; set; }

		INodeParams NodeParams { get; set; }

		IEnumerable<INode> SubNodes { get; set; }

		void InitializeNode();
		void TryGetNode(string key, out INode node);
		bool ContainsKey(string key);
		bool IsSubNodeExist(INode subsNode);
		bool IsSubNodeExist(string subsNodeLabel);
		int GetSubNodeIndex(INode subsNode);
		int GetSubNodeIndex(string nodeLabel);
		INode GetNode(int index);
		INode GetNode(string nodeLabel);
		void AddNode(INode subNode);
		void InsertNode(INode subsNode);
		void InsertNode(int parentNodeIndex, INode subsNode);
		void InsertNode(string parentNodeLabel, INode childNode);
		void RemoveNode(string key);

		void ModifyNodeState(INodeParams nodeParams);
	}
}
