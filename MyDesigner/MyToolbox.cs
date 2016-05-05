using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyTestAssembly;

namespace MyDesigner
{
	public class MyToolbox
	{

		public MyToolbox()
		{
			MyFooNodes = new ObservableCollection<MyFooNode>();

			//For poc, just add a node to our collection for each member of MyFooEnum.
			MyFooNodes.Add(new MyFooNode() { FooType = MyFooEnum.MyWidget, Id = 0 });
			MyFooNodes.Add(new MyFooNode() { FooType = MyFooEnum.MyWidget, Id = 1 });
			MyFooNodes.Add(new MyFooNode() { FooType = MyFooEnum.MyWidget, Id = 2 });
			MyFooNodes.Add(new MyFooNode() { FooType = MyFooEnum.TextWidget, Id = 0 });
		}

		public static MyToolbox Instance = new MyToolbox();
		public ObservableCollection<MyFooNode> MyFooNodes { get; private set; }
	}

	//Represents either a widget or form controls node. A folder structure would require a different node for folders? Like the AssemblyNode in Toolbox.
	public class MyFooNode
	{
		public MyFooEnum FooType { get; set; }

		public string Name
		{
			get
			{
				return this.FooType.ToString() + " - " + this.Id.ToString();
			}
		}

		public int Id { get; set; }
	}
}
