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
using System.Xml;
using System.IO;
using MyTestAssembly;
using ICSharpCode.WpfDesign.Designer.Xaml;
using ICSharpCode.WpfDesign.Designer.PropertyGrid;
using ICSharpCode.WpfDesign;
using ICSharpCode.WpfDesign.Designer.OutlineView;
using System.Reflection;

namespace MyDesigner
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//From the simple example, enough text to show a root container in the designer.
		private static string xaml = @"<Grid 
xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
mc:Ignorable=""d""
x:Name=""rootElement"" Background=""White""></Grid>";

		public MainWindow()
		{
			DataContext = MyDesignerModel.Instance;

			InitializeComponent();

			//Plug the design surface into the grid that is its parent
			this.DesignSurfaceGrid.Children.Add(MyDesignerModel.Instance.DesignSurface);

			//For testing property getting/setting only.
			MyDesignerModel.Instance.PropertyGrid = this.uxPropertyGridView.PropertyGrid;

			//Load the design surface with some minimal XAML that we have hardcoded.
			using (var xmlReader = XmlReader.Create(new StringReader(xaml)))
			{
				//Use the load settings to add our MyDisplaySizeExtension extension that shows a new adorner.
				Assembly extAsm = Assembly.GetAssembly(typeof(MyDisplaySizeExtension));

				XamlLoadSettings loadSettings = new XamlLoadSettings();
				loadSettings.DesignerAssemblies.Add(extAsm);

				MyDesignerModel.Instance.DesignSurface.LoadDesigner(xmlReader, loadSettings);
			}

			//We want to supply our own implementation of IComponentPropertyService so we return just the properties that we want.
			//This would probably go better in the MyDesignerModel class, but have to wait until after the DesignContext has been created.
			MyDesignerModel.Instance.DesignSurface.DesignContext.Services.AddOrReplaceService(typeof(IComponentPropertyService), new MyComponentPropertyService());

			//We want to supply our own implementation of IOutlineNodeNameService so the outline view has something like widget type and name & not the view typename.
			//This would probably go better in the MyDesignerModel class, but have to wait until after the DesignContext has been created.
			MyDesignerModel.Instance.DesignSurface.DesignContext.Services.AddOrReplaceService(typeof(IOutlineNodeNameService), new MyOutlineNodeNameService());

			//Can I replace the quick operation menu extension?

			//Tell the model to subscribe to some component service events (need to figure out the best time to do this)
			//MyDesignerModel.Instance.SubscribeToComponentEvents();

			//Also, we want to create a root outline node in the model for the outline view control. There won't be a root node in the design context until we've loaded some xaml, 
			//so we'll do it manually right here. This also would be better in the model after some kind of load/new operation/event that let's up know that the design surface has 
			//a document.
			MyDesignerModel.Instance.OutlineRoot = OutlineNode.Create(MyDesignerModel.Instance.DesignSurface.DesignContext.RootItem);
		}
	}
}
