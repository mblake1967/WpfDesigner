using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestAssembly
{
	/// <summary>
	/// Use the attribute to explicitely say that a property should show up in our designer.
	/// </summary>
	public class MyShowInDesignerAttribute : Attribute
	{
		public MyShowInDesignerAttribute()
		{
		}
	}
}
