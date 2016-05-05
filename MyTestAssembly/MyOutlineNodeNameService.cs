using ICSharpCode.WpfDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestAssembly
{
	public class MyOutlineNodeNameService : IOutlineNodeNameService
	{
		public string GetOutlineNodeName(DesignItem designItem)
		{
			return designItem.ComponentType.Name + " (FooBar name)";
		}
	}
}
