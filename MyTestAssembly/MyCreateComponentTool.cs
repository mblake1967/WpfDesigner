using ICSharpCode.WpfDesign.Designer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.WpfDesign;
using System.ComponentModel;
using System.Collections;
using System.Windows;
using System.Windows.Input;

namespace MyTestAssembly
{

	public class MyCreateComponentTool : CreateComponentTool
	{
		public MyCreateComponentTool(Type componentType, int Id) : base(componentType)
		{
			this.Id = Id;
		}

		public int Id { get; set; }

		protected override DesignItem CreateItem(DesignContext context)
		{
			DesignItem designItem = base.CreateItem(context);

			//We have what here? a WidgetView and a WidgetHolderView?
			//Do we need to get defs, create view models, hook the 2 together. RefreshData & EvaluateExpressions. 
			//Looks like Adrian did this by modifying ExtensionManager.CreateInstanceWithCustomInstanceFactory()

			MyWidgetHolderView whv = designItem.Component as MyWidgetHolderView;
			MyWidgetView wv = whv.Content as MyWidgetView;
			wv.Text = "MyWidgetView - " + this.Id.ToString();

			return designItem;
		}

	}

}
