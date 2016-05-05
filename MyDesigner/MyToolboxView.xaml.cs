using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ICSharpCode.WpfDesign.Designer.OutlineView;
using ICSharpCode.XamlDesigner; //Only for the ExtensionMethods class.
using ICSharpCode.WpfDesign.Designer.Services;
using MyTestAssembly;

namespace MyDesigner
{
	/// <summary>
	/// Interaction logic for MyToolboxView.xaml
	/// </summary>
	public partial class MyToolboxView : UserControl
	{
		public MyToolboxView()
		{
			this.DataContext = MyToolbox.Instance;
			InitializeComponent();

			new DragListener(this).DragStarted += Toolbox_DragStarted;
			uxTreeView.SelectedItemChanged += uxTreeView_SelectedItemChanged;
			uxTreeView.GotKeyboardFocus += uxTreeView_GotKeyboardFocus;
		}

		void uxTreeView_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			PrepareTool(uxTreeView.SelectedItem as MyFooNode, false);
		}

		void uxTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			PrepareTool(uxTreeView.SelectedItem as MyFooNode, false);
		}

		void Toolbox_DragStarted(object sender, MouseButtonEventArgs e)
		{
			PrepareTool(e.GetDataContext() as MyFooNode, true);
		}

		void PrepareTool(MyFooNode node, bool drag)
		{
			if (node != null)
			{

				//Get the Type we want to use for the MyFooNode being dragged out here.
				Type type = GetTypeForFooType(node.FooType);

				//var tool = new CreateComponentTool(type);

				//We can use an override of CreateComponentTool, but we have to wrap it in a DataObject and specify
				//the format by type where type is CreateComponentTool. The event handlers in CreateComponentTool
				//won't work otherwise because of a single GetData call. The drag doesn't work in that case.
				var tool = new MyCreateComponentTool(type, node.Id);
				DataObject dataObject = new DataObject(typeof(CreateComponentTool), tool);

				if (MyDesignerModel.Instance.DesignSurface != null)
				{
					MyDesignerModel.Instance.DesignSurface.DesignContext.Services.Tool.CurrentTool = tool;
					if (drag)
					{
						//DragDrop.DoDragDrop(this, tool, DragDropEffects.Copy);
						DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy);
					}
				}
			}
		}

		private Type GetTypeForFooType(MyFooEnum fooType)
		{
			Type retVal = null;

			if (fooType == MyFooEnum.MyWidget)
			{
				retVal = typeof(MyWidgetView);
			}
			else if (fooType == MyFooEnum.TextWidget)
				retVal = typeof(TextBlock);

			return retVal;
		}
	}
}
