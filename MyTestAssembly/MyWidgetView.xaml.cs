using ICSharpCode.WpfDesign.Extensions;
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
using ICSharpCode.WpfDesign;

namespace MyTestAssembly
{
	/// <summary>
	/// Interaction logic for MyWidgetView.xaml
	/// </summary>
	public partial class MyWidgetView : UserControl
	{
		public MyWidgetView()
		{
			DataContext = this;
			InitializeComponent();
		}

		private static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MyWidgetView));
		public string Text
		{
			get { return GetValue(MyWidgetView.TextProperty) as string; }
			set { SetValue(MyWidgetView.TextProperty, value); }
		}
	}

	[ExtensionFor(typeof(MyWidgetView))]
	public class MyWidgetHolderInstanceFactory : CustomInstanceFactory
	{
		public override object CreateInstance(Type type, params object[] arguments)
		{
			//Test. We want to wrap a widget view with a widget holder view
			MyWidgetHolderView widgetHolderView = new MyWidgetHolderView();
			
			object widgetView = base.CreateInstance(type, arguments);

			widgetHolderView.Content = widgetView;

			return widgetHolderView;
		}
	}

}
