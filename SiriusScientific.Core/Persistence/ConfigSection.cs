using System.Collections.ObjectModel;
using System.IO;
using SiriusScientific.Core.Containers;

namespace SiriusScientific.Core.Persistence
{
	public class ConfigSection : Node, INode
	{
		private string FilePath { get; set; }

		private ConfigSection _newSection;

		private ConfigSection _previousSection;

		private ConfigSection _content;

		public ConfigSection()
		{
			NodeLabel = "Root";

			HasParent = false;

			HasChild = false;

			IsRoot = true;

			SubNodes = new ObservableCollection<INode>();
		}

		// ********************************************************************************
		/// <summary>
		/// </summary>
		/// <created>David,4/16/2018</created>
		/// <changed>David,4/16/2018</changed>
		// ********************************************************************************
		public void Create()
		{
			File.Create(FilePath);
		}

		// ********************************************************************************
		/// <summary>
		/// </summary>
		/// <created>David,4/16/2018</created>
		/// <changed>David,4/16/2018</changed>
		// ********************************************************************************
		public void Refresh()
		{
			StreamReader reader = new StreamReader(FilePath);

			string line = reader.ReadLine();

			_newSection = new ConfigSection() { NodeLabel = line, HasParent = true, HasChild = false, IsRoot = false, ParentNode = this, Key = line};

			AddNode(_newSection);

			while ( ( line = reader.ReadLine() ) != null )
			{
				if ( line.Contains("[") )
				{
					_previousSection = _newSection;

					_newSection = new ConfigSection() { NodeLabel = line, HasParent = true, IsRoot = false, ParentNode = _previousSection, Key = line};

					AddNode(_newSection);

					_previousSection.HasChild = true;
				}
				else
				{
					var contentArray = line.Split('=', ';');

					_content = new ConfigSection(){NodeLabel = line, HasParent = true, IsRoot = false, ParentNode =  _newSection, Key = contentArray[0], Value = contentArray[1]};

					_newSection.AddNode(_content);

					_newSection.HasChild = true;
				}
			}

			reader.Close();
		}

		// ********************************************************************************
		/// <summary>
		/// </summary>
		/// <created>David,4/16/2018</created>
		/// <changed>David,4/17/2018</changed>
		// ********************************************************************************
		public void Update()
		{
			Update(FilePath);
		}

		// ********************************************************************************
		/// <summary>
		/// </summary>
		/// <param name="filePath"></param>
		/// <created>David,4/16/2018</created>
		/// <changed>David,4/16/2018</changed>
		// ********************************************************************************
		public void Update( string filePath )
		{
			StreamWriter writer = new StreamWriter(filePath);

			foreach ( INode configSection in SubNodes )
			{
				writer.WriteLine(configSection.NodeLabel);

				foreach ( INode subSection in configSection.SubNodes )
				{
					writer.WriteLine(subSection.NodeLabel);
				}
			}

			writer.Flush();

			writer.Close();
		}

		// ********************************************************************************
		/// <summary>
		/// </summary>
		/// <created>David,4/16/2018</created>
		/// <changed>David,4/16/2018</changed>
		// ********************************************************************************
		public void Delete()
		{
			File.Delete(FilePath);
		}

		// ********************************************************************************
		/// <summary>
		/// Creates a new section in memory. Update() must be called to make changes persistent.
		/// </summary>
		/// <param name="sectionHeader"></param>
		/// <created>David,4/16/2018</created>
		/// <changed>David,4/16/2018</changed>
		// ********************************************************************************
		public void CreateSection(string sectionHeader)
		{
			if (!ContainsKey(sectionHeader))
			{
				ConfigSection newSection = new ConfigSection()
				{
					HasChild = false,
					HasParent = true,
					IsRoot = false,
					NodeLabel = $"[{sectionHeader}]",
					ParentNode = this,
				};

				AddNode(newSection);
			}
		}

		// ********************************************************************************
		/// <summary>
		/// </summary>
		/// <param name="sectionHeader"></param>
		/// <created>David,4/16/2018</created>
		/// <changed>David,4/16/2018</changed>
		// ********************************************************************************
		public void RefreshSection(string sectionHeader)
		{
			TryGetNode(sectionHeader, out var section);


		}

		public void CreateSectionContent( string sectionHeader, string key, string value )
		{
		}

		// ********************************************************************************
		/// <summary>
		/// Modifies the section content. Update() must be called to make changes persistent.
		/// </summary>
		/// <param name="sectionHeader"></param>
		/// <param name="contentKey"></param>
		/// <created>David,4/16/2018</created>
		/// <changed>David,4/16/2018</changed>
		// ********************************************************************************
		public void UpdateSectionContent(string sectionHeader, string key, string value)
		{
		}

		// ********************************************************************************
		/// <summary>
		/// </summary>
		/// <param name="sectionHeader"></param>
		/// <created>David,4/16/2018</created>
		/// <changed>David,4/16/2018</changed>
		// ********************************************************************************
		public void DeleteSectionContent(string sectionHeader)
		{
		}

		public void DeleteSEectionContent( string sectionHeader, string key )
		{
		}

		public override void InitializeNode()
		{
			throw new System.NotImplementedException();
		}

		public override INode CreateChildNode(INodeParams nodeParams)
		{
			throw new System.NotImplementedException();
		}

		public override INode CreateChildNode(string nodeParamString)
		{
			throw new System.NotImplementedException();
		}

		public override INode GetNode(int index)
		{
			if ( ( (SubNodes as ObservableCollection<INode>)?.Count > 0 ) && ( (SubNodes as ObservableCollection<INode>)?.Count > index ) )
				return ( SubNodes as ObservableCollection<INode> )?[index];

			return null;
		}

		public override void AddNode(INode subNode)
		{
			HasChild = true;

			subNode.ParentNode = this;

			(SubNodes as ObservableCollection<INode>)?.Add(subNode);
		}

		public override void InsertNode(INode subsNode)
		{
			if ( !IsSubNodeExist(subsNode) )
			{
				HasChild = true;

				subsNode.ParentNode = this;

				(SubNodes as ObservableCollection<INode>)?.Add(subsNode);
			}
		}

		public override void RemoveNode(string key)
		{
			TryGetNode(key, out var node);

			if ( node != null )
				(SubNodes as ObservableCollection<INode>)?.Remove(node);
		}

		public override void ModifyNodeState(INodeParams nodeParams)
		{
			throw new System.NotImplementedException();
		}

		public override void RemoveNode(INode node)
		{
		}
	}
}
